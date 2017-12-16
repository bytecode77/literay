#define _USE_MATH_DEFINES
#include <math.h>
#include <vector>
#include <Windows.h>
using namespace std;

#include "Vector3f.h"
#include "Vector2f.h"
#include "Color.h"
#include "Texture.h"
#include "PickResult.h"
#include "Entity.h"
#include "Light.h"
#include "Plane.h"
#include "Cube.h"
#include "Sphere.h"
#include "Cylinder.h"
#include "Tube.h"
#include "Global.h"

const float InversePi2 = .5f / float(M_PI);
const int EntropyCount = 1024 * 1024;
#define clip(value, start, end) min(max(value, start), end)
#define sind(value) sin((value) * float(M_PI / 180))
#define cosd(value) cos((value) * float(M_PI / 180))
#define tand(value) tan((value) * float(M_PI / 180))
#define randf() (rand() * 1.f / RAND_MAX)

extern "C"
{
	__declspec(dllexport) void Initialize();
	__declspec(dllexport) void Destroy();
	__declspec(dllexport) void SetGlobal(
		int screenWidth,
		int screenHeight,
		int maximumRecursion,
		int shadowSamples,
		int subPixelsCount,
		Vector2f *subPixels,
		Vector3f cameraPosition,
		Vector2f cameraAngle,
		float cameraZoom,
		Color clsColor,
		Color ambientColor,
		bool fogEnabled,
		float fogRange);
	__declspec(dllexport) int RenderPixel(int x, int y);

	__declspec(dllexport) void CreateLight(Vector3f position, Vector3f size, float brightness, Color color);
	__declspec(dllexport) void CreatePlane(
		Vector3f normal,
		float d,
		Color color,
		float reflection,
		float specularPower,
		float specularIntensity,
		int textureHandle,
		int texture2Handle,
		int bumpmapHandle,
		int bumpmap2Handle,
		Vector2f textureSize,
		Vector2f texture2Size,
		Vector2f bumpmapSize,
		Vector2f bumpmap2Size,
		float bumpmapStrength,
		float bumpmap2Strength);
	__declspec(dllexport) void CreateCube(
		Vector3f position,
		Vector3f size,
		Color color,
		float reflection,
		float specularPower,
		float specularIntensity,
		int textureHandle,
		int texture2Handle,
		int bumpmapHandle,
		int bumpmap2Handle,
		Vector2f textureSize,
		Vector2f texture2Size,
		Vector2f bumpmapSize,
		Vector2f bumpmap2Size,
		float bumpmapStrength,
		float bumpmap2Strength);
	__declspec(dllexport) void CreateSphere(
		Vector3f position,
		float radius,
		Color color,
		float reflection,
		float specularPower,
		float specularIntensity,
		int textureHandle,
		int texture2Handle,
		int bumpmapHandle,
		int bumpmap2Handle,
		Vector2f textureSize,
		Vector2f texture2Size,
		Vector2f bumpmapSize,
		Vector2f bumpmap2Size,
		float bumpmapStrength,
		float bumpmap2Strength);
	__declspec(dllexport) void CreateCylinder(
		Vector3f position,
		float radius,
		float height,
		Color color,
		float reflection,
		float specularPower,
		float specularIntensity,
		int textureHandle,
		int texture2Handle,
		int bumpmapHandle,
		int bumpmap2Handle,
		Vector2f textureSize,
		Vector2f texture2Size,
		Vector2f bumpmapSize,
		Vector2f bumpmap2Size,
		float bumpmapStrength,
		float bumpmap2Strength);
	__declspec(dllexport) void CreateTube(
		Vector3f position,
		float radius,
		float innerRadius,
		float height,
		Color color,
		float reflection,
		float specularPower,
		float specularIntensity,
		int textureHandle,
		int texture2Handle,
		int bumpmapHandle,
		int bumpmap2Handle,
		Vector2f textureSize,
		Vector2f texture2Size,
		Vector2f bumpmapSize,
		Vector2f bumpmap2Size,
		float bumpmapStrength,
		float bumpmap2Strength);
	__declspec(dllexport) void LoadTexture(int handle, int width, int height, int *buffer);
}

Color SendRay(Vector3f rayPosition, Vector3f rayNormal, int recursionCount);
void GetLightingColor(Vector3f position, Vector3f normal, Vector3f rayNormal, Entity *entity, Color &color1, Color &color2);
bool PickFast(Vector3f position, Vector3f normal);