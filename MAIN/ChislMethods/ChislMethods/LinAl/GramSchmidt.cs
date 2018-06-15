using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChislMethods.LinAl
{
    /// <summary>
    /// Решение системы линейных уравнений (матрицы) Методом Грамма-Шмидта
    /// </summary>
    public class GramSchmidt
    {
        public int Size = 0;

        public GramSchmidt(int size)
        {
            Size = size;
        }

        /// <summary>
        /// Решение системы линейных уравнений (матрицы) Методом Грамма-Шмидта
        /// </summary>
        public Vector Calculate(Matrix m, Vector vector)
        {
            if (Size < 1)
            {
                return null;
            }

            Vector nAn = new Vector(0);
            Matrix u = new Matrix(new double[Size, Size]);
            Matrix v = new Matrix(new double[Size, Size]);
            Vector temp = new Vector(Size);
            Vector h = new Vector(Size);
            Vector x = new Vector(Size);

            if (m.Col != m.Row) return nAn;
            if (Matrix.Determ(m) == 0) return nAn;
            
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                {
                    u[i, j] = 0;
                    v[i, j] = 0;
                }

            for (int i = 0; i < Size; i++)
                u[0, i] = m[0, i]; // u[0]

            v.SetRow(u.GetRow(0).Normalization(), 0); //v[0]

            Vector t = new Vector(Size);
            //#warning div0 (Проверка в самом начале Size)
            t[0] = vector[0] / u.GetRow(0).Len(); // h[0]

            int count = 1; // счетчик пременных, для которых уже найдены первые значения
            double te;
            do
            {
                double temp_h;
                temp_h = 0;

                for (int i = 0; i < Size; i++)
                    temp[i] = 0;

                for (int j = 0; j < count; j++)
                {
                    te = m.GetRow(count).Multiplication(v.GetRow(j));

                    for (int i = 0; i < Size; i++)
                        temp[i] += te * v[j, i]; // получили c[i, j] * v[k]

                    temp_h += te * t[j];
                }

                for (int i = 0; i < Size; i++) // находим u
                    u[count, i] = m[count, i] - temp[i];

                //#warning div0 (Проверка выше на Size)
                t[count] = (vector[count] - temp_h) / u.GetRow(count).Len(); // нашли h

                v.SetRow(u.GetRow(count).Normalization().Normalization(), count);

                count++;

            } while (count < Size);


            h = t;
            x = Matrix.Transposition(v) * t;

            return x;
        }
    }
}
