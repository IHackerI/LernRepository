using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.Sort.Base
{
    public static class Sort<T> where T: IComparable
    {
        public static List<T> BubbleSort(List<T> set) 
        {
            T temp;
            for (int i = 0; i < set.Count; i++)
            {
                for (int j = i + 1; j < set.Count; j++)
                {
                    if (set[i].CompareTo(set[j])>0)
                    {
                        temp = set[i];
                        set[i] = set[j];
                        set[j] = temp;
                    }
                }
            }
            return set;
        }
        public static List<T> InsertionSort(List<T> set) 
        {
            List<T> result = new List<T>();
            for (int i = 0; i < set.Count; i++)
            {
                int j = i;
                while (j > 0 && result[j - 1].CompareTo(set[i]) > 0)
                {
                    j--;
                }
                result.Insert(j,set[i]);
            }
            return result;
        }
        public static List<T> ShellSort(List<T> set)
        {
            List<T> shell = set;
            int step = shell.Count / 2;

            while (step > 0)
            {
                int i, j;
                for (i = step; i < shell.Count; i++)
                {
                    T value = shell[i];
                    for (j = i - step; (j >= 0) && (shell[j].CompareTo(value) > 0); j -= step)
                        shell[j + step] = shell[j];
                    shell[j + step] = value;
                }
                step /= 2;
            }
            return shell;
        }
        public static void Merge(int[] Mas, int left, int right, int medium)
        {
            int j = left;
            int k = medium + 1;
            int count = right - left + 1;

            if (count <= 1)
                return;

            int[] TmpMas = new int[count];

            for (int i = 0; i < count; ++i)
            {
                if (j <= medium && k <= right)
                {
                    if (Mas[j] < Mas[k])
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

        public static int[] MergeSort(int[] a, int l, int r)
        {
            int m;
            
            if (l >= r)// Условие выхода из рекурсии
                return a;

            m = (l + r) / 2;

            // Рекурсивная сортировка полученных массивов
            MergeSort(a, l, m);
            MergeSort(a, m + 1, r);
            Merge(a, l, r, m);
            return a;
        }
        public static int[] QuickSort(int[] _items, int l, int r) // Быстрая сортировка
        {
            int temp;
            int x = _items[l + (r - l) / 2];
            //запись эквивалентна (min+r)/2, 
            //но не вызввает переполнения на больших данных
            int i = l;
            int j = r;
            //код в while обычно выносят в процедуру particle
            while (i <= j)
            {
                while (_items[i] < x)
                    i++;
                while (_items[j] > x)
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
            return _items;
        }

    }
}
