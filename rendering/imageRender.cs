using System.Drawing;
using Lighting;
using Vector;
using Vec = Vector.Vector;

namespace RayTracing
{
    public interface IRenderableObject
    {
        bool Intersect(Ray ray, out float t);
        LightIntensity GetColor();
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

        public LightIntensity PhongLighting(Vec point, Vec viewDir, IRenderableObject obj, List<LightSource> lights)
        {
            Material material = obj.GetMaterial();
            Vec normal = obj.GetNormal(point);
            LightIntensity result = new LightIntensity(material.Color.x, material.Color.y, material.Color.z) * material.Ambient;
            foreach (var light in lights)
            {
                Vec lightDir = (light.Position - point).Normalize();
                float diff = Math.Max(Vec.DotProduct(normal, lightDir), 0);
                Vec reflectDir = (normal * Vec.DotProduct(normal, lightDir) * 2 - lightDir).Normalize();
                float spec = (float)Math.Pow(Math.Max(Vec.DotProduct(viewDir, reflectDir), 0), material.Shininess);

                result += new LightIntensity(material.Diffuse * diff) * light.Intensity + new LightIntensity(material.Specular * spec) * light.Intensity;
            }

            return result;
        }

        public void RenderScene(Camera camera, List<IRenderableObject> objects, List<LightSource> lights, Func<int, int, LightIntensity> backgroundColor, AdaptiveAntialiasing antialiasing)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Ray ray = camera.GenerateRay(i, j);
                    LightIntensity pixelColor = backgroundColor(i, j);

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

                    if (closestObject != null)
                    {
                        Vec hitPoint = ray.Origin + ray.Direction * closestT;
                        Vec viewDir = -ray.Direction.Normalize();
                        pixelColor = PhongLighting(hitPoint, viewDir, closestObject, lights);
                    }

                    SetPixel(i, j, pixelColor);
                }
            }
        }
    }
}
