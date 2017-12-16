#include "LiterayCore.h"

bool Sphere::Pick(Vector3f rayPosition, Vector3f rayNormal, bool infinite, PickResult &pickResult)
{
	float raylen = rayNormal.getLength();
	rayNormal = rayNormal.Normalize();
	Vector3f v = Position - rayPosition;
	float d = v.DotProduct(rayNormal);
	if (d > 0)
	{
		Vector3f p = rayPosition + rayNormal * d;
		d = (p - Position).DotProduct(p - Position);
		if (d < RadiusPow2)
		{
			d = sqrt(RadiusPow2 - d);
			pickResult.Position = p - rayNormal * d;
			pickResult.Distance = (pickResult.Position - rayPosition).getLength();
			if (!infinite && pickResult.Distance > raylen) return false;
			pickResult.Normal = (pickResult.Position - Position) * InverseRadius;
			pickResult.TextureCoordinates.X = atan2(pickResult.Position.X - Position.X, pickResult.Position.Z - Position.Z) * InversePi2;
			pickResult.TextureCoordinates.Y = (pickResult.Position.Y - Position.Y + Radius) * InverseRadius / 2;
			pickResult.TextureIndex = 1;
			return true;
		}
	}
	return false;
}