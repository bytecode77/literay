struct Color
{
public:
	int R, G, B;

	Color();
	Color(int r, int g, int b);

	int getRgb();
	Color Clip();

	Color operator +(Color b);
	Color operator *(Color value);
	Color operator *(float value);
	Color operator /(float value);
	void operator +=(Color b);
	void operator *=(Color value);
	void operator *=(float value);
	void operator /=(float value);
};