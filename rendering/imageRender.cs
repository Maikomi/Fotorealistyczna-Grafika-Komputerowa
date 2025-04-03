using System.Drawing;
using Lighting;
using Vector;

namespace RayTracing
{
    public interface IRenderableObject
    {
        bool Intersect(Ray ray, out float t);
        LightIntensity GetColor();

        Material GetMaterial();
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

        // NOWA FUNKCJA RENDERUJĄCA SCENĘ
        public void RenderScene(Camera camera, List<IRenderableObject> objects, Func<int, int, LightIntensity> backgroundColor, AdaptiveAntialiasing antialiasing)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    LightIntensity pixelColor = antialiasing.QuincunxSample(i, j, camera, objects, backgroundColor);
                    SetPixel(i, j, pixelColor);
                }
            }
        }
    }
}
