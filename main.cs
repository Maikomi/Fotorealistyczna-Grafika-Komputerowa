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
      //Part2Module.Part2();
      int width = 1000, height = 1000;
      ImageRenderer renderer = new ImageRenderer(width, height);
      AdaptiveAntialiasing antialiasing = new AdaptiveAntialiasing(0.5f, 4);

      PerspectiveCamera cameraPer = new PerspectiveCamera(width, height, 90.0f);
      OrthographicCamera cameraOrtho = new OrthographicCamera(width, height);

      LightIntensity color = new LightIntensity(1, 1, 1);
      Material material = new Material(color, 0.1f, 0.1f, 0.9f, 10);
      Sphere sphere = new Sphere(new Vec(0, 0, 0), 0.5f, color, material);

      List<IRenderableObject> objects = new List<IRenderableObject> { sphere };

      Func<int, int, LightIntensity> backgroundColor = GenerateBackground(6, 6, width, height);

      renderer.RenderScene(cameraOrtho, objects, backgroundColor, antialiasing);
      renderer.SaveToFile("render_output.png");
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

        if (i < 3 && j != (rows - 1))
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
