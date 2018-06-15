using System;
using System.Diagnostics;

namespace ChislMethods.WorkTesters
{
    /// <summary>
    /// Тестер интегрирования
    /// </summary>
    public static class IntegralTEST
    {
        /// <summary>
        /// Точка входа тестера
        /// </summary>
        public static void TEST()
        {
            try
            {
                Console.WriteLine("Функция x*x\n");
                Integral.Integral.F func = x => Math.Cos(x*5)+5;
                Stopwatch sw = new Stopwatch();
                sw.Start();

                Console.WriteLine("Метод прямоугольников");
                Console.WriteLine(Integral.Integral.CalcRectangle(11, 20, 0.001, func));
                sw.Stop();
                Console.WriteLine("Время расчёта");
                Console.WriteLine(sw.ElapsedTicks);
                sw.Restart();

                Console.WriteLine("\nМетод трапеций");
                Console.WriteLine(Integral.Integral.CalcTrapezium(11, 20, 0.00001, func));
                sw.Stop();
                Console.WriteLine("Время расчёта");
                Console.WriteLine(sw.ElapsedTicks);

                Console.WriteLine("\nМетод параллельного Cимпсона");
                GC.TryStartNoGCRegion(999999);
                sw.Restart();
                Console.WriteLine(Integral.Integral.ParallelSimpson(-1000000, 1000000, 0.00001, func, 4));
                sw.Stop();
                Console.WriteLine("Время расчёта");
                Console.WriteLine(sw.ElapsedTicks);

                Console.WriteLine("\nМетод Cимпсона");
                sw.Restart();
                GC.Collect();
                Console.WriteLine(Integral.Integral.CalcSimpson(-1000000, 1000000, 0.00001, func));
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
