class Plane : public Entity
{
public:
	Vector3f Normal;
	float D;

	bool Pick(Vector3f rayPosition, Vector3f rayNormal, bool infinite, PickResult &pickResult);
};