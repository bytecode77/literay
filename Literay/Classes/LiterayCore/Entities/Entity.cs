namespace Literay
{
	public class Entity : RaytracingObject
	{
		public LiterayColor Color;
		public float Reflection;
		public float SpecularPower;
		public float SpecularIntensity;
		public Texture Texture;
		public Texture Texture2;
		public Texture Bumpmap;
		public Texture Bumpmap2;
		public Vector2f TextureSize;
		public Vector2f Texture2Size;
		public Vector2f BumpmapSize;
		public Vector2f Bumpmap2Size;
		public float BumpmapStrength;
		public float Bumpmap2Strength;

		public Entity()
		{
			Color = new LiterayColor(255, 255, 255);
		}
	}
}