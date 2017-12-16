class Texture
{
public:
	int Handle;
	int Width, Height;
	int WidthExponent;
	int *Buffer;

	~Texture();

	Color GetTexel(Vector2f coordinates, Vector2f size);
	Vector3f GetNormal(Vector2f coordinates, Vector2f size, float strength);
};