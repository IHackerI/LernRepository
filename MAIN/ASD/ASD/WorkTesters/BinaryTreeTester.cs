using System;
using ASD.BinTree;
using ASD.WorkTesters.Helper;

namespace ASD.WorkTesters
{
    /// <summary>
    /// Тестер бинарного дерева
    /// </summary>
    public static class BinaryTreeTester
    {
        /// <summary>
        /// Точка входа в тестер
        /// </summary>
        public static void TEST()
        {
            #region Подготовка и вывод дерева
            BinaryTree<string> BN = new BinaryTree<string>();

            BN.Insert(10, "10");
            BN.Insert(4, "4");
            BN.Insert(2, "2");
            BN.Insert(3, "3");
            BN.Insert(16, "16");
            BN.Insert(13, "13");
            BN.Insert(18, "18");
            BN.Insert(17, "17");
            BN.Insert(19, "19");

            Console.WriteLine("Исходное дерево:");
            BinaryTreeExtensions<string>.RecursPrint(BN);
            Console.WriteLine();
            #endregion

            while (true)
            {
                //Запрашивает Инструменты ввода/вывода
                //предоставить выбор тестируемого модуля
                var input = IOSystem.SafeSimpleChoice("Выберите действие с деревом:", new string[]
                    {
                        "Добавление",
                        "Поиск по ключу",
                        "Определение уровня по ключу",
                        "Поиск Максимального элемента узла",
                        "Поиск Минимального элемента узла",
                        "Удаление узла",
                        "Рекурсивный вывод",
                        "Нерекурсивный вывод вверх",
                        "Нерекурсивный вывод вниз",
                        "Получить Root",
                        "Малый поворот вправо",
                        "Малый поворот влево",
                        "Большой поворот вправо",
                        "Большой поворот влево",
                        "Закончить тестирование"
                    });

                bool endTest = false;

                //В зависимости от запроса запускаем модуль
                //(отсчёт от нуля)
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
                        r = BN.MaxNode();
                        Console.WriteLine(r.Key + " " + r.Value);
                        break;

                    case 4:
                        r = BN.MinNode();
                        Console.WriteLine(r.Key + " " + r.Value);
                        break;
                    
                    case 5:
                        BN.Remove(IOSystem.GetInt("Введите ключ: "));
                        break;

                    case 6:
                        Console.WriteLine("Рекурсивный вывод: ");
                        BinaryTreeExtensions<string>.RecursPrint(BN);
                        break;

                    case 7:
                        Console.WriteLine("Нерекурсивный вывод вверх: ");
                        BinaryTreeExtensions<string>.UpNoRecursPrint(BN);
                        break;
                    case 8:
                        Console.WriteLine("Нерекурсивный вывод вниз: ");
                        BinaryTreeExtensions<string>.DownNoRecursPrint(BN);
                        break;
                    case 9:
                        Console.WriteLine("Корень: ");
                        Console.WriteLine(" Key: {0} Value: {1}", BN.Root.Key, BN.Root.Value);
                        break;
                    case 10:
                        BN.SmallRightRotate(BN.Find(IOSystem.GetInt("Введите ключ вращаемой вершины: ")));
                        BinaryTreeExtensions<string>.RecursPrint(BN);
                        break;
                    case 11:
                        BN.SmallLeftRotate(BN.Find(IOSystem.GetInt("Введите ключ  вращаемой вершины: ")));
                        BinaryTreeExtensions<string>.RecursPrint(BN);
                        break;
                    case 12:
                        BN.BigRightRotate(BN.Find(IOSystem.GetInt("Введите ключ  вращаемой вершины: ")));
                        BinaryTreeExtensions<string>.RecursPrint(BN);
                        break;
                    case 13:
                        BN.BigLeftRotate(BN.Find(IOSystem.GetInt("Введите ключ  вращаемой вершины: ")));
                        BinaryTreeExtensions<string>.RecursPrint(BN);
                        break;
                    case 14:
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
