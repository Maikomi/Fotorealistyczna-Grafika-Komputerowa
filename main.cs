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

      // ZADANIE 2
      Console.WriteLine("\n\nSPHERE");

      int width = 500, height = 500;
      ImageRenderer renderer = new ImageRenderer(width, height);
      OrthographicCamera camera = new OrthographicCamera(width, height);
      Sphere sphere = new Sphere(new Vec(0, 0, 0), 0.5f);

      LightIntensity objectColor = new LightIntensity(0, 0, 1); // Czerwony
      LightIntensity backgroundColor = new LightIntensity(0, 0, 0); // Czarny

      // RENDERUJEMY SCENĘ
      renderer.RenderSphereScene(camera, sphere, objectColor, backgroundColor);

      // ZAPISUJEMY OBRAZ
      renderer.SaveToFile("render_output.png");
    }
  }
}
