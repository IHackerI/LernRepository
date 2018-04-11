using System;

namespace Teach4M
{
	public class NewtonFind : IFindMethod
	{
		public double StartX;

		public NewtonFind(double startX){
			StartX = startX;
		}

		public double FindArgument(FunctionD function, double eps, double funcTarget)
		{
			double delta = 0.2 * eps; //Точность
			double currX, nextX, currFunc, defcurrFunc; //Текущее приближение, следующее приближение, текущая функция, производная текущей функции
			double h; //Длина интервала
			currX = StartX; //Задаём текущий Х
			do
			{
				currFunc = function(currX);// Подставляем аргумент в заданное уравнение
				defcurrFunc = (function(currX + delta) - currFunc) / delta; //Подставляем аргумент в производную функции
				nextX = currX - (currFunc / defcurrFunc); //Вычисляем следующее приближение
				h = nextX - currX; //Длина интервала
				currX = nextX; //Выбираем следующее приближение за текущее
			} while (Math.Abs(h) > eps);
			return nextX; //Возвращаем следующее приближение
		}
	}
}

