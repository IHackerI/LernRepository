namespace NumericalMethods.Structs
{
    using System;

    public class Spline
    {
        private Vector x;
        private Vector y;
        private Vector m;

        public Spline(Vector x, Vector y, Vector m)
        {
            this.x = x;
            this.y = y;
            this.m = m;
        }

        public double this[double x]
        {
            get
            {
                var index = -1;

                for(var i = 0; i < this.x.Size - 1; ++i)
                    if (this.x[i] <= x && x <= this.x[i + 1]) index = i + 1;
                
                if (index == -1) return double.NaN;

                var h = this.x[index] - this.x[index - 1];

                return this.m[index - 1] * Math.Pow(this.x[index] - x, 3) / (6 * h) +
                    this.m[index] * Math.Pow(x - this.x[index - 1], 3) / (6 * h) +
                    (this.y[index - 1] - this.m[index - 1] * Math.Pow(h, 2) / 6) * (this.x[index] - x) / h +
                    (this.y[index] - this.m[index] * Math.Pow(h, 2) / 6) * (x - this.x[index - 1]) / h;
            }
        }
    }
}