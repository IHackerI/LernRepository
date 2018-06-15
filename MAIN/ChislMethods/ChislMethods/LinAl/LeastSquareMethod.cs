
using System;
using System.Linq;

namespace ChislMethods.LinAl
{
    /// <summary>
    /// Решение системы линейных уравнений (матрицы) Методом наименьших квадратов
    /// </summary>
    public class LeastSquareMethod
    {
        Func<double, double>[] _func;
        Vector _koeff;
        public Vector Koeff { get { return _koeff; } }

        public LeastSquareMethod(Func<double, double>[] func)
        {
            _func = func;
        }

        //#warning CheckAprox
        public double CalcApprox(double x/*, Vector Koeff, Func<double, double>[] func*/)
        {
            var res = 0.0;
            for (var index = 0; index < _koeff.Size; ++index)
                res += _koeff[index] * _func[index](x);
            return res;
        }

        /// <summary>
        /// Решение системы линейных уравнений (матрицы) Методом наименьших квадратов
        /// </summary>
        public Vector Calculate(/*Func<double, double>[] func, */Vector x, Vector y)
        {
            var b = new Vector(_func.Length);
            var ksi = new Matrix(x.Size, _func.Length);

            for (var index = 0; index < ksi.Col; ++index)
            {
                ksi[index, 0] = 1;

                for (var inner = 0; inner < x.Size; ++inner)
                {
                    // для каждого элемента x находим ksi(x)
                    ksi[inner, index] = _func[index](x[inner]);
                    // находим правую часть уравнения
                    b[index] += ksi[inner, index] * y[inner];
                }
            }

            _koeff = Gauss.Calculate((Matrix.Transposition(ksi) * ksi), b);

            return _koeff;
        }
    }
}
