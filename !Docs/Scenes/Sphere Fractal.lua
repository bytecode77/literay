function createfractal(recursioncount, size, factor, x, y, z)
	sphere = createsphere()
	setposition(sphere, x, y, z)
	setradius(sphere, size)
	setcolor(sphere, 0, 0, 0)
	setspecular(sphere, 30, .01)
	setreflection(sphere, .75)
	
	if recursioncount > 0 then
		for j = -1, 1 do
			for i = 1, switch(j == 0, 5, 3) do
				rx = switch(j == 0, size * (1 + factor), size * (1 + factor) * .6)
				ry = switch(j == 0, 0, size * (1 + factor) * .85)
				count = switch(j == 0, 5, 3)
				ox = cos(i / count * pi() * 2 - .3) * rx
				oy = sin(j / count * pi() * 2 - .3) * ry
				oz = sin(i / count * pi() * 2 - .3) * rx
				createfractal(recursioncount - 1, size * factor, factor, x + ox, y + oy, z + oz)
			end
		end
	end
end

camera = createcamera()
setposition(camera, 0, 82, -50)
setangle(camera, 40, 0)
setclscolor(camera, 100, 100, 100)
setambientlight(camera, 0, 0, 0)
setfogenabled(camera, true)
setfogrange(camera, 750)

light = createlight()
setposition(light, -40, 80, -30)
setsize(light, 1, 1, 1)
setcolor(light, 255, 0, 0)
setbrightness(light, 100)
light = createlight()
setposition(light, 40, 80, -30)
setsize(light, 1, 1, 1)
setcolor(light, 0, 255, 0)
setbrightness(light, 100)
light = createlight()
setposition(light, 0, 80, 30)
setsize(light, 1, 1, 1)
setcolor(light, 0, 0, 255)
setbrightness(light, 100)

plane = createplane()
setcolor(plane, 150, 140, 50)
setreflection(plane, .5)

createfractal(3, 20, .25, 0, 40, 0)

render(camera)