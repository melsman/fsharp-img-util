## Example compilation and use of the ImgUtil library

To build the img_util.dll ressource, just execute the following command:

    $ make img_util.dll

See the Makefile for examples. Also, if you are not on macOS, you
probably need to adjust the Makefile...

## Examples

### Sierpinski

    $ make sierp.exe && mono sierp.exe

### Save png files

    $ make fig.exe && mono fig.exe

### Functional images

    $ make gui_wav.exe && mono gui_wav.exe

### Turtle graphics

    $ make turtle.exe && mono turtle.exe

## Issues

Currently, the img_util.dll library builds on top of the Cairo vector
library, which is portable accross platforms, but which is very slow
at raster graphics. We should somehow model a bitmap dirctly as a
Gdk.Pixbuf value, which would be much more efficient and also allow us
to read a pixel value, which is currently not supported.

## License

MIT license
