using Vector;
using Vec = Vector.Vector;
using sqrtMatrix = SqrMatrix.SqrMatrix;
using System.IO;
using System.Numerics;
using Plane = Vector.Plane;
using Quaternion = Vector.Quaternion;


namespace RayTracing
{
  class Program
  {
    static void Main(string[] args)
    {
      //ZADANIE 1
      Console.WriteLine("\nTASK 1: IMPLEMENTED VECTOR CLASS");

      Vec v1 = new Vec(0, 3, 0);
      Vec v2 = new Vec(5, 5, 0);
      Console.WriteLine("v1 = " + v1 + ", v2 = " + v2);
      
      //ZADANIE 2
      Console.WriteLine("\nTASK 2");
      if ( Vec.Add(v1, v2) == Vec.Add(v2, v1))
      {
        Console.WriteLine("Addition is commutative and the sum is: " + Vec.Add(v1, v2));
      }
      else Console.WriteLine("Sth went wrong");

      //ZADANIE 3
       Console.WriteLine("\nTASK 3");
       Console.WriteLine("Angle between v1 and v2 is: " + Vec.AngleBetweenVectors(v1, v2) + " degrees");

      //ZADANIE 4
      Console.WriteLine("\nTASK 4");
      Vec v3 = new Vec(4, 5, 1);
      Vec v4 = new Vec(4, 1, 3);
      Console.WriteLine("Perpendicular vector is: " + Vec.CrossProduct(v3, v4));

      //ZADANIE 5
      Console.WriteLine("\nTASK 5");
      Console.WriteLine("Normalized vector is: " + Vec.CrossProduct(v3, v4).Normalize());

      //ZADANIE 6
      Console.WriteLine("\nTASK 6: IMPLEMENTED RAY, PLANE AND SPHERE CLASSES");

      //ZADANIE 7 - 9
      Sphere sphere = new Sphere(new Vec(0, 0, 0), 10);
      Ray ray1 = new Ray( new Vec(0, 0, -20), new Vec(0, 0, 1));
      Ray ray2 = new Ray( new Vec(0, 0, -20), new Vec(0, 1, 0));
      Console.WriteLine("\nTASK 7-9: SPHERE AND RAYS DEFINED");

      //ZADANIE 10-11
      Console.WriteLine("\nTASK 10-11");
      if (Intersections.IntersectionSphere(ray1, sphere, out float t0, out float t1))
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
      if (Intersections.IntersectionSphere(ray2, sphere, out float t02, out float t12))
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
      Console.WriteLine("\nTASK 12");
      Ray ray3 = new Ray( new Vec(10, 0, 0), new Vec(0, 0, 1));
      if (Intersections.IntersectionSphere(ray3, sphere, out float t03, out float t13))
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
      Console.WriteLine("\nTASK 13: PLANE DEFINED");

    //ZADANIE 14
    Console.WriteLine("\nTASK 14");
    if (Intersections.IntersectionPlane(ray2, plane, out float tp))
        {
          Console.WriteLine("Plane intersects with ray 2 at point: " + tp);
          Vec inter0p = ray2.PointAt(tp);
          Console.WriteLine("Intersection 1: " + inter0p);
        }
        else
        {
          Console.WriteLine("Plane doesn't intersect with ray 2");
        }

    //ZADANIE 15
    Console.WriteLine("\nTASK 15");
    Console.WriteLine("CASE 1");
    Vec point1 = new Vec(-1, 0.5f, 0);
    Vec point2 = new Vec(1, 0.5f, 0);
    Vec direction = Vec.Subtract(point2, point1).Normalize();
    Ray rayT = new Ray(point1, direction);
    
    if (Intersections.IntersectonTriangle(new Vec(0, 0, 0), new Vec(1, 0, 0), new Vec(0, 1, 0), rayT, out Vec tt))
    {
      Console.WriteLine("Triangle intersects with line");
    }
    else
       {
         Console.WriteLine("Triangle doesn't intersect with line");
       }

    Console.WriteLine("CASE 2");
    Vec point3 = new Vec(2, -1, 0);
    Vec point4 = new Vec(2, 2, 0);
    Vec direction2 = Vec.Subtract(point4, point3).Normalize();
    Ray rayT2 = new Ray(point3, direction2);
    if (Intersections.IntersectonTriangle(new Vec(0, 0, 0), new Vec(1, 0, 0), new Vec(0, 1, 0), rayT2, out Vec tt2))
    {
      Console.WriteLine("Triangle intersects with line");
    }
    else
       {
         Console.WriteLine("Triangle doesn't intersect with line");
       }

    Console.WriteLine("CASE 3");
    Vec point5 = new Vec(0, 0, -1);
    Vec point6 = new Vec(0, 0, 1);
    Vec direction3 = Vec.Subtract(point6, point3).Normalize();
    Ray rayT3 = new Ray(point5, direction3);
    if (Intersections.IntersectonTriangle(new Vec(0, 0, 0), new Vec(1, 0, 0), new Vec(0, 1, 0), rayT3, out Vec tt3))
    {
      Console.WriteLine("Triangle intersects with line");
    }
    else
       {
         Console.WriteLine("Triangle doesn't intersect with line");
       }

    //ZADANIE DODATKOWE
      Console.WriteLine("\n----------------");
      Console.WriteLine("ADDITIONAL TASKS");
      Console.WriteLine("----------------");

    //ZADANIE 1
    Console.WriteLine("\nTASK 1: MATRIX CLASS IMPLEMENTED");

    //ZADANIE 2
    Console.WriteLine("\nTASK 2");
    sqrtMatrix matrix1 = new sqrtMatrix(3);
    sqrtMatrix matrix2 = new sqrtMatrix(3);
    float [ , ] arrayM1 = {
            { 1, 2, 0 },
            { 4, 0, 6 },
            { 0, 0, 0 }
    };
    float [ , ] arrayM2 = {
            { 1, -4, 2 },
            { -2, 1, 3 },
            { 2, 6, 8 }
    };

    matrix1.SetMatrix(arrayM1);
    matrix2.SetMatrix(arrayM2);

    //matrix1.ConsolePrint();
    //string m1 = matrix1.Print();
    //Console.WriteLine($"{m1}");

     string task2 = @"..\..\..\Task2.txt";

    sqrtMatrix adjointMatrix = matrix1.GetAdjointMatrix();
        
      // Create a file to write to.
      using (StreamWriter sw = new StreamWriter(task2, false))
      {
          sw.WriteLine("ADDITIONAL TASK 2");
          sw.WriteLine($"Matrix 1: {matrix1.Print()}");
          sw.WriteLine($"\nMatrix 2: {matrix2.Print()}");
          sw.WriteLine($"m1 + m2: {sqrtMatrix.Add(matrix1, matrix2).Print()}");
          sw.WriteLine($"m1 - m2: {sqrtMatrix.Subtract(matrix1, matrix2).Print()}");
          sw.WriteLine($"Cofactor matrix 1: {matrix1.Cofactor(2, 2)}");
          sw.WriteLine($"Adjoint matrix 1: {adjointMatrix.Print()}");
          sw.WriteLine($"Determinant matrix 1: {matrix1.Determinant()}");
          sw.WriteLine($"m2 * 3: {sqrtMatrix.MultiplyByScalar(matrix1, 3).Print()}");
          sw.WriteLine($"m1 * m2: {sqrtMatrix.Multiply(matrix1, matrix2).Print()}");
          sw.WriteLine($"m1 transposed: {sqrtMatrix.Transpose(matrix1).Print()}");
          sw.WriteLine($"m2 inverted: {sqrtMatrix.Invert(matrix2).Print()}");
          sw.WriteLine($"m1 scaled x2: {sqrtMatrix.Scale(matrix1, 2).Print()}");
          sw.WriteLine($"m1 rotated: {sqrtMatrix.Rotate(3, 90).Print()}");
          sw.WriteLine($"m1 translated: {sqrtMatrix.Translate(3, 90, 0).Print()}");
      }	

    Console.WriteLine("MATRIX CLASS TESTED, RESULTS IN TXT FILE");

    //ZADANIE 3
    Console.WriteLine("\nTASK 3");
    // Tworzymy wektor wejściowy
      Vec v = new Vec(1, 0, 1);

      // Macierz obrotu wokół osi Y o 90 stopni
      float rotateAngle = (float)(Math.PI / 2); // konwersja stopni na radiany
      sqrtMatrix rotationY = new sqrtMatrix(3);
      rotationY.SetMatrix(new float[,]
      {
          { (float)Math.Cos(rotateAngle), 0, (float)Math.Sin(rotateAngle) },
          { 0, 1, 0 },
          { -(float)Math.Sin(rotateAngle), 0, (float)Math.Cos(rotateAngle) }
      });

      // Mnożenie macierzy przez wektor
      Vec rotatedVec = rotationY.MultiplyVector(v);

      Console.WriteLine($"Original Vector: {v}");
      Console.WriteLine($"Rotated Vector: {rotatedVec}");

    //ZADANIE 4
      Console.WriteLine("\nTASK 4");

      sqrtMatrix A = new sqrtMatrix(2);
      A.SetMatrix(new float[,] { { 1, 2 }, { 3, 4 } });

      sqrtMatrix B = new sqrtMatrix(2);
      B.SetMatrix(new float[,] { { 0, 1 }, { 1, 0 } });

      // Obliczamy A * B i B * A
      sqrtMatrix AB = sqrtMatrix.Multiply(A,B);
      sqrtMatrix BA = sqrtMatrix.Multiply(B,A);

      // Wyświetlamy wyniki
      Console.WriteLine("A * B:");
      AB.ConsolePrint();

      Console.WriteLine("\nB * A:");
      BA.ConsolePrint();

      // Sprawdzamy, czy AB != BA
      if (!AB.Equals(BA))
      {
          Console.WriteLine("Mnożenie macierzy NIE jest przemienne!");
      }
      else
      {
          Console.WriteLine("Mnożenie macierzy jest przemienne (coś poszło nie tak)!");
      }

    //ZADANIE 5
      Console.WriteLine("\nTASK 5: QUATERIONS IMPLEMENTED");

    // ZADANIE 6: Obrót wektora [3,1,3] wokół wektora [1,0,1] o 90 stopni  
      Console.WriteLine("\nTASK 6");  

      Vec vector = new Vec(3, 1, 3);
      Vec axis = new Vec(1, 0, 1).Normalize();
      float angle = 90.0f;

      Vec rotatedVector = Quaternion.Rotate(angle, axis, vector);
  
      string task6 = @"..\..\..\Task6.txt";
        
      // Create a file to write to.
      using (StreamWriter sw = new StreamWriter(task6, false))
      {
          sw.WriteLine("ADDITIONAL TASK 6");
          sw.WriteLine($"Original vector: {vector}");
          sw.WriteLine($"Rotated vector: {rotatedVector}");
      }	
      Console.WriteLine("DATA SAVED IN TXT FILE.");  
    }
  }
}
