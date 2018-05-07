using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASD.Set.Base;

namespace ASD.Set
{
    public static class SetDeckNodeQueueStackTester
    {
        public static void TEST()
        {
            Console.WriteLine("Программа начала работу");
           
            
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

    }
}
