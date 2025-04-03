using System.Numerics;
using RayTracing;
using Vector;
using Vec = Vector.Vector;

namespace RayTracing
{
    abstract class LightSource
    {
        public abstract Vec Illuminate(Vec point, Vec normal, Vec viewDir, Material material, List<IRenderableObject> objects);
    }
}