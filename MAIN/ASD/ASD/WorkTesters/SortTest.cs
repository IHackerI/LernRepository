using System;
using System.Collections.Generic;
using ASD.Sort;
using ASD.WorkTesters.Helpers;

namespace ASD.WorkTesters
{
    public static class SortTest
    {
        public static void TEST()
        {
            List<int> set = new List<int>();
            int _count = IOSystem.GetInt("Введите размер генерируемого массива: ");
            int[] setIntResult = new int[_count];
            int[] setInt = new int[_count];
            Random rand = new Random();

            for (int i = 0; i < _count; i++) // заполнение массивов
            {
                set.Add(rand.Next(0, 50));
                setInt[i] = rand.Next(0, 50);
            }
                        
            var input = IOSystem.SafeSimpleChoice("Каким методом сортировать?", new string[]
                    {
                    "Сортировка пузырьком",
                    "Сортировка вставками",
                    "Сортировка Шелла",
                    "Сортировка Merge (слиянием)",
                    "Быстрая сортировка"
                    });

            switch (input)
            {
                case 0:
                    Console.Write("Несортированное множество: ");
                    ShowSet(set);
                    set = Sort<int>.BubbleSort(set);
                    Console.Write("Cортированное множество: ");
                    ShowSet(set);
                    break;

                case 1:
                    Console.Write("Несортированное множество: ");
                    ShowSet(set);
                    set = Sort<int>.InsertionSort(set);
                    Console.Write("Cортированное множество: ");
                    ShowSet(set);
                    break;

                case 2:
                    Console.Write("Несортированное множество: ");
                    ShowSet(set);
                    set = Sort<int>.ShellSort(set);
                    Console.Write("Cортированное множество: ");
                    ShowSet(set);
                    break;

                case 3:
                    Console.Write("Несортированное множество: ");
                    ShowSet(setInt);
                    setIntResult = Sort<int>.MergeSort(setInt, 0, setInt.Length - 1);
                    Console.Write("Cортированное множество: ");
                    ShowSet(setIntResult);
                    break;

                case 4:
                    Console.Write("Несортированное множество: ");
                    ShowSet(setInt);
                    setIntResult = Sort<int>.QuickSort(setInt, 0, setInt.Length - 1);
                    Console.Write("Cортированное множество: ");
                    ShowSet(setIntResult);
                    break;
            }

            Console.WriteLine();
        }

        static void ShowSet<T>(IList<T> arr)
        {
            var enumerator = arr.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.Write(enumerator.Current + " ");
            }
            Console.WriteLine();
        }
    }
}
