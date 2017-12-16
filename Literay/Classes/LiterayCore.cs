using System.Drawing;
using System.Runtime.InteropServices;

namespace Literay
{
	public static class LiterayCore
	{
		[DllImport("LiterayCore.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Initialize();
		[DllImport("LiterayCore.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Destroy();
		[DllImport("LiterayCore.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetGlobal(
			int screenWidth,
			int screenHeight,
			int maximumRecursion,
			int shadowSamples,
			int subPixelsCount,
			Vector2f[] subPixels,
			Vector3f cameraPosition,
			Vector2f cameraAngle,
			float cameraZoom,
			LiterayColor clsColor,
			LiterayColor ambientColor,
			bool fogEnabled,
			float fogRange);
		[DllImport("LiterayCore.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int RenderPixel(int x, int y);

		[DllImport("LiterayCore.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void CreateLight(Vector3f position, Vector3f size, float brightness, LiterayColor color);
		[DllImport("LiterayCore.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void CreatePlane(
			Vector3f normal,
			float d,
			LiterayColor color,
			float reflection,
			float specularPower,
			float specularIntensity,
			int textureHandle,
			int texture2Handle,
			int bumpmapHandle,
			int bumpmap2Handle,
			Vector2f textureSize,
			Vector2f texture2Size,
			Vector2f bumpmapSize,
			Vector2f bumpmap2Size,
			float bumpmapStrength,
			float bumpmap2Strength);
		[DllImport("LiterayCore.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void CreateCube(
			Vector3f position,
			Vector3f size,
			LiterayColor color,
			float reflection,
			float specularPower,
			float specularIntensity,
			int textureHandle,
			int texture2Handle,
			int bumpmapHandle,
			int bumpmap2Handle,
			Vector2f textureSize,
			Vector2f texture2Size,
			Vector2f bumpmapSize,
			Vector2f bumpmap2Size,
			float bumpmapStrength,
			float bumpmap2Strength);
		[DllImport("LiterayCore.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void CreateSphere(
			Vector3f position,
			float radius,
			LiterayColor color,
			float reflection,
			float specularPower,
			float specularIntensity,
			int textureHandle,
			int texture2Handle,
			int bumpmapHandle,
			int bumpmap2Handle,
			Vector2f textureSize,
			Vector2f texture2Size,
			Vector2f bumpmapSize,
			Vector2f bumpmap2Size,
			float bumpmapStrength,
			float bumpmap2Strength);
		[DllImport("LiterayCore.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void CreateCylinder(
			Vector3f position,
			float radius,
			float height,
			LiterayColor color,
			float reflection,
			float specularPower,
			float specularIntensity,
			int textureHandle,
			int texture2Handle,
			int bumpmapHandle,
			int bumpmap2Handle,
			Vector2f textureSize,
			Vector2f texture2Size,
			Vector2f bumpmapSize,
			Vector2f bumpmap2Size,
			float bumpmapStrength,
			float bumpmap2Strength);
		[DllImport("LiterayCore.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void CreateTube(
			Vector3f position,
			float radius,
			float innerRadius,
			float height,
			LiterayColor color,
			float reflection,
			float specularPower,
			float specularIntensity,
			int textureHandle,
			int texture2Handle,
			int bumpmapHandle,
			int bumpmap2Handle,
			Vector2f textureSize,
			Vector2f texture2Size,
			Vector2f bumpmapSize,
			Vector2f bumpmap2Size,
			float bumpmapStrength,
			float bumpmap2Strength);
		[DllImport("LiterayCore.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void LoadTexture(int handle, int width, int height, int[] buffer);
	}
}