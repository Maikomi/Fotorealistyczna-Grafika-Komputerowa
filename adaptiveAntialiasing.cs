using System;
using System.Drawing;
using Lighting;
using Vector;
using Vec = Vector.Vector;

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

            Vec A = RenderPixel(x, y, camera, objects, backgroundColor);   
            Vec B = RenderPixel(x + 1, y, camera, objects, backgroundColor);
            Vec C = RenderPixel(x, y + 1, camera, objects, backgroundColor); 
            Vec D = RenderPixel(x + 1, y + 1, camera, objects, backgroundColor);

            Vec avgColor = (A + B + C + D) * 0.25f;

            if (depth > 0 && (ColorDifference(A, B) > threshold || ColorDifference(B, C) > threshold ||
                              ColorDifference(C, D) > threshold || ColorDifference(D, A) > threshold))
            {
                Vec F = RenderPixel((int)midX, y, camera, objects, backgroundColor);     
                Vec G = RenderPixel(x, (int)midY, camera, objects, backgroundColor);     
                Vec H = RenderPixel((int)midX, (int)midY, camera, objects, backgroundColor);
                Vec I = RenderPixel(x, (int)midY, camera, objects, backgroundColor);
                Vec J = RenderPixel((int)midX, y + 1, camera, objects, backgroundColor);

                Vec subColor1 = QuincunxSampleRecursive(x, y, camera, objects, backgroundColor, depth - 1);
                Vec subColor2 = QuincunxSampleRecursive(x + 1, y, camera, objects, backgroundColor, depth - 1);
                Vec subColor3 = QuincunxSampleRecursive(x, y + 1, camera, objects, backgroundColor, depth - 1);
                Vec subColor4 = QuincunxSampleRecursive(x + 1, y + 1, camera, objects, backgroundColor, depth - 1);

                avgColor = (subColor1 + subColor2 + subColor3 + subColor4) * 0.1f
                + (F + G + I + J) * 0.15f
                + H * 0.3f;
            }
            return new LightIntensity(avgColor.x, avgColor.y, avgColor.z);
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

        private float ColorDifference(Vec color1, Vec color2)
        {
            float redDiff = (float)Math.Abs(color1.x - color2.x);
            float greenDiff = (float)Math.Abs(color1.y - color2.y);
            float blueDiff = (float)Math.Abs(color1.z - color2.z);

            return (redDiff + greenDiff + blueDiff) / 3.0f;
        }
    }
}