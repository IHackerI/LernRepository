using System;
using ASD.WorkTesters.Helper;
using System.Linq;

namespace ASD.WorkTesters
{
    /// <summary>
    /// Главный класс тестера
    /// Тестирует ВСЕ вещи АСД
    /// </summary>
    public static class WorkMainTester
    {
        /// <summary>
        /// Названия тестируемых модулей
        /// </summary>
        enum TesterName
        {
            None = 0,
            SetDeckQueueStack = 10,
            Sort = 20,
            HashTable = 30,
            BinaryTree = 40,
            Graph = 50
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
                    SetDeckQueueStackTester.TEST, //|
                    SortTest.TEST,          //|
                    HashTableTest.TEST,     //|указание методов отправляемых на тест
                    BinaryTreeTester.TEST,  //|
                    GraphTester.TEST        //|
                });

                //Проверка на выполненность теста

                if (!testResult)
                {
                    Console.WriteLine("Введите корректные значения!");
                    Console.WriteLine();
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine("Хотите выйти? y/n");
                if (Console.ReadLine().ToLower() == "y")
                    break; // вылет из программы
                Console.WriteLine();
            }
        }
    }
}
