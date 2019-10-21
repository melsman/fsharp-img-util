
FSC=fsharpc --nologo
#macOS:
INCL=-I /Library/Frameworks/Mono.framework/Versions/Current/lib/mono/gtk-sharp-2.0
#Linux:
#INCL=-I /usr/lib/cli/gtk-sharp-2.0 -I /usr/lib/cli/gdk-sharp-2.0 -I /usr/lib/cli/glib-sharp-2.0 -I /usr/lib/cli/gtk-dotnet-2.0
GTKDLLS=-r gdk-sharp.dll -r gtk-sharp.dll -r:Mono.Cairo.dll

%.exe: %.fs
	$(FSC) $<

clean:
	rm -rf *~ *.exe *.png *.dll

apple_grey.png: greyify.exe
	mono greyify.exe

img_util.dll: img_util.fsi img_util.fs
	$(FSC) $(INCL) $(GTKDLLS) -a $^

fun_img.dll: fun_img.fsi fun_img.fs img_util.dll
	$(FSC) -a -r img_util.dll fun_img.fsi fun_img.fs

%.dll: %.fsi %.fs
	$(FSC) -a $^

fig.exe: img_util.dll fig.fs
	$(FSC) -r $^

spiral.exe: img_util.dll spiral.fs
	$(FSC) -r $^

sierpinski.exe: img_util.dll sierpinski.fs
	$(FSC) -r $^

sierp.exe: img_util.dll sierp.fs
	$(FSC) $(INCL) $(GTKDLLS) -r $^

gui_wav.exe: gui_wav.fs img_util.dll fun_img.dll
	$(FSC) $(INCL) $(GTKDLLS) -r img_util.dll -r fun_img.dll $<

x_fractal.exe: x_fractal.fs img_util.dll
	$(FSC) -r img_util.dll $(INCL) $(GTKDLLS) $<

turtle.exe: img_util.dll turtle.fs
	$(FSC) -r $^

ex.exe: img_util.dll ex.fs
	$(FSC) -r $^
