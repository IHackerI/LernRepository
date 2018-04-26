using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.HashTable.Base
{
    class HashTableList<T>
    {
        public int Size { get; private set; }
        public int Count { get; private set; }
        List<MyData< T>>[] listH;
        public HashTableList(int size)
        {
            this.Size = size;
            listH = new List<MyData< T>>[size];
            for (int i = 0; i < size; i++)
                listH[i] = new List<MyData< T>>();
        }


        public int Hash(int key)
        {
            return key % Size;
        }
        //добавление
        public void Add(int key, T value)
        {
            MyData<T> nHash = new MyData<T>(key, value);
            int index = this.Hash(key);
            listH[index].Add(nHash);
            Count++;
        }
        //удаление
        public void Remove(int key)
        {
            int index = this.Hash(key);

            var elem = this.listH[index].FirstOrDefault(c => c!=null  && c.Key == key);

            if (elem == null) return;

            this.listH[index].Remove(elem);

            --Count;
        }
        //просмотр
        public void View()
        {
            for (int index = 0; index < Size; index++)
            {
                listH[index].ForEach(c => Console.WriteLine(c != null ? $"{c.Key} {c.Value}" : string.Empty));
            }

        }
    }
}
