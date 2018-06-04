using ChislMethods.FindFuncs;
using System;

namespace ChislMethods.WorkTesters
{
    public static class FincValueFindersTest
    {
        public static void TEST()
        {
            try
            {
                Console.WriteLine("Рекомендуемое значение: a = -0.5 b = 5 eps = 0.01");

                Console.WriteLine("Введите a: ");
                double a = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Введите b: ");
                double b = Convert.ToDouble(Console.ReadLine());

                //double x;

                Console.WriteLine("Введите eps: ");

                double eps = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine(); ;

                Console.Write("Метод половинного деления: ");
                Console.Write(HalfDif.PolDel(eps, a, b, x => (x * x-1)));

                Console.WriteLine();

                Console.Write("Метод Ньютона: ");
                Console.Write(NewtonMethod.Newton(eps, a, b, x => (x * x - 1)));

                Console.WriteLine();

                Console.Write("Метод Последовательного приближения: ");
                Console.Write(IterationMethod.Iteration(a, b, eps, x => (x*x - 1)));

                Console.WriteLine();
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
