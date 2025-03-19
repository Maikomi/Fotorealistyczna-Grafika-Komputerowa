using System;
using Vector;
using Vec = Vector.Vector;

namespace RayTracing
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");  
      Vec v1 = new Vec(1.0f,1.0f,1.0f);
      Console.WriteLine(v1.ToString()); 
      Console.WriteLine("Length: " + v1.vectorLength()); 
    }

    // public void Test()
    // {
    //     Vec v2 = new Vec(1,1,1);
    //     Console.WriteLine(v2.ToString());
    // }
  }
}