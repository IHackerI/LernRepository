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
                    Console.WriteLine(" Индексы вышли за пределы матрицы ");
                    return 0;
                }
                else
                    return vector[i];
            }
            set
            {
                if (i < 0 && i >= size)
                {
                    Console.WriteLine(" Индексы вышли за пределы матрицы ");
                }
                else
                    vector[i] = value;
            }
        }

        public int GetSize() { return size; }  // получение размера

        public bool SetElement(double element, int ind)// установить значение по индексу
        {
            if (ind < 0 || ind >= size) return false;
            vector[ind] = element;
            return true;
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

        public void View()
        {
            Console.Write("( ");
            for (int i = 0; i < this.size; i++)
                Console.Write("{0} ", this[i]);
            Console.WriteLine(")");
        }

        public double multiplication(Vector a) //умножение векторов cкалярное
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

        public Vector multiplication(double x)// умножение вектора на число
        {
            Vector rez = new Vector(size);
            for (int i = 0; i < size; i++)
                rez[i] = vector[i] * x;
            return rez;

        }

        public Vector addition(Vector a)//сложение
        {
            if (size == a.size)
            {
                Vector rez = new Vector(size);
                for (int i = 0; i < size; i++)
                    rez[i] = vector[i] + a[i];
                return rez;
            }
            Vector nan = new Vector(0);
            return nan;
        }

        public Vector subtraction(Vector a)//вычитание
        {
            if (size == a.size)
            {
                Vector rez = new Vector(size);
                for (int i = 0; i < size; i++)
                    rez[i] = vector[i] - a[i];
                return rez;
            }
            Vector nan = new Vector(0);
            return nan;
        }
        
        public double len() // длина вектора
        {

            double x = 0;
            for (int i = 0; i < size; i++)
                x += Math.Pow(vector[i], 2);
            x = Math.Sqrt(x);

            return x;
        }

        public Vector normalization()// нормализация вектора
        {
            Vector rez = new Vector(vector);
            double x = len();
            for (int i = 0; i < size; i++)
                rez[i] = rez[i] / x;
            return rez;
        }

        public Vector Gauss(Matrix matrix) // метод Гаусса
        {
            Vector v = new Vector(vector);

            Vector nan = new Vector(0);

            Vector b = new Vector(vector);
            double[] x = new double[size];
            double max;
            int jmax;
            for (int k = 0; k < size; k++)
            {
                // поиск макс по модулю элемента в к столбце начиная с к элемента 
                max = Math.Abs(matrix[k, k]);
                jmax = k;
                for (int j = k + 1; j < size; j++)
                {
                    if (Math.Abs(matrix[j, k]) > max)
                    {
                        max = Math.Abs(matrix[j, k]);
                        jmax = j;
                    }
                }
                if (jmax != k)
                {
                    //обмен строк 
                    for (int i = 0; i < size; i++)
                    {
                        double temp = matrix[k, i];
                        matrix[k, i] = matrix[jmax, i];
                        matrix[jmax, i] = temp;
                        double temp1 = b[k];
                        b[k] = b[jmax];
                        b[jmax] = temp1;
                    }
                }
                //Приведение к треугольному виду 
                double m;
                for (int z = 1; z < size; z++)
                {
                    for (int j = z; j < size; j++)
                    {
                        m = matrix[j, z - 1] / matrix[z - 1, z - 1];
                        for (int i = 0; i < size; i++)
                            matrix[j, i] = matrix[j, i] - m * matrix[z - 1, i];
                        b[j] = b[j] - m * b[z - 1];
                    }
                }
                //проверка на особбенность матрицы 
                if (max == 0) return nan;
            }
            //обратный ход 
            for (int q = size - 1; q >= 0; q--)
            {
                for (int j = q + 1; j < size; j++)
                    b[q] -= matrix[q, j] * b[j];
                b[q] = b[q] / matrix[q, q];
            }
            return b;
        } 

        public Vector GramSchmidt(Matrix m) // метод ортогонолизации
        {
            Vector nan = new Vector(0);
            if (m.Col != m.Row) return nan;
            if (Matrix.Determ(m) == 0) return nan;

            Matrix u = new Matrix(new double[size, size]);
            Matrix v = new Matrix(new double[size, size]);
            Vector temp = new Vector(size);
            Vector h = new Vector(size);
            Vector x = new Vector(size);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    u[i, j] = 0;
                    v[i, j] = 0;
                }

            for (int i = 0; i < size; i++)
                u[0, i] = m[0, i]; // u[0]

            v.SetRow(u.GetRow(0).normalization(), 0); //v[0]

            Vector t = new Vector(size);
            t[0] = vector[0] / u.GetRow(0).len(); // h[0]

            int count = 1; // счетчик пременных, для кот уже найдены первые значения
            double te;
            do
            {
                double temp_h;
                temp_h = 0;

                for (int i = 0; i < size; i++)
                    temp[i] = 0;

                for (int j = 0; j < count; j++)
                {
                    te = m.GetRow(count).multiplication(v.GetRow(j));

                    for (int i = 0; i < size; i++)
                        temp[i] += te * v[j, i]; // получили c[i, j] * v[k]

                    temp_h += te * t[j];
                }

                for (int i = 0; i < size; i++) // находим u
                    u[count, i] = m[count, i] - temp[i];

                t[count] = (vector[count] - temp_h) / u.GetRow(count).len(); // нашли h

                v.SetRow(u.GetRow(count).normalization().normalization(), count);

                count++;

            } while (count < size);


            h = t;
            x = Matrix.Transposition(v) * t;

            return x;
        } 

        
    }
}
