using ASD.SetDeckQueueStack;
using System;

namespace ASD.Sort
{
    public static class Sort<T> where T : IComparable
    {
        public static void BubbleSort(T[] set) // Сортировка пузырьком
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

        public static void InsertionSort(ref T[] set) // Сортировка вставкой
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
        public static void ShellSort(T[] set) // Сортировка Шелла
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

        public static void Merge(T[] Mas, int left, int right, int medium) // Сортировка слиянием
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
