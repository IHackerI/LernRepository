
using System;
using System.Linq;

namespace ChislMethods.LinAl
{
    public delegate double Psi(double x, int number);

    /// <summary>
    /// Метод наименьших квадратов
    /// </summary>
    public class LeastSquareMethod
    {
        public static double CalcApprox(double x, Vector Koeff, Func<double, double>[] func)
        {
            var res = 0.0;
            for (var index = 0; index < Koeff.Size; ++index)
                res += Koeff[index] * func[index](x);
            return res;
        }

        /// <summary>
        /// Least square method
        /// </summary>
        public static Vector LSM(Func<double, double>[] func, Vector x, Vector y)
        {
            var b = new Vector(func.Length);
            var ksi = new Matrix(x.Size, func.Length);

            for (var index = 0; index < ksi.Col; ++index)
            {
                ksi[index, 0] = 1;

                for (var inner = 0; inner < x.Size; ++inner)
                {
                    // для каждого элемента x находим ksi(x)
                    ksi[inner, index] = func[index](x[inner]);
                    // находим правую часть уравнения
                    b[index] += ksi[inner, index] * y[inner];
                }
            }
            
            return Gauss.Calc((Matrix.Transposition(ksi) * ksi), b);
        }
    }
}
