using System.Drawing;
using Lighting;
using Vector;
using Vec = Vector.Vector;

namespace RayTracing
{
    public interface IRenderableObject
    {
        bool Intersect(Ray ray, out float t);
        Material GetMaterial();
        Vec GetNormal(Vec point);
    }

    public class ImageRenderer
    {
        private Bitmap image;
        private int width, height;

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public ImageRenderer(int width, int height)
        {
            this.width = width;
            this.height = height;
            image = new Bitmap(width, height);
        }

        public void SetPixel(int x, int y, LightIntensity color)
        {
            int red = (int)Math.Clamp(color.GetRed() * 255, 0, 255);
            int green = (int)Math.Clamp(color.GetGreen() * 255, 0, 255);
            int blue = (int)Math.Clamp(color.GetBlue() * 255, 0, 255);

            image.SetPixel(x, y, Color.FromArgb(red, green, blue));
        }

        public void SaveToFile(string filename)
        {
            image.Save(filename);
            Console.WriteLine($"Image has been saved as {filename}");
        }

        

        public LightIntensity PhongLighting(Vec point, Vec viewDir, IRenderableObject obj, List<IRenderableObject> objects, List<LightSource> lights)
        {
            Material material = obj.GetMaterial();
            Vec normal = obj.GetNormal(point);
            LightIntensity result = new LightIntensity(material.Color.x, material.Color.y, material.Color.z) * material.Ambient;

            foreach (var light in lights)
            {
                result += light.Illuminate(point, normal, viewDir, material, objects, obj); // <--- teraz objects to cała scena!
            }

            return result.Clamped();
        }


        public LightIntensity TraceRay(Ray ray, List<IRenderableObject> objects, List<LightSource> lights, int depth = 0, int maxDepth = 4)
        {
            if (depth > maxDepth) return new LightIntensity(0, 0, 0);

            float closestT = float.MaxValue;
            IRenderableObject closestObject = null;

            foreach (var obj in objects)
            {
                if (obj.Intersect(ray, out float t) && t < closestT)
                {
                    closestT = t;
                    closestObject = obj;
                }
            }

            if (closestObject == null)
                return new LightIntensity(0, 0, 0);

            Vec hitPoint = ray.Origin + ray.Direction * closestT;
            Vec normal = closestObject.GetNormal(hitPoint).Normalize();
            Vec viewDir = -ray.Direction.Normalize();
            Material mat = closestObject.GetMaterial();

            if (mat is DiffuseMaterial)
            {
                return PhongLighting(hitPoint, viewDir, closestObject, objects, lights);
            }
            else if (mat is ReflectiveMaterial)
            {
                Vec reflectDir = Vec.Reflect(ray.Direction, normal).Normalize();
                Ray reflectRay = new Ray(hitPoint + normal * 0.001f, reflectDir);
                return TraceRay(reflectRay, objects, lights, depth + 1, maxDepth);
            }
            else if (mat is RefractiveMaterial refractive)
            {
                float eta = 1.0f / 1.5f; // domyślny współczynnik jeśli nie zrobiono pola
                float cosi = (float)Math.Clamp(Vec.DotProduct(viewDir, normal), -1, 1);
                Vec n = normal;

                if (cosi < 0)
                {
                    cosi = -cosi;
                }
                else
                {
                    eta = 1.5f; // lub refractive.RefractionIndex, jeśli dodasz to jako pole
                    n = -normal;
                }

                float k = 1 - eta * eta * (1 - cosi * cosi);
                if (k < 0)
                {
                    // Całkowite wewnętrzne odbicie
                    Vec reflectDir = Vec.Reflect(ray.Direction, normal).Normalize();
                    Ray reflectRay = new Ray(hitPoint + normal * 0.001f, reflectDir);
                    return TraceRay(reflectRay, objects, lights, depth + 1, maxDepth);
                }
                else
                {
                    Vec refractDir = (ray.Direction * eta + n * (eta * cosi - MathF.Sqrt(k))).Normalize();
                    Ray refractRay = new Ray(hitPoint - n * 0.001f, refractDir);
                    return TraceRay(refractRay, objects, lights, depth + 1, maxDepth);
                }
            }

            return new LightIntensity(0, 0, 0);

        }

        public void RenderScene(Camera camera, List<IRenderableObject> objects, List<LightSource> lights, Func<int, int, LightIntensity> backgroundColor, AdaptiveAntialiasing antialiasing)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Ray ray = camera.GenerateRay(i, j);
                    LightIntensity pixelColor = TraceRay(ray, objects, lights);
                    SetPixel(i, j, pixelColor);
                }
            }
        }
    }
}
