using System;

namespace Vector
{
    public class Quaternion
    {
        public float W { get; }
        public Vector V { get; } // Wektorowa część kwaternionu

        public Quaternion(float w, float x, float y, float z)
        {
            W = w;
            V = new Vector(x, y, z);
        }

        public float Get(int index)
        {
            return index switch
            {
                0 => W,
                1 => V.x,
                2 => V.y,
                3 => V.z,
                _ => throw new IndexOutOfRangeException("Index must be between 0 and 3.")
            };
        }

        public override string ToString()
        {
            return $"{W} + {V.x}i + {V.y}j + {V.z}k";
        }

        public Quaternion Multiply(Quaternion other)
        {
            float newW = W * other.W - Vector.DotProduct(V, other.V);
            Vector newV = Vector.Add(
                Vector.Add(Vector.MultiplyScalar(other.V, W), Vector.MultiplyScalar(V, other.W)),
                Vector.CrossProduct(V, other.V)
            );

            return new Quaternion(newW, newV.x, newV.y, newV.z);
        }

        public static Quaternion Add(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.W + q2.W, q1.V.x + q2.V.x, q1.V.y + q2.V.y, q1.V.z + q2.V.z);
        }

        public static Quaternion Subtract(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.W - q2.W, q1.V.x - q2.V.x, q1.V.y - q2.V.y, q1.V.z - q2.V.z);
        }

        public Quaternion MatrixMultiply(Quaternion other)
        {
            float scalar = W * other.W - Vector.DotProduct(V, other.V);
            Vector vectorPart = Vector.Add(
                Vector.Add(Vector.MultiplyScalar(other.V, W), Vector.MultiplyScalar(V, other.W)),
                Vector.CrossProduct(V, other.V)
            );

            return new Quaternion(scalar, vectorPart.x, vectorPart.y, vectorPart.z);
        }

        public Quaternion Divide(Quaternion other)
        {
            float denominator = other.W * other.W + Vector.DotProduct(other.V, other.V);
            if (denominator == 0)
                throw new DivideByZeroException("Quaternion division is not defined when denominator is zero.");

            Vector cross = Vector.CrossProduct(V, other.V);
            Vector newV = Vector.Subtract(
                Vector.Add(Vector.MultiplyScalar(V, -other.W), Vector.MultiplyScalar(other.V, W)),
                Vector.MultiplyScalar(cross, 1 / denominator)
            );

            return new Quaternion((W * other.W + Vector.DotProduct(V, other.V)) / denominator, newV.x, newV.y, newV.z);
        }

        public static Vector Rotate(float angle, Vector axis, Vector point)
        {
            float halfAngle = angle / 2;
            float sinHalfAngle = (float)Math.Sin(halfAngle);
            float cosHalfAngle = (float)Math.Cos(halfAngle);

            Vector normalizedAxis = axis.Normalize();
            Vector ijk = Vector.MultiplyScalar(normalizedAxis, sinHalfAngle);
            Quaternion q = new Quaternion(cosHalfAngle, ijk.x, ijk.y, ijk.z);
            Quaternion qInverse = new Quaternion(cosHalfAngle, -ijk.x, -ijk.y, -ijk.z);

            Quaternion rotatedQuaternion = q.Multiply(new Quaternion(0, point.x, point.y, point.z)).Multiply(qInverse);
            return new Vector(rotatedQuaternion.V.x, rotatedQuaternion.V.y, rotatedQuaternion.V.z);
        }

        public bool Equals(Quaternion other)
        {
            return W == other.W && V == other.V;
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(W * W + V.x * V.x + V.y * V.y + V.z * V.z);
        }

        public Quaternion Inverse()
        {
            float magnitudeSquared = W * W + V.x * V.x + V.y * V.y + V.z * V.z;
            return new Quaternion(W / magnitudeSquared, -V.x / magnitudeSquared, -V.y / magnitudeSquared, -V.z / magnitudeSquared);
        }

        public Vector ToEulerAngles()
        {
            float roll = (float)Math.Atan2(2 * (W * V.x + V.y * V.z), 1 - 2 * (V.x * V.x + V.y * V.y)) * 180 / (float)Math.PI;
            float pitch = (float)Math.Asin(2 * (W * V.y - V.z * V.x)) * 180 / (float)Math.PI;
            float yaw = (float)Math.Atan2(2 * (W * V.z + V.x * V.y), 1 - 2 * (V.y * V.y + V.z * V.z)) * 180 / (float)Math.PI;

            return new Vector(roll, pitch, yaw);
        }

        // Iloczyn skalarny dwóch kwaternionów 
        public static float DotProduct(Quaternion q1, Quaternion q2)
        {
            return Vector.DotProduct(q1.V, q2.V) + q1.W * q2.W;
        }

        // Iloczyn kwaterionowy (iloczyn Hamiltona)
        public static Quaternion CrossProduct(Quaternion q1, Quaternion q2)
        {
            float newW = q1.W * q2.W - Vector.DotProduct(q1.V, q2.V);
            Vector newV = Vector.Add(
                Vector.Add(Vector.MultiplyScalar(q2.V, q1.W), Vector.MultiplyScalar(q1.V, q2.W)),
                Vector.CrossProduct(q1.V, q2.V)
            );

            return new Quaternion(newW, newV.x, newV.y, newV.z);
        }

    }
}
