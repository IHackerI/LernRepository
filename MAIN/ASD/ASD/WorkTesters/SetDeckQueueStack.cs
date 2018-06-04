﻿using System;
using System.Linq;
using ASD.SetDeckQueueStack;
using ASD.WorkTesters.Helper;

namespace ASD.WorkTesters
{
    /// <summary>
    /// Тестер систем хранения данных 
    /// 
    /// Set - динамический массив 
    /// Deck - двусторонняя очередь
    /// Queue - очередь
    /// Stack - стек
    /// </summary>
    public static class SetDeckQueueStack
    {
        /// <summary>
        /// Названия тестируемых модулей
        /// </summary>
        enum ListName
        {
            None = 0,
            ДвустороннийСписок = 10,
            Очередь = 20,
            Множество = 30,
            Стек = 40
        }

        /// <summary>
        /// Точка входа тестера
        /// </summary>
        public static void TEST()
        {
            while (true)
            {

                //Получает названия модулей в виде массива строк
                var testerNames = Enum.GetNames(typeof(ListName));

                //Запрашивает Инструменты ввода/вывода
                //предоставить выбор тестируемой системы
                //и сразу запустить её
                bool testResult = IOSystem.InterfacedViewChoice(testerNames.Skip(1).ToArray(), new WorkMainTester.EmptyD[] {
                    DeckTest,   //|
                    QueueTest,  //|
                    SetTest,    //|указание методов отправляемых на тест
                    StackTest   //|
                });

                //Проверка на выполненность теста

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

        /// <summary>
        /// Тестирование двусторонней очереди
        /// </summary>
        private static void DeckTest()
        {
            Deck<string> deck = new Deck<string>();

            while (true)
            {
                //Запрашивает Инструменты ввода/вывода
                //предоставить выбор тестируемого модуля
                var input = IOSystem.SafeSimpleChoice("Выберите действие:", new string[]
                        {
                        "Добавить узел в начало списка", //0
                        "Добавить узел в конец списка",
                        "Добавить узел после элемента",//2
                        "Добавить узел перед элементом",
                        "Вывести с начала",//4
                        "Вывести с конца",
                        "Удалить элемент",//6
                        "Удалить в начале списка",
                        "Удалить в конце списка",//8
                        "Удалить по индексу",
                        "Закончить тест"//10
                        });
                
                bool endTest = false;

                //В зависимости от запроса запускаем модуль
                //(отсчёт от нуля)
                switch (input)
                {
                    case 0:
                        Console.Write("Введите добавляемое значение: ");
                        deck.AddHead(Console.ReadLine());
                        break;

                    case 1:
                        Console.Write("Введите добавляемое значение: ");
                        deck.AddTail(Console.ReadLine());
                        break;

                    case 2:
                        Console.Write("Введите искомое значение: ");
                        string findaValue = Console.ReadLine();
                        Console.Write("Введите добавляемое значение: ");
                        deck.InsertAfter(findaValue, Console.ReadLine());
                        break;

                    case 3:
                        Console.Write("Введите искомое значение: ");
                        string findbValue = Console.ReadLine();
                        Console.Write("Введите добавляемое значение: ");
                        deck.InsertBefore(findbValue, Console.ReadLine());
                        break;

                    case 4:
                        Console.WriteLine("Начало вывода");
                        deck.ViewHead();
                        Console.WriteLine("Конец вывода");
                        break;

                    case 5:
                        Console.WriteLine("Начало вывода");
                        deck.ViewTail();
                        Console.WriteLine("Конец вывода");
                        break;

                    case 6:
                        Console.Write("Введите значение, которое нужно удалить: ");
                        deck.Remove(Console.ReadLine());
                        break;

                    case 7:
                        deck.RemoveHead();
                        Console.WriteLine("Удалено");
                        break;

                    case 8:
                        deck.RemoveTail();
                        Console.WriteLine("Удалено");
                        break;

                    case 9:
                        deck.RemoveByIndex(IOSystem.GetInt("Введите индекс: "));
                        break;
                        
                    case 10:
                        endTest = true;
                        break;
                }

                Console.WriteLine();

                if (endTest)
                    break;
            }
        }
        
        /// <summary>
        /// Тестирование очереди
        /// </summary>
        private static void QueueTest()
        {
            Queue<string> queue = new Queue<string>();

            while (true)
            {
                //Запрашивает Инструменты ввода/вывода
                //предоставить выбор тестируемого модуля
                var input = IOSystem.SafeSimpleChoice("Выберите действие:", new string[]
                        {
                        "Добавить узел",
                        "Считать узел",
                        "Показать очередь",
                        "Закончить тест"
                        });

                bool endTest = false;

                //В зависимости от запроса запускаем модуль
                //(отсчёт от нуля)
                switch (input)
                {
                    case 0:
                        Console.Write("Введите добавляемое значение: ");
                        queue.Push(Console.ReadLine());
                        break;

                    case 1:
                        Console.WriteLine("Считанное значение: " + queue.Pop());
                        break;

                    case 2:
                        Console.WriteLine("Начало вывода");
                        queue.View();
                        Console.WriteLine("Конец вывода");
                        break;
                        
                    case 3:
                        endTest = true;
                        break;
                }

                Console.WriteLine();

                if (endTest)
                    break;
            }
        }

        /// <summary>
        /// Тестирование динамического массива
        /// </summary>
        private static void SetTest()
        {
            Set<string> set = new Set<string>();

            while (true)
            {
                //Запрашивает Инструменты ввода/вывода
                //предоставить выбор тестируемого модуля
                var input = IOSystem.SafeSimpleChoice("Выберите действие:", new string[]
                        {
                        "Добавить элемент",
                        "Добавить множество",
                        "Объединение множеств",
                        "Пересечение множеств",
                        "Дополение множества",
                        "Множество подмножеств",
                        "Вывести множество",
                        "Закончить тестирование"
                        });

                bool endTest = false;

                //В зависимости от запроса запускаем модуль
                //(отсчёт от нуля)
                switch (input)
                {
                    case 0:
                        Console.Write("Введите добавляемое значение: ");
                        set.Add(Console.ReadLine());
                        break;

                    case 1:
                        Console.Write("Введите добавляемые значения через пробел: ");
                        set.AddRange(Console.ReadLine().Split(' '));
                        break;

                    case 2:
                        Console.Write("Введите значения другого множества через пробел: ");
                        var secondRange = new Set<string>();
                        secondRange.AddRange(Console.ReadLine().Split(' '));
                        Set<string>.Union(set, secondRange).View();
                        break;

                    case 3:
                        Console.Write("Введите значения другого множества через пробел: ");
                        var secondiRange = new Set<string>();
                        secondiRange.AddRange(Console.ReadLine().Split(' '));
                        Set<string>.Intersection(set, secondiRange).View();
                        break;

                    case 4:
                        Console.Write("Введите значения другого множества через пробел: ");
                        var secondsRange = new Set<string>();
                        secondsRange.AddRange(Console.ReadLine().Split(' '));
                        Set<string>.Addition(set, secondsRange).View();
                        break;

                    case 5:
                        Console.WriteLine("Множество подмножеств: ");
                        var ans = Set<string>.Subset(set);
                        foreach (var subset in ans)
                        {
                            subset.View();
                        }
                        Console.WriteLine();
                        break;

                    case 6:
                        Console.Write("Множество:");
                        set.View();
                        break;

                    case 7:
                        endTest = true;
                        break;
                }

                Console.WriteLine();

                if (endTest)
                    break;
            }
            
        }

        /// <summary>
        /// Тестирование стека
        /// </summary>
        private static void StackTest()
        {
            Stack<string> stack = new Stack<string>();

            while (true)
            {
                //Запрашивает Инструменты ввода/вывода
                //предоставить выбор тестируемого модуля
                var input = IOSystem.SafeSimpleChoice("Выберите действие:", new string[]
                        {
                        "Добавить узел",
                        "Считать узел",
                        "Показать стек",
                        "Закончить тестирование"
                        });

                bool endTest = false;

                //В зависимости от запроса запускаем модуль
                //(отсчёт от нуля)
                switch (input)
                {
                    case 0: //Добавление в конец стека
                        Console.Write("Введите добавляемое значение: ");
                        stack.Push(Console.ReadLine());
                        break;

                    case 1:  //Изъятие из конца стека
                        Console.WriteLine("Считанное значение: " + stack.Pop());
                        break;

                    case 2: //Вывод стека
                        Console.WriteLine("Стек:");
                        stack.View();
                        Console.WriteLine("Конец вывода");
                        break;

                    case 3:
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
