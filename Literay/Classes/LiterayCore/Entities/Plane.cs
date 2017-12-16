namespace Literay
{
	public class Plane : Entity
	{
		public Vector3f Normal;
		public float D;

		public Plane()
		{
			Normal = new Vector3f(0, 1, 0);
		}
	}
}