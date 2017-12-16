#include "LiterayCore.h"

bool Plane::Pick(Vector3f rayPosition, Vector3f rayNormal, bool infinite, PickResult &pickResult)
{
	float d = Normal.DotProduct(rayNormal);
	if (d > -.001f) return false;
	float u = -(Normal.DotProduct(rayPosition) - D) / d;
	if (!infinite && (u < 0 || u > 1)) return false;
	pickResult.Position = rayPosition + rayNormal * u;
	pickResult.Distance = (pickResult.Position - rayPosition).getLength();
	pickResult.Normal = Normal;
	pickResult.TextureCoordinates.X = pickResult.Position.X;
	pickResult.TextureCoordinates.Y = pickResult.Position.Z;
	pickResult.TextureIndex = 1;
	return true;
}