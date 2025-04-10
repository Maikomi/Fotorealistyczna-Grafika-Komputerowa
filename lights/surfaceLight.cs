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
            int samples = 16; // You can tweak this for more/less quality
            LightIntensity accumulated = new LightIntensity(0, 0, 0);

            for (int i = 0; i < samples; i++)
            {
                Vec lightPoint = SamplePointOnSurface();
                Vec lightDir = (lightPoint - point).Normalize();

                Vec shadowRayOrigin = point + normal * 0.001f;
                bool inShadow = false;

                foreach (var obj in objects)
                {
                    if (obj != currentObject && obj.Intersect(new Ray(shadowRayOrigin, lightDir), out _))
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

                    Vec result = material.Color * material.Ambient + diffuse + specular;
                    accumulated += new LightIntensity(result.x, result.y, result.z);
                }
                else
                {
                    // Still apply ambient if in shadow
                    Vec result = material.Color * material.Ambient;
                    accumulated += new LightIntensity(result.x, result.y, result.z);
                }
            }

            return (accumulated / samples).Clamped();
        }

        private Vec SamplePointOnSurface()
        {
            Random rand = new Random();
            float uOffset = ((float)rand.NextDouble() - 0.5f) * Width;
            float vOffset = ((float)rand.NextDouble() - 0.5f) * Height;
            return Position + U * uOffset + V * vOffset;
        }
    }
}