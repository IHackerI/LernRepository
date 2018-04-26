using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASD.BinaryTree.Base;

namespace ASD.BinaryTree
{
    public static class BinaryTreeTester
    {
        public static void TEST()
        {
            BinNode BN = new BinNode();
            Node node;

            int x = 1;
            BN.Add(10, "1");
            BN.Add(4, "1");
            BN.Add(2, "1");
            BN.Add(3, "1");
            BN.Add(16, "1");
            BN.Add(13, "1");
            BN.Add(18, "1");
            BN.Add(17, "1");
            Console.Write("Команды: \n 1. Добавление \n 2. Поиск по ключу \n 3. Определение уровня по ключу \n" +
                " 4. Поиск максимального элемента узла \n 5. Поиск Минимального элемента узла \n 6. Поиск следующего элемента по индексу \n" +
                " 7. Удаление узла \n 8. Вывод дерева \n 9. Удаление нижнего элемента \n 0. Выход \n");

            while (x != 0)
            {
                Console.Write("\nВведите номер команды  ");
                x = Convert.ToInt32(Console.ReadLine());
                if (x == 1) //Добавление
                {
                    Console.WriteLine("Введите ключ и значение");
                    BN.Add(Convert.ToInt32(Console.ReadLine()), "1");
                }
                else if (x == 2) //Поиск по ключу
                {
                    Console.WriteLine("Введите ключ");
                    Console.WriteLine(BN.Value(Convert.ToInt32(Console.ReadLine())));
                }
                else if (x == 3) //Определение уровня по ключу
                {
                    Console.WriteLine("Введите ключ");
                    node = BN.Value(Convert.ToInt32(Console.ReadLine()));
                    Console.WriteLine(BN.Level(node));
                }
                else if (x == 4) //Поиск максимального элемента узла
                {
                    Console.WriteLine("Введите ключ");
                    node = BN.Value(Convert.ToInt32(Console.ReadLine()));
                    node = BN.MaxNode(node);
                    Console.WriteLine(node.Key);
                }
                else if (x == 5) //Поиск Минимального элемента узла
                {
                    Console.WriteLine("Введите ключ");
                    node = BN.Value(Convert.ToInt32(Console.ReadLine()));
                    node = BN.MinNode(node);
                    Console.WriteLine(node.Key);
                }

                else if (x == 6) //Поиск следующего элемента по индексу
                {
                    Console.WriteLine("Введите ключ");
                    node = BN.Value(Convert.ToInt32(Console.ReadLine()));
                    node = BN.NextNode(node);
                    Console.WriteLine(node.Key);
                }

                else if (x == 7) //Удаление узла
                {
                    Console.WriteLine("Введите ключ");
                    BN.DellNode(Convert.ToInt32(Console.ReadLine()));
                }
                else if (x == 8) //Вывод дерева
                {
                    Console.WriteLine("Введите ключ");
                    node = BN.Value(Convert.ToInt32(Console.ReadLine()));
                    Console.WriteLine(BN.View(node));
                }
                else if (x == 9) //Удаление нижнего элемента
                {
                    BN.Dell_node_Ur();
                    Console.WriteLine("Введите ключ");
                    node = BN.Value(Convert.ToInt32(Console.ReadLine()));
                    Console.WriteLine(BN.View(node));
                }

            }



            Console.WriteLine("Recursive");
            BN.recInOrder(BN.Root());
            Console.WriteLine("No Recursive");
            Node t = BN.MinNode(BN.Root());
            while (t != null)
            {
                Console.WriteLine(" {0} -> {1}", t.Key, t.Value);
                t = BN.NextNode(t);
            }
            Console.ReadKey();
        }
    }
}
