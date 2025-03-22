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


        public bool Intersects(Ray ray, out float t0, out float t1)
        {
            t0 = t1 = 0; 

           
            Vector oc = Vector.Subtract(ray.Origin, Center);

        
            float a = Vector.DotProduct(ray.Direction, ray.Direction);
            float b = 2.0f * Vector.DotProduct(oc, ray.Direction);
            float c = Vector.DotProduct(oc, oc) - (Radius * Radius);

            float discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                return false;
            }
            else
            {
                float sqrtDiscriminant = (float)Math.Sqrt(discriminant);
                t0 = (-b - sqrtDiscriminant) / (2.0f * a);
                t1 = (-b + sqrtDiscriminant) / (2.0f * a);

                if (t0 > t1)
                {
                    float temp = t0;
                    t0 = t1;
                    t1 = temp;
                }

                return true;
            }
        }

        public override string ToString()
        {
            return $"Sphere[Center: {Center}, Radius: {Radius}]";
        }
    }
}
