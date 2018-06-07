﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChislMethods.DifUr
{
    public delegate double[] FuncDelegate(double t, double[] x);

    /// <summary>
    /// Решение дифференциальных уравнений методом Эйлера
    /// </summary>
    public class Euler
    {
        double a; // начало отрезка
        double b; // конец отрезка
        double h; // шаг
        double[] x; // вектор начальных состояний

        public Euler(double a, double b, double[] xn, double h)
        {
            this.a = a;
            this.b = b;
            this.h = h;
            x = xn;
        }

        /// <summary>
        /// Решение дифференциальных уравнений методом Эйлера
        /// </summary>
        public double[,] Calculate(FuncDelegate func)
        {
            int n; //количество шагов
            double[] f1;

            n = (int)((b - a) / h);//количество шагов

            double[,] xr = new double[n + 1, x.Length];
            double t = a;
            double[] pr = new double[x.Length];

            for (int j = 0; j < x.Length; j++)
                xr[0, j] = x[j];
            for (int i = 1; i <= n; i++)
            {

                f1 = func(t, x); //1
                t = t + h;

                for (int k = 0; k < x.Length; k++)
                    x[k] = x[k] + h * f1[k];
                for (int j = 0; j < x.Length; j++)
                {
                    xr[i, j] = x[j];
                }
            }
            return xr;

        }
    }
}