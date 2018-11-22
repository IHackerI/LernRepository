using ChislMethods.Integral;
using ChislMethods.WorkTesters.Helpers;
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

                Console.WriteLine("Метод прямоугольников");
                Console.WriteLine("S = " + Integral.Integral.CalcRectangle(11, 20, 0.001, func));
                

                Console.WriteLine("\nМетод трапеций");
                Console.WriteLine("S = " + Integral.Integral.CalcTrapezium(11, 20, 0.00001, func));

                Console.WriteLine("\nМетод Cимпсона");
                Console.WriteLine("S = " + Integral.Integral.CalcSimpson(11, 20, 0.00001, func));

                Console.WriteLine("\nМетод Cимпсона для функции двух переменных\n");
                Console.WriteLine("Формула для расчёта: x * x + y * y");

                var N = 5; // Количество узлов сетки

                var xBot = 11;
                var xTop = 20;

                var yBot = 11;
                var yTop = 20;
                
                Console.WriteLine("Узлов сетки: " + N + "\n");
                
                var ans = Integral.Integral.CalcSimpson2Var(xBot, xTop, yBot, yTop, N, Parabloid);
                Console.WriteLine("V = " + ans);

            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        static double Parabloid(double x, double y)
        { return x * x + y * y; }
    }
}
