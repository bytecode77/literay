using LuaInterface;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Literay
{
	public class LuaScript
	{
		private Random Random = new Random(Environment.TickCount);
		private RenderProperties RenderProperties;
		private WindowRenderer Window;
		private Lua Lua;
		private string FileName;
		private string FilePath;
		private Thread[] Threads;
		private object CurrentLine;
		private int[] Pixels;
		private bool IsStopped;
		private bool HasRenderedAtLeastOnce;

		public LuaScript(RenderProperties renderProperties, string fileName, string filePath)
		{
			RenderProperties = renderProperties;
			FileName = fileName;
			FilePath = filePath;

			string[] functions = new[]
			{
				"abs", "acos", "asin", "atan", "atan2", "ceil", "cos", "cosh", "exp", "floor", "log", "log10", "max", "min", "pow", "round", "sign", "sin", "sinh", "sqrt", "tan", "tanh", "rand", "pi",
				"switch_",
				"createcamera", "createlight", "createplane", "createcube", "createsphere", "createcylinder", "createtube", "loadtexture",
				"setzoom", "setangle", "setclscolor", "setambientlight", "setfogenabled", "setfogrange", "setbrightness", "setplanevector", "setposition", "setsize", "setradius", "setinnerradius", "setheight", "setcolor", "setreflection", "setspecular", "settexture", "settexture2", "setbumpmap", "setbumpmap2",
				"render", "debug"
			};

			RaytracingObject.AllRaytracingObjects.Clear();
			Lua = new Lua();
			foreach (string function in functions)
			{
				Lua.RegisterFunction(function.Replace("_", ""), this, GetType().GetMethod(function, BindingFlags.Instance | BindingFlags.NonPublic));
			}
		}
		public void Run(string script)
		{
			try
			{
				Lua.DoString(script);
				if (!HasRenderedAtLeastOnce)
				{
					MessageBox.Show("No images were rendered. Did you forget to call render()?", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
			}
			catch (LuaException ex)
			{
				MessageBox.Show(ex.Message.Replace("[string \"chunk\"]:", "Line: "), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch { }
		}
		public void Stop()
		{
			IsStopped = true;
			foreach (Thread thread in Threads)
			{
				while (thread.IsAlive) Thread.Sleep(1);
			}
		}

		private float abs(float x)
		{
			return (float)Math.Abs(x);
		}
		private float acos(float x)
		{
			return (float)Math.Acos(x);
		}
		private float asin(float x)
		{
			return (float)Math.Asin(x);
		}
		private float atan(float x)
		{
			return (float)Math.Atan(x);
		}
		private float atan2(float y, float x)
		{
			return (float)Math.Atan2(y, x);
		}
		private float ceil(float x)
		{
			return (float)Math.Ceiling(x);
		}
		private float cos(float x)
		{
			return (float)Math.Cos(x);
		}
		private float cosh(float x)
		{
			return (float)Math.Cosh(x);
		}
		private float exp(float x)
		{
			return (float)Math.Exp(x);
		}
		private float floor(float x)
		{
			return (float)Math.Floor(x);
		}
		private float log(float x)
		{
			return (float)Math.Log(x);
		}
		private float log10(float x)
		{
			return (float)Math.Log10(x);
		}
		private float max(float x, float y)
		{
			return (float)Math.Max(x, y);
		}
		private float min(float x, float y)
		{
			return (float)Math.Min(x, y);
		}
		private float pow(float x, float y)
		{
			return (float)Math.Pow(x, y);
		}
		private float round(float x)
		{
			return (float)Math.Round(x);
		}
		private int sign(float x)
		{
			return Math.Sign(x);
		}
		private float sin(float x)
		{
			return (float)Math.Sin(x);
		}
		private float sinh(float x)
		{
			return (float)Math.Sinh(x);
		}
		private float sqrt(float x)
		{
			return (float)Math.Sqrt(x);
		}
		private float tan(float x)
		{
			return (float)Math.Tan(x);
		}
		private float tanh(float x)
		{
			return (float)Math.Tanh(x);
		}
		private float rand(float start, float end)
		{
			return (float)Random.NextDouble() * (end - start) + start;
		}
		private float pi()
		{
			return (float)Math.PI;
		}

		private object switch_(bool expression, object ifTrue, object ifFalse)
		{
			return expression ? ifTrue : ifFalse;
		}

		private Camera createcamera()
		{
			try
			{
				return new Camera();
			}
			catch (Exception ex)
			{
				RuntimeError("createcamera", ex);
				return null;
			}
		}
		private Light createlight()
		{
			try
			{
				return new Light();
			}
			catch (Exception ex)
			{
				RuntimeError("createlight", ex);
				return null;
			}
		}
		private Plane createplane()
		{
			try
			{
				return new Plane();
			}
			catch (Exception ex)
			{
				RuntimeError("createplane", ex);
				return null;
			}
		}
		private Cube createcube()
		{
			try
			{
				return new Cube();
			}
			catch (Exception ex)
			{
				RuntimeError("createcube", ex);
				return null;
			}
		}
		private Sphere createsphere()
		{
			try
			{
				return new Sphere();
			}
			catch (Exception ex)
			{
				RuntimeError("createsphere", ex);
				return null;
			}
		}
		private Cylinder createcylinder()
		{
			try
			{
				return new Cylinder();
			}
			catch (Exception ex)
			{
				RuntimeError("createcylinder", ex);
				return null;
			}
		}
		private Tube createtube()
		{
			try
			{
				return new Tube();
			}
			catch (Exception ex)
			{
				RuntimeError("createtube", ex);
				return null;
			}
		}
		private Texture loadtexture(string path)
		{
			try
			{
				return new Texture(Path.Combine(Path.GetDirectoryName(FilePath ?? AppDomain.CurrentDomain.BaseDirectory), path));
			}
			catch (Exception ex)
			{
				RuntimeError("loadtexture", ex);
				return null;
			}
		}

		private void setzoom(Camera camera, float zoom)
		{
			try
			{
				camera.Zoom = zoom;
			}
			catch (Exception ex)
			{
				RuntimeError("setzoom", ex);
			}
		}
		private void setangle(Camera camera, float pitch, float yaw)
		{
			try
			{
				camera.Angle = new Vector2f(pitch, yaw);
			}
			catch (Exception ex)
			{
				RuntimeError("setangle", ex);
			}
		}
		private void setclscolor(Camera camera, int r, int g, int b)
		{
			try
			{
				camera.ClsColor = new LiterayColor(r, g, b);
			}
			catch (Exception ex)
			{
				RuntimeError("setclscolor", ex);
			}
		}
		private void setambientlight(Camera camera, int r, int g, int b)
		{
			try
			{
				camera.AmbientColor = new LiterayColor(r, g, b);
			}
			catch (Exception ex)
			{
				RuntimeError("setambientlight", ex);
			}
		}
		private void setfogenabled(Camera camera, bool enabled)
		{
			try
			{
				camera.FogEnabled = enabled;
			}
			catch (Exception ex)
			{
				RuntimeError("setfogenabled", ex);
			}
		}
		private void setfogrange(Camera camera, float range)
		{
			try
			{
				camera.FogRange = range;
			}
			catch (Exception ex)
			{
				RuntimeError("setfogrange", ex);
			}
		}
		private void setbrightness(Light light, float brightness)
		{
			try
			{
				light.Brightness = brightness;
			}
			catch (Exception ex)
			{
				RuntimeError("setbrightness", ex);
			}
		}
		private void setplanevector(Plane plane, float nx, float ny, float nz, float d)
		{
			try
			{
				plane.Normal = new Vector3f(nx, ny, nz);
				plane.D = d;
			}
			catch (Exception ex)
			{
				RuntimeError("setplanevector", ex);
			}
		}
		private void setposition(object entity, float x, float y, float z)
		{
			try
			{
				if (entity is Camera) (entity as Camera).Position = new Vector3f(x, y, z);
				else if (entity is Light) (entity as Light).Position = new Vector3f(x, y, z);
				else if (entity is Cube) (entity as Cube).Position = new Vector3f(x, y, z);
				else if (entity is Sphere) (entity as Sphere).Position = new Vector3f(x, y, z);
				else if (entity is Cylinder) (entity as Cylinder).Position = new Vector3f(x, y, z);
				else if (entity is Tube) (entity as Tube).Position = new Vector3f(x, y, z);
				else throw new NullReferenceException();
			}
			catch (Exception ex)
			{
				RuntimeError("setposition", ex);
			}
		}
		private void setsize(object entity, float x, float y, float z)
		{
			try
			{
				if (entity is Light) (entity as Light).Size = new Vector3f(x, y, z);
				else if (entity is Cube) (entity as Cube).Size = new Vector3f(x, y, z);
				else throw new NullReferenceException();
			}
			catch (Exception ex)
			{
				RuntimeError("setsize", ex);
			}
		}
		private void setradius(Entity entity, float radius)
		{
			try
			{
				if (entity is Sphere) (entity as Sphere).Radius = radius;
				else if (entity is Cylinder) (entity as Cylinder).Radius = radius;
				else if (entity is Tube) (entity as Tube).Radius = radius;
				else throw new NullReferenceException();
			}
			catch (Exception ex)
			{
				RuntimeError("setradius", ex);
			}
		}
		private void setinnerradius(Tube tube, float innerRadius)
		{
			try
			{
				tube.InnerRadius = innerRadius;
			}
			catch (Exception ex)
			{
				RuntimeError("setinnerradius", ex);
			}
		}
		private void setheight(Entity entity, float height)
		{
			try
			{
				if (entity is Cylinder) (entity as Cylinder).Height = height;
				else if (entity is Tube) (entity as Tube).Height = height;
				else throw new NullReferenceException();
			}
			catch (Exception ex)
			{
				RuntimeError("setheight", ex);
			}
		}
		private void setcolor(object entity, int r, int g, int b)
		{
			try
			{
				if (entity is Light) (entity as Light).Color = new LiterayColor(r, g, b);
				else if (entity is Entity) (entity as Entity).Color = new LiterayColor(r, g, b);
				else throw new NullReferenceException();
			}
			catch (Exception ex)
			{
				RuntimeError("setcolor", ex);
			}
		}
		private void setreflection(Entity entity, float reflection)
		{
			try
			{
				entity.Reflection = reflection;
			}
			catch (Exception ex)
			{
				RuntimeError("setreflection", ex);
			}
		}
		private void setspecular(Entity entity, float specularPower, float specularIntensity)
		{
			try
			{
				entity.SpecularPower = specularPower;
				entity.SpecularIntensity = specularIntensity;
			}
			catch (Exception ex)
			{
				RuntimeError("setspecular", ex);
			}
		}
		private void settexture(Entity entity, Texture texture, float sizeX = 1, float sizeY = 1)
		{
			try
			{
				entity.Texture = texture;
				entity.TextureSize = new Vector2f(1 / sizeX, 1 / sizeY);
			}
			catch (Exception ex)
			{
				RuntimeError("settexture", ex);
			}
		}
		private void settexture2(Entity entity, Texture texture, float sizeX = 1, float sizeY = 1)
		{
			try
			{
				if (entity is Cylinder)
				{
					Cylinder cylinder = entity as Cylinder;
					cylinder.Texture2 = texture;
					cylinder.Texture2Size = new Vector2f(1 / sizeX, 1 / sizeY);
				}
				else if (entity is Tube)
				{
					Tube tube = entity as Tube;
					tube.Texture2 = texture;
					tube.Texture2Size = new Vector2f(1 / sizeX, 1 / sizeY);
				}
				else
				{
					throw new NullReferenceException();
				}
			}
			catch (Exception ex)
			{
				RuntimeError("settexture2", ex);
			}
		}
		private void setbumpmap(Entity entity, Texture texture, float sizeX = 1, float sizeY = 1, float strength = 1)
		{
			try
			{
				entity.Bumpmap = texture;
				entity.BumpmapSize = new Vector2f(1 / sizeX, 1 / sizeY);
				entity.BumpmapStrength = strength;
			}
			catch (Exception ex)
			{
				RuntimeError("setbumpmap", ex);
			}
		}
		private void setbumpmap2(Entity entity, Texture texture, float sizeX = 1, float sizeY = 1, float strength = 1)
		{
			try
			{
				if (entity is Cylinder)
				{
					Cylinder cylinder = entity as Cylinder;
					cylinder.Bumpmap2 = texture;
					cylinder.Bumpmap2Size = new Vector2f(1 / sizeX, 1 / sizeY);
					cylinder.Bumpmap2Strength = strength;
				}
				else if (entity is Tube)
				{
					Tube tube = entity as Tube;
					tube.Bumpmap2 = texture;
					tube.Bumpmap2Size = new Vector2f(1 / sizeX, 1 / sizeY);
					tube.Bumpmap2Strength = strength;
				}
				else
				{
					throw new NullReferenceException();
				}
			}
			catch (Exception ex)
			{
				RuntimeError("setbumpmap2", ex);
			}
		}

		private void render(Camera camera)
		{
			try
			{
				if (!IsStopped)
				{
					LiterayCore.Initialize();
					LiterayCore.SetGlobal(
						RenderProperties.ScreenWidth,
						RenderProperties.ScreenHeight,
						RenderProperties.MaximumRecursion,
						RenderProperties.ActualShadowSamples,
						RenderProperties.ActualAntiAliasMask.SubPixels.Length,
						RenderProperties.ActualAntiAliasMask.SubPixels,
						camera.Position,
						camera.Angle,
						camera.Zoom,
						camera.ClsColor,
						camera.AmbientColor,
						camera.FogEnabled,
						camera.FogRange);

					foreach (RaytracingObject raytracingObject in RaytracingObject.AllRaytracingObjects)
					{
						if (raytracingObject is Texture)
						{
							Texture texture = raytracingObject as Texture;
							LiterayCore.LoadTexture(texture.Handle, texture.Width, texture.Height, texture.Buffer);
						}
						if (raytracingObject is Light)
						{
							Light light = raytracingObject as Light;
							LiterayCore.CreateLight(light.Position, light.Size, light.Brightness, light.Color);
						}
						else if (raytracingObject is Plane)
						{
							Plane plane = raytracingObject as Plane;
							LiterayCore.CreatePlane(
								plane.Normal,
								plane.D,
								plane.Color,
								plane.Reflection,
								plane.SpecularPower,
								plane.SpecularIntensity,
								plane.Texture == null ? 0 : plane.Texture.Handle,
								plane.Texture2 == null ? 0 : plane.Texture2.Handle,
								plane.Bumpmap == null ? 0 : plane.Bumpmap.Handle,
								plane.Bumpmap2 == null ? 0 : plane.Bumpmap2.Handle,
								plane.TextureSize,
								plane.Texture2Size,
								plane.BumpmapSize,
								plane.Bumpmap2Size,
								plane.BumpmapStrength,
								plane.Bumpmap2Strength);
						}
						else if (raytracingObject is Cube)
						{
							Cube cube = raytracingObject as Cube;
							LiterayCore.CreateCube(
								cube.Position,
								cube.Size,
								cube.Color,
								cube.Reflection,
								cube.SpecularPower,
								cube.SpecularIntensity,
								cube.Texture == null ? 0 : cube.Texture.Handle,
								cube.Texture2 == null ? 0 : cube.Texture2.Handle,
								cube.Bumpmap == null ? 0 : cube.Bumpmap.Handle,
								cube.Bumpmap2 == null ? 0 : cube.Bumpmap2.Handle,
								cube.TextureSize,
								cube.Texture2Size,
								cube.BumpmapSize,
								cube.Bumpmap2Size,
								cube.BumpmapStrength,
								cube.Bumpmap2Strength);
						}
						else if (raytracingObject is Sphere)
						{
							Sphere sphere = raytracingObject as Sphere;
							LiterayCore.CreateSphere(
								sphere.Position,
								sphere.Radius,
								sphere.Color,
								sphere.Reflection,
								sphere.SpecularPower,
								sphere.SpecularIntensity,
								sphere.Texture == null ? 0 : sphere.Texture.Handle,
								sphere.Texture2 == null ? 0 : sphere.Texture2.Handle,
								sphere.Bumpmap == null ? 0 : sphere.Bumpmap.Handle,
								sphere.Bumpmap2 == null ? 0 : sphere.Bumpmap2.Handle,
								sphere.TextureSize,
								sphere.Texture2Size,
								sphere.BumpmapSize,
								sphere.Bumpmap2Size,
								sphere.BumpmapStrength,
								sphere.Bumpmap2Strength);
						}
						else if (raytracingObject is Cylinder)
						{
							Cylinder cylinder = raytracingObject as Cylinder;
							LiterayCore.CreateCylinder(
								cylinder.Position,
								cylinder.Radius,
								cylinder.Height,
								cylinder.Color,
								cylinder.Reflection,
								cylinder.SpecularPower,
								cylinder.SpecularIntensity,
								cylinder.Texture == null ? 0 : cylinder.Texture.Handle,
								cylinder.Texture2 == null ? 0 : cylinder.Texture2.Handle,
								cylinder.Bumpmap == null ? 0 : cylinder.Bumpmap.Handle,
								cylinder.Bumpmap2 == null ? 0 : cylinder.Bumpmap2.Handle,
								cylinder.TextureSize,
								cylinder.Texture2Size,
								cylinder.BumpmapSize,
								cylinder.Bumpmap2Size,
								cylinder.BumpmapStrength,
								cylinder.Bumpmap2Strength);
						}
						else if (raytracingObject is Tube)
						{
							Tube tube = raytracingObject as Tube;
							LiterayCore.CreateTube(
								tube.Position,
								tube.Radius,
								tube.InnerRadius,
								tube.Height,
								tube.Color,
								tube.Reflection,
								tube.SpecularPower,
								tube.SpecularIntensity,
								tube.Texture == null ? 0 : tube.Texture.Handle,
								tube.Texture2 == null ? 0 : tube.Texture2.Handle,
								tube.Bumpmap == null ? 0 : tube.Bumpmap.Handle,
								tube.Bumpmap2 == null ? 0 : tube.Bumpmap2.Handle,
								tube.TextureSize,
								tube.Texture2Size,
								tube.BumpmapSize,
								tube.Bumpmap2Size,
								tube.BumpmapStrength,
								tube.Bumpmap2Strength);
						}
					}

					Window = new WindowRenderer(RenderProperties.ScreenWidth, RenderProperties.ScreenHeight, FileName);
					Window.Closed += Window_Closed;
					Window.Title = FileName;
					Window.Show();

					DateTime startTime = DateTime.Now;

					CurrentLine = 0;
					Pixels = new int[RenderProperties.ScreenWidth * RenderProperties.ScreenHeight];
					for (int y = 0, pixelOffset = 0; y < RenderProperties.ScreenHeight; y++)
					{
						for (int x = 0; x < RenderProperties.ScreenWidth; x++)
						{
							Pixels[pixelOffset++] = ((y / 12) & 1) == 1 ^ ((x / 12) & 1) == 1 ? 0xffffff : 0xafafaf;
						}
					}
					Window.Bitmap.WritePixels(new Int32Rect(0, 0, RenderProperties.ScreenWidth, RenderProperties.ScreenHeight), Pixels, RenderProperties.ScreenWidth * 4, 0);

					Threads = new Thread[RenderProperties.ThreadCount];
					for (int i = 0; i < RenderProperties.ThreadCount; i++)
					{
						int j = i;
						Threads[i] = new Thread(new ThreadStart(delegate { RenderThread(j); }));
						Threads[i].Priority = ThreadPriority.Lowest;
						Threads[i].Start();
					}

					bool finished = false;
					while (!finished)
					{
						finished = true;
						foreach (Thread thread in Threads)
						{
							if (thread.IsAlive)
							{
								finished = false;
								break;
							}
						}
						Window.Bitmap.WritePixels(new Int32Rect(0, 0, RenderProperties.ScreenWidth, RenderProperties.ScreenHeight), Pixels, RenderProperties.ScreenWidth * 4, 0);
						Window.SetProgress((int)CurrentLine * 1f / RenderProperties.ScreenHeight);
						Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
						Thread.Sleep(100);
					}

					TimeSpan timeUsed = DateTime.Now - startTime;
					LiterayCore.Destroy();
					Window.Title += " - " + Math.Round(timeUsed.TotalSeconds, 3) + " s";
					Window.SetProgress(1);
					HasRenderedAtLeastOnce = true;
					if (RenderProperties.AutoSave)
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							BitmapEncoder bitmapEncoder = new BmpBitmapEncoder();
							bitmapEncoder.Frames.Add(BitmapFrame.Create(Window.Bitmap));
							bitmapEncoder.Save(memoryStream);
							using (Bitmap bitmap = new Bitmap(memoryStream))
							{
								string path = RenderProperties.AutoSavePath;
								Directory.CreateDirectory(path);
								bitmap.Save(Path.Combine(path, DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss.fff", CultureInfo.InvariantCulture) + " - " + Path.GetFileNameWithoutExtension(FileName) + ".png"), ImageFormat.Png);
							}
						}
					}

					if (RenderProperties.AutoClose)
					{
						Window.AutoClose();
					}
				}
			}
			catch (Exception ex)
			{
				RuntimeError("render", ex);
			}
		}
		private void debug(string message)
		{
			MessageBox.Show(message, "Debug", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void RenderThread(int threadNumber)
		{
			while (!IsStopped)
			{
				int line;
				lock (CurrentLine)
				{
					line = (int)CurrentLine;
					CurrentLine = line + 1;
				}
				if (line >= RenderProperties.ScreenHeight) break;

				for (int x = 0, pixelOffset = line * RenderProperties.ScreenWidth; x < RenderProperties.ScreenWidth && !IsStopped; x++)
				{
					Pixels[pixelOffset++] = LiterayCore.RenderPixel(x, line);
				}
			}
		}
		private void Window_Closed(object sender, System.EventArgs e)
		{
			if (!Window.WasAutoClosed) Stop();
		}

		private static void RuntimeError(string function, Exception exception)
		{
			MessageBox.Show("Runtime error in " + function + "()\r\n\r\n" + exception.Message + "\r\n\r\n" + exception.StackTrace, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
}