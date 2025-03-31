using System;
using System.Drawing;
using Vector;

public abstract class Camera
{
    public int Width;
    public int Height;

    protected Camera(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public abstract Ray GenerateRay(int x, int y);
}