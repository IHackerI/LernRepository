﻿using ASD.SetDeckQueueStack;
using System;
using System.Threading;

namespace ASD.Sort
{
    /// <summary>
    /// Класс сортировок 
    /// </summary>
    public static class Sort<T> where T : IComparable
    {
        /// <summary>
        /// Сортировка пузырьком
        /// </summary>
        public static void BubbleSort(T[] set)
        {
            T temp;
            for (int i = 0; i < set.Length; i++)
            {
                for (int j = i + 1; j < set.Length; j++)
                {
                    if (set[i].CompareTo(set[j]) > 0)
                    {
                        temp = set[i];
                        set[i] = set[j];
                        set[j] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Сортировка вставкой
        /// </summary>
        public static void InsertionSort(ref T[] set)
        {
            Set<T> result = new Set<T>();
            for (int i = 0; i < set.Length; i++)
            {
                int j = i;
                while (j > 0 && result[j - 1].CompareTo(set[i]) > 0)
                {
                    j--;
                }
                result.Insert(j, set[i]);
            }
            set = result.ToArray();
        }

        /// <summary>
        /// Сортировка Шелла
        /// </summary>
        public static void ShellSort(T[] set)
        {
            int step = set.Length / 2;

            while (step > 0)
            {
                int i, j;
                for (i = step; i < set.Length; i++)
                {
                    T value = set[i];
                    for (j = i - step; (j >= 0) && (set[j].CompareTo(value) > 0); j -= step)
                        set[j + step] = set[j];
                    set[j + step] = value;
                }
                step /= 2;
            }
        }
        
        /// <summary>
        /// Сортировка слиянием
        /// </summary>
        private static void Merge(T[] Mas, int left, int right, int medium)
        {
            int j = left;
            int k = medium + 1;
            int count = right - left + 1;

            if (count <= 1)
                return;

            T[] TmpMas = new T[count];

            for (int i = 0; i < count; ++i)
            {
                if (j <= medium && k <= right)
                {
                    if (Mas[j].CompareTo(Mas[k]) < 0)
                        TmpMas[i] = Mas[j++];
                    else
                        TmpMas[i] = Mas[k++];
                }
                else
                {
                    if (j <= medium)
                        TmpMas[i] = Mas[j++];
                    else
                        TmpMas[i] = Mas[k++];
                }
            }

            j = 0;
            for (int i = left; i <= right; ++i)
            {
                Mas[i] = TmpMas[j++];
            }
        }

        /// <summary>
        /// Сортировка слиянием
        /// </summary>
        private static void MergeSort(object o)
        {
            var a = (T[])((object[])o)[0];
            var l = (int)((object[])o)[1];
            var r = (int)((object[])o)[2];

            int m;

            if (l >= r)// Условие выхода из рекурсии
                return;

            m = (l + r) / 2;

            // Рекурсивная сортировка полученных массивов

            var firstThread = new Thread(MergeSort);
            firstThread.Start(new object[] { a, l, m });

            var secondThread = new Thread(MergeSort);
            secondThread.Start(new object[] { a, m + 1, r });

            while (firstThread.IsAlive || secondThread.IsAlive)
            {
                Thread.Sleep(10);
            }

            Merge(a, l, r, m);
            return;
        }

        public static void MergeSort(T[] a, int l, int r)
        {
            MergeSort(new object[] { a, l, r });
        }

        /// <summary>
        /// Быстрая сортировка
        /// </summary>
        public static void QuickSort(T[] _items, int l, int r)
        {
            T temp;
            T x = _items[l + (r - l) / 2]; //запись эквивалентна (l+r)/2, но не вызввает переполнения на больших данных
            int i = l;
            int j = r;
            
            while (i <= j) //код в while обычно выносят в процедуру particle
            {
                while (_items[i].CompareTo(x) < 0)
                    i++;
                while (_items[j].CompareTo(x) > 0)
                    j--;
                if (i <= j)
                {
                    temp = _items[i];
                    _items[i] = _items[j];
                    _items[j] = temp;
                    i++;
                    j--;
                }
            }
            if (i < r)
                QuickSort(_items, i, r);

            if (l < j)
                QuickSort(_items, l, j);
        }

    }
}
