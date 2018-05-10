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
                choiceStatus = IOSystem.InterfacedViewChoice(new string[] { "MyHashTable", "HashTableList" }, new EmptyD[] { MyHashTableTEST, HashTableListTEST });
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
            IHashTableTest(new MyHashTable<string>(10));
        }
        
        //Тестирование HashTableList
        private static void HashTableListTEST()
        {
            IHashTableTest(new HashTableList<string>(5));
        }

        private static void IHashTableTest(IHashTable<string> table)
        {
            while (true)
            {
                var input = IOSystem.SafeSimpleChoice("Выберите действие с таблицей:", new string[]
                    {
                        "Добавить узел",
                        "Удалить узел",
                        "Получить узел",
                        "Вывести таблицу",
                        "Закончить тест"
                    });

                bool endTest = false;

                switch (input)
                {
                    case 1:
                        int key = IOSystem.GetInt("Введите ключ: ");
                        Console.Write("Введите значение: ");
                        string value = Console.ReadLine();
                        table.Add(key, value);
                        break;

                    case 2:
                        table.Remove(IOSystem.GetInt("Введите ключ: "));
                        break;

                    case 3:
                        Console.WriteLine("Найденное значение: " + table.FindByKey(IOSystem.GetInt("Введите ключ: ")));
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
