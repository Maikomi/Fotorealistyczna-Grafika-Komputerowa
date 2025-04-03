using Vector;
using Vec = Vector.Vector;

namespace RayTracing
{
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
    }
} 