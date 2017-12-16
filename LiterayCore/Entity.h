class Entity
{
public:
	Color Color;
	float Reflection;
	float SpecularPower, SpecularIntensity;
	Texture *Texture, *Texture2, *Bumpmap, *Bumpmap2;
	Vector2f TextureSize, Texture2Size, BumpmapSize, Bumpmap2Size;
	float BumpmapStrength, Bumpmap2Strength;

	virtual bool Pick(Vector3f rayPosition, Vector3f rayNormal, bool infinite, PickResult &pickResult) = 0;
};