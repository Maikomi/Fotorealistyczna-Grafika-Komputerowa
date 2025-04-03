using System.Numerics;
using RayTracing;

namespace Vector
{
    abstract class LightSource
    {
        public abstract Vector Illuminate(Vector point, Vector normal, Vector viewDir, Material material, List<IRenderableObject> objects);
    }
}