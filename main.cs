using System;
using Vector;
using Vec = Vector.Vector;
using SqrMat = SqrMatrix.SqrMatrix;
using System.Diagnostics.CodeAnalysis;

namespace RayTracing
{
  class Program
  {
    static void Main(string[] args)
    {
      //ZADANIE 1
      Console.WriteLine("TASK 1: IMPLEMENTED VECTOR CLASS");

      Vec v1 = new Vec(0, 3, 0);
      Vec v2 = new Vec(5, 5, 0);
      Console.WriteLine("v1 = " + v1 + ", v2 = " + v2);
      
      //ZADANIE 2
      Console.WriteLine("TASK 2");
      if ( Vec.Add(v1, v2) == Vec.Add(v2, v1))
      {
        Console.WriteLine("Addition is commutative and the sum is: " + Vec.Add(v1, v2));
      }
      else Console.WriteLine("Sth went wrong");

      //ZADANIE 3
       Console.WriteLine("TASK 3");
       Console.WriteLine("Angle between v1 and v2 is: " + Vec.AngleBetweenVectors(v1, v2) + " degrees");

      //ZADANIE 4
      Console.WriteLine("TASK 4");
      Vec v3 = new Vec(4, 5, 1);
      Vec v4 = new Vec(4, 1, 3);
      Console.WriteLine("Perpendicular vector is: " + Vec.CrossProduct(v3, v4));

      //ZADANIE 5
      Console.WriteLine("TASK 5");
      Console.WriteLine("Normalized vector is: " + Vec.CrossProduct(v3, v4).Normalize());

      //ZADANIE 6
      Console.WriteLine("TASK 6: IMPLEMENTED RAY, PLANE AND SPHERE CLASSES");

      //ZADANIE 7 - 9
      Sphere sphere = new Sphere(new Vec(0, 0, 0), 10);
      Ray ray1 = new Ray( new Vec(0, 0, -20), new Vec(0, 0, 1));
      Ray ray2 = new Ray( new Vec(0, 0, -20), new Vec(0, 1, 0));
      Console.WriteLine("TASK 7-9: SPHERE AND RAYS DEFINED");

      //ZADANIE 10-11
      Console.WriteLine("TASK 10-11");
      if (sphere.Intersects(ray1, out float t0, out float t1))
      {
        Console.WriteLine("Sphere intersects with ray 1 at points: " + t0 + " and " + t1);
        Vec inter0 = ray1.PointAt(t0);
        Vec inter1 = ray1.PointAt(t1);
        Console.WriteLine("Intersection 1: " + inter0);
        Console.WriteLine("Intersection 2: " + inter1);
      }
      else
      {
        Console.WriteLine("Sphere doesn't intersect with ray 1");
      }
      if (sphere.Intersects(ray2, out float t02, out float t12))
      {
        Console.WriteLine("Sphere intersects with ray 1 at points: " + t02 + " and " + t12);
        Vec inter02 = ray2.PointAt(t02);
        Vec inter12 = ray2.PointAt(t12);
        Console.WriteLine("Intersection 1: " + inter02);
        Console.WriteLine("Intersection 2: " + inter12);
      }
      else
      {
        Console.WriteLine("Sphere doesn't intersect with ray 2");
      }

    //ZADANIE 12
      Console.WriteLine("TASK 12");
      Ray ray3 = new Ray( new Vec(10, 0, 0), new Vec(0, 0, 1));
      if (sphere.Intersects(ray3, out float t03, out float t13))
        {
          Console.WriteLine("Sphere intersects with ray 3 at point: " + t03);
          Vec inter03 = ray3.PointAt(t03);
          Console.WriteLine("Intersection 1: " + inter03);
        }
        else
        {
          Console.WriteLine("Sphere doesn't intersect with ray 3");
        }

    //ZADANIE 13
      Plane plane = new Plane(new Vec(0, 0, 0), new Vec(0, 1, 1));
      Console.WriteLine("TASK 13: PLANE DEFINED");

    //ZADANIE 14
    Console.WriteLine("TASK 14");
    if (plane.Intersects(ray2, out float tp))
        {
          Console.WriteLine("Plane intersects with ray 2 at point: " + tp);
          Vec inter0p = ray2.PointAt(tp);
          Console.WriteLine("Intersection 1: " + inter0p);
        }
        else
        {
          Console.WriteLine("Plane doesn't intersect with ray 2");
        }

    }
    
}
}