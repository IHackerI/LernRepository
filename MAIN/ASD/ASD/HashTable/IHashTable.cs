using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASD.HashTable
{
    public interface IHashTable<T>
    {
        void Add(int key, T value);
        T FindByKey(int key);
        void Remove(int key);
        void View();
    }
}
