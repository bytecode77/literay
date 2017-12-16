camera = createcamera()
setposition(camera, 0, 15, -60)
setclscolor(camera, 135, 205, 250)
setambientlight(camera, 127, 127, 127)
setfogenabled(camera, true)
setfogrange(camera, 200)

light = createlight()
setposition(light, 1000, 1000, -160)
setsize(light, 1000, 0, 1000)
setbrightness(light, 1000)

plane = createplane()
setcolor(plane, 255, 245, 190)
setreflection(plane, .1)

sphere = createsphere()
setposition(sphere, 0, 5, 0)
setradius(sphere, 10)
setcolor(sphere, 0, 0, 0)
setreflection(sphere, 1)
setspecular(sphere, 3, .0005)

for x = -1, 1, 2 do
	for y = -1, 1, 2 do
		cube = createcube()
		setposition(cube, x * 20, 10, y * 20)
		setsize(cube, 6, 20, 6)
		setcolor(cube, switch(x == 1, 255, 0), switch(x == y, 255, 0), switch(x == -1 and y == 1, 255, 0))
	end
end

render(camera)