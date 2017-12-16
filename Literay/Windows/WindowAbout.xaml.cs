using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace Literay
{
	public partial class WindowAbout : Window
	{
		public static string Version
		{
			get
			{
				return Assembly.GetCallingAssembly().GetName().Version.ToString(3);
			}
		}

		public WindowAbout()
		{
			InitializeComponent();
			lblVersion.Content += " " + Version;
		}

		private void lnkWebsite_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("http://bytecode77.com");
		}
		private void btnOK_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}