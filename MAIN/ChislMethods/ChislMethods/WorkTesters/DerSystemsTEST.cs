using ChislMethods.DifUr;
using ChislMethods.WorkTesters.Helpers;
using System;

namespace ChislMethods.WorkTesters
{
    public static class DerSystemsTEST
    {
        private static FunDelegate fprav;

        public static void TEST()
        {
            SimpleTest();
        }
        
        static void SimpleTest()
        {
            fprav += EasyMethod;
            
            while (true)
            {
                double[] x = new double[] { 1.0 };
                double[,] rez;
                EulerAndRungeKutta rk = new EulerAndRungeKutta(0, 1, x, 0.1);
                var choice = IOSystem.SafeSimpleChoice("Выберите метод: ", new string[]
                {
                    "Рунге-Кутта 2 порядка",
                    "Рунге-Кутта 4 порядка",
                    "Метод Эйлера",
                    "Закончить тест"
                });

                var endWork = false;

                switch (choice)
                {
                    case 0:
                        rez = rk.MethodRK2(fprav);
                        Write(rez, 0.0, 1.0, 0.1);
                        break;
                    case 1:
                        rez = rk.MethodRK4(fprav);
                        Write(rez, 0.0, 1.0, 0.1);
                        break;
                    case 2:
                        rez = rk.EulerMethod(fprav);
                        Write(rez, 0.0, 1.0, 0.1);
                        break;
                    case 3:
                        endWork = true;
                        break;
                }

                Console.WriteLine();

                if (endWork)
                    break;
            }
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
            Console.WriteLine();
            Console.WriteLine("Решение:");
            Console.WriteLine();
            int n = (int)((b - a) / h);//количество шагов
            double[,] xr = Matr;
            double t = a;
            int m = Matr.GetLength(1);
            double[] x = new double[m];


            for (int i = 0; i <= n; i++)
            {

                Console.Write(("t=" + t).PadRight(10));
                for (int j = 0; j < x.Length; j++)
                {
                    x[j] = xr[i, j];
                    Console.Write(("x["+ j + "]="+ x[j]).PadRight(25));
                }

                Console.WriteLine();
                t += h;
            }
        }
    }
}
