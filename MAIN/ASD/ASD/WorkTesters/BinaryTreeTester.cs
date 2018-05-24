using System;
using ASD.BinaryTree;
using ASD.WorkTesters.Helpers;

namespace ASD.WorkTesters
{
    public static class BinaryTreeTester
    {
        public static void TEST()
        {
            BinNode BN = new BinNode();

            BN.Add(10, "1");
            BN.Add(4, "1");
            BN.Add(2, "1");
            BN.Add(3, "1");
            BN.Add(16, "1");
            BN.Add(13, "1");
            BN.Add(18, "1");
            BN.Add(17, "1");

            Console.WriteLine("Исходный массив");
            Console.WriteLine(BN.View());
            Console.WriteLine();

            while (true)
            {
                var input = IOSystem.SafeSimpleChoice("Выберите действие с деревом:", new string[]
                    {
                        "Добавление",
                        "Поиск по ключу",
                        "Определение уровня по ключу",
                        "Поиск максимального элемента узла",
                        "Поиск Минимального элемента узла",
                        "Поиск следующего элемента по индексу",
                        "Удаление узла",
                        "Вывод дерева",
                        "Удаление нижнего элемента",
                        "Рекурсивный вывод",
                        "Нерекурсивный вывод",
                        "Закончить тест"
                    });

                bool endTest = false;

                switch (input)
                {
                    case 0:
                        BN.Add(IOSystem.GetInt("Введите ключ: "), "1");
                        break;

                    case 1:
                        var r = BN.Value(IOSystem.GetInt("Введите ключ: "));
                        Console.WriteLine(r == null ? null : r.Value);
                        break;

                    case 2:
                        Console.WriteLine(BN.Level(BN.Value(IOSystem.GetInt("Введите ключ: "))));
                        break;

                    case 3:
                        r = BN.MaxNode(BN.Value(IOSystem.GetInt("Введите ключ: ")));
                        Console.WriteLine();
                        break;

                    case 4:
                        r = BN.MinNode(BN.Value(IOSystem.GetInt("Введите ключ: ")));
                        Console.WriteLine(r == null ? null : r.Value);
                        break;

                    case 5:
                        r = BN.NextNode(BN.Value(IOSystem.GetInt("Введите ключ: ")));
                        Console.WriteLine(r == null ? null : r.Value);
                        break;

                    case 6:
                        BN.DellNode(IOSystem.GetInt("Введите ключ: "));
                        break;

                    case 7:
                        Console.WriteLine(BN.View());
                        break;

                    case 8:
                        BN.Dell_node_Ur();
                        break;

                    case 9:
                        BN.recInOrder(BN.Root());
                        break;

                    case 10:
                        Node t = BN.MinNode(BN.Root());
                        while (t != null)
                        {
                            Console.WriteLine(" Key: {0} Value: {1}", t.Key, t.Value);
                            t = BN.NextNode(t);
                        }
                        break;

                    case 11:
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
