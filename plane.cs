using System;

namespace Vector
{
    public class Plane
    {
        public Vector Point { get; set; } 
        public Vector Normal { get; set; } 
        public Plane(Vector point, Vector normal)
        {
            Point = point ?? new Vector(0, 0, 0);
            Normal = normal ?? new Vector(0, 1, 0);  
        }

        public bool Intersects(Ray ray, out float t)
        {
            t = 0; 
            float denominator = Vector.DotProduct(ray.Direction, Normal);

            if (Math.Abs(denominator) < float.Epsilon)
            {
                return false; 
            }

            Vector rayToPlane = Vector.Subtract(Point, ray.Origin);

            t = Vector.DotProduct(rayToPlane, Normal) / denominator;

            return t >= 0;
        }

        public override string ToString()
        {
            return $"Plane[Point: {Point}, Normal: {Normal}]";
        }
    }
}
