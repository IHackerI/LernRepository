using System;
using System.Diagnostics;

namespace ChislMethods.WorkTesters
{
    public static class IntegralTEST
    {
        public static void TEST()
        {
            try
            {
                Console.WriteLine("Функция x*x\n");
                Integral.Integral.F func = x => x * x;
                Stopwatch sw = new Stopwatch();
                sw.Start();

                Console.WriteLine("Метод прямоугольников");
                Console.WriteLine(Integral.Integral.Rectangle(11, 20, 0.001, func));
                sw.Stop();
                Console.WriteLine("Время расчёта");
                Console.WriteLine(sw.ElapsedTicks);
                sw.Restart();

                Console.WriteLine("\nМетод трапеций");
                Console.WriteLine(Integral.Integral.Trapezium(11, 20, 0.00001, func));
                sw.Stop();
                Console.WriteLine("Время расчёта");
                Console.WriteLine(sw.ElapsedTicks);
                sw.Start();

                Console.WriteLine("\nМетод Cимпсона");
                Console.WriteLine(Integral.Integral.Simpson(11, 20, 0.00001, func));
                sw.Stop();
                Console.WriteLine("Время расчёта");
                Console.WriteLine(sw.ElapsedTicks);
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
