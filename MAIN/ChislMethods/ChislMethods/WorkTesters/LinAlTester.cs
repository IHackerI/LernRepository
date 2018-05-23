using System;
using ChislMethods.LinAl;
using ChislMethods.WorkTesters.Helpers;
using static ChislMethods.WorkTesters.WorkMainTester;

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
                var ch = false;

                while (!ch)
                {
                    ch = IOSystem.InterfacedViewChoice(new string[] 
                    {
                        "Метод наименьших квадратов:",
                        "Метод Гаусса",
                        "Метод Грама-Шмидта"
                    },
                    
                    new EmptyD[] 
                    {
                        LSMTest,
                        GaussTest,
                        GramSchmidtTest
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

        private static void LSMTest()
        {
            Console.WriteLine("Метод наименьших квадратов:");
            Console.WriteLine();

            //Исходные данные
            double[] x = new double[] { -2, -1, 0, 1, 2 };
            double[] y = new double[] { 15, 4, 1, 0, -5 };

            while (true)
            {
                // Создание экземляра класса LSM
                LeastSquareMethod myReg = new LeastSquareMethod(x, y);

                var ch = IOSystem.SafeSimpleChoice("Выберите действие: ", new string[]
                {
                    "Вывести исходные данные",
                    "Вычислить полином",
                    "Закончить тест"
                });

                var endWork = false;

                switch (ch)
                {
                    case 0:
                        Console.Write("X= ");
                        foreach (var val in x)
                        {
                            Console.Write(val.ToString().PadRight(2) + ", ");
                        }
                        Console.WriteLine();
                        Console.Write("Y= ");
                        foreach (var val in y)
                        {
                            Console.Write(val.ToString().PadRight(2) + ", ");
                        }
                        Console.WriteLine();
                        break;
                    case 1:
                        myReg.Polynomial(IOSystem.GetInt("Введите степень: "));
                        // Вывод коэффициентов а0 и а1
                        Console.Write("y= ");
                        for (int i = 0; i < myReg.Coeff.Length; i++)
                        {
                            if (i > 0 && myReg.Coeff[i]>=0)
                                Console.Write(" + ");
                            if (myReg.Coeff[i] < 0)
                                Console.Write(" - ");
                            if (Math.Abs(myReg.Coeff[i]) == 1)
                                Console.Write("x^" + i);
                            else
                                Console.Write(Math.Abs(myReg.Coeff[i]) + " * x^" + i);
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                        // Среднеквадратичное отклонение
                        Console.WriteLine("Среднеквадратичное отклонение: " + myReg.Delta);
                        Console.WriteLine();
                        break;
                    case 2:
                        endWork = true;
                        break;
                }

                if (endWork)
                    break;
            }
        }

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
            Vector v = new Vector(va);

            Console.WriteLine("Метод Гаусса: ");
            v.Gauss(a).View();

            Console.WriteLine();

            Console.WriteLine("\nПроверка метода Гаусса:");
            (a * v.Gauss(a)).View();
        }

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
            Vector t = new Vector(va);

            Console.WriteLine("Метод Грама-Шмидта: ");
            t.GramSchmidt(m).View();

            Console.WriteLine();

            Console.WriteLine("\nПроверка метода Грама-Шмидта:");
            (m * t.GramSchmidt(m)).View();

            Console.WriteLine();
        }
    }
}
