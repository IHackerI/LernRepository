using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChislMethods.FindFuncs
{

    /// <summary>
    /// Поиск корней уравнения методом итераций
    /// </summary>
    public static class IterationMethod
    {
        /// <summary>
        /// Поиск корней уравнения методом итераций
        /// </summary>
        public static double Calculate(double left, double right, double eps, DelFunc Func)
        {
            int max_iter= 1000;
            double x1 = left + (right - left) / 2;
            double x0;

            double oDelta, nDelta = 0;

            int iterations = 0;
            
            //#warning CheckDelta

            do
            {
                oDelta = nDelta;

                x0 = x1;

                var M = -(Func(x1 + eps) - Func(x1 - eps)) / (2 * eps);
                x1 = x0 + Func(x0) / M;

                iterations++;

                nDelta = x1 - x0;
            } while (((Math.Abs(nDelta) >= eps) || (iterations > max_iter)) && (iterations<2 || Math.Abs(nDelta) < Math.Abs(oDelta)));

            if (Math.Abs(x1 - x0) <= eps)
                return x1;
            else
                return double.NaN;
        }
    }
}
