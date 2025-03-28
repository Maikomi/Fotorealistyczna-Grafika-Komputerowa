using System;
using System.Drawing;
using Vector;

public class OrthographicCamera
{
    public int Width { get; }
    public int Height { get; }
    public float PixelWidth { get; }
    public float PixelHeight { get; }

    public OrthographicCamera(int width, int height)
    {
        Width = width;
        Height = height;
        PixelWidth = 2.0f / width;  // Skalowanie do [-1,1]
        PixelHeight = 2.0f / height;
    }

    public Ray GenerateRay(int x, int y)
    {
        float centerX = -1.0f + (x + 0.5f) * PixelWidth;
        float centerY = 1.0f - (y + 0.5f) * PixelHeight;

        // Promień wychodzi w stronę -Z (patrzymy "na scenę")
        return new Ray(new Vector.Vector(centerX, centerY, 1), new Vector.Vector(0, 0, -1));
    }
}
