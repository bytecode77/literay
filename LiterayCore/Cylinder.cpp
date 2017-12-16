#include "LiterayCore.h"

bool Cylinder::Pick(Vector3f rayPosition, Vector3f rayNormal, bool infinite, PickResult &pickResult)
{
	float raylen = rayNormal.getLength();
	float a = rayNormal.X * rayNormal.X + rayNormal.Z * rayNormal.Z;
	float b = 2 * (rayNormal.X * (rayPosition.X - Position.X) + rayNormal.Z * (rayPosition.Z - Position.Z));
	float c = (rayPosition.X - Position.X) * (rayPosition.X - Position.X) + (rayPosition.Z - Position.Z) * (rayPosition.Z - Position.Z) - RadiusPow2;
	float d = b * b - 4 * a * c;
	if (d > 0)
	{
		float t = (-b - sqrt(d)) / (2 * a);
		if (t > 0)
		{
			pickResult.Position = rayPosition + rayNormal * t;
			if (pickResult.Position.Y >= BoxMin.Y && pickResult.Position.Y <= BoxMax.Y)
			{
				pickResult.Distance = (pickResult.Position - rayPosition).getLength();
				if (infinite || pickResult.Distance < raylen)
				{
					pickResult.Normal.X = (pickResult.Position.X - Position.X) * InverseRadius;
					pickResult.Normal.Y = 0;
					pickResult.Normal.Z = (pickResult.Position.Z - Position.Z) * InverseRadius;
					pickResult.TextureCoordinates.X = atan2(pickResult.Position.X - Position.X, pickResult.Position.Z - Position.Z) * InversePi2;
					pickResult.TextureCoordinates.Y = (pickResult.Position.Y - BoxMin.Y) / Height;
					pickResult.TextureIndex = 1;
					return true;
				}
			}
		}
	}
	pickResult.Normal.X = 0;
	pickResult.Normal.Z = 0;
	if (rayNormal.Y < 0 && BoxMax.Y < rayPosition.Y)
	{
		float u = (BoxMax.Y - rayPosition.Y) / rayNormal.Y;
		pickResult.Position.X = rayPosition.X + u * rayNormal.X;
		pickResult.Position.Y = BoxMax.Y;
		pickResult.Position.Z = rayPosition.Z + u * rayNormal.Z;
		if ((pickResult.Position.X - Position.X) * (pickResult.Position.X - Position.X) + (pickResult.Position.Z - Position.Z) * (pickResult.Position.Z - Position.Z) <= RadiusPow2)
		{
			pickResult.Distance = (pickResult.Position - rayPosition).getLength();
			if (infinite || pickResult.Distance < raylen)
			{
				pickResult.Normal.Y = 1;
				pickResult.TextureCoordinates.X = (pickResult.Position.X - BoxMin.X) * InverseRadius2;
				pickResult.TextureCoordinates.Y = (pickResult.Position.Z - BoxMin.Z) * InverseRadius2;
				pickResult.TextureIndex = 2;
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
		if ((pickResult.Position.X - Position.X) * (pickResult.Position.X - Position.X) + (pickResult.Position.Z - Position.Z) * (pickResult.Position.Z - Position.Z) <= RadiusPow2)
		{
			pickResult.Distance = (pickResult.Position - rayPosition).getLength();
			if (infinite || pickResult.Distance < raylen)
			{
				pickResult.Normal.Y = -1;
				pickResult.TextureCoordinates.X = (pickResult.Position.X - BoxMin.X) * InverseRadius2;
				pickResult.TextureCoordinates.Y = (pickResult.Position.Z - BoxMin.Z) * InverseRadius2;
				pickResult.TextureIndex = 2;
				return true;
			}
		}
	}
	return false;
}