class Cylinder : public Entity
{
public:
	Vector3f Position;
	float Radius, InverseRadius, InverseRadius2, RadiusPow2;
	float Height;
	Vector3f BoxMin, BoxMax;

	bool Pick(Vector3f rayPosition, Vector3f rayNormal, bool infinite, PickResult &pickResult);
};