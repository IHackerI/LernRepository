using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.HashTable
{
    /// <summary>
    /// Подсортировочная таблица хешей
    /// </summary>
    class HashTableList<T> : IHashTable<T>
    {
        public int Size { get; private set; }
        public int Count { get; private set; }

        private List<HashTableNode<T>>[] _table;
        public HashTableList(int size)
        {
            Size = size;

            _table = new List<HashTableNode<T>>[size];
            _table = (from x in _table select new List<HashTableNode<T>>()).ToArray();
        }


        /// <summary>
        /// Вычисление хэша
        /// </summary>
        public int CalcHash(int key)
        {
            return key % Size;
        }

        /// <summary>
        /// Добавление новой записи
        /// </summary>
        public void Add(int key, T value)
        {
            Remove(key);

            HashTableNode<T> newNode = new HashTableNode<T>(key, value);
            int index = CalcHash(key);
            _table[index].Add(newNode);
            Count++;
        }

        /// <summary>
        /// Поиск по ключу
        /// </summary>
        public T FindByKey(int key)
        {
            int index = CalcHash(key);

            var nodeIndex = _table[index].FindIndex(n => n != null && n.Key == key);

            if (nodeIndex < 0)
                return default(T);

            return _table[index][nodeIndex].Value;
        }

        /// <summary>
        /// Удаление по ключу
        /// </summary>
        public void Remove(int key)
        {
            int index = CalcHash(key);

            var nodeIndex = _table[index].FindIndex(n => n != null && n.Key == key);

            if (nodeIndex < 0) return;

            _table[index].RemoveAt(nodeIndex);

            Count--;
        }

        public void Resize(int size)
        {
            var tmpTable = _table;

            Size = size;

            _table = new List<HashTableNode<T>>[size];
            _table = (from x in _table select new List<HashTableNode<T>>()).ToArray();

            foreach (var ar in tmpTable)
            {
                foreach (var el in ar)
                {
                    Add(el.Key, el.Value);
                }
            }
        }

        /// <summary>
        /// Вывод таблицы
        /// </summary>
        public void View()
        {
            for (int index = 0; index < Size; index++)
            {
                _table[index].ForEach(c => Console.WriteLine(c != null ? $"{c.Key} {c.Value}" : string.Empty));
            }
        }
    }
}
