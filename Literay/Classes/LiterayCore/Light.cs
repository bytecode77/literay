namespace Literay
{
	public class Light : RaytracingObject
	{
		public Vector3f Position;
		public Vector3f Size;
		public float Brightness;
		public LiterayColor Color;

		public Light()
		{
			Color = new LiterayColor(255, 255, 255);
		}
	}
}