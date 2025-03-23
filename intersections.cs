using System;
namespace Vector
{
    public static class Intersections
    {
        public static bool IntersectionSphere(Ray ray, Sphere sphere, out float intersection0, out float intersection1)
        {
            intersection0 = intersection1 = 0;
            Vector OriginCentre = Vector.Subtract(ray.Origin, sphere.Center);

            float a = Vector.DotProduct(ray.Direction, ray.Direction);
            float b = 2.0f * Vector.DotProduct(OriginCentre, ray.Direction);
            float c = Vector.DotProduct(OriginCentre, OriginCentre) - (sphere.Radius * sphere.Radius);

            float discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
                return false;

            float sqrtD = (float)Math.Sqrt(discriminant);
            intersection0 = (-b - sqrtD) / (2.0f * a);
            intersection1 = (-b + sqrtD) / (2.0f * a);

            if (intersection0 > intersection1)
                (intersection0, intersection1) = (intersection1, intersection0);

            return intersection0 >= 0 || intersection1 >= 0;
        }

        public static bool IntersectionPlane(Ray ray, Plane plane, out float intersection)
        {
            intersection = 0;
            float denominator = Vector.DotProduct(ray.Direction, plane.Normal);

            if (Math.Abs(denominator) < float.Epsilon)
            {
                 if (Math.Abs(Vector.DotProduct(Vector.Subtract(ray.Origin, plane.Point), plane.Normal)) < float.Epsilon)
                 {
                    intersection = 0;
                    return true;
                 }
            return false;
            }

            Vector rayToPlane = Vector.Subtract(plane.Point, ray.Origin);
            intersection = Vector.DotProduct(rayToPlane, plane.Normal) / denominator;

            return intersection >= 0;
        }

    }
}