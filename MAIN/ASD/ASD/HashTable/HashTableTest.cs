using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASD.HashTable.Base;
using ASD.Helpers;
using static ASD.Program;

namespace ASD.HashTable
{
    public static class HashTableTest
    {
        public static void TEST()
        {
            bool choiceStatus = false;
            while (!choiceStatus) {
                choiceStatus = ChoiceController.ViewChoice(new string[] { "MyHashTable", "HashTableList" }, new EmptyD[] { MyHashTableTEST, HashTableListTEST });
                if (!choiceStatus)
                {
                    Console.WriteLine("Введите корректное значение!");
                    Console.WriteLine();
                }
            }
        }

        //Тестирование MyHashTable
        private static void MyHashTableTEST()
        {
            MyHashTable<string> table = new MyHashTable<string>(3);

            while (true)
            {
                Console.WriteLine("Выберите действие с таблицой:");
                Console.WriteLine("1   Добавить узел");
                Console.WriteLine("2   Удалить узел");
                Console.WriteLine("3   Получить узел");
                Console.WriteLine("4   Вывести таблицу");
                Console.WriteLine("5   Закончить тест");

                Console.WriteLine();
                var input = GetInt("");
                Console.WriteLine();

                if (input > 5 || input < 1)
                {
                    Console.WriteLine("Введите корректное значение!");
                    Console.WriteLine();
                    continue;
                }

                bool endTest = false;

                switch (input)
                {
                    case 1:
                        int key = GetInt("Введите ключ: ");
                        Console.Write("Введите значение: ");
                        string value = Console.ReadLine();
                        table.Add(key, value);
                        break;

                    case 2:
                        table.Remove(GetInt("Введите ключ: "));
                        break;

                    case 3:
                        Console.WriteLine("Найденноое значение: " + table.FindByKey(GetInt("Введите ключ: ")));
                        break;

                    case 4:
                        table.View();
                        break;

                    case 5:
                        endTest = true;
                        break;
                }

                Console.WriteLine();

                if (endTest)
                    break;
            }
        }
        
        private static int GetInt(string getText)
        {
            int ansInt;
            while (true)
            {
                Console.Write(getText);

                if (!int.TryParse(Console.ReadLine(), out ansInt))
                {
                    Console.WriteLine("Введите корректное значение!");
                    Console.WriteLine();
                }
                else
                {
                    break;
                }
            }

            return ansInt;
        }

        //Тестирование HashTableList
        private static void HashTableListTEST()
        {
            HashTableList<string> table = new HashTableList<string>(5);

            while (true)
            {
                Console.WriteLine("Выберите действие с таблицой:");
                Console.WriteLine("1   Добавить узел");
                Console.WriteLine("2   Удалить узел");
                Console.WriteLine("3   Получить узел");
                Console.WriteLine("4   Вывести таблицу");
                Console.WriteLine("5   Закончить тест");

                Console.WriteLine();
                var input = GetInt("");
                Console.WriteLine();

                if (input > 5 || input < 1)
                {
                    Console.WriteLine("Введите корректное значение!");
                    Console.WriteLine();
                    continue;
                }

                bool endTest = false;

                switch (input)
                {
                    case 1:
                        int key = GetInt("Введите ключ: ");
                        Console.Write("Введите значение: ");
                        string value = Console.ReadLine();
                        table.Add(key, value);
                        break;

                    case 2:
                        table.Remove(GetInt("Введите ключ: "));
                        break;

                    case 3:
                        Console.WriteLine("Найденноое значение: " + table.FindByKey(GetInt("Введите ключ: ")));
                        break;

                    case 4:
                        table.View();
                        break;

                    case 5:
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
