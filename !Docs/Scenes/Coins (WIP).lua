marble = loadtexture("Textures\\Marble.jpg")
coin1_bump = loadtexture("Textures\\Coin1_Bump.png")
coin2_bump = loadtexture("Textures\\Coin2_Bump.png")

camera = createcamera()
setposition(camera, 0, 6, -4)
setangle(camera, 60, 0)

light = createlight()
setposition(light, 7, 5, 5)
setsize(light, .5, .5, .5)
setbrightness(light, 2)
light = createlight()
setposition(light, -4, 3, 3)
setsize(light, .5, .5, .5)
setbrightness(light, 2)

plane = createplane()
settexture(plane, marble, 15, 15)

cylinder = createcylinder()
setposition(cylinder, -3, .3, 0)
setradius(cylinder, 2.5)
setheight(cylinder, .2)
setcolor(cylinder, 120, 120, 120)
setspecular(cylinder, 20, 1)
setbumpmap2(cylinder, coin1_bump, 1, 1, .3)
cylinder = createcylinder()
setposition(cylinder, 3, .1, 0)
setradius(cylinder, 2.5)
setheight(cylinder, .2)
setcolor(cylinder, 120, 120, 120)
setspecular(cylinder, 20, 1)
setbumpmap2(cylinder, coin2_bump, 1, 1, .3)

render(camera)