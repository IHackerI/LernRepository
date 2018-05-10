using ASD.HashTable;
using ASD.Set;
using ASD.Sort;
using ASD.BinaryTree;
using ASD.Graph;
using System;
using ASD.Helpers;
using System.Linq;

namespace ASD
{
    class Program
    {
        enum TesterName
        {
            None                    = 0,
            SetDeckNodeQueueStack   = 10,
            Sort                    = 20,
            HashTable               = 30,
            BinaryTree              = 40,
            Graph                   = 50
        }

        public delegate void EmptyD();

        static void Main(string[] args)
        {
            while (true)
            {
                var testerNames = Enum.GetNames(typeof(TesterName));
                bool testResult = ChoiceController.ViewChoice(testerNames.Skip(1).ToArray(), new EmptyD[] {
                    SetDeckNodeQueueStackTester.TEST,
                    SortTest.TEST,
                    HashTableTest.TEST,
                    BinaryTreeTester.TEST,
                    GraphTester.TEST
                });
                
                if (!testResult)
                {
                    Console.WriteLine("Введите корректные значения!");
                    Console.WriteLine();
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine("Хотите выйти? y/n");
                if (Console.ReadLine().ToLower() == "y")
                    break;
                Console.WriteLine();
            }
        }
    }
}
