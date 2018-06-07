namespace NumericalMethods
{
    using NumericalMethods.Structs;
    using System;

    public class Program
    {
        public static void Main()
        {
            //var n = 12;
            //var x = new Vector(n);
            //var y = new Vector(n);
            //for (int i = 0; i < n; i++)
            //{
            //    x[i] = i;
            //    y[i] = x[i] * x[i];
            //}

            //var spline = MatrixExt.BuildSpline(x, y);

            //for (float i = 0; i < n; i+= 0.5f)
            //{
            //    Console.WriteLine($"x ={i} y = {spline[i]}");
            //}

            // LSM
            var x = new Vector(4)
            {
                [0] = 0,
                [1] = 1,
                [2] = 2,
                [3] = 3
            };
            var y = new Vector(4) //{ 7.6, 6, 5.2, 4, 3.6 };
            {
                [0] = 0,
                [1] = 1.1,
                [2] = 3.9,
                [3] = 8.95
            };

            var func = new Func<double, double>[]
            {
                c => 1,
                c => c,
                c => c * c
            }.LSM(x, y);

            Console.WriteLine($"Koeff {func.Koeff}");

            for (var step = 0.0; step < 3; step += 0.25)
            {
                Console.WriteLine($"X ={step.ToString().PadLeft(10, ' ')}, Analitic = {(step * step).ToString().PadLeft(10, ' ')}, " +
                    $"Approx = {func[step].ToString().PadLeft(10, ' ')}");
            }

            Console.ReadKey();
        }
    }
}