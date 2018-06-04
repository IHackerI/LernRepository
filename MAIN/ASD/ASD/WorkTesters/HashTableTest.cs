using System;
using ASD.HashTable;
using ASD.WorkTesters.Helper;

namespace ASD.WorkTesters
{
    /// <summary>
    /// Тестирует таблицу хешей
    /// </summary>
    public static class HashTableTest
    {
        /// <summary>
        /// Точка входа в тестер
        /// </summary>
        public static void TEST()
        {
            bool choiceStatus = false;
            while (!choiceStatus)
            {
                //Запрашивает Инструменты ввода/вывода
                //предоставить выбор тестируемого модуля
                choiceStatus = IOSystem.InterfacedViewChoice(new string[] { "MyHashTable", "HashTableList" }, new WorkMainTester.EmptyD[] { MyHashTableTEST, HashTableListTEST });
                if (!choiceStatus)
                {
                    Console.WriteLine("Введите корректное значение!");
                    Console.WriteLine();
                }
            }
        }
        
        private static void MyHashTableTEST() // Тестирование MyHashTable
        {
            IHashTableTest(new MyHashTable<string>(10));
        }
        
        private static void HashTableListTEST() // Тестирование HashTableList
        {
            IHashTableTest(new HashTableList<string>(5));
        }

        /// <summary>
        /// Тестирование таблицы
        /// </summary>
        private static void IHashTableTest(IHashTable<string> table)
        {
            while (true)
            {
                //Запрашивает Инструменты ввода/вывода
                //предоставить выбор тестируемого модуля
                var input = IOSystem.SafeSimpleChoice("Выберите действие с таблицей:", new string[]
                    {
                        "Добавить узел",
                        "Удалить узел",
                        "Получить узел",
                        "Вывести таблицу",
                        "Закончить тестирование"
                    });

                bool endTest = false;

                //В зависимости от запроса запускаем модуль
                //(отсчёт от нуля)
                switch (input)
                {
                    case 0:
                        int key = IOSystem.GetInt("Введите ключ: ");
                        Console.Write("Введите значение: ");
                        string value = Console.ReadLine();
                        table.Add(key, value);
                        break;

                    case 1:
                        table.Remove(IOSystem.GetInt("Введите ключ: "));
                        break;

                    case 2:
                        Console.WriteLine("Найденное значение: " + table.FindByKey(IOSystem.GetInt("Введите ключ: ")));
                        break;

                    case 3:
                        Console.WriteLine("Начало вывода");
                        table.View();
                        Console.WriteLine("Конец вывода");
                        break;

                    case 4:
                        endTest = true;
                        break;
                }

                Console.WriteLine();

                if (endTest)
                    break;
            }
        }
    }
}
