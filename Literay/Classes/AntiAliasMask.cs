using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Literay
{
	public class AntiAliasMask : IItemsSource
	{
		private static AntiAliasMask[] _AllAntiAliasMasks;
		public static AntiAliasMask[] AllAntiAliasMasks
		{
			get
			{
				if (_AllAntiAliasMasks == null)
				{
					using (Stream stream = Application.GetResourceStream(new Uri("pack://application:,,,/Literay;component/Resources/AntiAlias.xml")).Stream)
					{
						_AllAntiAliasMasks = XDocument.Load(stream)
							.Root.Elements()
							.Select(element => new AntiAliasMask
							{
								Name = element.Attribute("Name").Value,
								SubPixels = element.Elements()
									.Select(element2 => new Vector2f
									{
										X = Convert.ToSingle(element2.Attribute("X").Value, CultureInfo.InvariantCulture),
										Y = Convert.ToSingle(element2.Attribute("Y").Value, CultureInfo.InvariantCulture)
									})
									.ToArray()
							})
							.ToArray();
					}
				}
				return _AllAntiAliasMasks;
			}
		}

		public ItemCollection GetValues()
		{
			ItemCollection itemCollection = new ItemCollection();
			foreach (AntiAliasMask antiAliasMask in AllAntiAliasMasks) itemCollection.Add(antiAliasMask.Name);
			return itemCollection;
		}

		public string Name;
		public Vector2f[] SubPixels;
	}
}