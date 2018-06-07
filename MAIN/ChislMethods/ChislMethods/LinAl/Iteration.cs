using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChislMethods.LinAl
{
<<<<<<< HEAD
    public class Iteration
    {
        /// <summary>
        /// Корень уравнения методом последовательных итераций
        /// </summary>
        /// <param name="m"></param>
        /// <param name="b"></param>
        /// <param name="e"></param>
        /// <returns></returns>
=======

    /// <summary>
    /// Решение системы линейных уравнений (матрицы) Методом последовательных итераций
    /// </summary>
    public class Iteration
    {
        /// <summary>
        /// Решение системы линейных уравнений (матрицы) Методом последовательных итераций
        /// </summary>
>>>>>>> ChM functional done. Visual not perfect
        public static Vector Calc(Matrix m, Vector b, double e)
        {
            if (m.Row != m.Col || m.Col != b.Size)
            {
                Console.WriteLine("Размер матрицы и вектора должны быть одинаковы.");
                return null;
                //throw new InvalidOperationException(
                //"Размер матрицы и вектора должны быть одинаковы.");
            }

            if (m.Norma() >= 1)
            {
                Console.WriteLine("Условие сходимости не выполняется.");
                return null;
            }

            var xPrev = new Vector(b);
            var d = 0.0;
            do
            {
                d = 0;

                var x = new Vector(b);

                x.Add(m * xPrev);

                d = (x - xPrev).Norma1();

                xPrev = x;
            }
            while (Math.Sqrt(d) > e);

            return xPrev;
        }
    }
}
