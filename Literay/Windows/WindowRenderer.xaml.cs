using Microsoft.Win32;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shell;

namespace Literay
{
	public partial class WindowRenderer : Window
	{
		public static List<WindowRenderer> AllWindows = new List<WindowRenderer>();
		public WriteableBitmap Bitmap { get; private set; }
		public bool WasAutoClosed { get; private set; }
		private string FileName;

		public WindowRenderer(int width, int height, string fileName)
		{
			FileName = fileName;

			InitializeComponent();
			AllWindows.Add(this);

			Bitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgr32, null);
			imgMain.Width = width;
			imgMain.Height = height;
			imgMain.Source = Bitmap;
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			int maxWidth = (int)SystemParameters.WorkArea.Width - 100;
			int maxHeight = (int)SystemParameters.WorkArea.Height - 100;
			MaxWidth = Width;
			MaxHeight = Height;

			if (ActualWidth > maxWidth)
			{
				MaxHeight = ActualHeight * maxWidth / ActualWidth;
				MaxWidth = maxWidth;
			}
			if (ActualHeight > maxHeight)
			{
				MaxWidth = ActualWidth * maxHeight / ActualHeight;
				MaxHeight = maxHeight;
			}

			Left = SystemParameters.WorkArea.Width / 2 - Width / 2;
			Top = SystemParameters.WorkArea.Height / 2 - Height / 2;
		}
		private void Window_Closed(object sender, System.EventArgs e)
		{
			AllWindows.Remove(this);
		}

		private void mnuFileSave_Click(object sender, RoutedEventArgs e)
		{
			if (mnuFileSave.IsEnabled)
			{
				SaveFileDialog dialog = new SaveFileDialog();
				dialog.Filter = "PNG (*.png)|*.png|JPG (*.jpg; *.jpeg)|*.jpg; *.jpeg";
				dialog.FileName = Path.GetFileNameWithoutExtension(FileName) + ".png";
				if (dialog.ShowDialog().Value)
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						BitmapEncoder bitmapEncoder = new BmpBitmapEncoder();
						bitmapEncoder.Frames.Add(BitmapFrame.Create(Bitmap));
						bitmapEncoder.Save(memoryStream);
						using (Bitmap bitmap = new Bitmap(memoryStream))
						{
							switch (Path.GetExtension(dialog.FileName))
							{
								case ".png":
									bitmap.Save(dialog.FileName, ImageFormat.Png);
									break;
								case ".jpg":
								case ".jpeg":
									ImageCodecInfo jpgEncoder = ImageCodecInfo.GetImageDecoders().First(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
									EncoderParameters encoderParameters = new EncoderParameters(1);
									encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 95L);
									bitmap.Save(dialog.FileName, jpgEncoder, encoderParameters);
									break;
							}
						}
					}
				}
			}
		}
		private void mnuFileClose_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		public void SetProgress(float percentage)
		{
			if (percentage == 1)
			{
				TaskbarItemInfo.ProgressState = TaskbarItemProgressState.None;
				mnuFileSave.IsEnabled = true;
			}
			else
			{
				TaskbarItemInfo.ProgressValue = percentage;
			}
		}
		public void AutoClose()
		{
			WasAutoClosed = true;
			Close();
		}
		public Bitmap GetImage()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BitmapEncoder bitmapEncoder = new BmpBitmapEncoder();
				bitmapEncoder.Frames.Add(BitmapFrame.Create(Bitmap));
				bitmapEncoder.Save(memoryStream);
				return new Bitmap(memoryStream);
			}
		}
	}
}