using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChislMethods.Integral
{
    /// <summary>
    /// Класс интегрирования
    /// </summary>
    public class Integral
    {
        public delegate double F(double x);

        /// <summary>
        /// Интегрирование Методом Симпсона
        /// </summary
        public static double CalcSimpson(double xBot, double xTop, double eps, F f)
        {
            int n = 1;
            double f0 = f(xBot) + f(xTop);
            double f1, f2 = 0, h, s = 0, res = 1;
            while (Math.Abs(Math.Abs(s) - Math.Abs(res)) > eps)
            {
                f1 = 0;
                s = res;
                n *= 2;
                h = (xTop - xBot) / n;
                for (int i = 1; i < n; i += 2)
                {
                    f1 += f(xBot + h * i);
                }
                res = h / 3 * (f0 + 4 * f1 + 2 * f2);
                f2 += f1;
            }
            return res;
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
