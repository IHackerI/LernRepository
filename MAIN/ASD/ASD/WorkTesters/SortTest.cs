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
            int _count = IOSystem.GetInt("Введите размер генерируемого массива: ");
            int[] setInt = new int[_count];
            Random rand = new Random();

            for (int i = 0; i < _count; i++) // заполнение массивов
            {
                setInt[i] = rand.Next(0, 50);
            }
                        
            var input = IOSystem.SafeSimpleChoice("Каким методом сортировать?", new string[]
                    {
                    "Сортировка пузырьком",
                    "Сортировка вставками",
                    "Сортировка Шелла",
                    "Сортировка Merge (слиянием)",
                    "Быстрая сортировка",
                    });

            switch (input)
            {
                case 0:
                    Console.Write("Несортированное множество: ");
                    ShowSet(setInt);
                    Sort<int>.BubbleSort(setInt);
                    Console.Write("Cортированное множество: ");
                    ShowSet(setInt);
                    break;

                case 1:
                    Console.Write("Несортированное множество: ");
                    ShowSet(setInt);
                    Sort<int>.InsertionSort(ref setInt);
                    Console.Write("Cортированное множество: ");
                    ShowSet(setInt);
                    break;

                case 2:
                    Console.Write("Несортированное множество: ");
                    ShowSet(setInt);
                    Sort<int>.ShellSort(setInt);
                    Console.Write("Cортированное множество: ");
                    ShowSet(setInt);
                    break;

                case 3:
                    Console.Write("Несортированное множество: ");
                    ShowSet(setInt);
                    Sort<int>.MergeSort(setInt, 0, setInt.Length - 1);
                    Console.Write("Cортированное множество: ");
                    ShowSet(setInt);
                    break;

                case 4:
                    Console.Write("Несортированное множество: ");
                    ShowSet(setInt);
                    Sort<int>.QuickSort(setInt, 0, setInt.Length - 1);
                    Console.Write("Cортированное множество: ");
                    ShowSet(setInt);
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
