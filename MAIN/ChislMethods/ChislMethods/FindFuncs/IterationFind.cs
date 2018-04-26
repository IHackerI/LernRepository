using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChislMethods.FindFuncs
{
    public static class IterationFind
    {
        public static double Iteration(double Xn,/*Текущий аргумент*/ double eps,/*Точность*/  DelF Func /*Уравнение*/) //Метод последовательных приближений
        {
            double currX = Xn; //Выбираем текущий Х
            double FuncX = Func(currX); //Подставляем в уравнение
            long iteration = 0; //Задаём номер итерации

            var t = Math.Abs(FuncX - currX); //Модуль разницы между F(x) и х
            do
            {
                currX = FuncX; // Х+1 = F(x) 
                FuncX = Func(currX); // обновляем значение функции от текущего Х
                var currentT = Math.Abs(FuncX - currX); // обновляем модуль разницы между F(x) и х
                if (currentT > t) // если обновленное значение больше предыдущего, то выполняем следующее:
                    return (double.NaN);

                ++iteration; // увеличиваем номер итерации
                t = currentT; // приравниваем обновленное значение t 
            }
            while (t >= eps); //цикл выполняется, пока не добьемся нужной точности

            return FuncX;//возвращаем полученное значение функции
        }
    }
}
