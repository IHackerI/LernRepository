using System;

namespace Teach4M
{
	public class BinFind : IFindMethod
	{
		public double LeftBorder;
		public double RightBorder;

		public BinFind(double leftBorder, double rightBorder){
			LeftBorder = leftBorder;
			RightBorder = rightBorder;
		}

		public double FindArgument(FunctionD function, double eps, double funcTarget){
			double funcfirst, funcsec, funcmid, mid; //Значение функции в левой границе, правой,функция от середины интервала, середина интервала
			funcfirst = function(LeftBorder);
			funcsec = function(RightBorder);
			if (funcfirst * funcsec < 0) //Если границы имеют разные знаки, выполняем следующее: *
			{
				do
				{
					mid = (LeftBorder + RightBorder) / 2; //Находим середину интервала
					funcmid = function(mid); //Функция от середины интервала
					if (funcfirst * funcmid < 0) //Если левая граница и середина интервала имеют разные знаки, то берём середину интервала за правую границу **
					{
						RightBorder = mid;
						funcsec = funcmid;
					}
					else //иначе, берём середину интервала за левую границу **
					{
						LeftBorder = mid; funcfirst = funcmid;
					}
				}
				while (RightBorder - LeftBorder > eps); // Пока длина интервала превышает значение точности, выполняем цикл
			}
			else //Иначе говорим, что границы взяли неверно *
			{
				Console.WriteLine("Неверные границы ");
				return Double.NaN;
			}
			return (LeftBorder + RightBorder) / 2; //Уменьшаем интервал
		}
	}
}

