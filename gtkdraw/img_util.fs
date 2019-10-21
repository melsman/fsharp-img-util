module ImgUtil

open Gtk
open System
open Cairo

// colors
type color = Cairo.Color

let fromArgb (a:int,r:int,g:int,b:int) : color =
  new Cairo.Color(float r/255.0,float g / 255.0,float b / 255.0, float a / 255.0)

let fromRgb (r:int,g:int,b:int) : color = fromArgb (255,r,g,b)

let red : color = fromRgb(255,0,0)
let green : color = fromRgb(0,255,0)
let blue : color = fromRgb(0,0,255)

let fromColor (c: color) : int * int * int * int =
    let a = (int)(c.A * 255.0)
    let r = (int)(c.A * 255.0)
    let g = (int)(c.A * 255.0)
    let b = (int)(c.A * 255.0)
    in (a,r,g,b)

// bitmaps
type bitmap = ImageSurface

let mk w h : bitmap =
  new ImageSurface(Format.Argb32,w,h)

let imageSurfaceToPixmap (s:ImageSurface) : Gdk.Pixmap =
  let pixmap = new Gdk.Pixmap(null, s.Width, s.Height, 24);
  let cr : Cairo.Context = Gdk.CairoHelper.Create(pixmap)
  in (cr.Operator <- Cairo.Operator.Add;
      cr.SetSource(s);
      cr.Paint();
      cr.Dispose();
      pixmap)

let height (is : bitmap) : int = is.Height
let width (is : bitmap) : int = is.Width

let setLine (c: color) (x1:int,y1:int) (x2:int,y2:int) (is:bitmap) : unit =
  let ctx = new Context(is)
  in (ctx.SetSourceColor c;
      ctx.MoveTo (float x1,float y1);
      ctx.LineTo (float x2,float y2);
      ctx.Stroke();
      ctx.Dispose())

let setPixel (c: color) (x,y) (bmp:bitmap) : unit =
  setLine c (x,y) (x+1,y+1) bmp

let setBox c (x1,y1) (x2,y2) bmp =
  do setLine c (x1,y1) (x2,y1) bmp
  do setLine c (x2,y1) (x2,y2) bmp
  do setLine c (x2,y2) (x1,y2) bmp
  do setLine c (x1,y2) (x1,y1) bmp

// read a bitmap file
let fromFile (fname : string) : bitmap =
  new ImageSurface(fname)

// save a bitmap as a png file
let toPngFile (fname : string) (is: bitmap) : unit =
  is.WriteToPng(fname)

// start and run an application with an action
let runApplication (action:unit -> unit) =
  (Application.Init();
   Application.Invoke(fun _ _ -> action());
   Application.Run())

type Key = Gdk.Key

// start an app that can listen to key-events
let runApp (t:string) (w:int) (h:int)
           (f:int -> int -> 's -> bitmap)
           (onKeyDown: 's -> Key -> 's option) (s:'s) : unit =
  runApplication (
    fun () ->
     let window = new Window(t)
     in (window.SetDefaultSize(w,h)
         window.DeleteEvent.Add(fun e -> window.Hide(); Application.Quit(); e.RetVal <- true)
         let state = ref s
         let drawing = new Gtk.DrawingArea()
         let draw () =
           let gc = drawing.Style.BaseGC(StateType.Normal)
           let (w,h) = window.GetSize()
           let bmp = f w h (!state)
           let pixmap = imageSurfaceToPixmap bmp
           in (drawing.GdkWindow.DrawDrawable(gc,pixmap,0,0,0,0,-1,-1);
               pixmap.Dispose();
               gc.Dispose()
              )
         in (drawing.ExposeEvent.Add( fun _ -> draw() );
             window.KeyPressEvent.Add( fun (e:KeyPressEventArgs) ->
                                       match e.Event.Key with
                                           | Gdk.Key.Escape -> Application.Quit()
                                           | k -> (match onKeyDown (!state) k with
                                                   | Some s -> (state := s; window.QueueDraw())
                                                   | None -> ())
                                     );
             window.Add(drawing);
             window.ShowAll();
             window.Show()))
  )

let runSimpleApp t w h (f:bitmap->unit) : unit =
  runApp t w h (fun w h () ->
                  let bitmap = mk w h
                  do f bitmap
                  bitmap)
                (fun _ _ -> None) ()

let show t (bmp:bitmap) : unit =
  runApp t (width bmp) (height bmp) (fun _ _ () -> bmp) (fun _ _ -> None) ()
