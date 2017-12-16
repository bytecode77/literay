class Sphere : public Entity
{
public:
	Vector3f Position;
	float Radius, InverseRadius, RadiusPow2;

	bool Pick(Vector3f rayPosition, Vector3f rayNormal, bool infinite, PickResult &pickResult);
};