using System;
using System.Drawing;
using Lighting;
using Vector;

namespace RayTracing
{
    public class AdaptiveAntialiasing
    {
        private float threshold;
        private int maxDepth;

        public AdaptiveAntialiasing(float threshold, int maxDepth)
        {
            this.threshold = threshold;
            this.maxDepth = maxDepth;
        }

        public LightIntensity QuincunxSample(int x, int y, Camera camera, List<IRenderableObject> objects, Func<int, int, LightIntensity> backgroundColor)
        {
            return QuincunxSampleRecursive(x, y, camera, objects, backgroundColor, maxDepth);
        }

        private LightIntensity QuincunxSampleRecursive(int x, int y, Camera camera, List<IRenderableObject> objects, Func<int, int, LightIntensity> backgroundColor, int depth)
        {
            float midX = x + 0.5f;
            float midY = y + 0.5f;

            LightIntensity A = RenderPixel(x, y, camera, objects, backgroundColor);   
            LightIntensity B = RenderPixel(x + 1, y, camera, objects, backgroundColor);
            LightIntensity C = RenderPixel(x, y + 1, camera, objects, backgroundColor); 
            LightIntensity D = RenderPixel(x + 1, y + 1, camera, objects, backgroundColor);

            LightIntensity avgColor = (A + B + C + D) * 0.25f;

            if (depth > 0 && (ColorDifference(A, B) > threshold || ColorDifference(B, C) > threshold ||
                              ColorDifference(C, D) > threshold || ColorDifference(D, A) > threshold))
            {
                LightIntensity F = RenderPixel((int)midX, y, camera, objects, backgroundColor);     
                LightIntensity G = RenderPixel(x, (int)midY, camera, objects, backgroundColor);     
                LightIntensity H = RenderPixel((int)midX, (int)midY, camera, objects, backgroundColor);
                LightIntensity I = RenderPixel(x, (int)midY, camera, objects, backgroundColor);
                LightIntensity J = RenderPixel((int)midX, y + 1, camera, objects, backgroundColor);

                LightIntensity subColor1 = QuincunxSampleRecursive(x, y, camera, objects, backgroundColor, depth - 1);
                LightIntensity subColor2 = QuincunxSampleRecursive(x + 1, y, camera, objects, backgroundColor, depth - 1);
                LightIntensity subColor3 = QuincunxSampleRecursive(x, y + 1, camera, objects, backgroundColor, depth - 1);
                LightIntensity subColor4 = QuincunxSampleRecursive(x + 1, y + 1, camera, objects, backgroundColor, depth - 1);

                avgColor = (subColor1 + subColor2 + subColor3 + subColor4) * 0.1f
                + (F + G + I + J) * 0.15f
                + H * 0.3f;
            }
            avgColor = avgColor.Clamped();
            return avgColor;
        }

        private LightIntensity RenderPixel(int x, int y, Camera camera, List<IRenderableObject> objects, Func<int, int, LightIntensity> backgroundColor)
        {
            Ray ray = camera.GenerateRay(x, y);
            LightIntensity pixelColor = backgroundColor(x, y);
            float closestT = float.MaxValue;

            foreach (var obj in objects)
            {
                if (obj.Intersect(ray, out float t) && t < closestT)
                {
                    closestT = t;
                    pixelColor = obj.GetColor();
                }
            }

            return pixelColor;
        }

        private float ColorDifference(LightIntensity color1, LightIntensity color2)
        {
            float redDiff = (float)Math.Abs(color1.GetRed() - color2.GetRed());
            float greenDiff = (float)Math.Abs(color1.GetGreen() - color2.GetGreen());
            float blueDiff = (float)Math.Abs(color1.GetBlue() - color2.GetBlue());

            return (redDiff + greenDiff + blueDiff) / 3.0f;
        }
    }
}