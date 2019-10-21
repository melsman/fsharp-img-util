(*
 * Bitmap Primitives Helpers
 * Partly inspired by http://fssnip.net/si.
*)
open System.Drawing

// deserialize a bitmap file
let fromFile (name : string) = new Bitmap(name)

// serialize a bitmap as png
let toPngFile (name : string) (bmp: Bitmap) =
    bmp.Save(name, Imaging.ImageFormat.Png) |> ignore
    bmp

let greyifyColor (c : System.Drawing.Color) =
  let w = c.ToArgb()
  let r = (w >>> 16) &&& 255
  let g = (w >>> 8) &&& 255
  let b = w &&& 255
  let v = (r + g + b) / 3
  in Color.FromArgb (v,v,v)

let greyify (bmp:System.Drawing.Bitmap) : unit =
  for x in [0..bmp.Width-1] do
    for y in [0..bmp.Height-1] do
      let c = bmp.GetPixel (x,y)
      in bmp.SetPixel(x,y,greyifyColor c)

let img_apple = fromFile @"apple.jpg"
do greyify img_apple
let _ = toPngFile @"apple_grey.png" img_apple
