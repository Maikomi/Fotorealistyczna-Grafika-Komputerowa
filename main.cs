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
  }
}
}