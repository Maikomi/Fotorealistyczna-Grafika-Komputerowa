using System;

namespace Vector
{
    public class Vector
    {
        public float x;
        public float y;
        public float z;
        public float w;

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
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        } 

        public Vector normalize()
        {
            float lenght = this.vectorLength();
            return length == 0 ? new Vector(this.x/lenght, this.y/lenght, this.z/lenght); 
        }
    }
}
