namespace NumericalMethods
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides the vector class that is the basis for this project.
    /// </summary>
    public class Vector : IEnumerable<double>
    {
        /*----------------------- Operator overloading below ------------------------------*/
        public static bool operator ==(Vector v1, Vector v2)
        {
            if (ReferenceEquals(v1, null)) ReferenceEquals(v2, null);

            return v1.Equals(v2);
        }

        public static bool operator !=(Vector v1, Vector v2)
            => !(v1 == v2);

        public static Vector operator +(Vector a, Vector b)
            => new Vector(a).Add(b);

        public static Vector operator -(Vector a, Vector b)
            => new Vector(a).Subtract(b);

        public static Vector operator *(Vector a, Vector b)
            => new Vector(a).Multiply(b);

        public static Vector operator /(Vector a, Vector b)
            => new Vector(a).Devide(b);

        public static Vector operator *(double a, Vector b)
            => new Vector(b).Multiply(a);

        public static Vector operator *(Vector b, double a)
            => a * b;

        public static Vector operator /(double a, Vector b)
            => 1 / a * b;

        public static Vector operator /(Vector b, double a)
            => a / b;

        public static Vector operator +(double a, Vector b)
            => new Vector(b).Add(a);

        public static Vector operator +(Vector b, double a)
            => a + b;

        public static Vector operator -(double a, Vector b)
            => -a * b;

        public static Vector operator -(Vector b, double a)
            => a - b;

        private double[] components;

        public double this[int index]
        {
            get => this.components[index];
            set => this.components[index] = value;
        }

        public int Size => this.components.Length;

        /// <summary>
        /// The length of the vector.
        /// </summary>
        public double Length
            => Math.Sqrt(this.SquaredLength);

        /// <summary>
        /// The squared length of the vector. Useful for optimalisation.
        /// </summary>
        public double SquaredLength
            => this.components.Sum(c => c * c);

        /// <summary>
        /// Main Constructor.
        /// </summary>
        /// <param name="xValue">The x value of the vector. </param>
        /// <param name="yValue">The y value of the vector. </param>
        public Vector(int size)
            => this.components = new double[size];

        public Vector Func(Func<double, double> func)
        {
            var res = new Vector(this.Size);
            for (var index = 0; index < this.Size; ++index)
                res[index] = func(this[index]);
            return res;
        }

        public double Sum()
        {
            var res = 0.0;
            this.ForEach(c => res += c);
            return res;
        }

        public Vector(Vector vector)
        {
            this.components = new double[vector.components.Length];

            for (var index = 0; index < vector.Size; ++index)
                this[index] = vector[index];
        }

        /// <summary>
        /// Копирует компоненты вектора в вектор v
        /// </summary>
        /// <param name="v">Место копирования компонентов в вектор</param>
        public void Clone(Vector v)
        {
            for (var index = 0; index < this.Size; ++index) v[index] = this[index];
        }

        public double Convolution(Vector v)
        {
            var res = 0.0;
            for(var index =0; index < this.Size; ++index)
                res += this[index] * v[index];
            return res;
        }

        public void Swap(int index1, int index2)
        {
            var tmp = this[index1];
            this[index1] = this[index2];
            this[index2] = tmp;
        }

        public Vector Add(Vector vector)
        {
            if (vector.components.Length != this.components.Length)
                throw new InvalidOperationException("Size of both vectors should be equls");

            return this.ForEach((ref double s, ref double o) => s += o, vector);
        }

        public Vector Subtract(Vector vector)
        {
            if (vector.components.Length != this.components.Length)
                throw new InvalidOperationException("Size of both vectors should be equls");

            return this.ForEach((ref double s, ref double o) => s -= o, vector);
        }

        public Vector Multiply(Vector vector)
        {
            if (vector.components.Length != this.components.Length)
                throw new InvalidOperationException("Size of both vectors should be equls");

            return this.ForEach((ref double s, ref double o) => s *= o, vector);
        }

        public double SumFunc(Func<double, double> func)
        {
            var res = 0.0;
            this.ForEach(c => res += func(c));
            return res;
        }

        public Vector Devide(Vector vector)
        {
            if (vector.components.Length != this.components.Length)
                throw new InvalidOperationException("Size of both vectors should be equls");

            return this.ForEach((ref double s, ref double o) => s *= o, vector);
        }

        public void Clear()
            => this.ForEach((ref double i) => i = 0);

        public Vector Add(double scalar)
            => this.ForEach((ref double s) => s += scalar);

        public Vector Subtract(double scalar)
            => this.Add(-scalar); 

        public Vector Multiply(double scalar)
            => this.ForEach((ref double s) => s *= scalar);

        public Vector Devide(double scalar)
            => this.Multiply(1 / scalar);

        public Vector Negate()
            => this.ForEach((ref double x) => x = -x);

        /// <summary>
        /// Overrides the Equals method to provice better equality for vectors.
        /// </summary>
        /// <param name="obj">The object to test equality against.</param>
        /// <returns>Whether the objects are equal. </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;

            if (ReferenceEquals(this, obj)) return true;

            var other = obj as Vector;

            if (other == null)return false;

            for (var index = 0; index < this.Size; index++)
                if (this[index] != other[index]) return false;

            return true;
        }

        /// <summary>
        /// Overrides the hashcode.
        /// </summary>
        /// <returns>The hashcode for the vector.</returns>
        public override int GetHashCode()
        {
            var result = 0;

            this.ForEach(c => result ^= c.GetHashCode());

            return result;
        }

        /// <summary>
        /// ToString method overriden for easy printing/debugging.
        /// </summary>
        /// <returns>The string representation of the vector.</returns>
        public override string ToString()
            => $"{{{string.Join(",", this.components)}}}";

        public Vector Normalize()
        {
            var d = this.Norma1();

            if (d == 0) return this;

            this.ForEach((ref double c) => c /= d);

            return this;
        }

        public double Norma1()
            => Math.Sqrt(this.SquaredLength);

        /// <summary>
        /// Only for 3 component vectors
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public Vector VectorProduct(Vector vector)
        {
            if (vector.Size != this.Size)
                throw new InvalidOperationException("Size of both vectors should be equls");

            if(this.Size != 3)
                throw new InvalidOperationException("Vector size should be equal 3");

            this[0] = this[1] * vector[2] - this[2] * vector[1];
            this[1] = this[0] * vector[2] - this[2] * vector[0];
            this[2] = this[0] * vector[1] - this[1] * vector[1];

            return this;
        }

        public double ScalarProduct(Vector vector)
        {
            if (vector.Size != this.Size)
                throw new InvalidOperationException("Size of both vectors should be equls");

            var result = 0.0;
            this.ForEach((ref double f, ref double l) => result += f * l, vector);
            return result;
        }

        public Vector ForEach(System.Action<double> action)
        {
            for (int index = 0; index < this.Size; ++index)
                action(this.components[index]);

            return this;
        }

        public Vector ForEach(Action<double, double> action, Vector vector)
        {
            if (vector.components.Length != this.components.Length)
                throw new InvalidOperationException("Size of both vectors should be equls");

            for (int index = 0; index < this.Size; ++index)
                action(this.components[index], vector.components[index]);

            return this;
        }

        private Vector ForEach(Action action)
        {
            for (int index = 0; index < this.Size; ++index)
                action(ref this.components[index]);

            return this;
        }

        private Vector ForEach(Action2 action, Vector vector)
        {
            for (int index = 0; index < this.Size; ++index)
                action(ref this.components[index], ref vector.components[index]);

            return this;
        }

        public IEnumerator<double> GetEnumerator()
            => this.components.Cast<double>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private delegate void Action(ref double v);
        private delegate void Action2(ref double v, ref double l);
    }
}