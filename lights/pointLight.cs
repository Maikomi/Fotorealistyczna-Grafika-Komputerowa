using System.Numerics;
using Lighting;
using RayTracing;

namespace Vector
{
    class PointLight : LightSource
    {
        public override Vector Position { get; }
        public override float Intensity { get; }

        public LightIntensity Color {get; set; }

        public PointLight(Vector position, float intensity = 1.0f, LightIntensity? color = null)
        {
            Position = position;
            Intensity = intensity;
            Color = color ?? new LightIntensity(1, 1, 1);
        }
        public override Vector Illuminate(Vector point, Vector normal, Vector viewDir, Material material, List<IRenderableObject> objects)
        {
            Vector lightDir = Position - point;
            lightDir.Normalize();

            // Shadow check
            Vector shadowRayOrigin = point + normal * 0.001f;
            foreach (var obj in objects)
            {
                if (obj.Intersect(new Ray(shadowRayOrigin, lightDir), out _))
                    return material.Color * material.Ambient;
            }

            // Diffuse shading
            float diff = MathF.Max(Vector.DotProduct(normal, lightDir), 0);
            Vector diffuse = material.Color * material.Diffuse * diff;

            // Specular shading
            Vector reflectDir = Vector.Reflect(-lightDir, normal);
            float spec = MathF.Pow(MathF.Max(Vector.DotProduct(viewDir, reflectDir), 0), material.Shininess);
            Vector specular = new Vector(1, 1, 1) * material.Specular * spec;

            return material.Color * material.Ambient + diffuse + specular;
        }
    }
}