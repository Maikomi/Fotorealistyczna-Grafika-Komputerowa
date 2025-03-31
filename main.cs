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

      Console.WriteLine("\n\nSPHERE");

      int width = 1000, height = 1000;
      ImageRenderer renderer = new ImageRenderer(width, height);
      OrthographicCamera camera = new OrthographicCamera(width, height);
      PerspectiveCamera camera2 = new PerspectiveCamera(width, height, 300.0f);
      Sphere sphere = new Sphere(new Vec(0, 0, -10), 0.5f, new LightIntensity(0, 1, 0));
      Sphere sphere2 = new Sphere(new Vec(0, 5.5f, -20.5f), 0.5f, new LightIntensity(0, 0, 1));

      List<IRenderableObject> objects = new List<IRenderableObject> { sphere, sphere2 };

      LightIntensity backgroundColor = new LightIntensity(0, 0, 0); 

      // RENDERUJEMY SCENĘ
      renderer.RenderScene(camera2, objects, backgroundColor);

      // ZAPISUJEMY OBRAZ
      renderer.SaveToFile("render_output.png");
    }
  }
}
