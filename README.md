# Literay

Literay is a raytracer for rendering of high-quality static scenes. This is not done in real-time, so therefore it is not in the same category as a 3D engine. However, raytracing facilitates different interesting graphics features. For instance, non-polygonal primitives such as those on the screenshots as well as textures, bump maps, anti-aliasing and especially realistic lighting are key features to raytracing. You can create [Lua](https://en.wikipedia.org/wiki/Lua_%28programming_language%29) scripts which generate scenes that are set up static, but contain entities generated on the fly, in loops or using recursive functions.

Literay's Lua scripts can also be used to render multiple sequentive images, thus creating a moving scene. These images can be then converted to a video *(conversion is not part of Literay)*. Lua scripts leave you enough options to create any set of entities and light settings to render. The best example of how Lua is superior over a static markup script is the sphere fractal as on the screenshot below.

Literay also takes advantage of multi-core processors. It renders multiple times faster on a CPU with 4 cores and hyper threading in comparison to using only a single CPU or thread. Raytracing is a task that can be very easily parallelized.

These scenes can also be downloaded as PNG files, rendered at 1920x1080 and high quality settings.





## Features

* Primitives
  * Plane
  * Cube
  * Sphere
  * Cylinder
  * Tube
* Lighting, shadows & soft shadows
* Reflection
* Anti-Aliasing (different pre-defined OG / RG variants, from 4x to 16x)
* Textures (with bi-linear filtering)
* Runtime generated scenes using Lua scripts
* A scene editor
* Multi-threaded rendering implemented in a native C++ DLL

## Screenshots

[![](https://bytecode77.com/cache/thumbs/?path=images/sites/garage/software/literay/gallery/001.jpg&height=100)](https://bytecode77.com/images/sites/garage/software/literay/gallery/001.jpg)
[![](https://bytecode77.com/cache/thumbs/?path=images/sites/garage/software/literay/gallery/002.jpg&height=100)](https://bytecode77.com/images/sites/garage/software/literay/gallery/002.jpg)
[![](https://bytecode77.com/cache/thumbs/?path=images/sites/garage/software/literay/gallery/003.jpg&height=100)](https://bytecode77.com/images/sites/garage/software/literay/gallery/003.jpg)
[![](https://bytecode77.com/cache/thumbs/?path=images/sites/garage/software/literay/gallery/004.jpg&height=100)](https://bytecode77.com/images/sites/garage/software/literay/gallery/004.jpg)
[![](https://bytecode77.com/cache/thumbs/?path=images/sites/garage/software/literay/gallery/005.jpg&height=100)](https://bytecode77.com/images/sites/garage/software/literay/gallery/005.jpg)
[![](https://bytecode77.com/cache/thumbs/?path=images/sites/garage/software/literay/gallery/006.jpg&height=100)](https://bytecode77.com/images/sites/garage/software/literay/gallery/006.jpg)
[![](https://bytecode77.com/cache/thumbs/?path=images/sites/garage/software/literay/gallery/007.jpg&height=100)](https://bytecode77.com/images/sites/garage/software/literay/gallery/007.jpg)
[![](https://bytecode77.com/cache/thumbs/?path=images/sites/garage/software/literay/gallery/008.jpg&height=100)](https://bytecode77.com/images/sites/garage/software/literay/gallery/008.jpg)
[![](https://bytecode77.com/cache/thumbs/?path=images/sites/garage/software/literay/gallery/009.jpg&height=100)](https://bytecode77.com/images/sites/garage/software/literay/gallery/009.jpg)
[![](https://bytecode77.com/cache/thumbs/?path=images/sites/garage/software/literay/gallery/010.jpg&height=100)](https://bytecode77.com/images/sites/garage/software/literay/gallery/010.jpg)
[![](https://bytecode77.com/cache/thumbs/?path=images/sites/garage/software/literay/gallery/011.jpg&height=100)](https://bytecode77.com/images/sites/garage/software/literay/gallery/011.jpg)

## Downloads

[![](https://bytecode77.com/images/shared/fileicons/zip.png) Literay 6.0.2 Binaries.zip](https://bytecode77.com/downloads/garage/software/Literay%206.0.2%20Binaries.zip)

[![](https://bytecode77.com/images/shared/fileicons/zip.png) Literay 6.0.2 Rendered Scenes.zip](https://bytecode77.com/downloads/garage/software/Literay%206.0.2%20Rendered%20Scenes.zip)

## Project Page

[![](https://bytecode77.com/images/shared/favicon16.png) bytecode77.com/garage/software/literay](https://bytecode77.com/garage/software/literay)