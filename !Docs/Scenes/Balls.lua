camera = createcamera()
setposition(camera, 15, 30, -50)
setangle(camera, 30, -20)
setambientlight(camera, 120, 120, 120)
setfogenabled(camera, true)
setfogrange(camera, 400)

light = createlight()
setposition(light, -20, 50, -30)
setsize(light, 2, 2, 2)
setcolor(light, 255, 240, 180)
setbrightness(light, 50)
light = createlight()
setposition(light, 10, 20, 10)
setsize(light, 2, 2, 2)
setcolor(light, 130, 140, 255)
setbrightness(light, 60)
light = createlight()
setposition(light, 30, 50, -15)
setsize(light, 2, 2, 2)
setcolor(light, 180, 255, 220)
setbrightness(light, 45)

plane = createplane()
setplanevector(plane, .2, 1, 0, -15)
setcolor(plane, 110, 110, 110)
setreflection(plane, .3)
plane = createplane()
setplanevector(plane, 1, -.1, -.1, -60)
setcolor(plane, 110, 110, 110)
setreflection(plane, .3)

for x = -1, 1 do
	for y = -1, 1 do
		for z = -1, 1 do
			sphere = createsphere()
			setposition(sphere, x * 15, y * 15 + 10, z * 15)
			setradius(sphere, 3)
			setcolor(sphere, 50, 50, 50)
			setreflection(sphere, 1)
			setspecular(sphere, 20, .01)
		end
	end
end

render(camera)