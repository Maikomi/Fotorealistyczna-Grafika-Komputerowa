
namespace Vector
{
    public class Vector
    {
        public float x;
        public float y;
        public float z;

        public Vector(float x = 0, float y = 0, float z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector operator -(Vector v)
        {
            return new Vector(-v.x, -v.y, -v.z);
        }

        // Mnożenie natężenia przez skalar
        public static Vector operator *(Vector a, float scalar)
        {
            return new Vector(a.x * scalar, a.y * scalar, a.z * scalar);
        }

        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(a.x * b.x, a.y * b.y, a.z * b.z);
        }


        public static Vector Reflect(Vector vector, Vector normal)
        {
            return vector - normal * Vector.DotProduct(vector, normal) * 2;
        }

        public override string ToString()
        {
            return $"Vector({x}, {y}, {z})";
        }

        public float VectorLength()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        public Vector Normalize()
        {
            float length = this.VectorLength();
            return length == 0 ? new Vector(0, 0, 0) : new Vector(this.x / length, this.y / length, this.z / length);
        }

        public static Vector Add(Vector v1, Vector v2)
        {
            return new Vector(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vector Subtract(Vector v1, Vector v2)
        {
            return new Vector(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vector MultiplyScalar(Vector v, float scalar)
        {
            return new Vector(v.x * scalar, v.y * scalar, v.z * scalar);
        }

        public static float DotProduct(Vector v1, Vector v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }

        public static Vector CrossProduct(Vector v1, Vector v2)
        {
            float x = v1.y * v2.z - v1.z * v2.y;
            float y = v1.z * v2.x - v1.x * v2.z;
            float z = v1.x * v2.y - v1.y * v2.x;
            return new Vector(x, y, z);
        }

        public static float AngleBetweenVectors(Vector v1, Vector v2)
        {
            float dot = DotProduct(v1, v2);
            float len1 = v1.VectorLength();
            float len2 = v2.VectorLength();
            float angleInRadians = (float)Math.Acos(dot / (len1 * len2));
            float angleInDegrees = angleInRadians * 180 / (float)Math.PI;
            return angleInDegrees;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static float LengthSquared(Vector v)
        {
            return v.x * v.x + v.y * v.y + v.z * v.z;
        }

        public static Vector Refract(Vector uv, Vector n, float etaiOverEtat)
        {
            float cosTheta = MathF.Min(DotProduct(-uv, n), 1.0f);
            Vector rOutPerp = (uv + n * cosTheta) * etaiOverEtat;
            Vector rOutParallel = n * -MathF.Sqrt(MathF.Abs(1.0f - LengthSquared(rOutPerp)));
            return rOutPerp + rOutParallel;
        }

        private static Random rand = new Random();

        public static Vector RandomUnitVector()
        {
            float a = (float)(rand.NextDouble() * 2 * Math.PI);
            float z = (float)(rand.NextDouble() * 2 - 1);
            float r = MathF.Sqrt(1 - z * z);
            return new Vector(r * MathF.Cos(a), r * MathF.Sin(a), z);
        }

    }
}
