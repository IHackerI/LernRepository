using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChislMethods.Spline
{
    public static class SplineTest
    {
        public static void TEST()
        {
            Spline spline = new Spline();

            double[] x = new double[] { -3, -2.5, -2, -1.5, -1, -0.5, 0, 0.5, 1, 1.5, 2, 2.5, 3 };
            double[] y = new double[x.Length];

            for (int i = 0; i < x.Length; i++)
                y[i] = 2 * x[i] * x[i] * x[i] + 3 * x[i] * x[i] - x[i] - 5;
            spline.Build(x, y, x.Length);

            for (double xt = -2; xt <= 2.0; xt += 0.25)
                Console.WriteLine("х = {0}\t\tspline = {1}\t\tПроверка ={2}", xt, spline.Interpolate(xt), 2 * xt * xt * xt + 3 * xt * xt - xt - 5);

            Console.ReadLine();
        }
    }
}
