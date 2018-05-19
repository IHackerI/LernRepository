using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChislMethods.FindFuncs
{
    public static class BinFind
    {
        public static double PolDel(double eps, double Begin, double End, DelF Func)
        {
            double Half;
            double fa;
            double fb;
            double fc;

            fa = Convert.ToDouble(Func(Begin));
            fb = Convert.ToDouble(Func(End));

            if (fa * fb > 0)
            {
                Console.WriteLine("Нет корня!");
                //throw new Exception("Нет корня!");
                return 0;
            }
            if (Math.Abs(fa) < eps) return End;

            do
            {
                Half = Begin + 0.5 * (End - Begin);
                fc = Convert.ToDouble(Func(Half));
                if (Math.Abs(fc) < eps) return Half; // если значение функции в этой точке меньше отклонения, тоглда возвращаем эту точку
                if (fa * fc < 0)
                {
                    End = Half;
                    fb = Convert.ToDouble(Func(End));
                }
                // если функция исеет корень м\у начальной точкой и центром , то сдвигаем точку в центр
                else
                {
                    Begin = Half;
                    fa = fc;
                }
                // иначе корень м\у центром и конечной точкой, и начало сдвигается в середину

            } while (Math.Abs(Begin - End) > eps);
            return Half; // возвращаем среднюю точку по концу цикла 
        }
    }
}
