using ChislMethods.DerSystems;
using ChislMethods.DerSystems.TMP.Structs;
using ChislMethods.DerSystems.TMP;
using System;

namespace ChislMethods.WorkTesters
{
    public static class DerSystemsTEST
    {
        private static FunDelegate fprav;

        public static void TEST()
        {
            var testNum = Helpers.IOSystem.SafeSimpleChoice("Выберите тест:", new string[] { "Оооочень Крутой... (содержит код: матриц, сплайнов, метод наименьших квадратов, адамса, кутта, метод ортогонализации, метод итераций, гаусса)", "Студенческий" });
            if (testNum == 0)
            {
                HardTest();
            }
            if (testNum == 1)
            {
                SimpleTest();
            }
        }

        static void HardTest()
        {
            var n = 12;
            var x = new Vector(n);
            var y = new Vector(n);
            for (int i = 0; i < n; i++)
            {
                x[i] = i;
                y[i] = x[i] * x[i];
            }

            var spline = MatrixExt.BuildSpline(x, y);

            for (float i = 0; i < n; i += 0.5f)
            {
                Console.WriteLine($"x ={i} y = {spline[i]}");
            }

            // LSM
            x = new Vector(4)
            {
                [0] = 0,
                [1] = 1,
                [2] = 2,
                [3] = 3
            };
            y = new Vector(4) //{ 7.6, 6, 5.2, 4, 3.6 };
            {
                [0] = 0,
                [1] = 1.1,
                [2] = 3.9,
                [3] = 8.95
            };

            var func = new Func<double, double>[]
            {
                c => 1,
                c => c,
                c => c * c
            }.LSM(x, y);

            Console.WriteLine($"Koeff {func.Koeff}");

            for (var step = 0.0; step < 3; step += 0.25)
            {
                Console.WriteLine($"X ={step.ToString().PadLeft(10, ' ')}, Analitic = {(step * step).ToString().PadLeft(10, ' ')}, " +
                    $"Approx = {func[step].ToString().PadLeft(10, ' ')}");
            }
        }

        static void SimpleTest()
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
