
namespace SqrMatrix
{
    public class SqrMatrix
    {
        protected float[,] data;
        private int size;

        public SqrMatrix(int n)
        {
            size = n;
            data = new float[n, n];
        }

        public void SetMatrix(float[,] inputArray)
        {
            if (inputArray.GetLength(0) == size && inputArray.GetLength(1) == size)
            {
                data = inputArray;
            }
            else
            {
            throw new ArgumentException("Input data dimensions do not match the matrix size.");
        }
        }

        public virtual void SetValue(int row, int col, float value)
        {
            if (row >= 0 && row < size && col >= 0 && col < size)
            {
                data[row, col] = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Invalid row or column index");
            }
        }

        public float GetValue(int row, int col)
        {
            if (row >= 0 && row < size && col >= 0 && col < size)
            {
                return data[row, col];
            }
            else
            {
                throw new ArgumentOutOfRangeException("Invalid row or column index");
            }
        }

        public int GetSize()
        {
            return size;
        }

        public void ConsolePrint()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(data[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public string Print()
        {
            string print = "\n";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    print = print + $"{data[i, j]} ";
                }
                print = print + "\n";
            }
            return print;
        }

        public float Cofactor(int row, int col)
        {
            int newSize = size - 1;
            SqrMatrix minorMatrix = new SqrMatrix(newSize);

            for (int i = 0, minorRow = 0; i < size; i++)
            {
                if (i == row) continue;
                for (int j = 0, minorCol = 0; j < size; j++)
                {
                    if (j == col) continue;
                    minorMatrix.SetValue(minorRow, minorCol, GetValue(i, j));
                    minorCol++;
                }
                minorRow++;
            }

            return minorMatrix.Determinant();
        }

        public SqrMatrix GetAdjointMatrix()
        {
            SqrMatrix adjointMatrix = new SqrMatrix(size);
            
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    float cofactorValue = Cofactor(i, j);
                    float sign = ((i + j) % 2 == 0) ? 1.0f : -1.0f;
                    adjointMatrix.SetValue(i, j, sign * cofactorValue);
                }
            }
            return adjointMatrix;
        }

        public float Determinant()
        {
            if (size == 1)
            {
                return GetValue(0, 0);
            }
            if (size == 2)
            {
                return GetValue(0, 0) * GetValue(1, 1) - GetValue(0, 1) * GetValue(1, 0);
            }

            float det = 0.0f;
            for (int j = 0; j < size; j++)
            {
                if (GetValue(0, j) != 0)
                {
                    SqrMatrix minorMatrix = new SqrMatrix(size - 1);
                    float sign = (j % 2 == 0) ? 1.0f : -1.0f;

                    for (int i = 1; i < size; i++)
                    {
                        int col = 0;
                        for (int k = 0; k < size; k++)
                        {
                            if (k == j) continue;
                            minorMatrix.SetValue(i - 1, col, GetValue(i, k));
                            col++;
                        }
                    }

                    det += sign * GetValue(0, j) * minorMatrix.Determinant();
                }
            }
            return det;
        }

        public static SqrMatrix Add(SqrMatrix m1, SqrMatrix m2)
        {
            int n = m1.GetSize();
            SqrMatrix result = new SqrMatrix(n);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    result.SetValue(i, j, m1.GetValue(i, j) + m2.GetValue(i, j));
            return result;
        }

        public static SqrMatrix Subtract(SqrMatrix m1, SqrMatrix m2)
        {
            int n = m1.GetSize();
            SqrMatrix result = new SqrMatrix(n);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    result.SetValue(i, j, m1.GetValue(i, j) - m2.GetValue(i, j));
            return result;
        }

        public static SqrMatrix MultiplyByScalar(SqrMatrix m, float scalar)
        {
            int n = m.GetSize();
            SqrMatrix result = new SqrMatrix(n);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    result.SetValue(i, j, m.GetValue(i, j) * scalar);
            return result;
        }

        public static SqrMatrix Multiply(SqrMatrix m1, SqrMatrix m2)
        {
            int n = m1.GetSize();
            SqrMatrix result = new SqrMatrix(n);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    float sum = 0;
                    for (int k = 0; k < n; k++)
                        sum += m1.GetValue(i, k) * m2.GetValue(k, j);
                    result.SetValue(i, j, sum);
                }
            return result;
        }

        public static SqrMatrix Transpose(SqrMatrix m)
        {
            int n = m.GetSize();
            SqrMatrix result = new SqrMatrix(n);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    result.SetValue(j, i, m.GetValue(i, j));
            return result;
        }

        public static SqrMatrix Invert(SqrMatrix m)
        {
            float determinant = m.Determinant();
            if(determinant == 0)
            {
                 throw new InvalidOperationException("Matrix is not invertible because its determinant is 0.");
            }
            SqrMatrix inverted = MultiplyByScalar(Transpose(m.GetAdjointMatrix()), 1/determinant);
            return inverted;
        }

        public static SqrMatrix Scale(SqrMatrix m, float scalar)
        {
            int n = m.GetSize();
            SqrMatrix scaledMatrix = new SqrMatrix(n);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    scaledMatrix.SetValue(i, j, m.GetValue(i, j) * scalar);
            return scaledMatrix;
        }

        public static SqrMatrix Rotate(int n, float angleInDegrees)
        {
            if (n < 2)
                throw new ArgumentException("Rotation matrix must be at least 2x2");

            float angleInRadians = (float)(angleInDegrees * Math.PI / 180);
            SqrMatrix rotationMatrix = new SqrMatrix(n);
            rotationMatrix.SetValue(0, 0, (float)Math.Cos(angleInRadians));
            rotationMatrix.SetValue(0, 1, (float)-Math.Sin(angleInRadians));
            rotationMatrix.SetValue(1, 0, (float)Math.Sin(angleInRadians));
            rotationMatrix.SetValue(1, 1, (float)Math.Cos(angleInRadians));
            for (int i = 2; i < n; i++)
            {
                rotationMatrix.SetValue(i, i, 1);
            }
            return rotationMatrix;
        }

        public static SqrMatrix Translate(int n, float tx, float ty)
        {
            if (n < 3)
                throw new ArgumentException("Translation matrix must be at least 3x3");

            SqrMatrix translationMatrix = new SqrMatrix(n);
            for (int i = 0; i < n; i++)
            {
                translationMatrix.SetValue(i, i, 1);
            }
            translationMatrix.SetValue(0, n - 1, tx);
            translationMatrix.SetValue(1, n - 1, ty);
            return translationMatrix;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is SqrMatrix other) || size != other.size)
            {
                return false;
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (data[i, j] != other.data[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    public Vector.Vector MultiplyVector(Vector.Vector v)
    {
    if (size != 3)
        throw new InvalidOperationException("Matrix must be 3x3 to multiply by a 3D vector.");

    float newX = data[0, 0] * v.x + data[0, 1] * v.y + data[0, 2] * v.z;
    float newY = data[1, 0] * v.x + data[1, 1] * v.y + data[1, 2] * v.z;
    float newZ = data[2, 0] * v.x + data[2, 1] * v.y + data[2, 2] * v.z;

    return new Vector.Vector(newX, newY, newZ);
    }

    }
    
    
    public class UnitMatrix : SqrMatrix
    {
        public UnitMatrix(int n) : base(n)
        {
            for (int i = 0; i < n; i++)
            {
                data[i, i] = 1;
            }
        }

        public override void SetValue(int row, int col, float value)
        {
            throw new InvalidOperationException("Cannot modify a unit matrix");
        }
    }
}
