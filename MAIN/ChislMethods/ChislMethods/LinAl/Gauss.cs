using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChislMethods.LinAl
{
    public class Gauss
    {
        public static Vector Calc(Matrix items, Vector b)
        {
            double max;
            int indmax;
            for (int i = 0; i < b.Size; i++)
            {
                max = Math.Abs(items[i, i]);
                indmax = i;
                for (int j = i; j < b.Size; j++)
                    if (Math.Abs(items[j, i]) > max)
                    {
                        max = Math.Abs(items[j, i]);
                        indmax = j;
                    }

                if (indmax != i)
                {
                    items.SwapRows(i, indmax);
                    double temp = b[i];
                    b[i] = b[indmax];
                    b[indmax] = temp;
                }

                double x;
                for (int z = 1; z < b.Size; z++)
                    for (int j = z; j < b.Size; j++)
                    {
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
                b[q] = b[q] / items[q, q];
            }
            return b;
        }
    }
}
