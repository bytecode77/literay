class Cube : public Entity
{
public:
	Vector3f Position, Size, BoxMin, BoxMax;

	bool Pick(Vector3f rayPosition, Vector3f rayNormal, bool infinite, PickResult &pickResult);
};