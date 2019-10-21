
FSC=fsharpc --nologo

.PHONY: simple
simple: simple.exe
	mono32 simple.exe

%.exe: %.fs
	$(FSC) $<

turtle.exe: img_util.dll turtle.fs
	$(FSC) -r $^

clean:
	rm -rf *~ *.exe *.png *.dll

apple_grey.png: greyify.exe
	mono greyify.exe

%.dll: %.fsi %.fs
	$(FSC) -a $^

gui_img.exe: gui_img.fs raster.dll img.dll
	$(FSC) -r raster.dll -r img.dll $<

spiral.exe: img_util.dll spiral.fs
	$(FSC) -r $^

sierpinski.exe: img_util.dll sierpinski.fs
	$(FSC) -r $^

gui_wav.exe: gui_wav.fs img_util.dll fun_img.dll
	$(FSC) -r img_util.dll -r fun_img.dll $<

x_fractal.exe: img_util.dll x_fractal.fs
	$(FSC) -r $^
