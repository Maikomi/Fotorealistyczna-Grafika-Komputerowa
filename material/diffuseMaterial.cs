using Vector;
using Vec = Vector.Vector;
using Lighting;

namespace RayTracing
{
    public class DiffuseMaterial : Material
    {
        public DiffuseMaterial(Vec color) : base(color) { }

        public override bool Scatter(Ray rayIn, Vec hitPoint, Vec normal, out Ray scattered, out LightIntensity attenuation)
        {
            Vec target = hitPoint + normal + Vec.RandomUnitVector();
            scattered = new Ray(hitPoint + normal * 0.001f, (target - hitPoint).Normalize());
            attenuation = new LightIntensity(Color.x, Color.y, Color.z);
            return true;
        }
    }
}
