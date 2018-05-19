using System;
using ChislMethods.LinAl;

namespace ChislMethods.WorkTesters
{
    public static class LinAlTester
    {
        public static void ram(Matrix r)// заполнение матрицы  случайными числами
        {
            Random random = new Random((DateTime.Now.Millisecond));
            for (int i = 0; i < r.getR(); i++)
                for (int j = 0; j < r.getC(); j++)
                    r[i, j] = random.Next(0, 9); // Math.Round(r.NextDouble() * (10 - (-10)) + (-10), 1);
        }

        public static void rem(Matrix r)// заполнение матрицы не случайными числами
        {
            for (int i = 0; i < r.getR(); i++)
                for (int j = 0; j < r.getC(); j++)
                    r[i, j] = Convert.ToDouble(Console.ReadLine());
        }

        public static void rav(Vector r)// заполнение вектора  случайными числами
        {
            Random random = new Random((DateTime.Now.Millisecond));
            for (int i = 0; i < r.GetSize(); i++)
                r[i] = random.Next(1, 5);
        }

        public static void rev(Vector r)// заполнение вектора не случайными числами
        {
            for (int i = 0; i < r.GetSize(); i++)
                r[i] = Convert.ToDouble(Console.ReadLine());
        }
        
        public static double xmin, xmax;
        public static double pss1(double x, int j)
        {
            return Math.Pow(x, j);
        }
        public static double pss2(double x, int j)
        {
            return Math.Cos(j * Math.Acos(x));
        }
        public static double psCh(double x, int j)
        {
            double xn = 2 * (x - xmin) / (xmax - xmin) - 1.0;
            return Legandre(xn, j);
        }
        public static double Legandre(double x, int n)
        {
            double[] step = new double[n + 1];
            if (n == 0)
                return 1;
            else
                if (n == 1)
                return x;
            else
            {
                step[0] = 1;
                step[1] = x;
                for (int i = 2; i < n + 1; i++)
                {
                    step[i] = ((2 * n - 1) * x * step[i - 1] - (n - 1) * step[i - 2]) / n;
                }
            }
            return step[n];
        }

        public static void TEST()
        {
            try
            {
                BigTest();

                Console.WriteLine("Тут ещё есть кривой тест матриц... Запустить? y/n");
                if (Console.ReadLine().ToLower() == "y")
                {
                    Console.WriteLine();
                    SmallTest();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static void BigTest()
        {
            const int A1 = 4;
            const int A2 = 4;

            //const int B1 = 3;
            //const int B2 = 3;

            Matrix aa = new Matrix(A1, A2);
            Vector va = new Vector(A1);

            //Matrix bb = new Matrix(B1, B2);
            //Vector vb = new Vector(B1);

            Console.WriteLine("Первая матрица: ");
            ram(aa);
            //rem(aa);
            aa.View();
            Console.WriteLine();


            Console.WriteLine("Первый вектор: ");
            rav(va);
            //rev(va);
            va.View();
            Console.WriteLine();


            //Console.WriteLine("Вторая матрица: ");
            //ram(bb);
            //bb.View();
            //Console.WriteLine();

            //rav(vb);

            //Console.WriteLine("Второй вектор: ");
            //vb.View();
            //Console.WriteLine();

            Matrix a = new Matrix(aa);
            Vector v = new Vector(va);

            Console.WriteLine("Метод Гаусса: ");
            v.Gauss(a).View();

            Console.WriteLine();

            Console.WriteLine("\nПроверка метода Гаусса:");
            a.MultiplyVector(v.Gauss(a)).View();

            Console.WriteLine();

            Matrix m = new Matrix(aa);
            Vector t = new Vector(va);

            Console.WriteLine("Метод Грама-Шмидта: ");
            t.GramSchmidt(m).View();

            Console.WriteLine();

            Console.WriteLine("\nПроверка метода Грама-Шмидта:");
            m.MultiplyVector(t.GramSchmidt(m)).View();

            Console.WriteLine();

            Matrix k = new Matrix(aa);
            Vector p = new Vector(va);

            Console.WriteLine();
        }

        static void SmallTest()
        {
            LeastSquareMethod test = new LeastSquareMethod();

            double[,] a = {
                              {-2,-1,0,1,2},
                              {15,4,1,0,-5}};
            double[,] ab = {
                              {-0.75,-0.5,0,0.5,0.75},
                              {0.73,0.87,1,0.87,0.73}};
            xmin = -2.0; xmax = 2.0;
            test.Minkv(a, 4, psCh);


            //Console.WriteLine(test.Legandre(3, 3));


            for (double xt = -2; xt <= 2.0; xt += 0.25)
            {
                //Console.Write("xt={0}\t  minkv={1}\t analitic={2}", xt, test.resh2(xt, psCh), 1 - xt + xt * xt - xt * xt * xt);
                Console.Write(("xt=" + (xt >= 0 ? "+" : "") + xt).PadRight(15));
                Console.Write(("minkv=" + test.resh2(xt, psCh)).PadRight(15));
                Console.Write(("analitic=" + (1 - xt + xt * xt - xt * xt * xt)).PadRight(15));
                Console.WriteLine();
            }
            //Console.ReadKey();
            //for (double xt = -1; xt <= 1.0; xt += 0.25)
            //    Console.WriteLine("xt={0}  minkv={1} analitic={2}", xt, test.resh2(xt), Math.Cos(xt));
            //Console.ReadKey();
            //Console.ReadKey();
        }
    }
}
