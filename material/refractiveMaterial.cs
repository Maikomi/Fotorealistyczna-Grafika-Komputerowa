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
            bool frontFace = Vec.DotProduct(rayIn.Direction, normal) < 0.0f;

            Vec norm = frontFace ? normal : -normal;

            float etaiOverEtat = frontFace ? 1.0f / RefractionIndex : RefractionIndex;

            Vec unitDirection = rayIn.Direction.Normalize();
            float cosTheta = MathF.Min(Vec.DotProduct(-unitDirection, norm), 1.0f);
            float sinTheta = MathF.Sqrt(1.0f - cosTheta * cosTheta);

            bool cannotRefract = etaiOverEtat * sinTheta > 1.0f || Reflectance(cosTheta, etaiOverEtat) > 1.0f;

            Vec direction;

            if (cannotRefract)
            {
                direction = Vec.Reflect(unitDirection, norm);
            }
            else
            {
                direction = Vec.Refract(unitDirection, norm, etaiOverEtat);
            }

            scattered = new Ray(hitPoint + direction * 0.001f, direction);
            return true;
        }

        private float Reflectance(float cosine, float refIdx)
        {
            // Schlick approximation
            float r0 = (1.0f - refIdx) / (1.0f + refIdx);
            r0 = r0 * r0;
            return r0 + (1.0f - r0) * MathF.Pow(1.0f - cosine, 5.0f);
        }
    }
}
