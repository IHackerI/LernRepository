using ChislMethods.WorkTesters.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChislMethods.Integral
{
    public delegate double F(double x, double y);

    public class Simpson2
    {
        double m_Simpson(F func, double t_fix, double t_limit, int N)
        {
            double sum1 = 0; // -¬                        //
            double sum2 = 0; //  ¦ временные переменные   //
            double sum3 = 0; // --                        //
            double sum;      // конечный результат        //
            double h = (2 * t_limit) / N; // шаг сетки //
            int i;           // временная                 //

            sum1 = func(t_fix, -t_limit) + func(t_fix, +t_limit);

            for (i = 1; i <= N - 1; i++) sum2 += func(t_fix, -t_limit + (i * h));
            sum2 *= 2;

            for (i = 1; i <= N; i++) sum3 += func(t_fix, -t_limit + ((i - 0.5) * h));
            sum3 *= 4;

            sum = sum1 + sum2 + sum3;
            sum = (h / 6) * sum;
            return sum;
        }

        //                       //
        // Глобальные переменные //
        // ~~~~~~~~~~~~~~~~~~~~~ //
        double PI = 3.1415926536;    // число П                                   //
        //double k;                  // параметр функции - задается пользователем //
        int N_MAX;                 // число узлов сетки разбиения     //
        //F currFunc; // выбранная пользователем функция //
        
        double FUNC(double dummy, double t)
        {
            double G;
            t = t + PI / 2; // сдвижка начала координат, чтобы пределы    
                            // были симметричны (в нашем случае - на П/2) 
            G = m_Simpson(MiniFunc, t, t, N_MAX);
            return G * Math.Sin(t);
        }


        public void TEST()
        {
            double integral;  // значение вычисленного интеграла 
            //int selection; // номер выбранной функции         
                           // массив доступных функций 
            //F[] functions = { f1, f2, f3 };

            Console.WriteLine("\n   Вычисление интеграла методом Симпсона (парабол)   ");
            Console.WriteLine("\n   ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~   ");
            Console.WriteLine("\n              --                                     ");
            Console.WriteLine("\n          I = ¦¦ sin k(x + y) f (x, y) dx dy         ");
            Console.WriteLine("\n              --                                     ");
            Console.WriteLine("\n              D                                      ");
            Console.WriteLine("\n где D = { (x, y): x, y >= 0; x + y <= П }, f Е C (D)");
            Console.WriteLine("\n");
            Console.WriteLine("\nДля какой функции рассчитывать:             ");
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~             ");
            Console.WriteLine("\n  1) f (x, y) = 0.5 * cos (y)               ");
            Console.WriteLine("\n     -                        -- 0; k != 1  ");
            Console.WriteLine("\n     ¦ sin x * sin (kx) dx => ¦             ");
            Console.WriteLine("\n     -                        L- П/2; k = 1 ");
            Console.WriteLine("\n                                            ");
            Console.WriteLine("\n  2) f (x, y) = 0.5 - sin (y)               ");
            Console.WriteLine("\n     -                                      ");
            Console.WriteLine("\n     ¦ x * sin (kx) dx =====> П; k = 1      ");
            Console.WriteLine("\n     -                                      ");
            Console.WriteLine("\n                                            ");
            Console.WriteLine("\n  3) f (x, y) = sqrt (x * x + y * y)");
            Console.WriteLine("\n");
            /*do
            {
                selection = IOSystem.GetInt("Ваш выбор: ");
            } while (!(1 <= selection && selection <= 3));*/
            
            do
            {
                N_MAX = IOSystem.GetInt("Число узлов сетки N: ");
            } while (!(N_MAX > 0));
            Console.WriteLine("\n");
            Console.WriteLine("\n Расчет интеграла ...");

            //currFunc = functions[selection - 1];          // текущая функция    
            integral = m_Simpson(FUNC, 0, PI / 2, N_MAX);    // вычисляем интеграл 
            Console.WriteLine("\n Значение интеграла равно: {0}", integral); // вывод   
            Console.WriteLine("\n Величины: П = {0} П/2 = {1}", PI, PI / 2);
        }
        
        double MiniFunc(double x, double y)
        { return Math.Sqrt(x * x + y * y); }
         
    }
}
