using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChislMethods.LinAl
{
    /// <summary>
    /// Решение системы линейных уравнений (матрицы) Методом ортогонализации
    /// </summary>
    public class Ortogonolization
    {
        /// <summary>
        /// Решение системы линейных уравнений (матрицы) Методом ортогонализации
        /// </summary>
        public static Vector Calclucate(Matrix matrixA, Vector vectorB)
        {
            if (!matrixA.IsSquare)
                throw new InvalidOperationException(
                    "System equals should be same dimension as variable numbers");

            if (matrixA.Col != vectorB.Size)
                throw new InvalidOperationException(
                    "Number of left side should be equal a right size");

            var result = new Vector(vectorB.Size);

            var ortho = Orthogonalization(matrixA);

            var rTranspose = Matrix.Transposition(ortho.R);

            var D = rTranspose * ortho.R;
            
            return  InverseTopDiagonal(ortho.T) * 
                    InverseDiagonal(D) *
                    rTranspose * 
                    vectorB;
        }

        /// <summary>
        /// Ортогонализирование матрицы
        /// </summary>
        public static (Matrix R, Matrix T) Orthogonalization(Matrix matrix)
        {
            var result = (R: new Matrix(matrix.Row, matrix.Col), T: new Matrix(matrix.Row, 1.0));

            result.R.SetColumn(matrix.Column(0), 0);

            var vec = new Vector(matrix.Col);
            var r = new Vector(matrix.Col);
            var a = new Vector(matrix.Col);

            for (var column = 1; column < matrix.Col; ++column)
            {
                vec.Clear();
                for (var row = 0; row < column; ++row)
                {
                    result.R.Column(row, r);
                    matrix.Column(column, a);

                    //#warning Check0
                    var prod = r.ScalarProduct(r);

                    result.T[row, column] = a.ScalarProduct(r) / (prod == 0?0.0000001: prod);

                    vec.Add(-result.T[row, column] * r);
                }

                result.R.SetColumn(matrix.Column(column).Add(vec), column);
            }

            return result;
        }

        /// <summary>
        /// Обращает верхнедиагональную матрицу
        /// </summary>
        public static Matrix InverseTopDiagonal(Matrix m)
        {
            var result = new Matrix(m.Row, m.Col);

            for (var row = 0; row < m.Row; ++row)
                for (var column = row; column < m.Col; ++column)
                {
                    if (row == column)
                        result[row, column] = 1 / m[row, column];
                    else
                    {
                        for (var k = row; k < column; ++k)
                            result[row, column] += result[row, k] * m[k, column];

                        result[row, column] /= -m[column, column];
                    }
                }

            return result;
        }
        
        /// <summary>
        /// Инвертирование диагонали матрицы
        /// </summary>
        public static Matrix InverseDiagonal(Matrix m)
        {
            var result = new Matrix(m.Row, m.Col);

            for (int i = 0; i < m.Row; i++)
                result[i, i] = 1 / m[i, i];

            return result;
        }
    }
}
