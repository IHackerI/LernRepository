using System;
using ChislMethods.LinAl;
using ChislMethods.DerSystems.TMP;

namespace ChislMethods.WorkTesters
{
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
        
        public static void TEST()
        {
            try
            {
                BigTest();
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
            Vector v = new Vector(va);

            Console.WriteLine("Метод Гаусса: ");
            v.Gauss(a).View();

            Console.WriteLine();

            Console.WriteLine("\nПроверка метода Гаусса:");
            (a * v.Gauss(a)).View();

            Console.WriteLine();

            Matrix m = new Matrix(aa);
            Vector t = new Vector(va);

            Console.WriteLine("Метод Грама-Шмидта: ");
            t.GramSchmidt(m).View();

            Console.WriteLine();

            Console.WriteLine("\nПроверка метода Грама-Шмидта:");
            (m * t.GramSchmidt(m)).View();

            Console.WriteLine();

            Matrix k = new Matrix(aa);
            Vector p = new Vector(va);

            Console.WriteLine();

            SmallTest();
        }

        static void SmallTest()
        {
            Console.WriteLine("Метод наименьших квадратов:");
            Console.WriteLine();

            //Исходные данные
            double[] x = new double[] { -2, -1, 0, 1, 2 };
            double[] y = new double[] { 15, 4, 1, 0, -5 };

            // Создание экземляра класса LSM
            LeastSquareMethod myReg = new LeastSquareMethod(x, y);

            // Апроксимация заданных значений линейным полиномом
            myReg.Polynomial(1);

            // Вывод коэффициентов а0 и а1
            for (int i = 0; i < myReg.Coeff.Length; i++)
            {
                Console.WriteLine(myReg.Coeff[i]);
            }
            Console.WriteLine();
            // Среднеквадратичное отклонение
            Console.WriteLine(myReg.Delta);
            Console.WriteLine();

            // Апроксимация заданных значений квадратным полиномом
            myReg.Polynomial(2);
            // Вывод коэффициентов а0, а1 и а2
            for (int i = 0; i < myReg.Coeff.Length; i++)
            {
                Console.WriteLine(myReg.Coeff[i]);
            }
            Console.WriteLine();
            // Среднеквадратичное отклонение
            Console.WriteLine(myReg.Delta);
        }
    }
}
