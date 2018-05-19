using System;

namespace ChislMethods.DerSystems.TMP.Structs
{
    public static class MatrixExt
    {
        public class RTMatrix
        {
            public Matrix R;
            public Matrix T;
        }

        /// <summary>
        /// Ортогонализирует матрицу
        /// </summary>
        /// <param name="m">Исходная матрица</param>
        /// <returns>Матрица поворота</returns>
        public static RTMatrix Orthogonalization(this Matrix m)
        {
            var result = new RTMatrix() { R = new Matrix(m.Rows, m.Columns), T = new Matrix(m.Rows, 1.0) };

            result.R.SetColumn(m.Column(0), 0);

            var vec = new Vector(m.Columns);
            var r   = new Vector(m.Columns);
            var a   = new Vector(m.Columns);

            for (var column = 1; column < m.Columns; ++column)
            {
                vec.Clear();
                for(var row = 0; row < column; ++row)
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
        /// Решение уравнения методом ортогонализации
        /// </summary>
        /// <param name="a">Правая часть</param>
        /// <param name="b">Левая часть</param>
        /// <returns></returns>
        public static Vector OrthogonalizationRoot(this Matrix a, Vector b)
        {
            if (!a.IsSquare)
                throw new InvalidOperationException(
                    "System equals should be same dimension as variable numbers");

            if(a.Columns != b.Size)
                throw new InvalidOperationException(
                    "Number of left side should be equal a right size");

            var result = new Vector(b.Size);

            var ortho = a.Orthogonalization();

            var rTranspose = ortho.R.Transpose();

            var D = rTranspose.Multiply(ortho.R);

            // T-1 * D-1 * R-1 * b
            return ortho.T.InverseTopDiagonal()
                        .Multiply(D.InverseDiagonal())
                        .Multiply(rTranspose)
                        .Multiply(b);
        }

        /// <summary>
        /// Обращает верхнедиагональную матрицу
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Matrix InverseTopDiagonal(this Matrix m)
        {
            var result = new Matrix(m.Rows, m.Columns);

            for(var row = 0; row < m.Rows; ++row)
                for (var column = row; column < m.Columns; ++column)
                {
                    if(row == column)
                        result[row, column] = 1 / m[row, column];
                    else
                    {
                        for (var k = row; k < column ; ++k)
                            result[row, column] += result[row, k] * m[k, column];

                        result[row, column] /= -m[column, column];
                    }
                }

            return result;
        }

        /// <summary>
        /// Корень уравнения методом последовательных итераций
        /// </summary>
        /// <param name="m"></param>
        /// <param name="b"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Vector IterationRoot(this Matrix m, Vector b, double e)
        {
            if(m.Rows != m.Columns || m.Columns != b.Size)
                throw new InvalidOperationException(
                   "Размер матрицы и вектора должны быть одинаковы.");

            if (m.Norma() >= 1)
                throw new InvalidOperationException(
                    "Условие сходимости не выполняется.");

            var xPrev = new Vector(b);
            var d     = 0.0;
            do
            {
                d = 0;

                var x = new Vector(b);

                x.Add(m.Multiply(xPrev));
                
                d = (x - xPrev).Norma1();

                xPrev = x;
            }
            while (Math.Sqrt(d) > e);

            return xPrev;
        }

        
        public static void Transform(this Matrix m, Vector b)
        {
            if (m.Rows != m.Columns || m.Columns != b.Size)
                throw new InvalidOperationException(
                   "Размер матрицы и вектора должны быть одинаковы.");

            for (var row = 0; row < m.Rows; ++row)
            {
                var tmp = m.MaxInColumn(row);

                if (tmp.index == row) continue;

                m.SwapRows(tmp.index, row);
                b.Swap(tmp.index, row);
            }

            for (var row = 0; row < m.Rows; ++row)
            {
                for (var column = 0; column < m.Columns; ++column)
                {
                    if (column != row)
                        m[row, column] /= -m[row, row];
                }

                b[row] /= m[row, row];
                m[row, row] = 0;
            }
        }

        /// <summary>
        /// Обращение диагональной матрицы
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Matrix InverseDiagonal(this Matrix m)
        {
            var result = new Matrix(m.Rows, m.Columns);

            for (int i = 0; i < m.Rows; i++)
                result[i, i] = 1 / m[i, i];

            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="n"></param>
        /// <param name="a"></param>
        /// <param name="c"></param>
        /// <param name="b"></param>
        /// <param name="f"></param>
        /// <param name="x"></param>
        public static Vector SweepMethod(this Matrix items, Vector b)
        {
            Vector aa = new Vector(b.Size);
            Vector bb = new Vector(b.Size);

            aa[1] = -items[0, 1] / items[0, 0];
            bb[1] = b[0] / items[0, 0];
            var n = b.Size;

            for (int i = 1; i < b.Size - 1; i++)
            {
                for (int j = 1; j < b.Size - 1; j++)
                {
                    aa[i + 1] = -items[i, j + 1] / (items[i, i] + items[i, j - 1] * aa[i]);
                    bb[i + 1] = (-items[i, j - 1] * bb[i] + b[i]) / (items[i, i] + items[i, j - 1] * aa[i]);
                }
            }

            Vector x = new Vector(b.Size)
            {
                [b.Size - 1] = (-items[n - 1, n - 2] * bb[n - 1] + b[n - 1]) / (items[n - 1, n - 1] + items[n - 1, n - 2] * aa[n - 1])
            };
            for (int i = n - 2; i >= 0; i--)
                x[i] = aa[i + 1] * x[i + 1] + bb[i + 1];

            return x;
        }

        /// <summary>
        /// Создание спалйна таблично заданной функции
        /// </summary>
        /// <param name="x">Набор значений х</param>
        /// <param name="y">Набор значений у</param>
        /// <returns>Сплай, значения которых можно получить индексатором</returns>
        public static Spline BuildSpline(this Vector x, Vector y)
        {
            if (x.Size != y.Size)
                throw new InvalidOperationException("x and y should have same size");

            var n = x.Size;
            var m = new Matrix(n - 2, n - 2);
            var b = new Vector(n - 2);

            m[0, 0] = ((x[1] - x[0]) + x[2] - x[1]) / 3;
            m[n - 3, n - 3] = ((x[n-1] - x[n-2]) + x[n-2] - x[n-3]) / 3;

            b[0] = (y[2] - y[1]) / (x[2] - x[1]) - (y[1] - y[0]) / (x[1] - x[0]);
            b[n-3] = (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2]) - (y[n - 2] - y[n - 3]) / (x[n - 2] - x[n - 3]);

            for (var i = 1; i < n - 2; i++)
            {
                var h1 = x[i] - x[i - 1];
                var h2 = x[i + 1] - x[i];

                b[i] = (y[i + 1] - y[i]) / h2 - (y[i] - y[i - 1]) / h1;
                m[i - 1, i - 1] = (h2 + h1) / 3;
                m[i - 1, i] = m[i, i - 1] = h2 / 6;
            }

            var r = new Vector(n);
            var ma = m.SweepMethod(b);
            for (var index = 0; index < ma.Size; ++index)
            {
                r[index + 1] = ma[index];
            }
            return new Spline(x, y, r);
        }

        /// <summary>
        /// Решение уравнения методом Гаусса
        /// </summary>
        /// <param name="items"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector Gauss(this Matrix items, Vector b)
        {
            double max;
            int indmax;
            for (int i = 0; i < b.Size; i++)
            {
                max = Math.Abs(items[i, i]);
                indmax = i;
                for (int j = i; j < b.Size; j++)
                    if (Math.Abs(items[j, i]) > max)
                    {
                        max = Math.Abs(items[j, i]);
                        indmax = j;
                    }

                if (indmax != i)
                {
                    items.SwapRows(i, indmax);
                    double temp = b[i];
                    b[i] = b[indmax];
                    b[indmax] = temp;
                }

                double x;
                for (int z = 1; z < b.Size; z++)
                    for (int j = z; j < b.Size; j++)
                    {
                        x = items[j, z - 1] / items[z - 1, z - 1];
                        for (int k = 0; k < b.Size; k++)
                            items[j, k] = items[j, k] - x * items[z - 1, k];
                        b[j] = b[j] - x * b[z - 1];
                    }
            }

            for (int q = b.Size - 1; q >= 0; q--)
            {
                for (int j = q + 1; j < b.Size; j++)
                    b[q] -= items[q, j] * b[j];
                b[q] = b[q] / items[q, q];
            }
            return b;
        }
    }
}