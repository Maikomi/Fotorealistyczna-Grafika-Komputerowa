using System;
using System.Runtime.Serialization;
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
                    return true;
                 }
            return false;
            }

            Vector rayToPlane = Vector.Subtract(plane.Point, ray.Origin);
            intersection = Vector.DotProduct(rayToPlane, plane.Normal) / denominator;

            return intersection >= 0;
        }

        public static bool IntersectonTriangle(Vector A, Vector B, Vector C, Ray ray, out Vector intersection)
        {
            intersection = null;

            Vector edge1 = Vector.Subtract(B, A);
            Vector edge2 = Vector.Subtract(C, A);
            Vector normal = Vector.CrossProduct(edge1, edge2);

            Plane trianglePlane = new Plane(A, normal);

            if (!IntersectionPlane(ray, trianglePlane, out float intersectionPlane))
            {             
                return false;
            }
            
            intersection = ray.PointAt(intersectionPlane);

            Vector v0 = Vector.Subtract(C, A);
            Vector v1 = Vector.Subtract(B, A);
            Vector v2 = Vector.Subtract(B, C);

            float d00 = Vector.DotProduct(v0, v0);
            float d01 = Vector.DotProduct(v0, v1);
            float d11 = Vector.DotProduct(v1, v1);
            float d02 = Vector.DotProduct(v0, v2);
            float d12 = Vector.DotProduct(v1, v2);

            float denom = d00 * d11 - d01 * d01;

            float u = (d11 * d02 - d01 * d12) / denom;
            float v = (d00 * d12 - d01 * d02) / denom;
            float w = 1.0f - u - v;
            
            return u >= 0 && v >= 0 && w >= 0;
        }

    }
}