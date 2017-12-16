#include "LiterayCore.h"

vector<Light*> *Global::Lights;
vector<Entity*> *Global::Entities;
vector<Texture*> *Global::Textures;

int Global::ScreenWidth;
int Global::ScreenHeight;
int Global::MaximumRecursion;
int Global::ShadowSamples;
int Global::SubPixelsCount;
Vector2f *Global::SubPixels;

Vector3f Global::CameraPosition;
Vector2f Global::CameraAngle;
float Global::CameraPitchSin;
float Global::CameraPitchCos;
float Global::CameraYawSin;
float Global::CameraYawCos;
float Global::CameraZoom;
Color Global::ClsColor;
Color Global::AmbientColor;
bool Global::FogEnabled;
float Global::FogRange;

float *Global::Entropy;
int Global::EntropyPointer;

Texture *Global::GetTextureByHandle(int handle)
{
	for (Texture *texture : *Textures)
	{
		if (texture->Handle == handle) return texture;
	}
	return nullptr;
}