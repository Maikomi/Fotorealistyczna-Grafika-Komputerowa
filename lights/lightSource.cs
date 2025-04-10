using System.Numerics;
using Lighting;
using RayTracing;
using Vector;
using Vec = Vector.Vector;

namespace RayTracing
{
    public abstract class LightSource
    {
        public abstract Vec Position { get; }
        public abstract float Intensity { get; }
        public abstract LightIntensity Illuminate(Vec point, Vec normal, Vec viewDir, Material material, List<IRenderableObject> objects,  IRenderableObject currentObject);
    }
}