# Literay

## Raytracer

Raytracing is the process of high-quality static image rendering. Literay implements different graphics features, such as non-polygonal primitives, textures, bump maps, anti-aliasing and especially realistic lighting. The [Lua](https://en.wikipedia.org/wiki/Lua_%28programming_language%29) scripting engine allows scene generation using the script editor.

Literay's Lua scripts can also be used to render multiple sequential images, thus creating a moving scene *(image to video conversion is not part of Literay)*. The scripting engine provides enough options to create any set of entities and lighting to render. The best example of how Lua is superior over a static markup script is the fractal scene.

Literay also takes advantage of multi-core processors, because raytracing is a task that can be very easily parallelized.

[![](/.github/screenshots/001.thumb.jpg)](/.github/screenshots/001.jpg)
[![](/.github/screenshots/002.thumb.jpg)](/.github/screenshots/002.jpg)
[![](/.github/screenshots/003.thumb.jpg)](/.github/screenshots/003.jpg)
[![](/.github/screenshots/004.thumb.jpg)](/.github/screenshots/004.jpg)
[![](/.github/screenshots/005.thumb.jpg)](/.github/screenshots/005.jpg)
[![](/.github/screenshots/006.thumb.jpg)](/.github/screenshots/006.jpg)
[![](/.github/screenshots/007.thumb.jpg)](/.github/screenshots/007.jpg)
[![](/.github/screenshots/008.thumb.jpg)](/.github/screenshots/008.jpg)
[![](/.github/screenshots/009.thumb.jpg)](/.github/screenshots/009.jpg)
[![](/.github/screenshots/010.thumb.jpg)](/.github/screenshots/010.jpg)

## Scene editor

Using the main application, scenes can be scripted and rendered.

*Please note that I have developed the scene editor when I was still learning WPF. This project's focus is the rendering engine.*

![](/.github/screenshots/app.png)

## Features

- Primitives *(Cube, Sphere, Cylinder, Tube, Plane)*
- Lighting, shadows & soft shadows
- Reflection
- Anti-Aliasing
- Textures, bi-linear filtering
- Lua scene editor for runtime generated scenes
- Multi-threaded rendering
- Actual rendering is implemented in native C++

## Downloads

[![](http://bytecode77.com/public/fileicons/zip.png) Literay 6.0.2.zip](/.github/downloads/Literay%206.0.2.zip)<br />
[![](http://bytecode77.com/public/fileicons/zip.png) Literay 6.0.2 Rendered Scenes.zip](/.github/downloads/Literay%206.0.2%20Rendered%20Scenes.zip)