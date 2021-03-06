﻿using ASD.CustomLists;
using System;

namespace ASD.Sort
{
    public static class Sort<T> where T : IComparable
    {
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

        public static void Merge(T[] Mas, int left, int right, int medium)
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

        public static void MergeSort(T[] a, int l, int r)
        {
            int m;

            if (l >= r)// Условие выхода из рекурсии
                return;

            m = (l + r) / 2;

            // Рекурсивная сортировка полученных массивов
            MergeSort(a, l, m);
            MergeSort(a, m + 1, r);
            Merge(a, l, r, m);
            return;
        }
        public static void QuickSort(T[] _items, int l, int r) // Быстрая сортировка
        {
            T temp;
            T x = _items[l + (r - l) / 2];
            //запись эквивалентна (l+r)/2, 
            //но не вызввает переполнения на больших данных
            int i = l;
            int j = r;
            //код в while обычно выносят в процедуру particle
            while (i <= j)
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

        public static void NoRecursQuickSort(T[] _items, int l, int r) // Быстрая сортировка
        {
            var curLR = new LRObj()
            {
                L = l,
                R = r
            };

            while (true)
            {
                #region QS
                T temp;
                T x = _items[curLR.L + (curLR.R - curLR.L) / 2];
                //запись эквивалентна (l+r)/2, 
                //но не вызввает переполнения на больших данных
                int i = curLR.L;
                int j = curLR.R;
                //код в while обычно выносят в процедуру particle
                while (i <= j)
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
                #endregion

                #region CalcLeftRight
                LRObj left = null;
                LRObj right = null;

                if (i < curLR.R) {
                    left = new LRObj()
                    {
                        L = i,
                        R = curLR.R
                    };
                }

                if (curLR.L < j)
                {
                    right = new LRObj()
                    {
                        L = curLR.L,
                        R = j
                    };
                }
                #endregion

                #region ReplaceThisToLeftRight
                if (left != null)
                {
                    left.Prev = curLR.Prev;
                    
                    left.Next = right;
                    if (left.Next == null)
                        left.Next = curLR.Next;
                }

                if (right != null)
                {
                    right.Next = curLR.Next;
                    
                    right.Prev = left;

                    if (right.Prev == null)
                        right.Prev = curLR.Prev;
                }

                if (left != null || right != null)
                {
                    if (curLR.Prev != null)
                        curLR.Prev.Next = (left == null) ? right : left;

                    if (curLR.Next != null)
                        curLR.Next.Prev = (right == null) ? left : right;
                } else
                {
                    if (curLR.Prev != null)
                        curLR.Prev.Next = curLR.Next;

                    if (curLR.Next != null)
                        curLR.Next.Prev = curLR.Prev;
                }
                #endregion

                #region SetupNextIteration
                if (left != null)
                {
                    curLR = left;
                    continue;
                }

                if (curLR.Prev != null)
                {
                    curLR = curLR.Prev;
                    continue;
                }

                if (right != null)
                {
                    curLR = right;
                    continue;
                }

                if (curLR.Next != null)
                {
                    curLR = curLR.Next;
                    continue;
                }
                #endregion       

                break;
            }
        }

        private class LRObj
        {
            public int L;
            public int R;
            public LRObj Prev;
            public LRObj Next;
        }
    }
}
