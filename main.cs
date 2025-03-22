using System;
using Vector;
using Vec = Vector.Vector;
using SqrMat = SqrMatrix.SqrMatrix;

namespace RayTracing
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");  
      Vec v1 = new Vec(1.0f,1.0f,1.0f);
      Console.WriteLine(v1.ToString()); 
      Console.WriteLine("Length: " + v1.VectorLength()); 

      // Test matrix operations
      SqrMat matrix = new SqrMat(3);
      matrix.SetValue(0, 0, 2);
      matrix.SetValue(0, 1, -1);
      matrix.SetValue(0, 2, 0);
      matrix.SetValue(1, 0, -1);
      matrix.SetValue(1, 1, 2);
      matrix.SetValue(1, 2, -1);
      matrix.SetValue(2, 0, 0);
      matrix.SetValue(2, 1, -1);
      matrix.SetValue(2, 2, 2);
      
      Console.WriteLine("Original Matrix:");
      matrix.Print();
      try
      {
          SqrMat inverseMatrix = SqrMat.Invert(matrix);
          Console.WriteLine("Inverse Matrix:");
          inverseMatrix.Print();
      }
      catch (InvalidOperationException e)
      {
          Console.WriteLine("Matrix inversion failed: " + e.Message);
      }
    } 

  }
}