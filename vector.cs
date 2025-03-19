using System;

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

        public override string ToString()
        {
            return $"Vector({x}, {y}, {z})";
        }

        public float vectorLength()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        } 

        public Vector normalize()
        {
            float length = this.vectorLength();
            return length == 0 ? new Vector(0, 0, 0) : new Vector(this.x/length, this.y/length, this.z/length); 
        }

        public static Vector add(Vector v1, Vector v2)
        {
            return new Vector(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vector subtract(Vector v1, Vector v2)
        {
            return new Vector(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vector multiplyScalar(Vector v, float scalar)
        {
            return new Vector(v.x * scalar, v.y * scalar, v.z * scalar);
        }

        public static float dotProduct(Vector v1, Vector v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }

        public static Vector crossProduct(Vector v1, Vector v2)
        {
            float x = v1.y * v2.z - v1.z * v2.y;
            float y = v1.z * v2.x - v1.x * v2.z;
            float z = v1.x * v2.y - v1.y * v2.x;
            return new Vector(x, y, z);
        }

        public static float angleBetweenVectors(Vector v1, Vector v2)
        {
            float dot = dotProduct(v1, v2);
            float len1 = v1.vectorLength();
            float len2 = v2.vectorLength();
            float angleInRadians = (float)Math.Acos(dot/(len1 * len2));
            float angleInDegrees = (angleInRadians * 180) / (float)Math.PI;
            return angleInDegrees;
        } 
    }
}
