namespace Literay
{
	public class Tube : Entity
	{
		public Vector3f Position;
		public float Radius;
		public float InnerRadius;
		public float Height;

		public Tube()
		{
			Radius = 1;
			InnerRadius = .5f;
			Height = 1;
			Color = new LiterayColor(255, 255, 255);
		}
	}
}