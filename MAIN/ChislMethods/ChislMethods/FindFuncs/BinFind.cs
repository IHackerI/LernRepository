using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChislMethods.FindFuncs
{
    public static class BinFind
    {
        public static double PolDel(double eps, double left, double right, DelF func)
        {
            var length = right - left;
            var error = length;
            double Fmin = func(left);
            double Fmax = func(right);
            if (Fmin * Fmax > 0) return Double.NaN;
            while (error > eps)
            {
                double x = (left + right) / 2;
                double Fx = func(x);
                if (Fmin * Fx < 0)
                {
                    right = x;
                }
                else
                {
                    left = x;
                    Fmin = Fx;
                }

                error = (right - left);
            }
            return (left + right) / 2;
        }
    }
}
