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

            if (Math.Abs(denominator) < 1e-20)
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

            Vector edgeAB = Vector.Subtract(B, A);
            Vector edgeAC = Vector.Subtract(C, A);
            Vector normal = Vector.CrossProduct(edgeAB, edgeAC);

            Plane trianglePlane = new Plane(A, normal);

            float dotRayNorm = Vector.DotProduct(normal, ray.Direction);

            if (Math.Abs(dotRayNorm) < 1e-20f)
            {
                float distanceToPlane = Vector.DotProduct(normal, Vector.Subtract(A, ray.Origin));
                if (Math.Abs(distanceToPlane) < 1e-20f)
                {
                    if (IntersectionPlane(ray, trianglePlane, out float intersectionP))
                    {
                        Vector planeIntersection = Vector.Add(ray.Origin, Vector.MultiplyScalar(ray.Direction, intersectionP));

                        if (IsPointInsideTriangle(planeIntersection, A, B, C))
                        {
                            intersection = planeIntersection;
                            return true;
                        }
                    }

                 return false;
                }
            }
               
            float t = Vector.DotProduct(normal, Vector.Subtract(A, ray.Origin)) / dotRayNorm;

            if (t < 0.0f)
                return false;
            
            intersection = Vector.Add(ray.Origin, Vector.MultiplyScalar(ray.Direction, t));

            Vector AP = Vector.Subtract(intersection, A);
            Vector BP = Vector.Subtract(intersection, B);
            Vector CP = Vector.Subtract(intersection, C);

            Vector v1 = Vector.CrossProduct(edgeAB, AP);
            Vector v2 = Vector.CrossProduct(edgeAC, BP);
            Vector v3 = Vector.CrossProduct(Vector.Subtract(B, C), CP);

            if (Vector.DotProduct(v1, normal) >= 0.0f && Vector.DotProduct(v2, normal) >= 0.0f && Vector.DotProduct(v3, normal) >= 0.0f)
                return true;
            
            return false;
    }

        private static bool IsPointInsideTriangle(Vector point, Vector A, Vector B, Vector C)
    {
        
        Vector edgeAB = Vector.Subtract(B, A);
        Vector edgeAC = Vector.Subtract(C, A);
        Vector edgeBC = Vector.Subtract(C, B);

        Vector AP = Vector.Subtract(point, A);
        Vector BP = Vector.Subtract(point, B);
        Vector CP = Vector.Subtract(point, C);

        Vector normal = Vector.CrossProduct(edgeAB, edgeAC);

        Vector v1 = Vector.CrossProduct(edgeAB, AP);
        Vector v2 = Vector.CrossProduct(edgeAC, BP);
        Vector v3 = Vector.CrossProduct(edgeBC, CP);

        return (Vector.DotProduct(v1, normal) >= 0.0f && Vector.DotProduct(v2, normal) >= 0.0f && Vector.DotProduct(v3, normal) >= 0.0f);
    }
 }
}