using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChislMethods.LinAl
{

    /// <summary>
    /// Решение системы линейных уравнений (матрицы) Методом последовательных итераций
    /// </summary>
    public class Iteration
    {
        // public delegate double(double x, int number);

        private static double RowNormalization(Matrix matrix)  //Нормализация строки для метода(максимальный элемент)
        {
            int m = matrix.Col;
            int n = matrix.Row;
            double normal = 0;

            for (int i = 0; i < n; i++)
            {
                double sum = 0;

                for (int j = 0; j < m - 1; j++)
                    sum += Math.Abs(matrix[i, j]);

                if (normal < sum)
                    normal = sum;
            }

            return normal;
        }
        private static double ColumnNormalization(Matrix matrix) //нормализация столбца(максимальный элемент)
        {
            int m = matrix.Col;
            int n = matrix.Row;
            double normal = 0;

            for (int j = 0; j < m - 1; j++)
            {
                double sum = 0;

                for (int i = 0; i < n; i++)
                    sum += Math.Abs(matrix[i, j]);

                if (normal < sum)
                    normal = sum;
            }

            return normal;
        }

        private static double VectorNorm(Vector vec) //нормализация вектора(максимальный элемент)
        {
            double normal = 0;
            double abs_vec_i = 0;
            for (int i = 0; i < vec.Size; i++)
            {
                abs_vec_i = Math.Abs(vec[i]);

                if (normal < abs_vec_i)
                    normal = abs_vec_i;
            }

            return normal;
        }

        public static bool IterationStop(Vector a, Vector b, double epsilon) //критерий точности для остановки
        {
            var c = new Vector(a.vector);

            // for (int i = 0; i < a.Size; i++)
            //    c[i] -= b[i];
            c = c - b;
            Console.WriteLine(VectorNorm(c) + " <==> " + epsilon);
            if (VectorNorm(c) <= epsilon) return false;
            else return true;
            //    return VectorNorm(c) < epsilon;
        }

        public static Vector Calculate(Matrix matrix, Vector v, double epsilon)
        {
            matrix.View();
            v.View();
            //var _matrix = ConnectVectorMatrix(matrix, v);
            var _matrix = DominantDiag(matrix, v);
            _matrix.View();
            var _work = BuildMatrix(_matrix);
            var _matrixSize = _matrix.Row;
            //  _matrix.View();


            var solution = new Vector(new double[_matrixSize]);

            var current_sol = new Vector(new double[_matrixSize]);

            for (int i = 0; i < _matrixSize; i++)
                current_sol[i] = _work[i, _matrixSize];

            var prev_iteration = new Vector(new double[_matrixSize]);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Begin parallel");
            while (IterationStop(current_sol, prev_iteration, epsilon))
            {
                prev_iteration = current_sol;
                current_sol = new Vector(new double[_matrixSize]);

                for (int i = 0; i < _matrixSize; i++)
                {
                    for (int j = 0; j < _matrixSize; j++)
                    {
                        current_sol[i] += _work[i, j] * prev_iteration[j];
                    }

                    current_sol[i] += _work[i, _matrixSize];
                }
            }
            sw.Stop();
            Console.WriteLine("Time is {0}", sw.ElapsedMilliseconds / 100.0);

            solution = current_sol;

            return solution;
        }

        private static bool ConverCondition(Matrix matrix) //Условие сходимости
        {
            if (RowNormalization(matrix) > 1 || ColumnNormalization(matrix) > 1) //Условие сходимости
                return false;
            else return true;

        }
        public static Matrix ConnectVectorMatrix(Matrix m, Vector v)
        {
            double[,] q = new double[m.Row, m.Col + 1];
            for (int i = 0; i < m.Row; i++)
            {
                for (int j = 0; j < m.Col + 1; j++)
                {
                    if (j < m.Col)
                    {
                        q[i, j] = m[i, j];
                        // Console.Write(" " + m[i, j] + " " + v[i]);
                    }
                    else
                    {
                        q[i, j] = v[i];
                        // Console.Write("vector: " + v[i]);
                    }
                    //    Console.Write(q[i, j]+ " ");
                }
                Console.WriteLine();
            }

            return new Matrix(q);
        }
        //построение матрицы C и вектора d Ax=B =>  x= Bx+g  g=D^(-1)*B и проверка условия сходимости
        public static Matrix BuildMatrix(Matrix matrix)
        {
            Matrix _work = new Matrix(matrix);

            var _matrixSize = matrix.Row;

            for (int i = 0; i < _matrixSize; i++)
            {
                int a_ii = (int)_work[i, i];

                if (a_ii != 0)
                {
                    _work[i, i] = 0;

                    _work[i, _matrixSize] /= a_ii;
                    for (int j = 0; j < _matrixSize; j++)
                    {
                        if (i != j)
                        {
                            _work[i, j] /= -a_ii;
                        }
                    }
                }
                else
                {
                    _work[i, i] = 1;

                    for (int j = 0; j < _matrixSize; j++)
                    {
                        if (i != j)
                        {
                            _work[i, j] *= -1;
                        }
                    }
                }
            }

            if (ConverCondition(_work) != true) throw new Exception("Матрица не сходима");
            return _work;
        }

        public static Matrix DominantDiag(Matrix matrix, Vector B) //построение доминантной диагонали в матрице значений
        {
            Matrix items = new Matrix(matrix);
            Vector b = new Vector(B.vector);
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

                if (i != indmax)
                {
                    items.SwapRows(i, indmax);
                    double temp = b[i];
                    b[i] = b[indmax];
                    b[indmax] = temp;
                }
            }
            for (int i = 0; i < items.Col; i++)
            {
                if (items[i, i] == 0) throw new Exception("Matrix diagonaly have zero values");
            }

            return ConnectVectorMatrix(items, b);
        }
    }
}

