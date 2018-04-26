using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ChislMethods.Integral
{
    public static class IntegralTEST
    {
        delegate double F(double x);

        public static void TEST()
        {
            F func = x => x * x;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //Console.WriteLine(Rectangle(11, 20, 0.001, func));
            //sw.Stop();
            //Console.WriteLine(sw.ElapsedTicks);
            //sw.Restart();
            Console.WriteLine(Trapezium(11, 20, 0.00001, func));
            sw.Stop();
            Console.WriteLine(sw.ElapsedTicks);
            sw.Start();
            Console.WriteLine(Simpson(11, 20, 0.00001, func));
            sw.Stop();
            Console.WriteLine(sw.ElapsedTicks);
            Console.ReadKey();
        }

        static double Simpson(double xBot, double xTop, double eps, F f)
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

        static double Rectangle(double xBot, double xTop, double eps, F f)
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

        static double Trapezium(double xBot, double xTop, double eps, F f)
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
