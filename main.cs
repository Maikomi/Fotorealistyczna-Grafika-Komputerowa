using Vector;
using Vec = Vector.Vector;
using sqrtMatrix = SqrMatrix.SqrMatrix;
using System.IO;
using System.Numerics;
using Plane = Vector.Plane;
using Quaternion = Vector.Quaternion;
using Lighting;
using System.Drawing;

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
      AdaptiveAntialiasing antialiasing = new AdaptiveAntialiasing(0.5f, 4);

      OrthographicCamera cameraOrtho = new OrthographicCamera(width, height);
      PerspectiveCamera cameraPer = new PerspectiveCamera(width, height, 90.0f);

      Sphere sphere = new Sphere(new Vec(0, 0, -10), 4, new LightIntensity(0, 0, 1));
      Sphere sphere2 = new Sphere(new Vec(6.5f, 0, -15), 3.5f , new LightIntensity(1, 0, 0));

      List<IRenderableObject> objects = new List<IRenderableObject> { sphere, sphere2 };

      Func<int, int, LightIntensity> backgroundColor = GenerateBackground(6, 6, width, height);

      //Orthographic cam
      renderer.RenderScene(cameraOrtho, objects, backgroundColor, antialiasing);
      renderer.SaveToFile("render_output_ortho.png");

      //Perspective cam
      renderer.RenderScene(cameraPer, objects, backgroundColor, antialiasing);
      renderer.SaveToFile("render_output_perspective.png");

    }

    static Func<int, int, LightIntensity> GenerateBackground(int rows, int cols, int width, int height)
    {
      int sqWidth = width / cols;
      int sqHeight = height / rows;

      double[,] firstRowColors = new double[,]
    {
        { 19 / 255.0, 1 / 255.0, 1 / 255.0 },
        { 3 / 255.0, 18 / 255.0, 0 },
        { 0, 2 / 255.0, 20 / 255.0 },
        { 243 / 255.0, 60 / 255.0, 51 / 255.0 },
        { 107 / 255.0, 242 / 255.0, 0 },
        { 255 / 255.0, 247 / 255.0, 38 / 255.0 }
    };

    double[,] lastRowColors = new double[,]
    {
        { 243 / 255.0, 60 / 255.0, 45 / 255.0 },
        { 107 / 255.0, 242 / 255.0, 0 },
        { 0, 75 / 255.0, 254 / 255.0 },
        { 238 / 255.0, 95 / 255.0, 255 / 255.0 },
        { 91 / 255.0, 250 / 255.0, 252 / 255.0 },
        { 255 / 255.0, 255 / 255.0, 255 / 255.0 }
    };

      return (x, y) =>
      {
        int i = x / sqWidth;
        int j = y / sqHeight;

        i = Math.Min(i, cols - 1);

        double t = (double)j / (rows - 1);

        double brightness = (double)j / rows; // Wartość od 0 do 1

        double R = firstRowColors[i, 0] * (1 - t) + lastRowColors[i, 0] * t;
        double G = firstRowColors[i, 1] * (1 - t) + lastRowColors[i, 1] * t;
        double B = firstRowColors[i, 2] * (1 - t) + lastRowColors[i, 2] * t;

        return new LightIntensity(R, G, B);
      };
    }

  }
}
