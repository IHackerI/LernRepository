using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChislMethods.LinAl
{
<<<<<<< HEAD
    public class Ortogonolization
    {
=======
    /// <summary>
    /// Решение системы линейных уравнений (матрицы) Методом ортогонализации
    /// </summary>
    public class Ortogonolization
    {
        /// <summary>
        /// Решение системы линейных уравнений (матрицы) Методом ортогонализации
        /// </summary>
>>>>>>> ChM functional done. Visual not perfect
        public static Vector Calc(Matrix a, Vector b)
        {
            if (!a.IsSquare)
                throw new InvalidOperationException(
                    "System equals should be same dimension as variable numbers");

            if (a.Col != b.Size)
                throw new InvalidOperationException(
                    "Number of left side should be equal a right size");

            var result = new Vector(b.Size);

            var ortho = Orthogonalization(a);

            var rTranspose = Matrix.Transposition(ortho.R);

            var D = rTranspose * ortho.R;
<<<<<<< HEAD

            // T-1 * D-1 * R-1 * b
=======
            
>>>>>>> ChM functional done. Visual not perfect
            return  InverseTopDiagonal(ortho.T) * 
                    InverseDiagonal(D) *
                    rTranspose * 
                    b;
        }

        /// <summary>
<<<<<<< HEAD
        /// Ортогонализирует матрицу
        /// </summary>
        /// <param name="m">Исходная матрица</param>
        /// <returns>Матрица поворота</returns>
        
=======
        /// Ортогонализирование матрицы
        /// </summary>
>>>>>>> ChM functional done. Visual not perfect
        public static (Matrix R, Matrix T) Orthogonalization(Matrix m)
        {
            var result = (R: new Matrix(m.Row, m.Col), T: new Matrix(m.Row, 1.0));

            result.R.SetColumn(m.Column(0), 0);

            var vec = new Vector(m.Col);
            var r = new Vector(m.Col);
            var a = new Vector(m.Col);

            for (var column = 1; column < m.Col; ++column)
            {
                vec.Clear();
                for (var row = 0; row < column; ++row)
                {
                    result.R.Column(row, r);
                    m.Column(column, a);

                    result.T[row, column] = a.ScalarProduct(r) / r.ScalarProduct(r);

                    vec.Add(-result.T[row, column] * r);
                }

                result.R.SetColumn(m.Column(column).Add(vec), column);
            }

            return result;
        }

        /// <summary>
        /// Обращает верхнедиагональную матрицу
        /// </summary>
<<<<<<< HEAD
        /// <param name="m"></param>
        /// <returns></returns>
=======
>>>>>>> ChM functional done. Visual not perfect
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
<<<<<<< HEAD

=======
        
        /// <summary>
        /// Инвертирование диагонали матрицы
        /// </summary>
>>>>>>> ChM functional done. Visual not perfect
        public static Matrix InverseDiagonal(Matrix m)
        {
            var result = new Matrix(m.Row, m.Col);

            for (int i = 0; i < m.Row; i++)
                result[i, i] = 1 / m[i, i];

            return result;
        }
    }
}
