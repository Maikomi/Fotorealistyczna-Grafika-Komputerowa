using Vector;
using Vec = Vector.Vector;
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

            PerspectiveCamera camera = new PerspectiveCamera(width, height, 90.0f);

            // Materiały
            var diffuseRed = new DiffuseMaterial(new Vec(1, 0, 0));
            var diffuseBlue = new DiffuseMaterial(new Vec(0, 0, 1));
            var diffuseGray = new DiffuseMaterial(new Vec(0.5f, 0.5f, 0.5f));
            var diffuseWhite = new DiffuseMaterial(new Vec(1, 1, 1));
            var diffuseBlack = new DiffuseMaterial(new Vec(0, 0, 0));

            var mirrorMaterial = new ReflectiveMaterial(new Vec(0.999f, 0.999f, 0.999f)); // prawie idealne lustro
            var glassMaterial = new RefractiveMaterial(new Vec(1.0f, 1.0f, 1.0f), 1.5f);  // współczynnik załamania 1.5

            // Ściany pudełka Cornell Box
            var floor = new Plane(new Vec(0, -1, 0), new Vec(0, 1, 0), diffuseWhite);
            var ceiling = new Plane(new Vec(0, 1, 0), new Vec(0, -1, 0), diffuseWhite);
            var backWall = new Plane(new Vec(0, 0, -3), new Vec(0, 0, 1), diffuseWhite);
            var frontWall = new Plane(new Vec(0, 0, -3), new Vec(0, 0, 1), diffuseWhite);
            var leftWall = new Plane(new Vec(-1, 0, 0), new Vec(1, 0, 0), diffuseRed);
            var rightWall = new Plane(new Vec(1, 0, 0), new Vec(-1, 0, 0), diffuseBlue);

            // Kule: jedna zwierciadlana, druga refrakcyjna
            var mirrorSphere = new Sphere(new Vec(-0.5f, -0.5f, -2), 0.5f, mirrorMaterial);
            var glassSphere = new Sphere(new Vec(0.5f, -0.5f, -1.5f), 0.5f, glassMaterial);

            // Światło
            var surfaceLight = new SurfaceLight(
                new Vec(0, 0.99f, -1), 
                new Vec(0, -1, 0),
                new Vec(1, 0, 0),
                width: 20.0f,
                height: 10.5f,
                intensity: 10000.0f,
                color: new LightIntensity(1, 1, 1)
            );

            var pointLight = new PointLight(new Vec(0, 0.99f, -1), 100, new LightIntensity(1, 1, 1));

            // Obiekty i światła
            var objects = new List<IRenderableObject>
            {
                floor, ceiling, backWall, leftWall, rightWall, frontWall,
                mirrorSphere, glassSphere
            };

            var lights = new List<LightSource>
            {
                surfaceLight
            };

            // Tło
            Func<int, int, LightIntensity> backgroundColor = (x, y) => new LightIntensity(0, 0, 0);

            // Renderowanie
            renderer.RenderScene(camera, objects, lights, backgroundColor, antialiasing);
            renderer.SaveToFile("render_output.png");
        }
    }
}
