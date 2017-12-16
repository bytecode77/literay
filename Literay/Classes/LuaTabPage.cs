using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace Literay
{
	public class LuaTabPage
	{
		private string _fileName;
		private string _filePath;
		private bool _isSaved;

		public TabItem TabItem { get; private set; }
		public TextEditor TextBox { get; private set; }
		public string FileName
		{
			get
			{
				return _fileName;
			}
			set
			{
				_fileName = value;
				UpdateTabPageHeader();
			}
		}
		public string FilePath
		{
			get
			{
				return _filePath;
			}
			set
			{
				_filePath = value;
				FileName = Path.GetFileName(_filePath);
			}
		}
		public bool IsSaved
		{
			get
			{
				return _isSaved;
			}
			set
			{
				bool changed = value != _isSaved;
				_isSaved = value;
				if (changed) UpdateTabPageHeader();
			}
		}
		public string Text
		{
			get
			{
				return TextBox.Text;
			}
			set
			{
				TextBox.Text = value;
			}
		}

		public LuaTabPage()
		{
			TextBox = new TextEditor();
			TextBox.Tag = this;
			TextBox.ShowLineNumbers = true;
			TextBox.FontFamily = new FontFamily("Consolas");

			using (Stream stream = Application.GetResourceStream(new Uri("pack://application:,,,/Literay;component/Resources/Lua.xshd")).Stream)
			{
				using (XmlTextReader reader = new XmlTextReader(stream))
				{
					TextBox.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
				}
			}

			TabItem = new TabItem();
			TabItem.Tag = this;
			TabItem.Content = TextBox;
			IsSaved = false;
			TextBox.TextChanged += TextBox_TextChanged;
		}

		public static string GenerateNewFilename(int index)
		{
			return "New " + index + ".lua";
		}

		private void UpdateTabPageHeader()
		{
			TabItem.Header = FileName + (IsSaved ? "" : " *");
		}
		private void TextBox_TextChanged(object sender, EventArgs e)
		{
			((sender as TextEditor).Tag as LuaTabPage).IsSaved = false;
		}
	}
}