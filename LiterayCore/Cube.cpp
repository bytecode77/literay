#include "LiterayCore.h"

bool Cube::Pick(Vector3f rayPosition, Vector3f rayNormal, bool infinite, PickResult &pickResult)
{
	pickResult.Normal = Vector3f();
	float raylen = rayNormal.getLength();
	if (rayNormal.X < 0 && BoxMax.X < rayPosition.X)
	{
		float u = (BoxMax.X - rayPosition.X) / rayNormal.X;
		pickResult.Position.X = BoxMax.X;
		pickResult.Position.Y = rayPosition.Y + u * rayNormal.Y;
		pickResult.Position.Z = rayPosition.Z + u * rayNormal.Z;
		if (pickResult.Position.Y >= BoxMin.Y && pickResult.Position.Y <= BoxMax.Y && pickResult.Position.Z >= BoxMin.Z && pickResult.Position.Z <= BoxMax.Z)
		{
			pickResult.Distance = (pickResult.Position - rayPosition).getLength();
			if (infinite || pickResult.Distance < raylen)
			{
				pickResult.Normal.X = 1;
				pickResult.TextureCoordinates.X = (pickResult.Position.Z - BoxMin.Z) / Size.Z;
				pickResult.TextureCoordinates.Y = (pickResult.Position.Y - BoxMin.Y) / Size.Y;
				pickResult.TextureIndex = 1;
				return true;
			}
		}
	}
	if (rayNormal.X > 0 && BoxMin.X > rayPosition.X)
	{
		float u = (BoxMin.X - rayPosition.X) / rayNormal.X;
		pickResult.Position.X = BoxMin.X;
		pickResult.Position.Y = rayPosition.Y + u * rayNormal.Y;
		pickResult.Position.Z = rayPosition.Z + u * rayNormal.Z;
		if (pickResult.Position.Y >= BoxMin.Y && pickResult.Position.Y <= BoxMax.Y && pickResult.Position.Z >= BoxMin.Z && pickResult.Position.Z <= BoxMax.Z)
		{
			pickResult.Distance = (pickResult.Position - rayPosition).getLength();
			if (infinite || pickResult.Distance < raylen)
			{
				pickResult.Normal.X = -1;
				pickResult.TextureCoordinates.X = (pickResult.Position.Z - BoxMin.Z) / Size.Z;
				pickResult.TextureCoordinates.Y = (pickResult.Position.Y - BoxMin.Y) / Size.Y;
				pickResult.TextureIndex = 1;
				return true;
			}
		}
	}
	if (rayNormal.Y < 0 && BoxMax.Y < rayPosition.Y)
	{
		float u = (BoxMax.Y - rayPosition.Y) / rayNormal.Y;
		pickResult.Position.X = rayPosition.X + u * rayNormal.X;
		pickResult.Position.Y = BoxMax.Y;
		pickResult.Position.Z = rayPosition.Z + u * rayNormal.Z;
		if (pickResult.Position.X >= BoxMin.X && pickResult.Position.X <= BoxMax.X && pickResult.Position.Z >= BoxMin.Z && pickResult.Position.Z <= BoxMax.Z)
		{
			pickResult.Distance = (pickResult.Position - rayPosition).getLength();
			if (infinite || pickResult.Distance < raylen)
			{
				pickResult.Normal.Y = 1;
				pickResult.TextureCoordinates.X = (pickResult.Position.X - BoxMin.X) / Size.X;
				pickResult.TextureCoordinates.Y = (pickResult.Position.Z - BoxMin.Z) / Size.Z;
				pickResult.TextureIndex = 1;
				return true;
			}
		}
	}
	if (rayNormal.Y > 0 && BoxMin.Y > rayPosition.Y)
	{
		float u = (BoxMin.Y - rayPosition.Y) / rayNormal.Y;
		pickResult.Position.X = rayPosition.X + u * rayNormal.X;
		pickResult.Position.Y = BoxMin.Y;
		pickResult.Position.Z = rayPosition.Z + u * rayNormal.Z;
		if (pickResult.Position.X >= BoxMin.X && pickResult.Position.X <= BoxMax.X && pickResult.Position.Z >= BoxMin.Z && pickResult.Position.Z <= BoxMax.Z)
		{
			pickResult.Distance = (pickResult.Position - rayPosition).getLength();
			if (infinite || pickResult.Distance < raylen)
			{
				pickResult.Normal.Y = -1;
				pickResult.TextureCoordinates.X = (pickResult.Position.X - BoxMin.X) / Size.X;
				pickResult.TextureCoordinates.Y = (pickResult.Position.Z - BoxMin.Z) / Size.Z;
				pickResult.TextureIndex = 1;
				return true;
			}
		}
	}
	if (rayNormal.Z < 0 && BoxMax.Z < rayPosition.Z)
	{
		float u = (BoxMax.Z - rayPosition.Z) / rayNormal.Z;
		pickResult.Position.X = rayPosition.X + u * rayNormal.X;
		pickResult.Position.Y = rayPosition.Y + u * rayNormal.Y;
		pickResult.Position.Z = BoxMax.Z;
		if (pickResult.Position.X >= BoxMin.X && pickResult.Position.X <= BoxMax.X && pickResult.Position.Y >= BoxMin.Y && pickResult.Position.Y <= BoxMax.Y)
		{
			pickResult.Distance = (pickResult.Position - rayPosition).getLength();
			if (infinite || pickResult.Distance < raylen)
			{
				pickResult.Normal.Z = 1;
				pickResult.TextureCoordinates.X = (pickResult.Position.X - BoxMin.X) / Size.X;
				pickResult.TextureCoordinates.Y = (pickResult.Position.Y - BoxMin.Y) / Size.Y;
				pickResult.TextureIndex = 1;
				return true;
			}
		}
	}
	if (rayNormal.Z > 0 && BoxMin.Z > rayPosition.Z)
	{
		float u = (BoxMin.Z - rayPosition.Z) / rayNormal.Z;
		pickResult.Position.X = rayPosition.X + u * rayNormal.X;
		pickResult.Position.Y = rayPosition.Y + u * rayNormal.Y;
		pickResult.Position.Z = BoxMin.Z;
		if (pickResult.Position.X >= BoxMin.X && pickResult.Position.X <= BoxMax.X && pickResult.Position.Y >= BoxMin.Y && pickResult.Position.Y <= BoxMax.Y)
		{
			pickResult.Distance = (pickResult.Position - rayPosition).getLength();
			if (infinite || pickResult.Distance < raylen)
			{
				pickResult.Normal.Z = -1;
				pickResult.TextureCoordinates.X = (pickResult.Position.X - BoxMin.X) / Size.X;
				pickResult.TextureCoordinates.Y = (pickResult.Position.Y - BoxMin.Y) / Size.Y;
				pickResult.TextureIndex = 1;
				return true;
			}
		}
	}
	return false;
}