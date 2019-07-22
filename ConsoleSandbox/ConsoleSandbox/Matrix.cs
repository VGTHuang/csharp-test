using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSandbox
{
    class Matrix
    {
        #region test
        public static void Test(int size)
        {
            Matrix m = new Matrix(size, size);
            Random rand = new Random();
            for(int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    m.setValue(i, j, rand.NextDouble()*2-1);
                }
            }
            //m.Print();
            //Console.WriteLine(m.getDetValue());
            Matrix inv = m.Inverse();
            //inv.Print();
            //Console.WriteLine(m.getDetValue());
            //Console.WriteLine(inv.getDetValue());

            inv.Multiply(m);
        }
        #endregion


        public int RowCount { get; }
        public int ColCount { get; }

        public double[,] data;

        public Matrix(int rowCount, int colCount)
        {
            this.RowCount = rowCount;
            this.ColCount = colCount;
            this.data = new double[RowCount, ColCount];
        }

        public Matrix Clone()
        {
            Matrix newMatrix = new Matrix(this.RowCount, this.ColCount);
            for (int rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < this.ColCount; colIndex++)
                {
                    newMatrix.data[rowIndex, colIndex] = this.data[rowIndex, colIndex];
                }
            }
            return newMatrix;
        }

        public Matrix GetSubMatrix(int fromRowIndex, int fromColIndex, int toRowIndex, int toColIndex)
        {
            if(fromRowIndex < 0 || fromRowIndex > this.RowCount - 1 ||
               fromColIndex < 0 || fromColIndex > this.ColCount - 1 ||
               toRowIndex < 0 || toRowIndex > this.RowCount - 1 ||
               toColIndex < 0 || toColIndex > this.ColCount - 1 || 
               fromRowIndex > toRowIndex || fromColIndex > toColIndex)
            {
                throw new MatrixValueOutOfRangeException();
            }
            Matrix newMatrix = new Matrix(toRowIndex - fromRowIndex + 1, toColIndex - fromColIndex + 1);
            for (int rowIndex = 0; rowIndex < newMatrix.RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < newMatrix.ColCount; colIndex++)
                {
                    newMatrix.data[rowIndex, colIndex] = this.data[rowIndex + fromRowIndex, colIndex + fromColIndex];
                }
            }
            return newMatrix;
        }

        public void setValue(int rowIndex, int colIndex, double value)
        {
            if(rowIndex > this.RowCount - 1 || colIndex > this.ColCount - 1)
            {
                throw new MatrixValueOutOfRangeException();
            }
            this.data[rowIndex, colIndex] = value;
        }

        public void Print()
        {
            for (int rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < this.ColCount - 1; colIndex++)
                {
                    Console.Write(string.Format("{0:0.0000000} ", this.data[rowIndex, colIndex]));
                }
                Console.WriteLine(string.Format("{0:0.0000000}", this.data[rowIndex, this.ColCount - 1]));
            }
            Console.WriteLine();
        }

        public void ResetAll(double resetValue)
        {
            for (int rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < this.ColCount - 1; colIndex++)
                {
                    this.data[rowIndex, colIndex] = resetValue;
                }
            }
        }

        public bool IsSquare()
        {
            return this.ColCount == this.RowCount;
        }

        public bool isInvertible()
        {
            return this.getDetValue() != 0;
        }

        public double getDetValue()
        {
            if (!this.IsSquare())
            {
                throw new MatrixMiscException("Not square matrix");
            }
            Matrix newMatrix = this.Clone();
            newMatrix.ToEchelon();
            double product = 1;
            for(int i = newMatrix.ColCount - 1; i >= 0; i--)
            {
                product *= newMatrix.data[i, i];
            }
            return product;
        }

        public void ToEchelon()
        {
            if (!this.IsSquare())
            {
                throw new MatrixMiscException("Not square matrix");
            }
            for(int colIndex = 0; colIndex < this.ColCount - 1; colIndex++)
            {
                for(int rowIndex = this.RowCount - 1; rowIndex > colIndex; rowIndex--)
                {
                    if (this.data[rowIndex, colIndex] != 0)
                    {
                        for (int i = 1; rowIndex - i >= colIndex; i++)
                        {
                            if (this.data[rowIndex - i, colIndex] != 0)
                            {
                                double ratio = this.data[rowIndex, colIndex] / this.data[rowIndex - i, colIndex];
                                this.AddRowRatio(rowIndex - i, rowIndex, -ratio);
                                this.setValue(rowIndex, colIndex, 0);
                                break;
                            }
                        }
                    }
                }
                if(this.data[colIndex, colIndex] == 0)
                {
                    for(int i = colIndex + 1; i < this.ColCount; i++)
                    {
                        if(this.data[i, colIndex] != 0)
                        {
                            this.SwapRow(i, colIndex);
                            break;
                        }
                    }
                }
            }
        }

        public int getRank()
        {
            if (!this.IsSquare())
            {
                throw new MatrixMiscException("Not square matrix");
            }
            Matrix clone = this.Clone();
            clone.ToEchelon();
            int rank = this.RowCount;
            for(int i = this.RowCount - 1; i >= 0; i--)
            {
                if(clone.data[i, i] == 0)
                {
                    rank--;
                }
            }
            return rank;
        }

        public Matrix Inverse()
        {
            if (!this.IsSquare())
            {
                throw new MatrixMiscException("Not square matrix");
            }
            UnitMatrix m = new UnitMatrix(this.RowCount);
            Matrix clone = this.Clone();
            for (int colIndex = 0; colIndex < clone.ColCount - 1; colIndex++)
            {
                for (int rowIndex = clone.RowCount - 1; rowIndex > colIndex; rowIndex--)
                {
                    if (clone.data[rowIndex, colIndex] != 0)
                    {
                        for (int i = 1; rowIndex - i >= colIndex; i++)
                        {
                            if (clone.data[rowIndex - i, colIndex] != 0)
                            {
                                double ratio = clone.data[rowIndex, colIndex] / clone.data[rowIndex - i, colIndex];
                                clone.AddRowRatio(rowIndex - i, rowIndex, -ratio);
                                clone.setValue(rowIndex, colIndex, 0);
                                m.AddRowRatio(rowIndex - i, rowIndex, -ratio);
                                break;
                            }
                        }
                    }
                }
                if (clone.data[colIndex, colIndex] == 0)
                {
                    for (int i = colIndex + 1; i < clone.ColCount; i++)
                    {
                        if (clone.data[i, colIndex] != 0)
                        {
                            clone.SwapRow(i, colIndex);
                            m.SwapRow(i, colIndex);
                            break;
                        }
                    }
                }
            }
            for (int colIndex = clone.ColCount - 1; colIndex > 0; colIndex--)
            {
                for (int rowIndex = colIndex - 1; rowIndex >= 0; rowIndex--)
                {
                    if (clone.data[rowIndex, colIndex] != 0)
                    {
                        if (clone.data[colIndex, colIndex] == 0)
                        {
                            throw new MatrixMiscException("not invertible");
                        }
                        double ratio = clone.data[rowIndex, colIndex] / clone.data[colIndex, colIndex];
                        clone.AddRowRatio(colIndex, rowIndex, -ratio);
                        clone.setValue(rowIndex, colIndex, 0);
                        m.AddRowRatio(colIndex, rowIndex, -ratio);
                    }
                }
            }
            for (int rowIndex = clone.RowCount - 1; rowIndex >= 0; rowIndex--)
            {
                double rat = 1 / clone.data[rowIndex, rowIndex];
                m.MultiplyRow(rowIndex, rat);
            }
            return m;
        }

        public void Transpose()
        {
            if (!this.IsSquare())
            {
                throw new MatrixMiscException("Not square matrix");
            }
            double temp = 0;
            for (int rowIndex = 1; rowIndex < this.RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < rowIndex; colIndex++)
                {
                    temp = data[colIndex, rowIndex];
                    data[colIndex, rowIndex] = data[rowIndex, colIndex];
                    data[rowIndex, colIndex] = data[colIndex, rowIndex];
                }
            }
        }

        public Matrix TransposeNew()
        {
            Matrix newMatrix = new Matrix(this.ColCount, this.RowCount);
            for (int rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < this.ColCount; colIndex++)
                {
                    newMatrix.data[colIndex, rowIndex] += this.data[rowIndex, colIndex];
                }
            }
            return newMatrix;
        }

        public void Operate(Func<double, double> operation)
        {
            for (int rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < this.ColCount; colIndex++)
                {
                    this.data[rowIndex, colIndex] = operation(this.data[rowIndex, colIndex]);
                }
            }
        }


        #region arithmetic -------------------------------------------------------------------------------------
        public void Add(Matrix other)
        {
            if(this.RowCount != other.RowCount || this.ColCount != other.ColCount)
            {
                throw new MatrixAddException();
            }
            for(int rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < this.ColCount; colIndex++)
                {
                    this.data[rowIndex, colIndex] += other.data[rowIndex, colIndex];
                }
            }
        }

        public Matrix AddNew(Matrix other)
        {
            if (this.RowCount != other.RowCount || this.ColCount != other.ColCount)
            {
                throw new MatrixAddException();
            }
            Matrix newMatrix = new Matrix(this.RowCount, this.ColCount);
            for (int rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < this.ColCount; colIndex++)
                {
                    newMatrix.data[rowIndex, colIndex] = this.data[rowIndex, colIndex] + other.data[rowIndex, colIndex];
                }
            }
            return newMatrix;
        }

        public void Subtract(Matrix other)
        {
            if (this.RowCount != other.RowCount || this.ColCount != other.ColCount)
            {
                throw new MatrixSubtractException();
            }
            for (int rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < this.ColCount; colIndex++)
                {
                    this.data[rowIndex, colIndex] += other.data[rowIndex, colIndex];
                }
            }
        }

        public Matrix SubtractNew(Matrix other)
        {
            if (this.RowCount != other.RowCount || this.ColCount != other.ColCount)
            {
                throw new MatrixSubtractException();
            }
            Matrix newMatrix = new Matrix(this.RowCount, this.ColCount);
            for (int rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < this.ColCount; colIndex++)
                {
                    newMatrix.data[rowIndex, colIndex] = this.data[rowIndex, colIndex] - other.data[rowIndex, colIndex];
                }
            }
            return newMatrix;
        }

        public Matrix Multiply(Matrix other)
        {
            if (this.ColCount != other.RowCount)
            {
                throw new MatrixMultiplyException();
            }
            Matrix newMatrix = new Matrix(this.RowCount, other.ColCount);
            for (int rowIndex = 0; rowIndex < newMatrix.RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < newMatrix.ColCount; colIndex++)
                {
                    double sum = 0;
                    for(int count = 0; count < this.ColCount; count++)
                    {
                        sum += this.data[rowIndex, count] * other.data[count, colIndex];
                    }
                    newMatrix.data[rowIndex, colIndex] = sum;
                }
            }
            return newMatrix;
        }

        public void Multiply(double multiplier)
        {
            for (int rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < this.ColCount; colIndex++)
                {

                    this.data[rowIndex, colIndex] = this.data[rowIndex, colIndex] * multiplier;
                }
            }
        }

        public Matrix MultiplyNew(double multiplier)
        {
            Matrix newMatrix = new Matrix(this.RowCount, this.ColCount);
            for (int rowIndex = 0; rowIndex < newMatrix.RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < newMatrix.ColCount; colIndex++)
                {

                    newMatrix.data[rowIndex, colIndex] = this.data[rowIndex, colIndex] * multiplier;
                }
            }
            return newMatrix;
        }

        public void MultiplyRow(int rowIndex, double multiplier)
        {
            if (rowIndex > this.RowCount - 1)
            {
                throw new MatrixValueOutOfRangeException();
            }
            for (int colIndex = 0; colIndex < this.ColCount; colIndex++)
            {
                this.data[rowIndex, colIndex] = this.data[rowIndex, colIndex] * multiplier;
            }
        }

        public void AddRow(int fromRowIndex, int toRowIndex)
        {
            if (fromRowIndex > this.RowCount - 1 || toRowIndex > this.RowCount - 1)
            {
                throw new MatrixValueOutOfRangeException();
            }
            for (int colIndex = 0; colIndex < this.ColCount; colIndex++)
            {
                this.data[toRowIndex, colIndex] += this.data[fromRowIndex, colIndex];
            }
        }

        public void AddRowRatio(int fromRowIndex, int toRowIndex, double multiplier)
        {
            if (fromRowIndex > this.RowCount - 1 || toRowIndex > this.RowCount - 1)
            {
                throw new MatrixValueOutOfRangeException();
            }
            for (int colIndex = 0; colIndex < this.ColCount; colIndex++)
            {
                this.data[toRowIndex, colIndex] += this.data[fromRowIndex, colIndex] * multiplier;
            }
        }

        public void SwapRow(int RowIndex1, int RowIndex2)
        {
            if (RowIndex1 > this.RowCount - 1 || RowIndex1 > this.RowCount - 1 ||
                RowIndex1 < 0 || RowIndex1 < 0)
            {
                throw new MatrixValueOutOfRangeException();
            }
            double temp = 0;
            for (int colIndex = 0; colIndex < this.ColCount; colIndex++)
            {
                temp = this.data[RowIndex1, colIndex];
                this.data[RowIndex1, colIndex] = this.data[RowIndex2, colIndex];
                this.data[RowIndex2, colIndex] = temp;
            }
        }
        #endregion

        #region inheritances
        class UnitMatrix : Matrix
        {
            public UnitMatrix(int orderCount) : base(orderCount, orderCount)
            {
                for(int i = 0; i < orderCount; i++)
                {
                    this.data[i, i] = 1;
                }
            }
        }
        #endregion

        #region exceptions
        public class MatrixValueOutOfRangeException : Exception
        {

        }
        public class MatrixAddException : Exception
        {

        }
        public class MatrixSubtractException : Exception
        {

        }
        public class MatrixMultiplyException : Exception
        {

        }
        public class MatrixMiscException : Exception
        {
            public MatrixMiscException(string message) : base (message)
            {

            }
        }
        #endregion
    }
}
