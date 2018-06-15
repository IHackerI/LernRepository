
using System;
using System.Linq;

namespace ChislMethods.LinAl
{
    /// <summary>
    /// Решение системы линейных уравнений (матрицы) Методом наименьших квадратов
    /// </summary>
    public class LeastSquareMethod
    {
        #warning CheckAprox
        public static double CalcApprox(double x, Vector Koeff, Func<double, double>[] func)
        {
            var res = 0.0;
            for (var index = 0; index < Koeff.Size; ++index)
                res += Koeff[index] * func[index](x);
            return res;
        }

        /// <summary>
        /// Решение системы линейных уравнений (матрицы) Методом наименьших квадратов
        /// </summary>
        public static Vector Calculate(Func<double, double>[] func, Vector x, Vector y)
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
            
            return Gauss.Calculate((Matrix.Transposition(ksi) * ksi), b);
        }
    }
}
