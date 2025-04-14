using System.Numerics;
using RayTracing;
using Vector;
using Vec = Vector.Vector;

namespace Lighting
{
    class SurfaceLight : LightSource
    {
        public override Vec Position { get; }
        public override float Intensity { get; }
        public Vec Normal { get; }
        public Vec U { get; }
        public Vec V { get; }
        public float Width { get; }
        public float Height { get; }

        public LightIntensity Color { get; set; }

        private static readonly Random rand = new Random();

        public SurfaceLight(Vec position, Vec normal, Vec u, float width, float height, float intensity = 1.0f, LightIntensity? color = null)
        {
            Position = position;
            Normal = normal.Normalize();
            U = u.Normalize();
            V = Vec.CrossProduct(Normal, U).Normalize();
            Width = width;
            Height = height;
            Intensity = intensity;
            Color = color ?? new LightIntensity(1, 1, 1);
        }
        public override LightIntensity Illuminate(Vec point, Vec normal, Vec viewDir, Material material, List<IRenderableObject> objects, IRenderableObject currentObject)
    {
        int samples = 128;
        LightIntensity accumulated = new LightIntensity(0, 0, 0);
        Vec ambient = material.Color * material.Ambient;

        for (int i = 0; i < samples; i++)
        {
            Vec lightPoint = SamplePointOnSurface();
            Vec lightDir = (lightPoint - point);
            float lightDistance = lightDir.VectorLength();
            lightDir = lightDir.Normalize();

            Vec shadowRayOrigin = point + normal * 0.001f;
            Ray shadowRay = new Ray(shadowRayOrigin, lightDir);
            bool inShadow = false;

            foreach (var obj in objects)
            {
                if (obj == currentObject) continue;
                if (obj.Intersect(shadowRay, out float t) && t > 0.001f && t < lightDistance)
                {
                    inShadow = true;
                    break;
                }
            }

            if (!inShadow)
            {
                Vec effectiveColor = this.Color * this.Intensity;

                float diff = MathF.Max(Vec.DotProduct(normal, lightDir), 0);
                Vec diffuse = effectiveColor * material.Color * material.Diffuse * diff;

                Vec reflectDir = Vec.Reflect(-lightDir, normal);
                float spec = MathF.Pow(MathF.Max(Vec.DotProduct(viewDir, reflectDir), 0), material.Shininess);
                Vec specular = effectiveColor * material.Specular * spec;

                accumulated += new LightIntensity(diffuse.x + specular.x, diffuse.y + specular.y, diffuse.z + specular.z);
            }
        }

        // Dodajemy ambient tylko raz
        accumulated += new LightIntensity(ambient.x, ambient.y, ambient.z) * samples;

        return (accumulated / samples).Clamped();
    }

        private Vec SamplePointOnSurface()
        {
            float uOffset = ((float)rand.NextDouble() - 0.5f) * Width;
            float vOffset = ((float)rand.NextDouble() - 0.5f) * Height;
            return Position + U * uOffset + V * vOffset;
        }
    }
}