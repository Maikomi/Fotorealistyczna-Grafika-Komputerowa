using System;

namespace Vector
{
    public class Sphere
    {
        public Vector Center { get; set; }
        public float Radius { get; set; }

        public Sphere(Vector center, float radius)
        {
            Center = center ?? new Vector(0, 0, 0);
            Radius = radius;
        }

        public override string ToString()
        {
            return $"Sphere[Center: {Center}, Radius: {Radius}]";
        }
    }
}
