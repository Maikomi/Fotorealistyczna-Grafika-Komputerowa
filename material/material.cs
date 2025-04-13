using Vector;
using Vec = Vector.Vector;
using Lighting;

namespace RayTracing
{
    public enum MaterialType
    {
        Diffuse,
        Reflective,
        Refractive
    }

    public class Material
    {
        public Vec Color { get; }
        public float Ambient { get; }
        public float Diffuse { get; }
        public float Specular { get; }
        public float Shininess { get; }

        public Material(Vec color, float ambient = 0.1f, float diffuse = 0.7f, float specular = 0.2f, float shininess = 10f)
        {
            Color = color;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininess;
        }

        // Domyślnie materiał nie rozprasza promienia – nadpisują to klasy pochodne
        public virtual bool Scatter(Ray rayIn, Vec hitPoint, Vec normal, out Ray scattered, out LightIntensity attenuation)
        {
            scattered = null!;
            attenuation = new LightIntensity(0, 0, 0);
            return false;
        }
    }
} 