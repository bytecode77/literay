using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Literay
{
	public unsafe class Texture : RaytracingObject
	{
		public int Width, Height;
		public int WidthExponent;
		public int[] Buffer;

		public Texture(string path)
		{
			Bitmap image = (Bitmap)Bitmap.FromFile(path);

			if ((image.Width & (image.Width - 1)) != 0 || (image.Height & (image.Height - 1)) != 0)
			{
				BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
				Bitmap newImage = new Bitmap(NextPowerOfTwo(image.Width), NextPowerOfTwo(image.Height));
				BitmapData newBitmapData = newImage.LockBits(new Rectangle(0, 0, newImage.Width, newImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
				int* ptr = (int*)bitmapData.Scan0;
				int* ptr2 = (int*)newBitmapData.Scan0;
				for (int y = 0; y < newImage.Height; y++)
				{
					for (int x = 0; x < newImage.Width; x++)
					{
						float u = x * image.Width * 1f / newImage.Width;
						float v = y * image.Height * 1f / newImage.Height;

						int x1 = (int)u;
						int y1 = (int)v;
						int x2 = (x1 + 1) % image.Width;
						int y2 = (y1 + 1) % image.Height;
						float u1 = u - x1;
						float v1 = v - y1;
						float u2 = 1 - u1;
						float v2 = 1 - v1;

						int r00 = (ptr[x1 + y1 * image.Width] & 0xff0000) >> 16;
						int g00 = (ptr[x1 + y1 * image.Width] & 0xff00) >> 8;
						int b00 = ptr[x1 + y1 * image.Width] & 0xff;
						int r01 = (ptr[x1 + y2 * image.Width] & 0xff0000) >> 16;
						int g01 = (ptr[x1 + y2 * image.Width] & 0xff00) >> 8;
						int b01 = ptr[x1 + y2 * image.Width] & 0xff;
						int r10 = (ptr[x2 + y1 * image.Width] & 0xff0000) >> 16;
						int g10 = (ptr[x2 + y1 * image.Width] & 0xff00) >> 8;
						int b10 = ptr[x2 + y1 * image.Width] & 0xff;
						int r11 = (ptr[x2 + y2 * image.Width] & 0xff0000) >> 16;
						int g11 = (ptr[x2 + y2 * image.Width] & 0xff00) >> 8;
						int b11 = ptr[x2 + y2 * image.Width] & 0xff;

						int r = (int)((r00 * u2 + r10 * u1) * v2 + (r01 * u2 + r11 * u1) * v1);
						int g = (int)((g00 * u2 + g10 * u1) * v2 + (g01 * u2 + g11 * u1) * v1);
						int b = (int)((b00 * u2 + b10 * u1) * v2 + (b01 * u2 + b11 * u1) * v1);
						*ptr2++ = r << 16 | g << 8 | b;
					}
				}
				newImage.UnlockBits(newBitmapData);
				image.UnlockBits(bitmapData);
				image.Dispose();
				image = newImage;
			}

			{
				BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
				int* ptr = (int*)bitmapData.Scan0;

				Width = image.Width;
				Height = image.Height;
				WidthExponent = (int)Math.Log(Width, 2);

				Buffer = new int[Width * Height];
				for (int y = 0, bufferOffset = 0; y < Height; y++)
				{
					for (int x = 0; x < Width; x++)
					{
						Buffer[bufferOffset++] = *ptr++;
					}
				}

				image.UnlockBits(bitmapData);
				image.Dispose();
			}
		}

		private int NextPowerOfTwo(int number)
		{
			if ((number & number - 1) != 0)
			{
				for (int i = 31; i >= 0; i--)
				{
					if ((number & 1 << i) != 0) return 1 << (i + 1);
				}
			}
			return number;
		}
	}
}
