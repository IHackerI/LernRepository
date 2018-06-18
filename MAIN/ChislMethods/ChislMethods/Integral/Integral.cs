using System;
using System.Threading.Tasks;

namespace ChislMethods.Integral
{
    /// <summary>
    /// Класс интегрирования
    /// </summary>
    public class Integral
    {
        public delegate double F(double x);
        public delegate double F2(double x, double y);
        
        public static double CalcSimpson2(double xBot, double xTop, int N, F2 func)
        {
            double sum1, sum2, sum3;   // временные переменные
            double sum;                // конечный результат
            double h = (2 * xTop) / N; // шаг сетки

            sum1 = sum2 = sum3 = 0;

            sum1 = func(xBot, -xTop) + func(xBot, +xTop);

            for (int i = 1; i <= N - 1; i++)
                sum2 += func(xBot, -xTop + (i * h));
            sum2 *= 2;

            for (int i = 1; i <= N; i++)
                sum3 += func(xBot, -xTop + ((i - 0.5) * h));
            sum3 *= 4;

            sum = sum1 + sum2 + sum3;
            sum = (h / 6) * sum;
            return sum;
        }

        /// <summary>
        /// Интегрирование Методом Симпсона
        /// </summary
        public static double CalcSimpson(double xBot, double xTop, double eps, F f)
        {
            int n = 1;
            double f2 = 0, os = 0, ns = 1;
            while (Math.Abs(Math.Abs(os) - Math.Abs(ns)) > eps)
            {
                os = ns;
                n *= 2;
                ns = OneStep(xBot, xTop, f, n, ref f2);
            }
            return ns;
        }
        
        public static double ParallelSimpson(double xBot, double xTop, double eps, F f, int thCount)
        {
            var step = (xTop - xBot) / thCount;
            double ns = 0;

            Parallel.For(0, thCount, delegate (int i)
            {
                ns += CalcSimpson(xBot + (i * step), xBot + ((i + 1) * step), eps, f);
            });

            return ns;
        }

        private static double OneStep(double xBot, double xTop, F f, int n,ref double f2)
        {
            double f0 = f(xBot) + f(xTop);
            
            double f1 = 0;
            var h = (xTop - xBot) / n;

            for (int i = 1; i < n; i+=2)
            {
                f1 += f(xBot + h * i);
            }
            
            var ans = (h / 3) * (f0 + 4 * f1 + 2 * f2);
            
            f2 += f1;
            return ans;
        }
        
        /// <summary>
        /// Интегрирование Методом прямоугольников с запоминанием предыдущих значений
        /// </summary>ы
        public static double CalcRectangle(double xBot, double xTop, double eps, F f)
        {
            int n = 1;
            double s = 0;
            double prev = 0, h;
            do
            {
                prev = s;
                n *= 2;
                h = (xTop - xBot) / n;
                for (int i = 0; i < n; i++)
                {
                    s += f(xBot + h * (i + 0.5));
                }
                s *= h;
            } while (Math.Abs(s - prev) > eps);
            return s;
        }

        /// <summary>
        /// Интегрирование Методом трапеций
        /// </summary>
        public static double CalcTrapezium(double xBot, double xTop, double eps, F f)
        {
            double x = xBot;
            int n = 1;
            double h = (xTop - xBot) / n;
            double prev, s = 0, res = 0;
            do
            {
                prev = res;
                n *= 2;
                h = (xTop - xBot) / n;
                for (long i = 1; i < n; i = i + 2)
                {
                    s += f(xBot + i * h);
                }
                res = h / 2 * (f(xBot) + 2 * s + f(xTop));
            } while (Math.Abs(res - prev) > eps);
            return res;
        }
    }
}
