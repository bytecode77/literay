#include "LiterayCore.h"

Color::Color()
{
	R = 0;
	G = 0;
	B = 0;
}
Color::Color(int r, int g, int b)
{
	R = r;
	G = g;
	B = b;
}

int Color::getRgb()
{
	return R << 16 | G << 8 | B;
}
Color Color::Clip()
{
	return Color(clip(R, 0, 255), clip(G, 0, 255), clip(B, 0, 255));
}

Color Color::operator +(Color b)
{
	return Color(R + b.R, G + b.G, B + b.B);
}
Color Color::operator *(Color b)
{
	return Color(int(R * b.R >> 8), int(G * b.G >> 8), int(B * b.B >> 8));
}
Color Color::operator *(float value)
{
	return Color(int(R * value), int(G * value), int(B * value));
}
Color Color::operator /(float value)
{
	return Color(int(R / value), int(G / value), int(B / value));
}
void Color::operator +=(Color b)
{
	R += b.R;
	G += b.G;
	B += b.B;
}
void Color::operator *=(Color value)
{
	R = R * value.R >> 8;
	G = G * value.G >> 8;
	B = B * value.B >> 8;
}
void Color::operator *=(float value)
{
	R = int(R * value);
	G = int(G * value);
	B = int(B * value);
}
void Color::operator /=(float value)
{
	R = int(R / value);
	G = int(G / value);
	B = int(B / value);
}