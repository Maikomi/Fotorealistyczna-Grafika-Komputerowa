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

        public Quaternion Add(Quaternion other)
        {
            return new Quaternion(W + other.W, V.x + other.V.x, V.y + other.V.y, V.z + other.V.z);
        }

        public Quaternion Subtract(Quaternion other)
        {
            return new Quaternion(W - other.W, V.x - other.V.x, V.y - other.V.y, V.z - other.V.z);
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
    }
}
