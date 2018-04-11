using System;

namespace Teach4M
{
	public delegate double FunctionD (double arg);

	class MainClass
	{
		public static double Function(double x) //Заданное уравнение
		{
			return x * x - 1;
		}

		public static void Main (string[] args)
		{
			var binFind = new BinFind (-0.5, 4);
			var newtonFind = new NewtonFind (5);
			var progressiveFind = new ProgressivFind (5);

			var binFindValue = binFind.FindArgument (Function, 0.00001, 0);
			Console.WriteLine("{0} and {1} ", binFindValue, Function(binFindValue));

			var newtonFindValue = newtonFind.FindArgument (Function, 0.00001, 0);
			Console.WriteLine("{0} and {1} ", newtonFindValue, Function(newtonFindValue));

			var progressiveFindValue = progressiveFind.FindArgument (Function, 0.00001, 0);
			Console.WriteLine("{0} and {1} ", progressiveFindValue, Function(progressiveFindValue));

			Console.ReadKey();
		}
	}

	/*
		class Program
    {
    								Левая граница интервала Правая граница интервала Точность
        public static double MidDiv(double firstborder, double secondborder, double eps, Func<double, double> func) //Половинное деление
	{
		double funcfirst, funcsec, funcmid, mid; //Значение функции в левой границе, правой,функция от середины интервала, середина интервала
		funcfirst = func(firstborder);
		funcsec = func(secondborder);
		if (funcfirst * funcsec < 0) //Если границы имеют разные знаки, выполняем следующее: *
		{
			do
			{
				mid = (firstborder + secondborder) / 2; //Находим середину интервала
				funcmid = func(mid); //Функция от середины интервала
				if (funcfirst * funcmid < 0) //Если левая граница и середина интервала имеют разные знаки, то берём середину интервала за правую границу **
				{
					secondborder = mid;
					funcsec = funcmid;
				}
				else //иначе, берём середину интервала за левую границу **
				{
					firstborder = mid; funcfirst = funcmid;
				}
			}
			while (secondborder - firstborder > eps); // Пока длина интервала превышает значение точности, выполняем цикл
		}
		else //Иначе говорим, что границы взяли неверно *
		{
			Console.WriteLine("Неверные границы ");
			return Double.NaN;
		}
		return (firstborder + secondborder) / 2; //Уменьшаем интервал
	}
									Текущий Х Точность                     Функция
	public static double NewtonMet(double Xn, double eps, Func<double, double> func) // Метод Ньютона (метод касательных)
	{
		double delta = 0.2 * eps; //Точность
		double currX, nextX, currFunc, defcurrFunc; //Текущее приближение, следующее приближение, текущая функция, производная текущей функции
		double h; //Длина интервала
		currX = Xn; //Задаём текущий Х
		do
		{
			currFunc = func(currX);// Подставляем аргумент в заданное уравнение
			defcurrFunc = (func(currX + delta) - currFunc) / delta; //Подставляем аргумент в производную функции
			nextX = currX - (currFunc / defcurrFunc); //Вычисляем следующее приближение
			h = nextX - currX; //Длина интервала
			currX = nextX; //Выбираем следующее приближение за текущее
		} while (Math.Abs(h) > eps);
		return nextX; //Возвращаем следующее приближение
	}

								Текущий аргумент Точность                     Уравнение
	public static double PoslPribl(double Xn, double eps, Func<double, double> func) //Метод последовательных приближений
	{
		double currX = Xn; //Выбираем текущий Х
		double FuncX = func(currX); //Подставляем в уравнение
		long iteration = 0; //Задаём номер итерации

		var t = Math.Abs(FuncX - currX); //Модуль разницы между F(x) и х
		do
		{
			currX = FuncX; // Х+1 = F(x) 
			FuncX = func(currX); // обновляем значение функции от текущего Х
			var currentT = Math.Abs(FuncX - currX); // обновляем модуль разницы между F(x) и х
			if (currentT > t) // если обновленное значение больше предыдущего, то выполняем следующее:
				return (double.NaN); 

			++iteration; // увеличиваем номер итерации
			t = currentT; // приравниваем обновленное значение t 
		}
		while (t >= eps); //цикл выполняется, пока не добьемся нужной точности

		return FuncX;//возвращаем полученное значение функции
	}

	public static double Function(double x) //Заданное уравнение
	{
		return x * x * x - 1;
	}

	static void Main(string[] args)
	{
		double m = MidDiv(-2.0, 5.0, 0.00001, Function);
		Console.WriteLine("{0} and {1} ", m, Function(m));
		double n = NewtonMet(1.5, 0.00001, Function);
		Console.WriteLine("{0} and {1} ", n, Function(n));

		Console.ReadKey();
	}

}
	*/
}
