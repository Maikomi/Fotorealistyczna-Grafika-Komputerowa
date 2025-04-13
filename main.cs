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
      int width = 1000, height = 1000;
      ImageRenderer renderer = new ImageRenderer(width, height);
      AdaptiveAntialiasing antialiasing = new AdaptiveAntialiasing(0.5f, 4);

      PerspectiveCamera cameraPer = new PerspectiveCamera(width, height, 90.0f);
      OrthographicCamera cameraOrtho = new OrthographicCamera(width, height);

      LightIntensity color = new LightIntensity(0, 0, 1);
      Material material = new Material(color, 0.15f, 0.7f, 0.9f, 100);

      LightIntensity color2 = new LightIntensity(1, 0, 0);
      Material material2 = new Material(color2, 0.4f, 0.9f, 0.8f, 50);

      LightIntensity colorWallGrey= new LightIntensity(0.5f, 0.5f, 0.5f);
      LightIntensity colorWallRed = new LightIntensity(1, 0, 0);
      LightIntensity colorWallBlue = new LightIntensity(0, 0, 1);
      Material wallMaterial = new Material(colorWallGrey, 0.1f, 0.1f, 0.9f, 10);
      Material wallMaterialRed = new Material(colorWallRed, 0.1f, 0.1f, 0.9f, 10);
      Material wallMaterialBlue = new Material(colorWallBlue, 0.1f, 0.1f, 0.9f, 10);
      // Ściany pudełka Cornell Box
      Plane floor = new Plane(new Vec(0, -1, 0), new Vec(0, 1, 0), wallMaterial); // podłoga
      Plane ceiling = new Plane(new Vec(0, 1, 0), new Vec(0, -1, 0), wallMaterial); // sufit
      Plane backWall = new Plane(new Vec(0, 0, -3), new Vec(0, 0, 1), wallMaterial); // tylna ściana
      Plane leftWall = new Plane(new Vec(-1, 0, 0), new Vec(1, 0, 0), wallMaterialRed); // lewa ściana (czerwona)
      Plane rightWall = new Plane(new Vec(1, 0, 0), new Vec(-1, 0, 0), wallMaterialBlue); // prawa ściana (niebieska)

      // Obiekty w środku
      Sphere sphere1 = new Sphere(new Vec(-0.5f, -0.5f, -2), 0.5f, material); // niebieska kula
      Sphere sphere2 = new Sphere(new Vec(0.5f, -0.5f, -1.5f), 0.5f, material2); // czerwona kula

      // Światło powierzchniowe w suficie
      SurfaceLight surfaceLight = new SurfaceLight(
        new Vec(0, 0.99f, -2),    // pozycja tuż pod sufitem
        new Vec(0, -1, 0),        // skierowane w dół
        new Vec(1, 0, 0),         // wektor „prawo”
        width: 2.0f,
        height: 1.5f,
        intensity: 5000.0f,
        color: new LightIntensity(1, 1, 1)
      );
      PointLight pointLight = new PointLight(new Vec(0, 0.99f, -1), 100, new LightIntensity(1, 1, 1));     

      List<IRenderableObject> objects = new List<IRenderableObject>
      {
          floor, ceiling, backWall, leftWall, rightWall,
          sphere1, sphere2
      };

      List<LightSource> lights = new List<LightSource>
      {
          surfaceLight, pointLight
      };

      Func<int, int, LightIntensity> backgroundColor = (x, y) => new LightIntensity(0, 0, 0);

      renderer.RenderScene(cameraPer, objects, lights, backgroundColor, antialiasing);
      renderer.SaveToFile("render_per_output.png");
        
    }
  }
}
