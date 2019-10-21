
open Img
open System.Windows.Forms

let f = Raster.make_app "Img"
           (fun s bmp ->
              Img.imgToBitmap bmp (float s) (Img.fracToColor << Img.wavDist))
           (fun s e ->
              if e.KeyCode = Keys.Up then Some (s+1)
              else if e.KeyCode = Keys.Down then Some (s-1)
              else None) 10

do Application.Run f
