using Vector;
using Vec = Vector.Vector;
using Lighting;

namespace RayTracing
{
    public class ReflectiveMaterial : Material
    {
        public ReflectiveMaterial(Vec color) : base(color) { }

        public override bool Scatter(Ray rayIn, Vec hitPoint, Vec normal, out Ray scattered, out LightIntensity attenuation)
        {
            Vec reflected = Vec.Reflect(rayIn.Direction.Normalize(), normal);
            scattered = new Ray(hitPoint + normal * 0.001f, reflected);
            attenuation = new LightIntensity(Color.x, Color.y, Color.z);
            return true;
        }
    }
}
