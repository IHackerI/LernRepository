using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChislMethods.DerSystems
{
    public static class DerSystemsTEST
    {
        private static FunDelegate fprav;

        public static void TEST()
        {
            fprav += EasyMethod;

            double[] x = new double[] { 1.0 };
            double[,] rez;
            RangeKutta rk = new RangeKutta(0, 1, x, 0.1);
            Console.WriteLine("RK2");
            rez = rk.MetodRK2(fprav);
            Write(rez, 0.0, 1.0, 0.1);

            Console.WriteLine("RK4");
            x = new double[] { 1.0 };
            rk = new RangeKutta(0, 1, x, 0.1);
            rez = rk.MetodRK4(fprav);
            Write(rez, 0.0, 1.0, 0.1);

            Console.WriteLine("Euler");
            x = new double[] { 1.0 };
            rk = new RangeKutta(0, 1, x, 0.1);
            rez = rk.MetodEulera(fprav);
            Write(rez, 0.0, 1.0, 0.1);

            Console.ReadKey();
        }

        static double[] EasyMethod(double t, double[] x)
        {
            for (int i = 0; i < x.Length; i++)
            {
                x[i] += t;
            }
            return x;
        }

        public static void Write(double[,] Matr, double a, double b, double h)
        {
            Console.WriteLine("Solve");
            int n = (int)((b - a) / h);//количество шагов
            double[,] xr = Matr;
            double t = a;
            int m = Matr.GetLength(1);
            double[] x = new double[m];


            for (int i = 0; i <= n; i++)
            {

                Console.Write("t={0} ", t);
                for (int j = 0; j < x.Length; j++)
                {
                    x[j] = xr[i, j];
                    Console.Write("x[{0}]={1}", j, x[j]);
                }

                Console.WriteLine();
                t += h;
            }
        }
    }
}
