using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Literay
{
	public partial class App : Application
	{
		public static RoutedUICommand FileSaveAll = new RoutedUICommand("", "FileSaveAll", typeof(MainWindow));
		public static RoutedUICommand ScriptRun = new RoutedUICommand("", "ScriptRun", typeof(MainWindow));
		public static RoutedUICommand ScriptStop = new RoutedUICommand("", "ScriptStop", typeof(MainWindow));
	}
}