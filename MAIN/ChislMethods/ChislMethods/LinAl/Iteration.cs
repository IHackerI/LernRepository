using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Решение системы линейных уравнений (матрицы) Методом последовательных итераций
        /// </summary>
        public static Vector Calculate(Matrix A, Vector b, double eps)
        {
            Matrix D = A.GetMatrixDiag();

            Matrix B = D.InverseMatrix() * (D.Substract(A));
            Vector g = D.InverseMatrix() * b;

            Vector Xk = new Vector(b.size);
            Vector Xk1 = new Vector(b.size);

            do
            {
                for (int i = 0; i < Xk.size; i++)
                {
                    Xk[i] = Xk1[i];
                }

                for (int i = 0; i < Xk1.size; i++)
                {
                    if (A[i, i] != 0)
                    {
                        Xk1[i] = (1 / A[i, i]) * (b[i] - (Summator(A, i, Xk)));
                    }
                    else
                    {
                        Xk1[i] = double.PositiveInfinity;
                    }
                }
            } while (MaxAbsEl(Xk1 - Xk) > eps);

            return Xk1;
        }

        static double MaxAbsEl(Vector v)
        {
            double ans = -1;

            for (int i = 0; i < v.size; i++)
            {
                if (ans < Math.Abs(v[i]))
                {
                    ans = Math.Abs(v[i]);
                }
            }

            return ans;
        }

        static double Summator(Matrix a, int ignoreI, Vector Xk)
        {
            double ans = 0;

            for (int j = 0; j < a.Col; j++)
            {
                if (j == ignoreI)
                    continue;

                ans += a[ignoreI, j] * Xk[j];
            }

            return ans;
        }
    }
}

