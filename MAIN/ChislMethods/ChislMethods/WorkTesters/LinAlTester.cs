using System;
using ChislMethods.LinAl;
using ChislMethods.WorkTesters.Helpers;
using static ChislMethods.WorkTesters.WorkMainTester;

namespace ChislMethods.WorkTesters
{
    /// <summary>
    /// Тестирование линейной алгебры
    /// </summary>
    public static class LinAlTester
    {
        public static void ram(Matrix r)// заполнение матрицы  случайными числами
        {
            Random random = new Random((DateTime.Now.Millisecond));
            for (int i = 0; i < r.Row; i++)
                for (int j = 0; j < r.Col; j++)
                    r[i, j] = random.Next(0, 9);
        }

        public static void rav(Vector r)// заполнение вектора  случайными числами
        {
            Random random = new Random((DateTime.Now.Millisecond));
            for (int i = 0; i < r.GetSize(); i++)
                r[i] = random.Next(1, 5);
        }


        /// <summary>
        /// Точка входа тестера
        /// </summary>
        public static void TEST()
        {
            try
            {
                var ch = false;

                while (!ch)
                {
                    ch = IOSystem.InterfacedViewChoice(new string[]
                    {
                        "Метод наименьших квадратов:",
                        "Метод Гаусса",
                        "Метод Грама-Шмидта",
                        "Метод ортогонализации",
                        "Метод итераций"
                    },

                    new EmptyD[]
                    {
                        LSMTest,
                        GaussTest,
                        GramSchmidtTest,
                        OrthoTest,
                        IterationTest
                    }
                    );
                }

                //BigTest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Метод наименьшх квадратов
        /// </summary>
        private static void LSMTest()
        {
            Console.WriteLine("Метод наименьших квадратов:");
            Console.WriteLine();

            var x = new Vector(5)
            {
                [0] = -2,
                [1] = -1,
                [2] = 0,
                [3] = 1,
                [4] = 2
            };
            var y = new Vector(5)
            {
                [0] = 15,
                [1] = 4,
                [2] = 1,
                [3] = 0,
                [4] = -5
            };

            var func = new Func<double, double>[]
            {
                //c => Math.Cos(c),
                //c => Math.Pow(2.71, c),
                //c => Math.Sin(c)
                c => 1,
                c => c,
                c => c * c,
                //c => c * c * c
            };

            var Koeff = LeastSquareMethod.LSM(func, x, y);

            Console.WriteLine($"Koeff {Koeff}");
            Console.WriteLine();

            for (var step = -3.0; step < 3; step += 0.25)
            {
                Console.WriteLine($"X ={step.ToString().PadLeft(10, ' ')}, KVStep = {(step * step).ToString().PadLeft(10, ' ')}, " +
                    $"Approx = { LeastSquareMethod.CalcApprox(step, Koeff, func).ToString().PadLeft(10, ' ')}");
            }
        }

        /// <summary>
        /// Метод гаусса
        /// </summary>
        private static void GaussTest()
        {
            const int A1 = 4;
            const int A2 = 4;

            Matrix aa = new Matrix(new double[A1, A2]);
            Vector va = new Vector(A1);

            Console.WriteLine("Первая матрица: ");
            ram(aa);
            aa.View();
            Console.WriteLine();

            Console.WriteLine("Первый вектор: ");
            rav(va);
            va.View();
            Console.WriteLine();

            Matrix a = new Matrix(aa);
            //var g = new Gauss(va.size);
            //Vector v = new Vector(va);

            Console.WriteLine("Метод Гаусса: ");
            Gauss.Calc(new Matrix(a), new Vector(va)).View();

            Console.WriteLine();

            Console.WriteLine("\nПроверка метода Гаусса:");
            (a * Gauss.Calc(new Matrix(a), new Vector(va))).View();
        }

        /// <summary>
        /// Метод Грамма-Шмидта
        /// </summary>
        private static void GramSchmidtTest()
        {
            const int A1 = 4;
            const int A2 = 4;

            Matrix aa = new Matrix(new double[A1, A2]);
            Vector va = new Vector(A1);

            Console.WriteLine("Первая матрица: ");
            ram(aa);
            aa.View();
            Console.WriteLine();

            Console.WriteLine("Первый вектор: ");
            rav(va);
            va.View();
            Console.WriteLine();

            Matrix m = new Matrix(aa);
            var g = new GramSchmidt(va.size);
            //Vector t = new Vector(va);

            Console.WriteLine("Метод Грама-Шмидта: ");
            g.Calc(m, va).View();

            Console.WriteLine();

            Console.WriteLine("\nПроверка метода Грама-Шмидта:");
            (m * g.Calc(m, va)).View();

            Console.WriteLine();
        }

        /// <summary>
        /// Метод Ортогонализации
        /// </summary>
        private static void OrthoTest()
        {
            const int A1 = 4;
            const int A2 = 4;

            Matrix aa = new Matrix(new double[A1, A2]);
            Vector va = new Vector(A1);

            Console.WriteLine("Первая матрица: ");
            ram(aa);
            aa.View();
            Console.WriteLine();

            Console.WriteLine("Первый вектор: ");
            rav(va);
            va.View();
            Console.WriteLine();

            Matrix m = new Matrix(aa);
            //Vector t = new Vector(va);

            Console.WriteLine("Метод Ортогонализации: ");
            Ortogonolization.Calc(m, va).View();

            Console.WriteLine();

            Console.WriteLine("\nПроверка метода Ортогонализации:");
            (m * Ortogonolization.Calc(m, va)).View();

            Console.WriteLine();
        }

        /// <summary>
        /// Метод Итераций
        /// </summary>
        public static void IterationTest()
        {
            const int A1 = 4;
            const int A2 = 4;

            Matrix aa = new Matrix(new double[A1, A2]);
            Vector va = new Vector(A1);

            Console.WriteLine("Первая матрица: ");
            //ram(aa);

            aa[0, 0] = 0.01;
            aa[0, 1] = 0.01;
            aa[0, 2] = 0.01;
            aa[0, 3] = 0.01;
            aa[1, 0] = 0;
            aa[1, 1] = 0.01;
            aa[1, 2] = 0.01;
            aa[1, 3] = 0.01;
            aa[2, 0] = 0.01;
            aa[2, 1] = 0;
            aa[2, 2] = 0.01;
            aa[2, 3] = 0.01;
            aa[3, 0] = 0.01;
            aa[3, 1] = 0.01;
            aa[3, 2] = 0;
            aa[3, 3] = 0.01;


            aa.View();
            Console.WriteLine();

            Console.WriteLine("Первый вектор: ");
            rav(va);
            va.View();
            Console.WriteLine();

            Matrix m = new Matrix(aa);
            //Vector t = new Vector(va);

            Console.WriteLine("Метод Итераций: ");

            var v = Iteration.Calc(m, va, 0.00001);

            if (v != null)
            {
                v.View();

                Console.WriteLine();

                Console.WriteLine("\nПроверка метода Итераций:");
                (va).View();
            }

            Console.WriteLine();
        }
    }
}
