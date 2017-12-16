class Global
{
public:
	static vector<Light*> *Lights;
	static vector<Entity*> *Entities;
	static vector<Texture*> *Textures;

	static int ScreenWidth;
	static int ScreenHeight;
	static int MaximumRecursion;
	static int ShadowSamples;
	static int SubPixelsCount;
	static Vector2f *SubPixels;

	static Vector3f CameraPosition;
	static Vector2f CameraAngle;
	static float CameraPitchSin;
	static float CameraPitchCos;
	static float CameraYawSin;
	static float CameraYawCos;
	static float CameraZoom;
	static Color ClsColor;
	static Color AmbientColor;
	static bool FogEnabled;
	static float FogRange;

	static float *Entropy;
	static int EntropyPointer;

	static Texture *GetTextureByHandle(int handle);
};