using ChislMethods.DifUr;
using ChislMethods.WorkTesters.Helpers;
using System;
using System.Linq;

namespace ChislMethods.WorkTesters
{
    /// <summary>
    /// Тестер решения дифуров
    /// </summary>
    public static class DifUrsTEST
    {
        private static FuncDelegate fprav;

        public static void TEST()
        {
            SimpleTest();
        }

        static void SimpleTest()
        {
            fprav += EasyMethod;
            
            double[] rc2x = new double[] { 1.0, 2.0 };

            var rc4x = (from xx in rc2x select xx).ToArray();
            var eulerx = (from xx in rc2x select xx).ToArray();
            
            var rk2 = new RungeKutta2(0, 1, rc2x, 0.1);
            var rezrc2 = rk2.Calculate(fprav);

            var rk4 = new RungeKutta4(0, 1, rc4x, 0.1);
            var rezec4 = rk4.Calculate(fprav);

            var euler = new Euler(0, 1, eulerx, 0.1);
            var rezeuler = euler.Calculate(fprav);

            Write(rezrc2, rezec4, rezeuler, 0.0, 1.0, 0.1);
          

            Console.WriteLine();
            
        }

        /// <summary>
        /// Функция дифуров
        /// </summary>
        static double[] EasyMethod(double t, double[] x)
        {
            for (int i = 0; i < x.Length; i++)
            {
                x[i] += t;
            }
            return x;
        }

        /// <summary>
        /// Вывод решения
        /// </summary>
        public static void Write(double[,] rc2Matr, double[,] rc4Matr, double[,] eulerMatr, double a, double b, double h)
        {
            Console.WriteLine();
            Console.WriteLine("Решение:");
            Console.WriteLine();
            int n = (int)((b - a) / h);//количество шагов
            double t = a;
            int m = rc2Matr.GetLength(1);
            double[] x = new double[m];


            Console.WriteLine("".PadRight(10) + "RungeKutta2".PadRight(25) + "RungeKutta4".PadRight(25) + "Euler".PadRight(25));
            Console.WriteLine();

            for (int i = 0; i <= n; i++)
            {
                Console.Write(("t=" + t).PadRight(10));
                for (int j = 0; j < x.Length; j++)
                {
                    x[j] = rc2Matr[i, j];
                    Console.Write(("x[" + j + "]=" + x[j]).PadRight(25));

                    x[j] = rc4Matr[i, j];
                    Console.Write(("x[" + j + "]=" + x[j]).PadRight(25));

                    x[j] = eulerMatr[i, j];
                    Console.Write(("x[" + j + "]=" + x[j]).PadRight(25));
                    
                    Console.WriteLine();
                    Console.Write("".PadRight(10));
                }

                Console.WriteLine();
                t += h;
            }
        }
    }
}
