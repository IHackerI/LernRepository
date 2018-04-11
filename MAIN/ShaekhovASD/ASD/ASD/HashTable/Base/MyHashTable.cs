using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.HashTable.Base
{
    class MyHashTable< TValue>
    {
        private int Size { get; set; }
        private int capacity = 0;

        MyData< TValue>[] myHash;

        public int Count
        {
            get
            {
                return Size;
            }
        }

        public MyHashTable(int size)
        {
            this.Size = size;
            myHash = new MyData< TValue>[size];
            for (int i = 0; i < Size; i++)
                myHash[i] = null;
        }

        private int GetIndexByKey(int key)
        {
            return key.GetHashCode() % this.Size;
        }

        public MyData< TValue> FindByKey(int key)
        {
            var item = this.myHash[this.GetIndexByKey(key)];

            if (item != null) return item;

            for (var i = 0; i < this.Size; ++i)
                if (myHash[i] == null &&
                    myHash[i].Key.Equals(key))
                    return myHash[i];

            return null;
        }

        public void Add(int key, TValue value)
        {
            if (this.capacity >= this.Size * 0.75)
                this.Resize();

            MyData< TValue> nHash = new MyData<TValue>(key, value);
            int index = this.GetIndexByKey(key);
            if (myHash[index] == null)
            {
                myHash[index] = nHash;
                ++this.capacity;
            }
            else
            {
                while(myHash[index] !=null)
                {
                    index++; if (index == Size) index = 0;
                }
                myHash[index] = nHash;
                ++this.capacity;
               
            }
           
        }

        public void Remove(int key)
        {
            int index = this.GetIndexByKey(key);

            if (myHash[index] != null)
            {
                myHash[index] = null;
                --this.capacity;
                return;
            }
            else
            {
                for (var i = 0; i < this.Size; ++i)
                    if (myHash[i] == null &&
                        myHash[i].Key.Equals(key))
                    {
                        myHash[i] = null;
                        --this.capacity;
                        return;
                    }
            }
        }

        public void View()
        {
            for (int i = 0; i < Size; i++)
            {
                if (myHash[i] == null) continue;

                Console.WriteLine("Key = " + myHash[i].Key.ToString() + " Value = " + myHash[i].Value.ToString());
            }

        }

        private void Resize()
        {
            this.capacity = 0;
            var tmp = new List<MyData<TValue>>(this.myHash);
            this.myHash = new MyData< TValue>[this.Size *= 2];

            tmp.ForEach(c => this.Add(c.Key, c.Value));
        }
    }
}
