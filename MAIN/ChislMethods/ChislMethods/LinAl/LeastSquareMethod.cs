using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace ChislMethods.LinAl
{
    public delegate double Psi(double x, int number);
    public class LeastSquareMethod
    {
        Matrix answ;
        public double xmin, xmax;
        public double[,] Minkv(double[,] xyTable, int basis, Psi ps)
        {
            double[,] matrix = new double[basis, basis + 1];

            xmin = xyTable[0, 0];
            xmax = xyTable[0, xyTable.Length / 2 - 1];
            for (int i = 0; i < basis; i++)
            {
                for (int j = 0; j < basis; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            for (int i = 0; i < basis; i++)
            {
                double sumB = 0;
                for (int j = 0; j < basis; j++)
                {
                    double sumA = 0;
                    for (int k = 0; k < xyTable.Length / 2; k++)
                    {
                        // sumA += Math.Pow(xyTable[0, k], i) * Math.Pow(xyTable[0, k], j);
                        // sumB += xyTable[1, k] * Math.Pow(xyTable[0, k], i);
                        sumA += ps(xyTable[0, k], i) * ps(xyTable[0, k], j);

                    }
                    matrix[i, j] = sumA;

                }
                for (int k = 0; k < xyTable.Length / 2; k++)
                {
                    sumB += xyTable[1, k] * ps(xyTable[0, k], i);
                }
                matrix[i, basis] = sumB;
            }
            Matrix m = new Matrix(matrix);
            //m = m.toTriangleForm(m);
            //Console.WriteLine(m.Gauss());
            answ = m.Gauss(m);
            return matrix;
        }

        public double resh2(double x, Psi ps)
        {
            if (answ == null)
                return 0;

            double answer = answ.matrix[0, 0];
            for (int i = 1; i < answ.matrix.GetLength(0); i++)
            {
                answer += answ.matrix[i, 0] * ps(x, i);
            }
            return answer;
        }
    }
}
