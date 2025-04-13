
using Lighting;
using RayTracing;

namespace Vector
{
    public class Plane : IRenderableObject
    {
        private Vector a;

        public Vector Point { get; set; } 
        public Vector Normal { get; set; } 
        public Material Material {get; set;}

        public Plane(Vector point, Vector normal, Material material)
        {
            Point = point ?? new Vector(0, 0, 0);
            Normal = normal ?? new Vector(0, 1, 0);  
            Material = material ?? new Material(new LightIntensity(1, 1, 1), 0.1f, 0.1f, 0.9f, 10);
        }

        public Plane(Vector a, Vector normal)
        {
            this.a = a;
            Normal = normal;
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
