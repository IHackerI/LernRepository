using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ASD.Sort
{
    /// <summary>
    /// Класс сортировок 
    /// </summary>
    public static class Sort<T> where T : IComparable
    {
        private class LRCont : IEnumerable<LRCont>
        {
            public int left;
            public int middle = -1;
            public int right;
            public LRCont next;

            #region Enumers
            public IEnumerator<LRCont> GetEnumerator()
            {
                return new LRContEnumerator(this);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public class LRContEnumerator : IEnumerator<LRCont>
            {
                LRCont src;

                public LRCont Current { get { return _cur; } }
                LRCont _cur;

                object IEnumerator.Current { get { return _cur; } }

                public LRContEnumerator(LRCont source)
                {
                    src = source;
                }

                public void Dispose()
                {
                    
                }

                public bool MoveNext()
                {
                    if (_cur == null)
                    {
                        _cur = src;
                    } else
                        _cur = _cur.next;
                    return _cur != null;
                }

                public void Reset()
                {

                }
            }
            #endregion
        }

        public static void ParallelMergeSort(T[] a, int l, int r, int threadsCount)
        {

            int step = (r - l) / threadsCount;

            LRCont start = new LRCont();
            start.left = l;
            start.right = l + step;

            LRCont end = start;

            while (end.right < r)
            {
                var newC = new LRCont();
                newC.left = end.right + 1;
                newC.right = Math.Min(newC.left + step, r);
                end.next = newC;

                end = newC;
            }

            Parallel.ForEach(start, delegate(LRCont cur)
            {
                MergeSort(a, cur.left, cur.right);
            });



            while (start.next != null)
            {
                var cur = start;

                while (cur != null && cur.next != null)
                {
                    cur.middle = cur.right;
                    cur.right = cur.next.right;
                    cur.next = cur.next.next;

                    cur = cur.next;
                }
                

                Parallel.ForEach(start, delegate(LRCont el) 
                {
                    if (el.middle < 0) return;

                    Merge(a, el.left, el.right, el.middle);
                });
                
            }

            
        }

        /// <summary>
        /// Сортировка слиянием
        /// </summary>
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

        /// <summary>
        /// Сортировка слиянием
        /// </summary>
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
    }
}
