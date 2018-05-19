using System;
using System.Text;

namespace ChislMethods.DerSystems.TMP.Structs
{
    public class Matrix 
    {
        public class Container
        {
            public int index;
            public double value;
        }

        //   O P E R A T O R 
        public static Matrix operator -(Matrix m)
            => new Matrix(m).Negate();

        public static Matrix operator +(Matrix m1, Matrix m2)
            => new Matrix(m1).Add(m2);

        public static Matrix operator -(Matrix m1, Matrix m2)
            => new Matrix(m1).Subtract(m2);

        public static Matrix operator *(Matrix m1, Matrix m2)
            => m1.Multiply(m2);
        
        public static Matrix operator *(double n, Matrix m)
            => new Matrix(m).Multiply(n);

        public static Matrix operator *(Matrix m, double n)
            => new Matrix(m).Multiply(n);

        public static Matrix operator /(double n, Matrix m)
            => new Matrix(m).Devide(n);

        public static Matrix operator /(Matrix m, double n)
            => new Matrix(m).Devide(n);

        public static Matrix operator +(Matrix m, double n)
            => new Matrix(m).Add(n);

        public static Matrix operator +(double n, Matrix m)
            => new Matrix(m).Add(n);

        public static Matrix operator -(Matrix m, double n)
            => new Matrix(m).Subtract(n);

        public static Matrix operator -(double n, Matrix m)
            => new Matrix(m).Subtract(n);

        private double[] matrix;

        public int Columns { get; private set; }
        public int Rows    { get; private set; }

        public Boolean IsSquare => this.Rows == this.Columns;

        public double this[int row, int column]      // Access this matrix as a 2D array
        {
            get => this.matrix[row * this.Columns + column];
            set => this.matrix[row * this.Columns + column] = value;
        }

        public Matrix(int row, int column)         // Matrix Class constructor
        {
            this.Columns = column;
            this.Rows    = row;
            this.matrix  = new double[this.Rows * this.Columns];
        }

        public Matrix(int dimension, double value)
        {
            this.Rows    = this.Columns = dimension;
            this.matrix  = new double[this.Rows * this.Columns];

            for (var index = 0; index < this.Rows; ++index)
                this[index, index] = value;
        }

        private Matrix(Matrix matrix)         // Matrix Class constructor
        {
            this.Columns = matrix.Columns;
            this.Rows    = matrix.Rows;
            this.matrix  = new double[this.Rows * this.Columns];

            for (int index = 0; index < this.Rows; ++index)
                this.SetColumn(matrix, index);
        }

        public Vector Column(int k)
        {
            var vector = new Vector(this.Rows);
            for (int index = 0; index < this.Rows; index++) vector[index] = this[index, k];
            return vector;
        }

        public void Column(int k, Vector vector)
        {
            for (int index = 0; index < this.Rows; index++) vector[index] = this[index, k];
        }

        public Matrix SetColumn(Matrix matrix, int k)
        {
            this.Foreach((int c, ref double v) => v = matrix[c, k], DimensionType.Row, k);
            return this;
        }

        public Matrix SetColumn(Vector vector, int k)
        {
            this.Foreach((int c, ref double v) => v = vector[c], DimensionType.Row, k);
            return this;
        }

        public Vector Row(int k)
        {
            var vector = new Vector(this.Columns);
            for (var index = 0; index < this.Rows; index++) matrix[index] = this[k, index];
            return vector;
        }

        /// <summary>
        /// Возвращает строку и копирует в вектор, в итой строке
        /// </summary>
        /// <param name="k">Строка, значения которых будут копироваться в к</param>
        /// <param name="vector">Вектор куда будет копироваться</param>
        public void Row(int k, Vector vector)
        {
            for (var index = 0; index < this.Rows; index++) matrix[index] = this[k, index];
        }

        public Matrix SetRow(Matrix matrix, int k)
        {
            this.Foreach((int r, ref double v) => v = matrix[k, r], DimensionType.Column, k);
            return this;
        }

        public Matrix SetRow(Vector vector, int k)
        {
            this.Foreach((int r, ref double v) => v = vector[r], DimensionType.Column, k);
            return this;
        }

        public Container MaxInColumn(int column)
        {
            var result = new Container() { index = -1, value = double.MinValue};

            for (var index = 0; index < this.Rows; index++)
                if (result.value < this[index, column])
                    result = new Container() { index = index, value = this[index, column] };

            return result;
        }

        public Container MaxInRow(int row)
        {
            var result = new Container() { index = -1, value = double.MinValue };

            for (var index = 0; index < this.Columns; index++)
                if (result.value < this[row, index])
                    result = new Container() { index = index, value = this[row, index] };

            return result;
        }

        public void SwapColumns(int index1, int index2)
        {
            for(var row =0; row < this.Rows; ++row)
            {
                var tmp = this[row, index1];
                this[row, index1] = this[row, index2];
                this[row, index2] = tmp;
            }
        }

        public void SwapRows(int index1, int index2)
        { 
            for (var column = 0; column < this.Columns; ++column)
            {
                var tmp = this[index1, column];
                this[index1, column] = this[index2, column];
                this[index2, column] = tmp;
            }
        }

        public double Norma()
        {
            var result = 0.0;

            this.Foreach((r, c, m) => result += m[r, c] * m[r, c]);

            return Math.Sqrt(result);
        }

        /// <summary>
        /// Creates a new matrix of equal production of itself on the matrix parameter
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public Matrix Multiply(Matrix matrix)
        {
            if (this.Columns != matrix.Rows) throw new InvalidOperationException(
                "Size column of first matrix should be equal to size row of second matrix");

            return new Matrix(this.Rows, matrix.Columns).Foreach((row, column, m) =>
            {
                for (var index = 0; index < matrix.Rows; index++)
                    m[row, column] += this[row, index] * matrix[index, column];
            });
        }

        public Vector Multiply(Vector vector)
        {
            if (this.Columns != vector.Size) throw new InvalidOperationException(
                "Size column of first matrix should be equal to size row of second matrix");

            var result = new Vector(vector.Size);

            this.Foreach((r, c, m) => result[r] += this[r, c] * vector[c]);

            return result;
        }

        public Matrix Multiply(double n)                          // Multiplication by constant n
            => this.Foreach((r, c, m) => m[r, c] *= n);

        public Matrix Devide(double n)                          // Multiplication by constant n
          => this.Multiply(1 / n);

        public Matrix Add(Matrix matrix) 
        {
            if (this.Rows != matrix.Rows || this.Columns != matrix.Columns)
                throw new InvalidOperationException("Matrices must have the same dimensions!");

            this.Foreach((row, column, m) =>
            {
                for (var index = 0; index < matrix.Columns; index++)
                    m[row, column] += matrix[index, column];
            });

            return this;
        }

        public Matrix Subtract(Matrix matrix)
        {
            if (this.Rows != matrix.Rows || this.Columns != matrix.Columns)
                throw new InvalidOperationException("Matrices must have the same dimensions!");

            this.Foreach((row, column, m) =>
            {
                for (var index = 0; index < matrix.Columns; index++)
                    m[row, column] -= matrix[index, column];
            });

            return this;
        }

        public Matrix Add(double scalar) 
            => this.Foreach((row, column, m) => m[row, column] += scalar);

        public Matrix Subtract(double scalar)
           => this.Add(-scalar);

        public Matrix Transpose()              // Matrix transpose, for any rectangular matrix
            => new Matrix(this.Columns, this.Rows).Foreach((r, c, m) => m[r, c] = this[c, r]);

        private Matrix Negate()
            => this.Foreach((row, column, m) => m[row, column] = -m[row, column]);

        public static Matrix RandomMatrix(int iRows, int iCols, int dispersion)       // Function generates the random matrix
        {
            Random random = new Random();
            Matrix matrix = new Matrix(iRows, iCols);
            for (int i = 0; i < iRows; i++)
                for (int j = 0; j < iCols; j++)
                    matrix[i, j] = random.Next(-dispersion, dispersion);
            return matrix;
        }

        public override string ToString()                           // Function returns matrix as a string
        {
            var s = new StringBuilder();
            this.Foreach((r, c, m) => s.Append($"{{{m[r, c]},5:E2}}  "));
            return s.ToString();
        }


        //public Matrix Foreach(Action<int, int, double> action)
        //{
        //    for (int row = 0; row < this.rows; ++row)
        //        for (var column = 0; column < this.columns; ++column)
        //            action(row, column, this.matrix[row * columns + column]);

        //    return this;
        //}

        //public Matrix Foreach(Action<int, int, double, double> action, Matrix matrix)
        //{
        //    if (this.Rows != matrix.Rows || this.Columns != matrix.Columns)
        //        throw new InvalidOperationException("Size of both matrix should be equals");

        //    for (int row = 0; row < this.rows; ++row)
        //        for (var column = 0; column < this.columns; ++column)
        //            action(
        //                row,
        //                column,
        //                this.matrix[row * Columns + column],
        //                matrix.matrix[row * Columns + column]);

        //    return this;
        //}

        /// <summary>
        /// First action argument is current row
        /// second action argument is current column
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Matrix Foreach(Action<int, int, Matrix> action)
        {
            for (int row = 0; row < this.Rows; ++row)
                for (var column = 0; column < this.Columns; ++column)
                    action(row, column, this);

            return this;
        }

        private Matrix Foreach(Action action, DimensionType type, int index)
        {
            var indexator = type == DimensionType.Row
                ? new Func<int, int>(row => row * this.Columns + index)
                : new Func<int, int>(col => index * this.Columns + col);

            var end = type == DimensionType.Row
                ? this.Rows : this.Columns;

            for (int row = 0; row < end; ++row)
                action(row, ref this.matrix[indexator(row)]);

            return this;
        }

        private delegate void Action(int index, ref double value);

        public enum DimensionType
        {
            /// <summary>
            /// 
            /// </summary>
            Column,

            /// <summary>
            /// 
            /// </summary>
            Row
        }
    }
}