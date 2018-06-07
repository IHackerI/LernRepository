using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChislMethods.DifUr
{
    /// <summary>
    /// Решение дифференциальных уравнений методом Рунге-Кутта 4-го порядка
    /// </summary>
    public class RungeKutta4
    {
        double a, b; // начало и конец отрезка
        double h; //шаг
        double[] x; //вектор начальных состояний

        public RungeKutta4(double a, double b, double[] xn, double h)
        {
            this.a = a;
            this.b = b;
            this.h = h;
            x = xn;
        }

        /// <summary>
        /// Решение дифференциальных уравнений методом Рунге-Кутта 4-го порядка
        /// </summary>
        public double[,] Calculate(FuncDelegate func)
        {
            int n; //количество шагов
            double[] f1;
            double[] f2;
            double[] f3;
            double[] f4;

            n = (int)((b - a) / h);//количество шагов
            double[,] xr = new double[n + 1, x.Length];
            double t = a;
            double[] pr = new double[x.Length];

            for (int j = 0; j < x.Length; j++)//begin point
                xr[0, j] = x[j];
            for (int i = 1; i <= n; i++)
            {

                f1 = func(t, x);// first prav
                t = t + h / 2;
                for (int k = 0; k < x.Length; k++)
                    pr[k] = (x[k] + h * f1[k] / 2);

                f2 = func(t, pr); // second prav
                for (int k = 0; k < x.Length; k++)
                    pr[k] = (x[k] + h * f2[k] / 2);

                f3 = func(t, pr); //third prav
                t = t + h / 2;
                for (int k = 0; k < x.Length; k++)
                    pr[k] = (x[k] + h * f3[k]);

                f4 = func(t, pr);
                for (int k = 0; k < x.Length; k++)
                    x[k] = x[k] + (h / 6) * (f1[k] + f2[k] * 2 + f3[k] * 2 + f4[k]);

                for (int j = 0; j < x.Length; j++)
                {
                    xr[i, j] = x[j];
                }
            }
            return xr;
        }
    }
}