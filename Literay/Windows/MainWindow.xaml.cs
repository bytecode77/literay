using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Literay
{
	public partial class MainWindow : Window
	{
		private bool FreshWorkspace
		{
			get
			{
				if (tabMain.Items.Count == 1)
				{
					LuaTabPage tabPage = (tabMain.SelectedItem as TabItem).Tag as LuaTabPage;
					return tabPage.FilePath == null && tabPage.Text == "";
				}
				else
				{
					return false;
				}
			}
		}
		private bool AnyFilesUnsaved
		{
			get
			{
				return tabMain.Items.Cast<TabItem>().Any(tabItem => !(tabItem.Tag as LuaTabPage).IsSaved);
			}
		}
		private LuaScript Script;

		public MainWindow()
		{
			InitializeComponent();
			propMain.SelectedObject = RenderProperties.Default;
			mnuFileNew_Click(null, RoutedEventArgs.Empty as RoutedEventArgs);

			DispatcherTimer enableTimer = new DispatcherTimer();
			enableTimer.Tick += new EventHandler(EnableTimer_Tick);
			enableTimer.Interval = TimeSpan.FromMilliseconds(100);
			enableTimer.Start();

			Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Idle;
		}
		private void Window_Closing(object sender, CancelEventArgs e)
		{
			if (mnuScriptRun.IsEnabled)
			{
				if (!FreshWorkspace && AnyFilesUnsaved)
				{
					switch (MessageBox.Show("Do you want to save changes to unsaved files?", "Confirmation", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
					{
						case MessageBoxResult.Yes:
							if (!FileSaveAll()) e.Cancel = true;
							break;
						case MessageBoxResult.Cancel:
							e.Cancel = true;
							break;
					}
				}
			}
			else
			{
				Environment.Exit(0);
			}
		}

		private void mnuFileNew_Click(object sender, RoutedEventArgs e)
		{
			FileNew();
		}
		private void mnuFileOpen_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = "lua";
			dialog.Filter = "Lua script files (*.lua)|*.lua";
			dialog.Multiselect = true;
			if (dialog.ShowDialog().Value)
			{
				if (FreshWorkspace) tabMain.Items.Clear();
				foreach (string fileName in dialog.FileNames)
				{
					LuaTabPage tabPage = new LuaTabPage();
					tabPage.FilePath = fileName;
					tabPage.Text = File.ReadAllText(fileName);
					tabPage.IsSaved = true;
					tabMain.Items.Add(tabPage.TabItem);
					tabPage.TabItem.IsSelected = true;
					tabPage.TabItem.MouseUp += tabMain_MouseUp;
				}
			}
		}
		private void mnuFileClose_Click(object sender, RoutedEventArgs e)
		{
			FileClose((tabMain.SelectedItem as TabItem).Tag as LuaTabPage);
		}
		private void mnuFileSave_Click(object sender, RoutedEventArgs e)
		{
			FileSave((tabMain.SelectedItem as TabItem).Tag as LuaTabPage);
		}
		private void mnuFileSaveAs_Click(object sender, RoutedEventArgs e)
		{
			FileSaveAs((tabMain.SelectedItem as TabItem).Tag as LuaTabPage);
		}
		private void mnuFileSaveAll_Click(object sender, RoutedEventArgs e)
		{
			FileSaveAll();
		}
		private void mnuFileExit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
		private void mnuEditUndo_Click(object sender, RoutedEventArgs e)
		{
			LuaTabPage tabPage = (tabMain.SelectedItem as TabItem).Tag as LuaTabPage;
			tabPage.TextBox.Undo();
		}
		private void mnuEditRedo_Click(object sender, RoutedEventArgs e)
		{
			LuaTabPage tabPage = (tabMain.SelectedItem as TabItem).Tag as LuaTabPage;
			tabPage.TextBox.Redo();
		}
		private void mnuEditCut_Click(object sender, RoutedEventArgs e)
		{
			LuaTabPage tabPage = (tabMain.SelectedItem as TabItem).Tag as LuaTabPage;
			tabPage.TextBox.Cut();
		}
		private void mnuEditCopy_Click(object sender, RoutedEventArgs e)
		{
			LuaTabPage tabPage = (tabMain.SelectedItem as TabItem).Tag as LuaTabPage;
			tabPage.TextBox.Copy();
		}
		private void mnuEditPaste_Click(object sender, RoutedEventArgs e)
		{
			LuaTabPage tabPage = (tabMain.SelectedItem as TabItem).Tag as LuaTabPage;
			tabPage.TextBox.Paste();
		}
		private void mnuEditSelectAll_Click(object sender, RoutedEventArgs e)
		{
			LuaTabPage tabPage = (tabMain.SelectedItem as TabItem).Tag as LuaTabPage;
			tabPage.TextBox.SelectAll();
		}
		private void mnuViewCloseAllRenderWindows_Click(object sender, RoutedEventArgs e)
		{
			WindowRenderer.AllWindows.ToList().ForEach(renderWindow => renderWindow.Close());
		}
		private void mnuScriptRun_Click(object sender, RoutedEventArgs e)
		{
			mnuScriptRun.IsEnabled = btnScriptRun.IsEnabled = false;
			mnuScriptStop.IsEnabled = btnScriptStop.IsEnabled = true;

			LuaTabPage tabPage = (tabMain.SelectedItem as TabItem).Tag as LuaTabPage;
			if (tabPage.FilePath != null) FileSave(tabPage);
			Script = new LuaScript(propMain.SelectedObject as RenderProperties, tabPage.FileName, tabPage.FilePath);
			Script.Run(tabPage.Text);

			mnuScriptRun.IsEnabled = btnScriptRun.IsEnabled = true;
			mnuScriptStop.IsEnabled = btnScriptStop.IsEnabled = false;
		}
		private void mnuScriptStop_Click(object sender, RoutedEventArgs e)
		{
			Script.Stop();
			mnuScriptRun.IsEnabled = btnScriptRun.IsEnabled = true;
			mnuScriptStop.IsEnabled = btnScriptStop.IsEnabled = false;
		}
		private void mnuRenderPropertiesLoadDefault_Click(object sender, RoutedEventArgs e)
		{
			propMain.SelectedObject = RenderProperties.Default;
		}
		private void mnuHelpAbout_Click(object sender, RoutedEventArgs e)
		{
			new WindowAbout().ShowDialog();
		}

		private void tabMain_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Middle)
			{
				FileClose((sender as TabItem).Tag as LuaTabPage);
			}
		}
		private void EnableTimer_Tick(object sender, EventArgs e)
		{
			LuaTabPage tabPage = (tabMain.SelectedItem as TabItem).Tag as LuaTabPage;
			mnuEditUndo.IsEnabled = cmnuEditUndo.IsEnabled = btnEditUndo.IsEnabled = tabPage.TextBox.CanUndo;
			mnuEditRedo.IsEnabled = cmnuEditRedo.IsEnabled = btnEditRedo.IsEnabled = tabPage.TextBox.CanRedo;
			mnuEditCut.IsEnabled = cmnuEditCut.IsEnabled = btnEditCut.IsEnabled = tabPage.TextBox.SelectionLength > 0;
			mnuEditCopy.IsEnabled = cmnuEditCopy.IsEnabled = btnEditCopy.IsEnabled = tabPage.TextBox.SelectionLength > 0;
			mnuViewCloseAllRenderWindows.IsEnabled = WindowRenderer.AllWindows.Count > 0;
		}

		private void FileNew()
		{
			int index = 1;
			while (tabMain.Items.Cast<TabItem>().Any(tabItem => (tabItem.Tag as LuaTabPage).FileName == LuaTabPage.GenerateNewFilename(index)))
			{
				index++;
			}

			LuaTabPage tabPage = new LuaTabPage();
			tabPage.FileName = LuaTabPage.GenerateNewFilename(index);
			tabMain.Items.Add(tabPage.TabItem);
			tabPage.TabItem.IsSelected = true;
			tabPage.TabItem.MouseUp += tabMain_MouseUp;
		}
		private void FileClose(LuaTabPage tabPage)
		{
			bool close;
			if (tabPage.IsSaved || tabPage.FilePath == null && tabPage.Text == "")
			{
				close = true;
			}
			else
			{
				switch (MessageBox.Show("Do you want to save changes to " + tabPage.FileName + "?", "Confirmation", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
				{
					case MessageBoxResult.Yes:
						close = FileSave(tabPage);
						break;
					case MessageBoxResult.No:
						close = true;
						break;
					default:
						close = false;
						break;
				}
			}

			if (close)
			{
				tabMain.Items.Remove(tabPage.TabItem);
				if (tabMain.Items.Count == 0) FileNew();
			}
		}
		private bool FileSave(LuaTabPage tabPage)
		{
			if (tabPage.FilePath == null)
			{
				return FileSaveAs(tabPage);
			}
			else
			{
				tabPage.IsSaved = true;
				File.WriteAllText(tabPage.FilePath, tabPage.Text);
				return true;
			}
		}
		private bool FileSaveAs(LuaTabPage tabPage)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.DefaultExt = "lua";
			dialog.Filter = "Lua script files (*.lua)|*.lua";
			dialog.FileName = tabPage.FileName;
			if (dialog.ShowDialog().Value)
			{
				tabPage.FilePath = dialog.FileName;
				tabPage.IsSaved = true;
				File.WriteAllText(tabPage.FilePath, tabPage.Text);
				return true;
			}
			else
			{
				return false;
			}
		}
		private bool FileSaveAll()
		{
			foreach (TabItem tabItem in tabMain.Items)
			{
				if (!FileSave(tabItem.Tag as LuaTabPage)) return false;
			}
			return true;
		}
	}
}