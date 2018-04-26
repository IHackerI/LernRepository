using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChislMethods.FindFuncs
{
    public static class FincValueFindersTest
    {
        public static void TEST()
        {
            Console.WriteLine("Введите a: ");
            double a = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Введите b: ");
            double b = Convert.ToDouble(Console.ReadLine());

            //double x;

            Console.WriteLine("Введите eps: ");

            double eps = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine(); ;

            Console.Write("Метод половинного деления: ");
            Console.Write(BinFind.PolDel(eps, a, b, x => Math.Pow(x, 2) - 1));

            Console.WriteLine();

            Console.Write("Метод Ньютона: ");
            Console.Write(NewtonFind.Newton(eps, a, x => Math.Pow(x, 2) - 1));

            Console.WriteLine();

            Console.Write("Метод Последовательного приближения: ");
            Console.Write(IterationFind.Iteration(a, eps, x => Math.Pow(x, 2) - 1));

            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
