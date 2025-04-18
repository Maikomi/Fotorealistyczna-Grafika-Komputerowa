using RayTracing;
using Lighting;

namespace Vector
{
    public class Sphere : IRenderableObject
    {
        public Vector Center { get; set; }
        public float Radius { get; set; }
        public Material Material {get; set;}

        public Sphere(Vector center, float radius, Material material)
        {
            Center = center ?? new Vector(0, 0, 0);
            Radius = radius;
            Material = material;
        }

         public Material GetMaterial()
        {
            return Material;
        }

        public Vector GetNormal(Vector point)
        {
            return (point - Center).Normalize();
        }

        public bool Intersect(Ray ray, out float t)
        {
            if (Intersections.IntersectionSphere(ray, this, out float intersection0, out float intersection1))
            {
                t = intersection0 >= 0 ? intersection0 : intersection1; 
                return t >= 0;
            }

            t = 0;
            return false;
        }

        public override string ToString()
        {
            return $"Sphere[Center: {Center}, Radius: {Radius}]";
        }
    }
}
