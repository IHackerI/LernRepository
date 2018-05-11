using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.HashTable
{
    class MyHashTable<TValue> : IHashTable<TValue>
    {
        private int _count = 0;

        HashTableNode<TValue>[] _table;

        public int Count
        {
            get { return _count; }
        }

        #region Constructor
        public MyHashTable()
        {
            _table = new HashTableNode<TValue>[0];
        }

        public MyHashTable(int startSize)
        {
            _table = new HashTableNode<TValue>[startSize];
        }
        #endregion

        private int FullGetIndexByKey(int key)
        {
            var tmpIndex = key.GetHashCode() % _table.Length;

            while (true)
            {
                if (_table[tmpIndex] == null || _table[tmpIndex].Key.Equals(key))
                {
                    return tmpIndex;
                }
                tmpIndex++;
                if (tmpIndex >= _table.Length)
                    tmpIndex = 0;
            }
        }

        public TValue FindByKey(int key)
        {
            var index = FullGetIndexByKey(key);
            if (_table[index] == null)
            {
                Console.WriteLine("Ключ " + key + " не найден!");
                return default(TValue);
            }
            return _table[index].Value;
        }

        public void Add(int key, TValue value)
        {
            if (_count >= _table.Length * 0.75)
                Resize();

            HashTableNode< TValue> nHash = new HashTableNode<TValue>(key, value);
            int index = FullGetIndexByKey(key);

            _table[index] = nHash;
            
            _count++;
        }

        public void Remove(int key)
        {
            int index = FullGetIndexByKey(key);

            if (_table[index] != null)
            {
                _table[index] = null;
                _count--;
                return;
            }
            else
            {
                Console.WriteLine("Ключ " + key + " не найден!");
                //throw new KeyNotFoundException();
            }
        }

        public void View()
        {
            for (int i = 0; i < _table.Length; i++)
            {
                if (_table[i] == null) continue;

                Console.WriteLine("Key = " + _table[i].Key.ToString() + " Value = " + _table[i].Value.ToString());
            }
        }

        private void Resize()
        {
            Array.Resize(ref _table, _table.Length<<1);
        }
    }
}
