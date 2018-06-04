using System;
using ASD.BinTree;
using ASD.WorkTesters.Helper;

namespace ASD.WorkTesters
{
    public static class BinaryTreeTester
    {
        //http://flash2048.com/post/bin-tree
        public static void TEST()
        {
            BinaryTree<string> BN = new BinaryTree<string>();

            BN.Insert(10, "1");
            BN.Insert(4, "2");
            BN.Insert(2, "3");
            BN.Insert(3, "4");
            BN.Insert(16, "5");
            BN.Insert(13, "6");
            BN.Insert(18, "7");
            BN.Insert(17, "8");
            BN.Insert(19, "9");

            Console.WriteLine("Исходное дерево:");
            BinaryTreeExtensions<string>.Print(BN);
            Console.WriteLine();

            while (true)
            {
                var input = IOSystem.SafeSimpleChoice("Выберите действие с деревом:", new string[]
                    {
                        "Добавление",
                        "Поиск по ключу",
                        "Определение уровня по ключу",
                        "Поиск Максимального элемента узла",
                        "Поиск Минимального элемента узла",
                        "Удаление узла",
                        "Рекурсивный вывод",
                        "Нерекурсивный вывод",
                        "Закончить тестирование"
                    });

                bool endTest = false;

                switch (input)
                {
                    case 0:
                        BN.Insert(IOSystem.GetInt("Введите ключ: "), "1");
                        break;

                    case 1:
                        var r = BN.Find(IOSystem.GetInt("Введите ключ: "));
                        Console.WriteLine(r == null ? null : r.Value);
                        break;

                    case 2:
                        Console.WriteLine(BN.Find(IOSystem.GetInt("Введите ключ: ")).Level());
                        break;

                    case 3:
                        r = BN.Find(IOSystem.GetInt("Введите ключ: ")).MaxNode();
                        Console.WriteLine(r.Key + " " + r.Value);
                        break;

                    case 4:
                        r = BN.Find(IOSystem.GetInt("Введите ключ: ")).MinNode();
                        Console.WriteLine(r == null ? null : r.Value);
                        break;
                    
                    case 5:
                        BN.Remove(IOSystem.GetInt("Введите ключ: "));
                        break;

                    case 6:
                        Console.WriteLine("Рекурсивный вывод: ");
                        BinaryTreeExtensions<string>.Print(BN);
                        break;

                    case 7:
                        Console.WriteLine("Нерекурсивный вывод: ");
                        var t = BN.MinNode();
                        while (t != null)
                        {
                            Console.WriteLine(" Key: {0} Value: {1}", t.Key, t.Value);
                            t = t.NextNode();
                        }
                        break;

                    case 8:
                        endTest = true;
                        break;
                }

                Console.WriteLine();

                if (endTest)
                    break;
            }
        }
    }
}
