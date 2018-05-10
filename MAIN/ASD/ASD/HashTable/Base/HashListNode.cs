using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.HashTable.Base
{
    [Obsolete("Class don't use")]
    class HashListNode<T>
    {
        public int Key;
        public T Value;
        public HashListNode<T> Next { get; set; }

        public HashListNode(int key, T value)
        {
            Next = null;
            Key = key;
            Value = value;
        }
    }
}
