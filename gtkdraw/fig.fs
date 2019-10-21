type point = int * int          // a point (x, y) in the plane
type colour = int * int * int   // (red, green, blue), 0..255 each
type figure =
  | Circle of point * int * colour
     // defined by center , radius , and colour
  | Rectangle of point * point * colour
     // defined by bottom-left corner, top-right corner, and colour
  | Triangle of point * point * point * colour
     // defined by the three points and colour
  | Mix of figure * figure
     // combine figures with mixed colour at overlap

let triarea2 (x1,y1) (x2,y2) (x3,y3) : int =
  abs(x1*(y2-y3)+x2*(y3-y1)+x3*(y1-y2))

// finds colour of figure at point
let rec colourAt (x,y) figure =
  match figure with
    | Circle ((cx,cy), r, col) ->
      if (x-cx)*(x-cx)+(y-cy)*(y-cy) <= r*r then
        // uses Pythagoras ' formular to determine
        // distance to center
        Some col
      else None
    | Rectangle ((x0,y0), (x1,y1), col) ->
      if x0<=x && x <= x1 && y0 <= y && y <= y1 // within corners
      then Some col else None
    | Triangle (p1,p2,p3,col) ->
      let sum = triarea2 p1 p2 (x,y) + triarea2 p1 p3 (x,y) + triarea2 p2 p3 (x,y)
      if sum > triarea2 p1 p2 p3 then None else Some col
    | Mix (f1, f2) ->
      match (colourAt (x,y) f1, colourAt (x,y) f2) with
        | (None, c) -> c // no overlap
        | (c, None) -> c // no overlap
        | (Some (r1,g1,b1), Some (r2,g2,b2)) ->
          // average color
          Some ((r1+r2)/2, (g1+g2)/2, (b1+b2)/2)

let checkColor (r,g,b) =
  0 <= r && r <= 255 &&
  0 <= g && g <= 255 &&
  0 <= b && b <= 255

let rec checkFigure fig : bool =
  match fig with
   | Circle ((x,y),r,c) -> r >= 0 && checkColor c
   | Rectangle ((x1,y1),(x2,y2),c) -> x2 >= x1 && y2 >= y1 && checkColor c
   | Triangle (_,_,_,c) -> checkColor c
   | Mix(f1,f2) -> checkFigure f1 && checkFigure f2

let rec move fig (a,b) : figure =
  match fig with
   | Circle ((x,y),r,c) -> Circle ((x+a,y+b),r,c)
   | Rectangle ((x1,y1),(x2,y2),c) -> Rectangle ((x1+a,y1+b),(x2+a,y2+b),c)
   | Triangle ((x1,y1),(x2,y2),(x3,y3),c) -> Triangle ((x1+a,y1+b),(x2+a,y2+b),(x3+a,y3+b),c)
   | Mix(f1,f2) -> Mix(move f1 (a,b), move f2 (a,b))

let minPoint (x1,y1) (x2,y2) = (min x1 x2,min y1 y2)
let maxPoint (x1,y1) (x2,y2) = (max x1 x2,max y1 y2)

let rec boundingBox fig : point * point =
  match fig with
    | Circle ((x,y),r,_) -> ((x-r,y-r),(x+r,y+r))
    | Rectangle (p1,p2,_) -> (p1,p2)
    | Triangle (p1,p2,p3,_) ->
      let pmin = minPoint (minPoint p1 p2) p3
      let pmax = maxPoint (maxPoint p1 p2) p3
      in (pmin, pmax)
    | Mix(f1,f2) ->
      let (p1,q1) = boundingBox f1
      let (p2,q2) = boundingBox f2
      in (minPoint p1 p2, maxPoint q1 q2)

let fig_test : figure =
  Mix (Circle ((50,50),45,(255,0,0)),
       Rectangle ((40,40),(90,110),(0,0,255)))
let fig_test_move = move fig_test (-20,20)

let makePicture filnavn figur b h =
  let bmp = ImgUtil.mk b h
  for x in [0..b-1] do
    for y in [0..h-1] do
      let c =
        match colourAt (x,y) figur with
          | None -> (128,128,128)
          | Some c -> c
      do ImgUtil.setPixel (ImgUtil.fromRgb c) (x,y) bmp
  let target = filnavn + ".png"
  do ImgUtil.toPngFile target bmp
  do printfn "Wrote file: %s" target
  do printfn "Bounding box: %A" (boundingBox figur)

do makePicture "fig_test" fig_test 100 150
do makePicture "fig_test_move" fig_test_move 100 150

let yellow = (255,255,0)
let blue = (0,0,255)
let red = (255,0,0)
let fig_test_house =
  Mix (Circle((70,20),10,yellow),
       Mix(Rectangle((20,70),(80,120),red),
           Triangle((15,80),(45,30),(85,70),blue)))

do makePicture "fig_test_house" fig_test_house 100 150
