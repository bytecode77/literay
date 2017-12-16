using System.Collections.Generic;
using System.Linq;

namespace Literay
{
	public class RaytracingObject
	{
		public static List<RaytracingObject> AllRaytracingObjects = new List<RaytracingObject>();
		private static int CurrentHandle = 1000000;

		public int Handle { get; private set; }

		public RaytracingObject()
		{
			Handle = CurrentHandle++;
			AllRaytracingObjects.Add(this);
		}

		public static RaytracingObject GetFromHandle(int handle)
		{
			return AllRaytracingObjects.FirstOrDefault(raytracingObject => raytracingObject.Handle == handle);
		}
	}
}