#include "LiterayCore.h"

void Initialize()
{
	Global::Lights = new vector<Light*>();
	Global::Entities = new vector<Entity*>();
	Global::Textures = new vector<Texture*>();
}
void Destroy()
{
	for (Light *light : *Global::Lights) delete light;
	for (Entity *entity : *Global::Entities) delete entity;
	for (Texture *texture : *Global::Textures) delete texture;
	Global::Lights->clear();
	Global::Entities->clear();
	Global::Textures->clear();
	delete Global::Lights;
	delete Global::Entities;
	delete Global::Textures;
	delete[] Global::SubPixels;
}
void SetGlobal(
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
	float fogRange)
{
	Global::ScreenWidth = screenWidth;
	Global::ScreenHeight = screenHeight;
	Global::MaximumRecursion = maximumRecursion;
	Global::ShadowSamples = shadowSamples;
	Global::SubPixelsCount = subPixelsCount;
	Global::SubPixels = new Vector2f[subPixelsCount];
	memcpy(Global::SubPixels, subPixels, subPixelsCount * 8);

	Global::CameraPosition = cameraPosition;
	Global::CameraAngle = cameraAngle;
	Global::CameraPitchSin = sind(cameraAngle.X);
	Global::CameraPitchCos = cosd(cameraAngle.X);
	Global::CameraYawSin = sind(cameraAngle.Y);
	Global::CameraYawCos = cosd(cameraAngle.Y);
	Global::CameraZoom = cameraZoom;
	Global::ClsColor = clsColor;
	Global::AmbientColor = ambientColor;
	Global::FogEnabled = fogEnabled;
	Global::FogRange = fogRange;

	Global::Entropy = new float[EntropyCount];
	Global::EntropyPointer = 0;
	for (int i = 0; i < EntropyCount; i++) Global::Entropy[i] = rand() * 1.f / RAND_MAX - .5f;
}
int RenderPixel(int x, int y)
{
	Color sum;

	for (int i = 0; i < Global::SubPixelsCount; i++)
	{
		Vector3f normal = Vector3f(
			x + Global::SubPixels[i].X - Global::ScreenWidth / 2,
			Global::ScreenHeight / 2 - y - Global::SubPixels[i].Y,
			Global::ScreenWidth * Global::CameraZoom / 2);

		if (Global::CameraAngle.X != 0 || Global::CameraAngle.Y != 0)
		{
			normal = Vector3f(
				normal.X * Global::CameraYawCos + (normal.Y * Global::CameraPitchSin + normal.Z * Global::CameraPitchCos) * Global::CameraYawSin,
				normal.Y * Global::CameraPitchCos - normal.Z * Global::CameraPitchSin,
				-normal.X * Global::CameraYawSin + (normal.Y * Global::CameraPitchSin + normal.Z * Global::CameraPitchCos) * Global::CameraYawCos);
		}

		sum += SendRay(Global::CameraPosition, normal, 0);
	}

	if (Global::SubPixelsCount > 1) sum /= float(Global::SubPixelsCount);
	return sum.getRgb();
}

void CreateLight(Vector3f position, Vector3f size, float brightness, Color color)
{
	Light *light = new Light();
	light->Position = position;
	light->Size = size;
	light->Brightness = brightness;
	light->Color = color;
	Global::Lights->push_back(light);
}
void CreatePlane(Vector3f normal,
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
	float bumpmap2Strength)
{
	Plane *plane = new Plane();
	plane->Normal = normal.Normalize();
	plane->D = d;
	plane->Color = color;
	plane->Reflection = reflection;
	plane->SpecularPower = specularPower;
	plane->SpecularIntensity = specularIntensity;
	plane->Texture = Global::GetTextureByHandle(textureHandle);
	plane->Texture2 = Global::GetTextureByHandle(texture2Handle);
	plane->Bumpmap = Global::GetTextureByHandle(bumpmapHandle);
	plane->Bumpmap2 = Global::GetTextureByHandle(bumpmap2Handle);
	plane->TextureSize = textureSize;
	plane->Texture2Size = texture2Size;
	plane->BumpmapSize = bumpmapSize;
	plane->Bumpmap2Size = bumpmap2Size;
	plane->BumpmapStrength = bumpmapStrength;
	plane->Bumpmap2Strength = bumpmap2Strength;
	Global::Entities->push_back(plane);
}
void CreateCube(
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
	float bumpmap2Strength)
{
	Cube *cube = new Cube();
	cube->Position = position;
	cube->Size = size;
	cube->BoxMin = position - size / 2;
	cube->BoxMax = position + size / 2;
	cube->Color = color;
	cube->Reflection = reflection;
	cube->SpecularPower = specularPower;
	cube->SpecularIntensity = specularIntensity;
	cube->Texture = Global::GetTextureByHandle(textureHandle);
	cube->Texture2 = Global::GetTextureByHandle(texture2Handle);
	cube->Bumpmap = Global::GetTextureByHandle(bumpmapHandle);
	cube->Bumpmap2 = Global::GetTextureByHandle(bumpmap2Handle);
	cube->TextureSize = textureSize;
	cube->Texture2Size = texture2Size;
	cube->BumpmapSize = bumpmapSize;
	cube->Bumpmap2Size = bumpmap2Size;
	cube->BumpmapStrength = bumpmapStrength;
	cube->Bumpmap2Strength = bumpmap2Strength;
	Global::Entities->push_back(cube);
}
void CreateSphere(
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
	float bumpmap2Strength)
{
	Sphere *sphere = new Sphere();
	sphere->Position = position;
	sphere->Radius = radius;
	sphere->InverseRadius = 1 / radius;
	sphere->RadiusPow2 = radius * radius;
	sphere->Color = color;
	sphere->Reflection = reflection;
	sphere->SpecularPower = specularPower;
	sphere->SpecularIntensity = specularIntensity;
	sphere->Texture = Global::GetTextureByHandle(textureHandle);
	sphere->Texture2 = Global::GetTextureByHandle(texture2Handle);
	sphere->Bumpmap = Global::GetTextureByHandle(bumpmapHandle);
	sphere->Bumpmap2 = Global::GetTextureByHandle(bumpmap2Handle);
	sphere->TextureSize = textureSize;
	sphere->Texture2Size = texture2Size;
	sphere->BumpmapSize = bumpmapSize;
	sphere->Bumpmap2Size = bumpmap2Size;
	sphere->BumpmapStrength = bumpmapStrength;
	sphere->Bumpmap2Strength = bumpmap2Strength;
	Global::Entities->push_back(sphere);
}
void CreateCylinder(
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
	float bumpmap2Strength)
{
	Cylinder *cylinder = new Cylinder();
	cylinder->Position = position;
	cylinder->Radius = radius;
	cylinder->RadiusPow2 = radius * radius;
	cylinder->InverseRadius = 1 / radius;
	cylinder->InverseRadius2 = .5f / radius;
	cylinder->Height = height;
	cylinder->BoxMin = cylinder->Position - Vector3f(radius, height / 2, radius);
	cylinder->BoxMax = cylinder->Position + Vector3f(radius, height / 2, radius);
	cylinder->Color = color;
	cylinder->Reflection = reflection;
	cylinder->SpecularPower = specularPower;
	cylinder->SpecularIntensity = specularIntensity;
	cylinder->Texture = Global::GetTextureByHandle(textureHandle);
	cylinder->Texture2 = Global::GetTextureByHandle(texture2Handle);
	cylinder->Bumpmap = Global::GetTextureByHandle(bumpmapHandle);
	cylinder->Bumpmap2 = Global::GetTextureByHandle(bumpmap2Handle);
	cylinder->TextureSize = textureSize;
	cylinder->Texture2Size = texture2Size;
	cylinder->BumpmapSize = bumpmapSize;
	cylinder->Bumpmap2Size = bumpmap2Size;
	cylinder->BumpmapStrength = bumpmapStrength;
	cylinder->Bumpmap2Strength = bumpmap2Strength;
	Global::Entities->push_back(cylinder);
}
void CreateTube(
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
	float bumpmap2Strength)
{
	Tube *tube = new Tube();
	tube->Position = position;
	tube->Radius = radius;
	tube->RadiusPow2 = radius * radius;
	tube->InverseRadius = 1 / radius;
	tube->InverseRadius2 = .5f / radius;
	tube->InnerRadius = innerRadius;
	tube->InverseInnerRadius = 1 / innerRadius;
	tube->Height = height;
	tube->BoxMin = tube->Position - Vector3f(radius, height / 2, radius);
	tube->BoxMax = tube->Position + Vector3f(radius, height / 2, radius);
	tube->Color = color;
	tube->Reflection = reflection;
	tube->SpecularPower = specularPower;
	tube->SpecularIntensity = specularIntensity;
	tube->Texture = Global::GetTextureByHandle(textureHandle);
	tube->Texture2 = Global::GetTextureByHandle(texture2Handle);
	tube->Bumpmap = Global::GetTextureByHandle(bumpmapHandle);
	tube->Bumpmap2 = Global::GetTextureByHandle(bumpmap2Handle);
	tube->TextureSize = textureSize;
	tube->Texture2Size = texture2Size;
	tube->BumpmapSize = bumpmapSize;
	tube->Bumpmap2Size = bumpmap2Size;
	tube->BumpmapStrength = bumpmapStrength;
	tube->Bumpmap2Strength = bumpmap2Strength;
	Global::Entities->push_back(tube);
}
void LoadTexture(int handle, int width, int height, int *buffer)
{
	Texture *texture = new Texture();
	texture->Handle = handle;
	texture->Width = width;
	texture->Height = height;
	texture->WidthExponent = int(log2(texture->Width));
	texture->Buffer = new int[width * height];
	memcpy(texture->Buffer, buffer, width * height << 2);
	Global::Textures->push_back(texture);
}

Color SendRay(Vector3f rayPosition, Vector3f rayNormal, int recursionCount)
{
	Color color = Global::ClsColor;

	Entity *pickedEntity = nullptr;
	PickResult pickResult;
	for (Entity *entity : *Global::Entities)
	{
		PickResult newPickResult;
		if (entity->Pick(rayPosition, rayNormal, true, newPickResult) && (pickedEntity == nullptr || newPickResult.Distance < pickResult.Distance))
		{
			pickedEntity = entity;
			pickResult = newPickResult;
		}
	}

	if (pickedEntity != nullptr)
	{
		color = pickedEntity->Color;

		if (pickResult.TextureIndex == 1 && pickedEntity->Texture != nullptr)
		{
			color *= pickedEntity->Texture->GetTexel(pickResult.TextureCoordinates, pickedEntity->TextureSize);
		}
		else if (pickResult.TextureIndex == 2 && pickedEntity->Texture2 != nullptr)
		{
			color *= pickedEntity->Texture2->GetTexel(pickResult.TextureCoordinates, pickedEntity->Texture2Size);
		}
		if (pickResult.TextureIndex == 1 && pickedEntity->Bumpmap != nullptr)
		{
			Vector3f bumpNormal = pickedEntity->Bumpmap->GetNormal(pickResult.TextureCoordinates, pickedEntity->BumpmapSize, pickedEntity->BumpmapStrength);
			pickResult.Normal = (pickResult.Normal + bumpNormal).Normalize();
		}
		else if (pickResult.TextureIndex == 2 && pickedEntity->Bumpmap2 != nullptr)
		{
			Vector3f bumpNormal = pickedEntity->Bumpmap2->GetNormal(pickResult.TextureCoordinates, pickedEntity->Bumpmap2Size, pickedEntity->Bumpmap2Strength);
			pickResult.Normal = (pickResult.Normal + bumpNormal).Normalize();
		}

		if (recursionCount < Global::MaximumRecursion && pickedEntity->Reflection > 0)
		{
			Vector3f reflection = rayNormal - pickResult.Normal * 2 * pickResult.Normal.DotProduct(rayNormal);
			color += SendRay(pickResult.Position, reflection, recursionCount + 1) * pickedEntity->Reflection;
		}

		Color lightColor1, lightColor2;
		GetLightingColor(pickResult.Position, pickResult.Normal, rayNormal, pickedEntity, lightColor1, lightColor2);
		color = color * lightColor1 + lightColor2;

		if (Global::FogEnabled)
		{
			float fogDistance = max((Global::FogRange - pickResult.Distance) / Global::FogRange, 0);
			color = color * fogDistance + Global::ClsColor * (1 - fogDistance);
		}
	}

	return color.Clip();
}
void GetLightingColor(Vector3f position, Vector3f normal, Vector3f rayNormal, Entity *entity, Color &color1, Color &color2)
{
	color1 = Global::AmbientColor;
	rayNormal = rayNormal.Normalize();

	for (Light *light : *Global::Lights)
	{
		Vector3f lightVector = light->Position - position;
		Vector3f lightVectorNormalized = lightVector.Normalize();
		Vector3f reflection = lightVectorNormalized - normal * 2 * normal.DotProduct(lightVectorNormalized);

		if (Global::ShadowSamples == 0 || light->Size.X == 0 && light->Size.Y == 0 && light->Size.Z == 0)
		{
			if (!PickFast(position, lightVector))
			{
				float diffuse = lightVector.DotProduct(normal) / lightVector.DotProduct(lightVector) * light->Brightness;
				float specular = abs(pow(max(reflection.DotProduct(rayNormal), 0), entity->SpecularPower)) * entity->SpecularIntensity * light->Brightness;
				if (diffuse > 0) color1 += light->Color * diffuse;
				if (specular > 0) color2 += light->Color * specular;
			}
		}
		else
		{
			int hits = Global::ShadowSamples;
			for (int i = 0; i < Global::ShadowSamples; i++)
			{
				float entropy1 = Global::Entropy[Global::EntropyPointer++];
				float entropy2 = Global::Entropy[Global::EntropyPointer++];
				float entropy3 = Global::Entropy[Global::EntropyPointer++];
				Global::EntropyPointer %= EntropyCount - 3;
				if (PickFast(position, lightVector + Vector3f(entropy1, entropy2, entropy3) * light->Size)) hits--;
			}
			if (hits > 0)
			{
				float diffuse = lightVector.DotProduct(normal) / lightVector.DotProduct(lightVector) * light->Brightness * hits / Global::ShadowSamples;
				float specular = abs(pow(max(reflection.DotProduct(rayNormal), 0), entity->SpecularPower)) * entity->SpecularIntensity * light->Brightness;
				if (diffuse > 0) color1 += light->Color * diffuse;
				if (specular > 0) color2 += light->Color * specular;
			}
		}
	}
}
bool PickFast(Vector3f position, Vector3f normal)
{
	PickResult pickResult;
	for (Entity *entity : *Global::Entities)
	{
		if (entity->Pick(position, normal, false, pickResult)) return true;
	}
	return false;
}