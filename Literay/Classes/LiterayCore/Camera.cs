namespace Literay
{
	public class Camera : RaytracingObject
	{
		public Vector3f Position { get; set; }
		public Vector2f Angle { get; set; }
		public float Zoom { get; set; }
		public LiterayColor ClsColor { get; set; }
		public LiterayColor AmbientColor { get; set; }
		public bool FogEnabled { get; set; }
		public float FogRange { get; set; }

		public Camera()
		{
			Zoom = 1;
			AmbientColor = new LiterayColor(127, 127, 127);
			FogRange = 1000;
		}
	}
}