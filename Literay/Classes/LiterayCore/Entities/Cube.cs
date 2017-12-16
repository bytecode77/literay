namespace Literay
{
	public class Cube : Entity
	{
		public Vector3f Position;
		public Vector3f Size;

		public Cube()
		{
			Size = new Vector3f(1, 1, 1);
		}
	}
}