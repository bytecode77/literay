# Literay

## Raytracer

Raytracing is the process of high-quality static image rendering. Literay implements different graphics features, such as non-polygonal primitives, textures, bump maps, anti-aliasing and especially realistic lighting. The [Lua](https://en.wikipedia.org/wiki/Lua_%28programming_language%29) scripting engine allows scene generation using the script editor.

Literay's Lua scripts can also be used to render multiple sequential images, thus creating a moving scene *(image to video conversion is not part of Literay)*. The scripting engine provides enough options to create any set of entities and lighting to render. The best example of how Lua is superior over a static markup script is the fractal scene.

Literay also takes advantage of multi-core processors, because raytracing is a task that can be very easily parallelized.

[![](https://bytecode77.com/images/pages/literay/001.thumb.jpg)](https://bytecode77.com/images/pages/literay/001.jpg)
[![](https://bytecode77.com/images/pages/literay/002.thumb.jpg)](https://bytecode77.com/images/pages/literay/002.jpg)
[![](https://bytecode77.com/images/pages/literay/003.thumb.jpg)](https://bytecode77.com/images/pages/literay/003.jpg)
[![](https://bytecode77.com/images/pages/literay/004.thumb.jpg)](https://bytecode77.com/images/pages/literay/004.jpg)
[![](https://bytecode77.com/images/pages/literay/005.thumb.jpg)](https://bytecode77.com/images/pages/literay/005.jpg)
[![](https://bytecode77.com/images/pages/literay/006.thumb.jpg)](https://bytecode77.com/images/pages/literay/006.jpg)
[![](https://bytecode77.com/images/pages/literay/007.thumb.jpg)](https://bytecode77.com/images/pages/literay/007.jpg)
[![](https://bytecode77.com/images/pages/literay/008.thumb.jpg)](https://bytecode77.com/images/pages/literay/008.jpg)
[![](https://bytecode77.com/images/pages/literay/009.thumb.jpg)](https://bytecode77.com/images/pages/literay/009.jpg)
[![](https://bytecode77.com/images/pages/literay/010.thumb.jpg)](https://bytecode77.com/images/pages/literay/010.jpg)

## Scene editor

Using the main application, scenes can be scripted and rendered.

*Please note that I have developed the scene editor when I was still learning WPF. This project's focus is the rendering engine.*

![](https://bytecode77.com/images/pages/literay/app.png)

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

[![](http://bytecode77.com/public/fileicons/zip.png) Literay 6.0.2.zip](https://downloads.bytecode77.com/Literay%206.0.2.zip)<br />
[![](http://bytecode77.com/public/fileicons/zip.png) Literay 6.0.2 Rendered Scenes.zip](https://downloads.bytecode77.com/Literay%206.0.2%20Rendered%20Scenes.zip)

## Project Page

[![](https://bytecode77.com/public/favicon16.png) bytecode77.com/literay](https://bytecode77.com/literay)