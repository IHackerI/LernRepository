using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChislMethods.DifUr
{
    /// <summary>
    /// Решение дифференциальных уравнений методом Рунге-Кутта 2-го порядка
    /// </summary>
    public class RungeKutt2
    {
        double a;   // начало отрезка
        double b;   // конец отрезка
        double h;   // шаг
        double[] x; //вектор начальных состояний

        public RungeKutt2(double a, double b, double[] xn, double h)
        {
            this.a = a;
            this.b = b;
            this.h = h;
            x = xn;
        }

        /// <summary>
        /// Решение дифференциальных уравнений методом Рунге-Кутта 2-го порядка
        /// </summary>
        public double[,] Calculate(FuncDelegate func)
        {            
            var n = (int)((b - a) / h); //количество шагов
            double[,] xr = new double[n + 1, x.Length];
            double t = a;
            double[] pr = new double[x.Length];

            for (int j = 0; j < x.Length; j++)
                xr[0, j] = x[j];
            for (int i = 1; i <= n; i++)
            {

                double[] f1 = func(t, x);
                t = t + h;
                for (int k = 0; k < x.Length; k++)
                    pr[k] = x[k] + h * f1[k];

                double[] f2 = func(t, pr);
                for (int k = 0; k < x.Length; k++)
                    x[k] = x[k] + (h / 2) * (f1[k] + f2[k]);

                for (int j = 0; j < x.Length; j++)
                {
                    xr[i, j] = x[j];
                }
            }
            return xr;
        }
    }
}