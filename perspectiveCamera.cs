using System;
using System.Drawing;
using Vector;

public class PerspectiveCamera : Camera
{
    public float FieldOfView { get; }
    private float aspectRatio;

    public PerspectiveCamera(int width, int height, float fieldOfView) : base(width, height)
    {
        FieldOfView = fieldOfView;
        aspectRatio = (float)width / height;
    }

    public override Ray GenerateRay(int x, int y)
    {
        float fovAdjustment = (float)Math.Tan(FieldOfView * 0.5 * Math.PI / 180.0);
        float centerX = (2 * ((x + 0.5f) / Width) - 1) * aspectRatio * fovAdjustment;
        float centerY = (1 - 2 * ((y + 0.5f) / Height)) * fovAdjustment;

        Vector.Vector origin = new Vector.Vector(0, 0, 0);
        Vector.Vector direction = new Vector.Vector(centerX, centerY, -1).Normalize();
        
        return new Ray(origin, direction);
    }
}