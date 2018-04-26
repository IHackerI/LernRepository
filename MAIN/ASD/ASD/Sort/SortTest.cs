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
            Random rand = new Random();

            for (int i = 0; i < _count; i++) // заполнение массивов
            {
                set.Add(rand.Next(0, 50));
                //setInt[i] = rand.Next(0, 50);
                //set2.Add(rand.Next(0, 50));
            }

            #region Сортировки
            //set = Sort<int>.BubbleSort(set);
            set = Sort<int>.InsertionSort(set);
            //set = Sort<int>.ShellSort(set);
            //setIntResult = Sort<int>.MergeSort(setInt, 0, setInt.Length - 1);
            //setIntResult = Sort<int>.QuickSort(setInt, 0, setInt.Length - 1);
            #endregion


            Console.Write("Cортированное множество: ");
            ShowSet(set);
            //ShowSet(setIntResult);
            //set3 = Set<int>.Addition(set, set2);
            //ShowSet(set3);
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
