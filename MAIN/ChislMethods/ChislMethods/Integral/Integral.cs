﻿using ChislMethods.LinAl;
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

        /// <summary>
        /// Интегрирование Методом Симпсона для двойного интеграла
        /// </summary
        public static double CalcSimpson2Var(double xBot, double xTop, double yBot, double yTop, int N, F2 func)
        {
            Matrix testMat = new Matrix(2 * N, 2 * N);

            double h = (xTop - xBot) / (2 * N); //Шаг по х
            double k = (yTop - yBot) / (2 * N); //Шаг по у

            double[] str = new double[2 * N];

            str[0] = 1;

            bool flag = false;

            for (int i = 1; i < str.Length - 1; i++)
            {
                if (flag)
                    str[i] = 2;
                else
                    str[i] = 4;

                flag = !flag;
            }

            str[str.Length - 1] = 1;

            double sum = 0;

            for (int i = 0; i < 2 * N; i++)
            {
                for (int j = 0; j < 2 * N; j++)
                {
                    var lambda = str[i];

                    if (j > 0 && j < (2 * N) - 1)
                    {
                        if ((j & 1) == 1)
                            lambda *= 4;
                        else
                            lambda *= 2;
                    }

                    testMat[i, j] = lambda;

                    sum += lambda * func(xBot + h * i, yBot + k * j);
                }
            }

            testMat.View();

            return (h * k * sum) / 9;
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

        private static double OneStep(double xBot, double xTop, F f, int n, ref double f2)
        {
            double f0 = f(xBot) + f(xTop);

            double f1 = 0;
            var h = (xTop - xBot) / n;

            for (int i = 1; i < n; i += 2)
            {
                f1 += f(xBot + h * i);
            }

            var ans = (h / 3) * (f0 + 4 * f1 + 2 * f2);

            f2 += f1;
            return ans;
        }

        /// <summary>
        /// Интегрирование Методом прямоугольников с запоминанием предыдущих значений
        /// </summary>
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
