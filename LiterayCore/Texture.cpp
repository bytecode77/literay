#include "LiterayCore.h"

Texture::~Texture()
{
	delete[] Buffer;
}

Color Texture::GetTexel(Vector2f coordinates, Vector2f size)
{
	coordinates.X = coordinates.X * Width * size.X - .5f;
	coordinates.Y = (1 - coordinates.Y) * Height * size.Y - .5f;

	int x1 = int(floor(coordinates.X));
	int y1 = int(floor(coordinates.Y));
	float u1 = coordinates.X - x1;
	float v1 = coordinates.Y - y1;
	float u2 = 1 - u1;
	float v2 = 1 - v1;
	int x2 = (x1 + 1) & (Width - 1);
	int y2 = (y1 + 1) & (Height - 1);
	x1 &= Width - 1;
	y1 &= Height - 1;

	int r00 = (Buffer[x1 + (y1 << WidthExponent)] & 0xff0000) >> 16;
	int g00 = (Buffer[x1 + (y1 << WidthExponent)] & 0xff00) >> 8;
	int b00 = Buffer[x1 + (y1 << WidthExponent)] & 0xff;
	int r01 = (Buffer[x1 + (y2 << WidthExponent)] & 0xff0000) >> 16;
	int g01 = (Buffer[x1 + (y2 << WidthExponent)] & 0xff00) >> 8;
	int b01 = Buffer[x1 + (y2 << WidthExponent)] & 0xff;
	int r10 = (Buffer[x2 + (y1 << WidthExponent)] & 0xff0000) >> 16;
	int g10 = (Buffer[x2 + (y1 << WidthExponent)] & 0xff00) >> 8;
	int b10 = Buffer[x2 + (y1 << WidthExponent)] & 0xff;
	int r11 = (Buffer[x2 + (y2 << WidthExponent)] & 0xff0000) >> 16;
	int g11 = (Buffer[x2 + (y2 << WidthExponent)] & 0xff00) >> 8;
	int b11 = Buffer[x2 + (y2 << WidthExponent)] & 0xff;

	return Color(
		int((r00 * u2 + r10 * u1) * v2 + (r01 * u2 + r11 * u1) * v1),
		int((g00 * u2 + g10 * u1) * v2 + (g01 * u2 + g11 * u1) * v1),
		int((b00 * u2 + b10 * u1) * v2 + (b01 * u2 + b11 * u1) * v1));
}
Vector3f Texture::GetNormal(Vector2f coordinates, Vector2f size, float strength)
{
	Color color = GetTexel(coordinates, size);

	Vector3f bumpNormal = Vector3f((127 - color.R) / 255.f, (127 - color.G) / 255.f, (255 - color.B) / 255.f);
	float alpha = atan2(bumpNormal.X, bumpNormal.Y);
	float beta = atan2(bumpNormal.Z, bumpNormal.Y);

	return Vector3f(
		-(bumpNormal.X * cos(alpha) + (bumpNormal.Y * sin(beta) + bumpNormal.Z * cos(beta)) * sin(alpha)) * strength,
		(bumpNormal.Y * cos(beta) - bumpNormal.Z * sin(beta)) * strength,
		(-bumpNormal.Z * sin(alpha) + (bumpNormal.Y * sin(beta) + bumpNormal.Z * cos(beta)) * cos(alpha)) * strength);
}