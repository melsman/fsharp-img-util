open System.Windows.Forms
open ImgUtil

let xfrac bmp s len p =
  let rec f len (x,y) =
        if len < pown 3 s then setBox blue (x,y) (x+len,y+len) bmp
        else let third = len / 3
             do f third (x,y)
             do f third (x+third*2,y)
             do f third (x+third,y+third)
             do f third (x,y+third*2)
             do f third (x+third*2,y+third*2)
  in f len p

let init = pown 3 6

do ImgUtil.runApp "X-fractal" (init+60) (init+60)
           (fun w h s ->
              let bmp = ImgUtil.mk w h
              do xfrac bmp s init (20,20)
              bmp)
           (fun s e ->
              if e.KeyCode = Keys.Up then Some (s+1)
              else if e.KeyCode = Keys.Down && s > 1 then Some (s-1)
              else None) 3
