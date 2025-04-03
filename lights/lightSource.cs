using System.Numerics;
using RayTracing;
using Vector;
using Vec = Vector.Vector;

namespace RayTracing
{
    public abstract class LightSource
    {
        public abstract Vec Position { get; }
        public abstract float Intensity { get; }
        public abstract Vec Illuminate(Vec point, Vec normal, Vec viewDir, Material material, List<IRenderableObject> objects);
    }
}