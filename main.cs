using Vector;
using Vec = Vector.Vector;
using sqrtMatrix = SqrMatrix.SqrMatrix;
using System.IO;
using System.Numerics;
using Plane = Vector.Plane;
using Quaternion = Vector.Quaternion;
using Lighting;

namespace RayTracing
{
  class Program
  {
    static void Main(string[] args)
    {
    //ZADANIE 1
    //Part1Module.Part1();

    //ZADANIE 2
      Console.WriteLine("\n\nPART TWO");
      LightIntensity light1 = new LightIntensity(0.7, 0.8, 0.5);
      LightIntensity light2 = new LightIntensity(0.5, 0.3, 0.6);
      LightIntensity sum = light1 + light2; // nie może przekroczyć 1
      LightIntensity scaled = light1 * 2;  // nie może przekroczyć 1
      LightIntensity negativeTest = light1 * -1; // nie może spaść poniżej 0

      Console.WriteLine("Light 1: " + light1);
      Console.WriteLine("Light 2: " + light2);
      Console.WriteLine("Sum (light1 + light2): " + sum);
      Console.WriteLine("Scaled (light1 * 2): " + scaled);
      Console.WriteLine("Negative Test (light1 * -1): " + negativeTest);

    }
  }
}
