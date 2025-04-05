
using RayTracing;

namespace Vector
{
    public class Plane : IRenderableObject
    {
        public Vector Point { get; set; } 
        public Vector Normal { get; set; } 
        public Material Material {get; set;}

        public Plane(Vector point, Vector normal)
        {
            Point = point ?? new Vector(0, 0, 0);
            Normal = normal ?? new Vector(0, 1, 0);  
        }

        public Material GetMaterial()
        {
            return Material;
        }

         public Vector GetNormal(Vector point)
        {
            return Normal.Normalize();
        }

         public bool Intersect(Ray ray, out float t)
        {
            if (Intersections.IntersectionPlane(ray, this, out float intersection ))
            {
                t = intersection; 
                return t >= 0;
            }

            t = 0;
            return false;
        }

        public override string ToString()
        {
            return $"Plane[Point: {Point}, Normal: {Normal}]";
        }
    }
}
