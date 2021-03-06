﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChislMethods.DerSystems
{
    public delegate double[] FunDelegate(double t, double[] x);
    public class RangeKutta
    {
        double a, b;//отрезок
        double h;//шаг
        double[] x;//вектор начальных состояний

        public RangeKutta(double a, double b, double[] xn, double h)
        {
            this.a = a;
            this.b = b;
            this.h = h;
            x = xn;
        }

        public double[,] MetodEulera(FunDelegate fun)
        {
            int n;//количество шагов
            double[] f1;


            n = (int)((b - a) / h);//количество шагов
            double[,] xr = new double[n + 1, x.Length];
            double t = a;
            double[] pr = new double[x.Length];

            for (int j = 0; j < x.Length; j++)
                xr[0, j] = x[j];
            for (int i = 1; i <= n; i++)
            {

                f1 = fun(t, x); //1
                t = t + h;

                for (int k = 0; k < x.Length; k++)
                    x[k] = x[k] + h * f1[k];
                Console.Write(("t=" + t).PadRight(10));
                for (int j = 0; j < x.Length; j++)
                {
                    xr[i, j] = x[j];
                    WriteArgs(j, x[j], Math.Exp(t));
                }
                Console.WriteLine();

            }
            return xr;

        }

        public double[,] MetodRK2(FunDelegate fun)
        {
            int n;//количество шагов
            double[] f1;
            double[] f2;


            n = (int)((b - a) / h);//количество шагов
            double[,] xr = new double[n + 1, x.Length];
            double t = a;
            double[] pr = new double[x.Length];

            for (int j = 0; j < x.Length; j++)
                xr[0, j] = x[j];
            for (int i = 1; i <= n; i++)
            {

                f1 = fun(t, x); //1
                t = t + h;
                for (int k = 0; k < x.Length; k++)
                    pr[k] = x[k] + h * f1[k];

                f2 = fun(t, pr);
                for (int k = 0; k < x.Length; k++)
                    x[k] = x[k] + (h / 2) * (f1[k] + f2[k]);

                Console.Write(("t=" + t).PadRight(10));
                for (int j = 0; j < x.Length; j++)
                {
                    xr[i, j] = x[j];
                    WriteArgs(j, x[j], Math.Exp(t));
                }
                Console.WriteLine();
            }
            return xr;
        }

        public double[,] MetodRK4(FunDelegate fun)
        {
            int n;//количество шагов
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

                f1 = fun(t, x);// first prav
                t = t + h / 2;
                for (int k = 0; k < x.Length; k++)
                    pr[k] = (x[k] + h * f1[k] / 2);

                f2 = fun(t, pr); // second prav
                for (int k = 0; k < x.Length; k++)
                    pr[k] = (x[k] + h * f2[k] / 2);

                f3 = fun(t, pr); //third prav
                t = t + h / 2;
                for (int k = 0; k < x.Length; k++)
                    pr[k] = (x[k] + h * f3[k]);

                f4 = fun(t, pr);
                for (int k = 0; k < x.Length; k++)
                    x[k] = x[k] + (h / 6) * (f1[k] + f2[k] * 2 + f3[k] * 2 + f4[k]);

                Console.Write(("t=" + t).PadRight(10));
                for (int j = 0; j < x.Length; j++)
                {
                    xr[i, j] = x[j];
                    WriteArgs(j, x[j], Math.Exp(t));
                }
                Console.WriteLine();
            }
            return xr;
        }

        private void WriteArgs(params object[] args)
        {
            Console.Write(("x["+ args[0] + "]="+ args[1]).PadRight(20) + " xa=" + args[2]);
        }
    }
}