using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChislMethods.FindFuncs
{
    public static class NewtonMethod
    {
        public static double Newton(double eps, double left, double right, DelF Func)
        {
            var curr = left + (right - left) / 2;

            bool flag = false;
            double nextCurr = 0;
            double incr = 0;
            double delta;
            do
            {
                if (flag)
                    curr = nextCurr;

                double der = (Func(curr + eps / 2) - Func(curr)) / (eps / 2);
                nextCurr = curr - Func(curr) / der;
                delta = Math.Abs(nextCurr - curr);

                if ((delta <= incr) || !flag)
                    incr = delta;
                else return Double.NaN;

                flag = true;
            } while (delta > eps);
            return nextCurr;
        }
    }
}
