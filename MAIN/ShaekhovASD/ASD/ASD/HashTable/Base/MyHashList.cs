using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.HashTable.Base
{
    class MyHashList<T>
    {
        public int Key;
        public T Value;
        public MyHashList<T> Next { get; set; }

        public MyHashList(int key, T value)
        {
            Next = null;
            Key = key;
            Value = value;
        }
    }
}
