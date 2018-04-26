using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASD.Set.Base;

namespace ASD.Set
{
    public static class SetTester
    {
        public static void TEST()
        {
            Console.WriteLine("Программа начала работу");
            int _count = 10;
            Random rand = new Random();
            Set<int> set = new Set<int>();
            int[] setInt = new int[10];
            int[] setIntResult = new int[10];
            Set<int> set2 = new Set<int>();
            Set<int> set3 = new Set<int>();

            for (int i = 0; i < _count; i++) // заполнение массивов
            {
                set.Add(rand.Next(0, 50));
                setInt[i] = rand.Next(0, 50);
                set2.Add(rand.Next(0, 50));
            }

            Console.Write("Несортированное множество: ");
            ShowSet(set);
            
            #region Списки
            Deck<int> deck = new Deck<int>();

            deck.AddHead(1);
            deck.InsertAfter(1, 6);
            Console.WriteLine("Длина списка: " + deck.Length);
            Console.Write("С головы списка: ");
            deck.ViewHead();
            Console.Write("С хвоста списка: ");
            deck.ViewTail();
            Console.WriteLine("Содержит ли список единицу? " + deck.Contains(1));
            #endregion

            Console.ReadLine();
        }

        static void ShowSet<T>(System.Collections.Generic.IList<T> arr)
        {
            var enumerator = arr.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.Write(enumerator.Current + " ");
            }
            Console.WriteLine();
        }
    }
}
