using Vector;
using Vec = Vector.Vector;
using Lighting;

namespace RayTracing
{
  class Part2Module
  {
    public static void Part2()
    {
    Console.WriteLine("\n\nPART TWO");
      LightIntensity light1 = new LightIntensity(0.7f, 0.8f, 0.5f);
      LightIntensity light2 = new LightIntensity(0.5f, 0.3f, 0.6f);
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
      Sphere sphere2 = new Sphere(new Vec(6.5f, 0, -15), 3.5f, new LightIntensity(1, 0, 0));

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

      float[,] lastRowColors = new float[,]
      {
        { 1, 0, 0 },
        { 0, 1, 0 },
        { 0, 0, 1 },
        { 1, 0, 1 },
        { 0, 1, 1 },
        { 1, 1, 1 }
      };

      return (x, y) =>
      {
        int i = x / sqWidth;
        int j = y / sqHeight;

        i = Math.Min(i, rows - 1);

        float t = (float)j / (rows - 1);

        if (i < 3 && j != (rows-1))
        {
          float factor = t * 0.3f + 0.3f;
          float R = lastRowColors[i, 0] * factor;
          float G = lastRowColors[i, 1] * factor;
          float B = lastRowColors[i, 2] * factor;
          return new LightIntensity(R, G, B);
        }
        else
        {
          float R = lastRowColors[i, 0];
          float G = lastRowColors[i, 1];
          float B = lastRowColors[i, 2] * t;

          return new LightIntensity(R, G, B);
        }
      };
    }
  }
}