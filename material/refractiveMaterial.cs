using Vector;
using Vec = Vector.Vector;
using Lighting;

namespace RayTracing
{
    public class RefractiveMaterial : Material
    {
        public float RefractionIndex { get; }

        public RefractiveMaterial(Vec color, float refractionIndex) : base(color)
        {
            RefractionIndex = refractionIndex;
        }

        public override bool Scatter(Ray rayIn, Vec hitPoint, Vec normal, out Ray scattered, out LightIntensity attenuation)
        {
            attenuation = new LightIntensity(Color.x, Color.y, Color.z);
            float etaiOverEtat = Vec.DotProduct(rayIn.Direction, normal) < 0 ? 1.0f / RefractionIndex : RefractionIndex;

            Vec unitDirection = rayIn.Direction.Normalize();
            float cosTheta = MathF.Min(Vec.DotProduct(-unitDirection, normal), 1.0f);
            float sinTheta = MathF.Sqrt(1.0f - cosTheta * cosTheta);

            bool cannotRefract = etaiOverEtat * sinTheta > 1.0f;

            Vec direction;

            if (cannotRefract || Reflectance(cosTheta, etaiOverEtat) > Random.Shared.NextSingle())
            {
                direction = Vec.Reflect(unitDirection, normal);
            }
            else
            {
                direction = Vec.Refract(unitDirection, normal, etaiOverEtat);
            }

            scattered = new Ray(hitPoint + direction * 0.001f, direction);
            return true;
        }

        private float Reflectance(float cosine, float refIdx)
        {
            // Schlick approximation
            float r0 = (1 - refIdx) / (1 + refIdx);
            r0 = r0 * r0;
            return r0 + (1 - r0) * MathF.Pow(1 - cosine, 5);
        }
    }
}
