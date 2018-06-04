﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.SetDeckQueueStack
{
    /// <summary>
    /// Динамический список
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Set<T>: IList<T> where T : IComparable
    {
        T[] _items;
        public int Count { get; private set; }

        public bool IsReadOnly
        {
            get { return false; }
        }
        
        /// <summary>
        /// Обращение по индексу
        /// </summary>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
                _items[index] = value;
            }
        }
        public bool Contains(T obj)
        {
            var index = IndexOf(obj);
            return index > -1;
        }

        public int IndexOf(T obj)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_items[i].Equals(obj))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Добавление элемента в множество
        /// </summary>
        public void Add(T obj)
        {
            if (IndexOf(obj) > -1)
            {
                return;
            }

            CheckArray(Count);

            _items[Count] = obj;
            Count++;
        }

        /// <summary>
        /// Добавление в конец исходного множества переданного множества
        /// </summary>
        public void AddRange(IList<T> range)
        {
            var ar = (from x in range where !Contains(x) select x).ToArray();

            var finalLen = ar.Length + Count;

            CheckArray(finalLen - 1);

            for (int i = 0; i < ar.Length; i++)
            {
                _items[Count + i] = ar[i];
            }

            Count = finalLen;
        }

        /// <summary>
        /// Объединение множеств
        /// </summary>
        public static Set<TT> Union<TT>(Set<TT> FirstRange, Set<TT> SecondRange) where TT : IComparable
        {
            var ans = new Set<TT>();
            ans.AddRange(FirstRange.ToArray());
            ans.AddRange(SecondRange.ToArray());
            return ans;
        }

        /// <summary>
        /// Пересечение множеств
        /// </summary>
        public static Set<TT> Intersection<TT>(Set<TT> FirstRange, Set<TT> SecondRange) where TT : IComparable
        {
            var ans = new Set<TT>();

            var firstAr = FirstRange.ToArray();
            var secondAr = SecondRange.ToArray();

            ans.AddRange((from x in firstAr where SecondRange.Contains(x) select x).ToArray());
            ans.AddRange((from x in secondAr where FirstRange.Contains(x) select x).ToArray());

            return ans;
        }

        /// <summary>
        /// Дополнение множества
        /// </summary>
        public static Set<TT> Addition<TT>(Set<TT> FirstRange, Set<TT> SecondRange) where TT : IComparable
        {
            var ans = new Set<TT>();

            var firstAr = FirstRange.ToArray();
            var secondAr = SecondRange.ToArray();

            ans.AddRange((from x in firstAr where !SecondRange.Contains(x) select x).ToArray());
            ans.AddRange((from x in secondAr where !FirstRange.Contains(x) select x).ToArray());

            return ans;
        }

        /// <summary>
        /// Удаление элемента множества
        /// </summary>
        public bool Remove(T obj)
        {
            var index = IndexOf(obj);
            if (index < 0)
                return false;
            RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Удаление элемента множества по индексу
        /// </summary>
        public void RemoveAt(int index)
        {
            for (int i = index; i < Count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
            Count--;
        }

        /// <summary>
        /// Добавление элемента с заданным индексом
        /// </summary>
        public void Insert(int index, T value)
        {
            if (index < 0 || index > Count) throw new ArgumentOutOfRangeException();
            Add(default(T));

            for (int i = Count - 1; i > index; i--)
            {
                this[i] = this[i - 1];
            }
            this[index] = value;
        }

        /// <summary>
        ///  Проверяет вываливается ли индекс за пределы массива и длиняет его, если это необходимо
        /// </summary>
        public void CheckArray(int index)
        {
            if (_items == null)
            {
                _items = new T[index + 1];
                return;
            }

            int newLen = _items.Length > 0 ? _items.Length : 1;


            while (newLen <= index)
            {
                newLen <<= 1; // Умножение на 2
            }

            if (newLen > _items.Length)
            {
                T[] ans = new T[newLen];

                for (int i = 0; i < _items.Length; i++)
                {
                    ans[i] = _items[i];
                }

                _items = ans;
            }
        }

        /// <summary>
        /// Преобразование динамического массива в статический
        /// </summary>
        public T[] ToArray()
        {
            var ans = new T[Count];

            for (int i = 0; i < ans.Length; i++)
            {
                ans[i] = _items[i];
            }
            return ans;
        }

        /// <summary>
        /// Вывод множество всех подмножеств
        /// </summary>
        public static List<Set<TT>> Subset<TT>(Set<TT> set) where TT : IComparable
        {
            var listSub = new List<Set<TT>>();


            for (int mask = 0; mask < (1 << set.Count); mask++)
            {
                Set<TT> SubS = new Set<TT>();
                for (int j = 0; j < set.Count; j++)
                {
                    if ((mask & (1 << j)) != 0)
                        SubS.Add(set[j]);

                }
                listSub.Add(SubS);
            }
            return listSub;
        }

        /// <summary>
        /// Очистка множества
        /// </summary>
        public void Clear()
        {
            _items = new T[0];
            Count = 0;
        }

        /// <summary>
        /// Вывод множества
        /// </summary>
        public void View()
        {
            foreach (var element in ToArray())
            {
                Console.Write(element + " ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Копирует в переданный массив элементы начиная с переданного индекса
        /// </summary>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException();
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException();
            if ((arrayIndex + Count) >= array.Length) throw new ArgumentException();

            for(int i = 0; i < Count; i++)
            {
                array[i + arrayIndex] = this[i];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SetEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class SetEnumerator : IEnumerator<T>
        {
            private Set<T> _set;

            public SetEnumerator(Set<T> set)
            {
                _set = set;
            }

            public T Current
            {
                get { return _set[_currIndex]; }
            }
            int _currIndex;

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                _currIndex++;
                 return _currIndex > -1 && _currIndex < _set.Count;
            }

            public void Reset()
            {
                _currIndex = -1;
            }
        }
    }
}