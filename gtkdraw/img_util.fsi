module ImgUtil

// colors
type color = Cairo.Color
val red      : color
val blue     : color
val green    : color
val fromRgb  : int * int * int -> color
val fromArgb : int * int * int * int -> color
val fromColor : color -> int * int * int * int

// bitmaps
type bitmap = Cairo.ImageSurface
val mk       : int -> int -> bitmap
val setPixel : color -> int * int -> bitmap -> unit
val setLine  : color -> int * int -> int * int -> bitmap -> unit
val setBox   : color -> int * int -> int * int -> bitmap -> unit
val width    : bitmap -> int
val height   : bitmap -> int

// read a bitmap file
val fromFile : string -> bitmap

// save a bitmap as a png file
val toPngFile : string -> bitmap -> unit

// show bitmap in a gui
val show : string -> bitmap -> unit

// start a simple app
val runSimpleApp : string -> int -> int
                -> (bitmap -> unit) -> unit

type Key = Gdk.Key

// start an app that can listen to key-events
val runApp : string -> int -> int
          -> (int -> int -> 's -> bitmap)
          -> ('s -> Key -> 's option)
          -> 's -> unit
