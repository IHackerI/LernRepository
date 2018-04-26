using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChislMethods.Spline
{
    public class Spline
    {
        Splinee[] splines;
        private struct Splinee
        {
            public double a, b, c, d, x;// a, b, c, d - коэф.полинома, х - переменная            
        }

        public void Build(double[] x, double[] y, int n)//построение сплайна
        {
            splines = new Splinee[n];//массив сплайнов            

            for (int i = 0; i < n; i++)
            {
                splines[i].x = x[i];
                splines[i].a = y[i];
            }
            splines[0].c = splines[n - 1].c = 0;

            //вычисление коэффициентов методом прогонки
            double[] alpha = new double[n];
            alpha[0] = 0;
            double[] betta = new double[n];
            betta[0] = 0;

            for (int i = 1; i < n - 1; i++)
            {
                double h1 = x[i] - x[i - 1];
                double h2 = x[i + 1] - x[i];
                double A = h1;
                double C = 2 * (h1 + h2);
                double B = h2;
                double F = 6 * ((y[i + 1] - y[i]) / h2 - (y[i] - y[i - 1]) / h1);

                alpha[i] = -B / (A * alpha[i - 1] + C);
                betta[i] = (F - A * betta[i - 1]) / (A * alpha[i - 1] + C);
            }

            for (int i = n - 2; i > 0; --i) //находим решение (обратный ход)
                splines[i].c = alpha[i] * splines[i + 1].c + betta[i];

            for (int i = n - 1; i > 0; --i)
            {
                double hi = x[i] - x[i - 1];
                splines[i].d = (splines[i].c - splines[i - 1].c) / hi; //значение d
                splines[i].b = hi * (2.0 * splines[i].c + splines[i - 1].c) / 6.0 + (y[i] - y[i - 1]) / hi; //значение b                
            }
        }

        public double Interpolate(double x)  //интерполирование функции
        {
            int n = splines.Length;
            Splinee s;

            if (x <= splines[0].x) //если х меньше x[0]
                s = splines[0];

            else if (x >= splines[n - 1].x) //если х больше x[n-1]
                s = splines[n - 1];

            else //х между точками, производим поиск
            {
                int i = 0;
                int j = n - 1;
                while (i + 1 < j)
                {
                    int k = i + (j - i) / 2;
                    if (x <= splines[k].x)
                        j = k;
                    else
                        i = k;
                }
                s = splines[j];
            }
            double dx = x - s.x;
            return s.a + (s.b + (s.c / 2.0 + s.d * dx / 6.0) * dx) * dx;
        }
    }
}
