using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChislMethods.FindFuncs
{
    public static class IterationFind
    {
        public static double Iteration(double left, double right, double eps, DelF Func)
        {
            int max_iter= 1000;
            double x1 = left + (right - left) / 2;
            double x0;

            int iterations = 0;

            do
            {
                x0 = x1;

                var M = -(Func(x1 + eps) - Func(x1 - eps)) / (2 * eps);
                x1 = x0 + Func(x0) / M;

                iterations++;
            } while ((Math.Abs(x1 - x0) >= eps) || (iterations > max_iter));

            if (Math.Abs(x1 - x0) <= eps)
                return x1;
            else
                return double.NaN;
        }
    }
}
