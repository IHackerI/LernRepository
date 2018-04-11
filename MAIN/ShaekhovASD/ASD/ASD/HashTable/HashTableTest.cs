using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASD.HashTable.Base;

namespace ASD.HashTable
{
    public static class HashTableTest
    {
        public static void TEST()
        {
            MyHashTable<string> table = new MyHashTable<string>(3);
            table.Add(312, "abcbca");
            table.Add(123, "qwer");
            table.Add(2, "qw");
            table.View();
            Console.WriteLine("");

            table.Remove(123);
            table.View();
            Console.WriteLine();

            HashTableList<string> t = new HashTableList<string>(5);
            t.Add(1, "abc");
            t.Add(6, "abc");
            t.Add(11, "abc");
            t.Add(16, "a");
            t.Add(0, "bc");
            Console.WriteLine("");
            t.View();

            t.Remove(11);
            t.Remove(0);
            Console.WriteLine("");
            t.View();

            Console.ReadKey();
        }
    }
}
