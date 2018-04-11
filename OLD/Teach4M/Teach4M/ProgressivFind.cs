using System;

namespace Teach4M
{
	public class ProgressivFind : IFindMethod
	{
		public double StartX;

		public ProgressivFind(double startX){
			StartX = startX;
		}

		public double FindArgument(FunctionD function, double eps, double funcTarget)
		{
			double currX = StartX; //Выбираем текущий Х
			double FuncX = function(currX); //Подставляем в уравнение
			long iteration = 0; //Задаём номер итерации

			var t = Math.Abs(FuncX - currX); //Модуль разницы между F(x) и х
			do
			{
				currX = FuncX; // Х+1 = F(x) 
				FuncX = function(currX); // обновляем значение функции от текущего Х
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

