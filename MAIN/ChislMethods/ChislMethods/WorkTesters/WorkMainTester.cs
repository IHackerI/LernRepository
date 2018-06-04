using System;
using ChislMethods.WorkTesters.Helpers;
using System.Linq;

namespace ChislMethods.WorkTesters
{
    public static class WorkMainTester
    {
        /// <summary>
        /// Названия тестируемых модулей
        /// </summary>
        enum TesterName
        {
            None = 0,
            DerSystems = 10,
            FincValueFinders = 20,
            Integral = 30,
            LinAl = 40,
            Spline = 50
        }

        public delegate void EmptyD();

        /// <summary>
        /// Точка входа в тестер
        /// </summary>
        public static void TEST()
        {
            while (true)
            {
                //Получает названия модулей в виде массива строк
                var testerNames = Enum.GetNames(typeof(TesterName));

                //Запрашивает Инструменты ввода/вывода
                //предоставить выбор тестируемой системы
                //и сразу запустить её
                bool testResult = IOSystem.InterfacedViewChoice(testerNames.Skip(1).ToArray(), new EmptyD[] {
                    DerSystemsTEST.TEST,        //|
                    FincValueFindersTest.TEST,  //|
                    IntegralTEST.TEST,          //|указание методов отправляемых на тест
                    LinAlTester.TEST,           //|
                    SplineTest.TEST             //|
                });

                if (!testResult)
                {
                    Console.WriteLine("Введите корректные значения!");
                    Console.WriteLine();
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine("Хотите выйти? y/n");
                if (Console.ReadLine().ToLower() == "y")
                    break;
                Console.WriteLine();
            }
        }
    }
}
