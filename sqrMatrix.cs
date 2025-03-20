using System;

namespace SqrMatrix
{
    public class SqrMatrix
    {
        private float[,] data;
        private int size;

        public SqrMatrix(int n)
        {
            size = n;
            data = new float[n, n];
        }

        public void SetValue(int row, int col, float value)
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

        public void Print()
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
    }
}
