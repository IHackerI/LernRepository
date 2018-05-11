using System;
using ASD.WorkTesters.Helpers;
using System.Linq;

namespace ASD.WorkTesters
{
    public static class WorkMainTester
    {
        enum TesterName
        {
            None = 0,
            CustomLists = 10,
            Sort = 20,
            HashTable = 30,
            BinaryTree = 40,
            Graph = 50
        }

        public delegate void EmptyD();

        public static void TEST()
        {
            while (true)
            {
                var testerNames = Enum.GetNames(typeof(TesterName));
                bool testResult = IOSystem.InterfacedViewChoice(testerNames.Skip(1).ToArray(), new EmptyD[] {
                    CustomListsTester.TEST,
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
