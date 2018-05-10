using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASD.CustomLists.Base;
using static ASD.Program;
using ASD.Helpers;

namespace ASD.CustomLists
{
    public static class CustomListsTester
    {
        enum ListName
        {
            None = 0,
            Deck = 10,
            Queue = 20,
            Set = 30,
            Stack = 40
        }

        public static void TEST()
        {
            while (true)
            {
                var testerNames = Enum.GetNames(typeof(ListName));
                bool testResult = IOSystem.InterfacedViewChoice(testerNames.Skip(1).ToArray(), new EmptyD[] {
                    DeckTest,
                    QueueTest,
                    SetTest,
                    StackTest
                });

                if (!testResult)
                {
                    Console.WriteLine("Введите корректные значения!");
                    Console.WriteLine();
                    continue;
                }
                
                Console.WriteLine();
                break;
            }
        }

        private static void DeckTest()
        {
            Deck<int> deck = new Deck<int>();

            while (true)
            {
                var input = IOSystem.SafeSimpleChoice("Выберите действие с таблицей:", new string[]
                        {
                        "Добавить узел в начало списка",
                        "Добавить узел в конец списка",
                        "Добавить узел после чего-то",
                        "Добавить узел перед чем-то",
                        "Показать начальный элемент",
                        "Показать конечный элемент",
                        "Удалить элемент",
                        "Удалить в начале списка",
                        "Удалить в конце списка",
                        "Удалить по индексу",
                        "Закончить тест"
                        });

                bool endTest = false;

                switch (input)
                {
                    case 1:
                        
                        break;

                    case 2:
                        
                        break;

                    case 3:
                        
                        break;

                    case 4:
                        
                        break;

                    case 5:
                        endTest = true;
                        break;
                }

                Console.WriteLine();

                if (endTest)
                    break;
            }

            /*deck.AddHead(1);
            deck.InsertAfter(1, 6);
            Console.WriteLine("Длина списка: " + deck.Length);
            Console.Write("С головы списка: ");
            deck.ViewHead();
            Console.Write("С хвоста списка: ");
            deck.ViewTail();
            Console.WriteLine("Содержит ли список единицу? " + deck.Contains(1));*/
        }

        private static void QueueTest()
        {
            Queue<int> queue = new Queue<int>();

            while (true)
            {
                var input = IOSystem.SafeSimpleChoice("Выберите действие с таблицей:", new string[]
                        {
                        "Добавить узел",
                        "Считать узел",
                        "Показать очередь",
                        "Закончить тест"
                        });

                bool endTest = false;

                switch (input)
                {
                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    case 5:
                        endTest = true;
                        break;
                }

                Console.WriteLine();

                if (endTest)
                    break;
            }

            //Console.WriteLine(queue);
        }

        private static void SetTest()
        {
            Set<int> set = new Set<int>();

            while (true)
            {
                var input = IOSystem.SafeSimpleChoice("Выберите действие с таблицей:", new string[]
                        {
                        "Добавить элемент",
                        "Добавить множество",
                        "Объединение множеств",
                        "Пересечение множеств",
                        "Дополение множества",
                        "Множество подмножеств",
                        "Закончить тест"
                        });

                bool endTest = false;

                switch (input)
                {
                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    case 5:
                        endTest = true;
                        break;
                }

                Console.WriteLine();

                if (endTest)
                    break;
            }

            //Console.WriteLine(set);
        }

        private static void StackTest()
        {
            Stack<int> stack = new Stack<int>();

            while (true)
            {
                var input = IOSystem.SafeSimpleChoice("Выберите действие с таблицей:", new string[]
                        {
                        "Добавить узел",
                        "Считать узел",
                        "Показать стек",
                        "Закончить тест"
                        });

                bool endTest = false;

                switch (input)
                {
                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    case 5:
                        endTest = true;
                        break;
                }

                Console.WriteLine();

                if (endTest)
                    break;
            }

            //Console.WriteLine(stack);
        }
    }
}
