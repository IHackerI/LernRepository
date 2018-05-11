using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.HashTable
{
    class HashTableNode<TValue>
    {

        public int Key;
        public TValue Value;

        public HashTableNode(int key, TValue value)
        {

            Key = key;
            Value = value;

        }
    }
}
