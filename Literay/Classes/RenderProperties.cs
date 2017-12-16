using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Literay
{
	[DisplayName("Render Properties")]
	public class RenderProperties
	{
		public static RenderProperties Default
		{
			get
			{
				RenderProperties renderProperties = new RenderProperties();
				renderProperties.ScreenWidth = 1280;
				renderProperties.ScreenHeight = 720;
				renderProperties.SoftShadowsEnabled = false;
				renderProperties.MaximumRecursion = 5;
				renderProperties.ShadowSamples = 10;
				renderProperties.AntiAlias = AntiAliasMask.AllAntiAliasMasks[0].Name;
				renderProperties.ThreadCount = Environment.ProcessorCount;
				renderProperties.AutoSave = true;
				renderProperties.AutoSavePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AutoSave");
				return renderProperties;
			}
		}

		[Browsable(false)]
		public int ActualShadowSamples
		{
			get
			{
				return SoftShadowsEnabled ? ShadowSamples : 0;
			}
		}
		[Browsable(false)]
		public AntiAliasMask ActualAntiAliasMask
		{
			get
			{
				return AntiAliasMask.AllAntiAliasMasks.First(antiAliasMask => antiAliasMask.Name == AntiAlias);
			}
		}

		[Category("Display")]
		[DisplayName("Screen Width")]
		[Description("Width of render output in pixels")]
		[PropertyOrder(0)]
		public int ScreenWidth { get; set; }
		[Category("Display")]
		[DisplayName("Screen Height")]
		[Description("Height of render output in pixels")]
		[PropertyOrder(1)]
		public int ScreenHeight { get; set; }

		[Category("Quality")]
		[DisplayName("Maximum Recursion")]
		[Description("Maximum recursion depth for reflections")]
		[PropertyOrder(0)]
		public int MaximumRecursion { get; set; }
		[Category("Quality")]
		[DisplayName("Soft Shadows")]
		[Description("Soft shadows enabled?")]
		[PropertyOrder(1)]
		public bool SoftShadowsEnabled { get; set; }
		[Category("Quality")]
		[DisplayName("Shadow Samples")]
		[Description("Amount of samples for soft shadows")]
		[PropertyOrder(2)]
		public int ShadowSamples { get; set; }
		[Category("Quality")]
		[DisplayName("Anti Aliasing")]
		[Description("Which anti-aliasing mask to use")]
		[ItemsSource(typeof(AntiAliasMask))]
		[PropertyOrder(3)]
		public string AntiAlias { get; set; }

		[Category("Rendering")]
		[DisplayName("Threads")]
		[Description("Number of threads to use")]
		[PropertyOrder(0)]
		public int ThreadCount { get; set; }
		[Category("Rendering")]
		[DisplayName("Auto Save")]
		[Description("Save after rendering is done")]
		[PropertyOrder(1)]
		public bool AutoSave { get; set; }
		[Category("Rendering")]
		[DisplayName("Auto Save Path")]
		[Description("Path for AutoSave (if enabled)")]
		[PropertyOrder(2)]
		public string AutoSavePath { get; set; }
		[Category("Rendering")]
		[DisplayName("Auto Close")]
		[Description("Close after image is rendered")]
		[PropertyOrder(3)]
		public bool AutoClose { get; set; }
	}
}