## Example compilation and use of the ImgUtil library

See the Makefile for details and more examples.

### Compilation

    $ fsharpc -a img_util.fsi img_util.fs
    $ fsharpc -r img_util.dll sierpinski.fs

or just:

    $ make img_util.dll

### Execution on Linux+Windows

    $ mono32 sierpinski.exe

or

    $ mono sierpinski.exe

### Execution on MacOS

    $ mono32 sierpinski.exe

### Functional Images

    $ make gui_wav.exe && mono32 gui_wav.exe

### Turtle graphics

    $ make turtle.exe && mono32 turtle.exe

## License

MIT License