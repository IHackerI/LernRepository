using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace ChislMethods.LinAl
{
    public class Vector
    {
        public double[] vector;
        public int size = 0;

        public Vector(int size) //конструктор 
        {
            this.size = size;
            vector = new double[size];
        }

        public Vector(double[] m) //конструктор
        {
            this.size = m.Length;
            vector = new double[size];
            for (int i = 0; i < size; i++)
                vector[i] = m[i];

        }

        public Vector(Vector m)//конструктор
        {
            this.size = m.size;
            vector = new double[size];
            for (int i = 0; i < size; i++)
                vector[i] = m[i];

        }

        public double this[int i]
        {
            get
            {
                if (i < 0 && i >= size)
                {
                    Console.WriteLine("Индексы вышли за пределы матрицы ");
                    return 0;
                }
                else
                    return vector[i];
            }
            set
            {
                if (i < 0 && i >= size)
                {
                    Console.WriteLine("Индексы вышли за пределы матрицы ");
                }
                else
                    vector[i] = value;
            }
        }

        public override string ToString()
            => $"{{{string.Join(",  ", this.vector)}}}";

        public int Size { get { return GetSize(); } }

        public int GetSize() { return size; }  // получение размера

        public bool SetElement(double element, int ind)// установить значение по индексу
        {
            if (ind < 0 || ind >= size) return false;
            vector[ind] = element;
            return true;
        }

        public double ScalarProduct(Vector vector)
        {
            if (vector.Size != this.Size)
                throw new InvalidOperationException("Size of both vectors should be equls");

            var result = 0.0;
            this.ForEach((ref double f, ref double l) => result += f * l, vector);
            return result;
        }

        private Vector ForEach(Action2 action, Vector vector)
        {
            for (int index = 0; index < this.Size; ++index)
                action(ref this.vector[index], ref vector.vector[index]);

            return this;
        }

        public Vector Add(Vector vector)
        {
            if (vector.vector.Length != this.vector.Length)
                throw new InvalidOperationException("Size of both vectors should be equls");

            return this.ForEach((ref double s, ref double o) => s += o, vector);
        }

        public double GetElement(int ind)// получить значение по индексу
        {
            if (ind < 0 || ind >= size) return default(double);
            return vector[ind];
        }

        public Vector Copy()// копирование
        {
            Vector rez = new Vector(vector);
            return rez;
        }

        public void View() // вывод вектора
        {
            Console.Write("( ");
            for (int i = 0; i < this.size; i++)
                Console.Write("{0} ", this[i]);
            Console.WriteLine(")");
        }

        public double Multiplication(Vector a) // Скалярное умножение векторов
        {
            if (size == a.size)
            {
                double s = 0;
                for (int i = 0; i < size; i++)
                    s += vector[i] * a[i];
                return s;
            }
            return 0;
        }

        public Vector Multiplication(double x)// Умножение вектора на число
        {
            Vector rez = new Vector(size);
            for (int i = 0; i < size; i++)
                rez[i] = vector[i] * x;
            return rez;

        }

        public Vector Addition(Vector a)// Сложение векторов
        {
            if (size == a.size)
            {
                Vector rez = new Vector(size);
                for (int i = 0; i < size; i++)
                    rez[i] = vector[i] + a[i];
                return rez;
            }

            return null;
        }

        public Vector Subtraction(Vector a)// Вычитание векторов
        {
            if (size == a.size)
            {
                Vector rez = new Vector(size);
                for (int i = 0; i < size; i++)
                    rez[i] = vector[i] - a[i];
                return rez;
            }

            return null;
        }
        
        public double Len() // Длина вектора
        {

            double x = 0;
            for (int i = 0; i < size; i++)
                x += Math.Pow(vector[i], 2);
            x = Math.Sqrt(x);

            return x;
        }

        public Vector Normalization()// Нормализация вектора
        {
            Vector rez = new Vector(vector);
            double x = Len();
            for (int i = 0; i < size; i++)
                rez[i] = rez[i] / x;
            return rez;
        }

        public static Vector operator *(double a, Vector b)
            => new Vector(b).Multiply(a);

        public static Vector operator -(Vector a, Vector b)
            => new Vector(a).Subtract(b);

        public Vector Subtract(Vector vector)
        {
            if (vector.vector.Length != this.vector.Length)
                throw new InvalidOperationException("Size of both vectors should be equls");

            return this.ForEach((ref double s, ref double o) => s -= o, vector);
        }

        public Vector Multiply(double scalar)
            => this.ForEach((ref double s) => s *= scalar);

        private Vector ForEach(Action action)
        {
            for (int index = 0; index < this.Size; ++index)
                action(ref this.vector[index]);

            return this;
        }

        public void Clear()
            => this.ForEach((ref double i) => i = 0);

        public double Norma1()
            => Math.Sqrt(this.SquaredLength);

        public double SquaredLength
            => this.vector.Sum(c => c * c);

        private delegate void Action2(ref double v, ref double l);
        private delegate void Action(ref double v);
    }
}
