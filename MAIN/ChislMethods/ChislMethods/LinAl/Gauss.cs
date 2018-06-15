using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChislMethods.LinAl
{
    /// <summary>
    /// Решение системы линейных уравнений (матрицы) Методом Гаусса
    /// </summary>
    public class Gauss
    {
        /// <summary>
        /// Решение системы линейных уравнений (матрицы) Методом Гаусса
        /// </summary>
        public static Vector Calculate(Matrix items, Vector b)
        {
            double max;
            int maxIndex;
            for (int i = 0; i < b.Size; i++)
            {
                max = Math.Abs(items[i, i]);
                maxIndex = i;
                for (int j = i; j < b.Size; j++)
                    if (Math.Abs(items[j, i]) > max)
                    {
                        max = Math.Abs(items[j, i]);
                        maxIndex = j;
                    }

                if (maxIndex != i)
                {
                    items.SwapRows(i, maxIndex);
                    double temp = b[i];
                    b[i] = b[maxIndex];
                    b[maxIndex] = temp;
                }

                double x;
                for (int z = 1; z < b.Size; z++)
                    for (int j = z; j < b.Size; j++)
                    {
                        #warning Check0
                        x = items[j, z - 1] / items[z - 1, z - 1];
                        for (int k = 0; k < b.Size; k++)
                            items[j, k] = items[j, k] - x * items[z - 1, k];
                        b[j] = b[j] - x * b[z - 1];
                    }
            }

            for (int q = b.Size - 1; q >= 0; q--)
            {
                for (int j = q + 1; j < b.Size; j++)
                    b[q] -= items[q, j] * b[j];
                #warning Check0
                b[q] = b[q] / items[q, q];
            }
            return b;
        }
    }
}
