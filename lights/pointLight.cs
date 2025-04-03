using System.Numerics;
using RayTracing;

namespace Vector
{
    class PointLight : LightSource
    {
        public Vector Position { get; }
        public float Intensity { get; }

        public PointLight(Vector position, float intensity = 1.0f)
        {
            Position = position;
            Intensity = intensity;
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
            Vector diffuse = material.Color * material.Diffuse *  diff;

            // Specular shading
            Vector reflectDir = Vector.Reflect(-lightDir, normal);
            float spec = MathF.Pow(MathF.Max(Vector.DotProduct(viewDir, reflectDir), 0), material.Shininess);
            Vector specular = new Vector(1, 1, 1) * material.Specular * spec;

            return material.Color * material.Ambient + diffuse + specular;
        }
    }
}