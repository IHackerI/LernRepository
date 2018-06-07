namespace NumericalMethods
{
    using System;
    using System.Collections.Generic;

    using NumericalMethods.Structs;

    public static class MathExt
    {
        public static List<Vector> Euler(this Func<double, Vector, double>[] func, double begin, double end, Vector y, double h)
        {
            var result   = new List<Vector>();
            var buf      = new Vector(y.Size);
            Vector state = null;

            for (var i = begin; i < end; i += h)
            {
                // save current state
                state = new Vector(y.Size + 1) { i };
                for (var index = 1; index < y.Size + 1; ++index)
                    state[index] = y[index - 1];
                result.Add(state);

                y.Clone(buf);

                for (var index = 0; index < y.Size; ++index)
                    y[index] += h * func[index](i, buf);
            }

            // save last state
            state = new Vector(y.Size + 1) { end };
            for (var index = 1; index < y.Size+ 1; ++index)
                state[index] = y[index - 1];
            result.Add(state);

            return result;
        }

        /// <summary>
        /// Метод Адамса 3-го порядка
        /// </summary>
        /// <param name="func"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="y"></param>
        /// <param name="h"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static List<Vector> Adam(this Func<double, Vector, double>[] func, double begin, double end, Vector y, double h)
        { 
            var koeff    = new Vector(y.Size);
            var buf      = new Vector(y.Size);
            var step     = new Vector(y.Size);
            Vector state = null;

            // get first 2 step integration
            var result   = RungeKutte4(func, begin, begin +  h, y, h);

            Action<int> UpdateBuffer = k =>
            {
                for (var index = 0; index < y.Size; ++index)
                    buf[index] = result[result.Count - 1 - k][index + 1];
            };

            for (var i = begin + 2 * h; i < end; i += h)
            {
                // save current state
                state = new Vector(y.Size + 1) { i };
                for (var index = 1; index < y.Size + 1; ++index)
                    state[index] = y[index - 1];
                result.Add(state);

                step.Clear();

                // k1
                UpdateBuffer(0);
                for (var index = 0; index < y.Size; ++index)
                    step[index] += 23 * func[index](i - h, buf) * h;
                
                // k2
                UpdateBuffer(1);
                for (var index = 0; index < y.Size; ++index)
                    step[index] -= 16 * func[index](i -  2 * h, buf) * h;
                
                // k3
                UpdateBuffer(2);
                for (var index = 0; index < y.Size; ++index)
                    step[index] += 5 * func[index](i - 3 * h, buf) * h;

                y += step /= 12;
            }

            // save last state
            state = new Vector(y.Size + 1) { end };
            for (var index = 1; index < y.Size + 1; ++index)
                state[index] = y[index - 1];
            result.Add(state);

            return result;
        }

        public static List<Vector> RungeKutte4(this Func<double, Vector, double>[] func, double begin, double end, Vector y, double h)
        {
            var result = new List<Vector>();
            var koeff = new Vector(y.Size);
            var buf = new Vector(y.Size);
            var step = new Vector(y.Size);
            Vector state = null;

            void UpdateBuffer(double k)
            {
                for (var index = 0; index < y.Size; ++index)
                    buf[index] = y[index] + koeff[index] * h * k;
            }

            for (var i = begin; i < end; i += h)
            {
                // save current state
                state = new Vector(y.Size + 1) { i };
                for (var index = 1; index < y.Size + 1; ++index)
                    state[index] = y[index - 1];
                result.Add(state);

                step.Clear();

                // k1
                for (var index = 0; index < y.Size; ++index)
                    step[index] += koeff[index] = func[index](i, y);


                UpdateBuffer(0.5);
                // k2
                for (var index = 0; index < y.Size; ++index)
                    step[index] += (koeff[index] = func[index](i + h / 2, buf)) * 2;


                UpdateBuffer(0.5);
                // k3
                for (var index = 0; index < y.Size; ++index)
                    step[index] += (koeff[index] = func[index](i + h / 2, buf)) * 2;


                UpdateBuffer(1);
                // k4
                for (var index = 0; index < y.Size; ++index)
                    step[index] += func[index](i + h, buf);

                // New system state
                y += step * h / 6;
            }

            // save last state
            state = new Vector(y.Size + 1) { end };
            for (var index = 1; index < y.Size + 1; ++index)
                state[index] = y[index - 1];
            result.Add(state);

            return result;
        }

        public static List<Vector> RungeKutte2(this Func<double, Vector, double>[] func, double begin, double end, Vector y, double h)
        {
            var result   = new List<Vector>();
            var buf      = new Vector(y.Size);
            Vector state = null;

            for (var i = begin; i < end; i += h)
            {
                // save current state
                state = new Vector(y.Size + 1) { i };
                for (var index = 1; index < y.Size + 1; ++index)
                    state[index] = y[index - 1];
                result.Add(state);

                for (var index = 0; index < y.Size; ++index)
                    buf[index] = y[index] + h / 2 * func[index](i, y);

                for (var index = 0; index < y.Size; ++index)
                    y[index] += h * func[index](i + h / 2, buf);
            }

            // save last state
            state = new Vector(y.Size + 1) { end };
            for (var index = 1; index < y.Size + 1; ++index)
                state[index] = y[index - 1];
            result.Add(state);

            return result;
        }

        public static double IntegralBySquare(this Func<double, double> func, double begin, double end, double epsilon)
        {
            double l0 = 0, l1 = double.PositiveInfinity;
            var n = 5;

            while (Math.Abs(l1 - l0) > epsilon)
            {
                l0 = l1;
                l1 = 0;

                var h = (end - begin) / n;
                for (int i = 0; i < n - 1; i++)
                    l1 += func(begin + i * h);

                l1 *= h;
                n *= 2;
            }

            return l1;
        }

        public static double IntegralByTrapecial(this Func<double, double> func, double begin, double end, double epsilon)
        {
            double l0 = 0, l1 = double.PositiveInfinity;
            var n = 5;

            while (Math.Abs(l1 - l0) > epsilon)
            {
                l0 = l1;
                l1 = 0;
                var h = (end - begin) / n;
                for (var i = begin + h; i < end; i += h)
                    l1 += func(i);

                l1 = (l1 + (func(begin) + func(end)) / 2) * h;
                n *= 2;
            }

            return l1;
        }

        public static double IntegralBySimpson(this Func<double, double> func, double begin, double end, double epsilon)
        {
            double l0 = 0, l1 = double.PositiveInfinity;
            var n = 5;

            while (Math.Abs(l1 - l0) > epsilon)
            {
                l0 = l1;
                l1 = 0;
                var h = (end - begin) / n;
                var isOdd = 1;
                for (var i = begin + h; i < end - h; i += h, ++isOdd)
                    l1 += isOdd % 2 == 0 ? func(i) : 2.0 * func(i);

                l1 = 2.00 / 3.00 * h * (l1 + (func(begin) + func(end)) / 2);
                n *= 2;
            }

            return l1;
        }

        /// <summary>
        /// Least square method
        /// </summary>
        public static LSMFunction LSM(this Func<double, double>[] func, Vector x, Vector y)
        {
            var b   = new Vector(func.Length);
            var ksi = new Matrix(x.Size, func.Length);

            for(var index = 0; index < ksi.Columns; ++index)
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

            return new LSMFunction((ksi.Transpose() * ksi).Gauss(b), func);
        }
    }
}