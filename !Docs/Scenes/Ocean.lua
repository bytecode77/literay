water_bump = loadtexture("Textures\\Water_Bump.jpg")

camera = createcamera()
setposition(camera, 0, 7.5, -15)
setangle(camera, 20, 0)
setclscolor(camera, 200, 230, 255)
setfogenabled(camera, true)
setfogrange(camera, 100)

light = createlight()
setposition(light, 40, 20, 100)
setsize(light, 1.5, 1.5, 1.5)
setcolor(light, 255, 240, 80)
setbrightness(light, 100)

plane = createplane()
setcolor(plane, 10, 40, 90)
setreflection(plane, .3)
setspecular(plane, 1000, .5)
setbumpmap(plane, water_bump, 20, 20, 2)

render(camera)