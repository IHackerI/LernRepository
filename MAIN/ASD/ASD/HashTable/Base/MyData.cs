using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.HashTable.Base
{
    class MyData<TValue>
    {

        public int Key;
        public TValue Value;

        public MyData(int key, TValue value)
        {

            Key = key;
            Value = value;

        }
    }
}
