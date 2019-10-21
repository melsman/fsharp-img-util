//
// Simple Example opening a window
//
// Compile with "fsharpc -r img_util.dll ex.fs"
// Run with "mono ex.exe"
//
let w = 500
let h = 300
let bmp = ImgUtil.mk w h
do ImgUtil.setLine ImgUtil.red (0,0) (100,200) bmp
do ImgUtil.setLine ImgUtil.green (100,0) (0,200) bmp

do ImgUtil.setBox ImgUtil.blue (30,30) (70,90) bmp

do ImgUtil.setPixel ImgUtil.blue (40,40) bmp

do ImgUtil.show "Ex" bmp
