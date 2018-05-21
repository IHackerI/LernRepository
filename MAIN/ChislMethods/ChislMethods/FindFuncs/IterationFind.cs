using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChislMethods.FindFuncs
{
    public static class IterationFind
    {
        public static double Iteration(double Xn,/*Текущий аргумент*/ double eps,/*Точность*/  DelF Func /*Уравнение*/) //Метод последовательных приближений
        {
            double x = 0;
            int i = 0;
            bool error = false;

            do
            {
                x = Func(Xn);
                i++;
                if (Math.Abs(x - Xn) >= eps && i == 1000)
                {
                    error = true;
                    break;
                }
                Xn = x;
            } while (Math.Abs(Xn - Func(Xn)) > eps);
            if (error) return Double.NaN;
            else return x;
        }
    }
}
