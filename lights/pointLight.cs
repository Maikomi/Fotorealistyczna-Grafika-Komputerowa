
using RayTracing;
using Vector;
using Vec = Vector.Vector;

namespace Lighting
{
    class PointLight : LightSource
    {
        public override Vec Position { get; }
        public override float Intensity { get; }

        public LightIntensity Color { get; set; }

        public PointLight(Vec position, float intensity = 1.0f, LightIntensity? color = null)
        {
            Position = position;
            Intensity = intensity;
            Color = color ?? new LightIntensity(1, 1, 1);
        }
        public override Vec Illuminate(Vec point, Vec normal, Vec viewDir, Material material, List<IRenderableObject> objects)
        {
            Vec lightDir = Position - point;
            lightDir.Normalize();

            // Shadow check
            Vec shadowRayOrigin = point + normal * 0.001f;
            foreach (var obj in objects)
            {
                if (obj.Intersect(new Ray(shadowRayOrigin, lightDir), out _))
                    return material.Color * material.Ambient;
            }

            Vec effectiveColor = this.Color * this.Intensity;

            // Diffuse shading
            float diff = MathF.Max(Vec.DotProduct(normal, lightDir), 0);
            Vec diffuse = effectiveColor * material.Color * material.Diffuse * diff;

            // Specular shading
            Vec reflectDir = Vec.Reflect(-lightDir, normal);
            float spec = MathF.Pow(MathF.Max(Vec.DotProduct(viewDir, reflectDir), 0), material.Shininess);
            Vec specular = effectiveColor * material.Specular * spec;

            return material.Color * material.Ambient + diffuse + specular;
        }
    }
}