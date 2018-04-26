using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASD.Sort.Base;

namespace ASD.Sort
{
    public static class SortTest
    {
        public static void TEST()
        {
            List<int> set = new List<int>();
            int _count = 10;
            int[] setIntResult = new int[_count];
            int[] setInt = new int[_count];
            int _choice = -1;
            Random rand = new Random();

            for (int i = 0; i < _count; i++) // заполнение массивов
            {
                set.Add(rand.Next(0, 50));
                setInt[i] = rand.Next(0, 50);
            }

            
            while (_choice < 0 || _choice > 5)
            {
                Console.WriteLine("Каким методом сортировать? 1 - сортировка пузырьком, 2 - сортировка вставками, 3 - сортировка Шелла, " +
               "4 - сортировка Merge (слиянием), 5 - быстрая сортировка");

                _choice = int.Parse(Console.ReadLine());
            }


            switch (_choice)
            {
                case 1:
                    Console.Write("Несортированное множество: ");
                    ShowSet(set);
                    set = Sort<int>.BubbleSort(set);
                    Console.Write("Cортированное множество: ");
                    ShowSet(set);
                break;


                case 2:
                    Console.Write("Несортированное множество: ");
                    ShowSet(set);
                    set = Sort<int>.InsertionSort(set);
                    Console.Write("Cортированное множество: ");
                    ShowSet(set);
                break;


                case 3:
                    Console.Write("Несортированное множество: ");
                    ShowSet(set);
                    set = Sort<int>.ShellSort(set);
                    Console.Write("Cортированное множество: ");
                    ShowSet(set);
                break;


                case 4:
                    Console.Write("Несортированное множество: ");
                    ShowSet(setInt);
                    setIntResult = Sort<int>.MergeSort(setInt, 0, setInt.Length - 1);
                    Console.Write("Cортированное множество: ");
                    ShowSet(setIntResult);
                break;


                case 5:
                    Console.Write("Несортированное множество: ");
                    ShowSet(setInt);
                    setIntResult = Sort<int>.QuickSort(setInt, 0, setInt.Length - 1);
                    Console.Write("Cортированное множество: ");
                    ShowSet(setIntResult);
                break;

            }
            Console.ReadKey();
            
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
