class Tube : public Entity
{
public:
	Vector3f Position;
	float Radius, InverseRadius, InverseRadius2, RadiusPow2;
	float InnerRadius, InverseInnerRadius;
	float Height;
	Vector3f BoxMin, BoxMax;

	bool Pick(Vector3f rayPosition, Vector3f rayNormal, bool infinite, PickResult &pickResult);
};