using System;
using System.Drawing;
using Lighting;
using Vector;

namespace RayTracing
{
    public class ImageRenderer
    {
        private Bitmap image;
        private int width, height;

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
        public void RenderSphereScene(OrthographicCamera camera, Sphere sphere, LightIntensity objectColor, LightIntensity backgroundColor)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Ray ray = camera.GenerateRay(i, j);

                    if (Intersections.IntersectionSphere(ray, sphere, out _, out _))
                    {
                        SetPixel(i, j, objectColor);
                    }
                    else
                    {
                        SetPixel(i, j, backgroundColor);
                    }
                }
            }
        }
    }
}
