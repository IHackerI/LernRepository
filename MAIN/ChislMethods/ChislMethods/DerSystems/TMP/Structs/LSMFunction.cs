namespace ChislMethods.DerSystems.TMP.Structs
{
    using System;

    /// <summary>
    /// Апроксимирующая функция метода наименьших квадратов
    /// </summary>
    public class LSMFunction
    {
        /// <summary>
        /// Члены функции
        /// </summary>
        private Func<double, double>[] functions;

        /// <summary>
        /// Коэффиценты при членах функции
        /// </summary>
        private Vector a;

        public LSMFunction(Vector a, Func<double, double>[] functions)
        {
            this.functions = functions;
            this.a = a;
        }

        public double this[double x]
        {
            get
            {
                var res = 0.0;
                for (var index = 0; index < a.Size; ++index)
                    res += a[index] * functions[index](x);
                return res;
            }
        }

        public Vector Koeff => new Vector(this.a);
    }
}