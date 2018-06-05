using System;
using System.Collections.Generic;
using ASD.Graph;
using ASD.WorkTesters.Helper;

namespace ASD.WorkTesters
{
    /// <summary>
    /// Тестирование графа
    /// </summary>
    public static class GraphTester
    {

        /// <summary>
        /// Точка входа в тестер
        /// </summary>
        public static void TEST()
        {
            #region Создание графа
            int vertCount = 10;
            var graph = new Graph<int>();
            Vertex<int>[] verts = new Vertex<int>[vertCount];

            for (int i = 0; i < vertCount; i++)
            {
                verts[i] = new Vertex<int>(i);
                graph.Add(verts[i]);
            }
            
            graph.Connect(verts[0], verts[1]);
            graph.Connect(verts[1], verts[2]);
            graph.Connect(verts[0], verts[3]);
            graph.Connect(verts[2], verts[4]);
            graph.Connect(verts[1], verts[4]);
            graph.Connect(verts[5], verts[6]);
            graph.Connect(verts[5], verts[9]);
            graph.Connect(verts[5], verts[8]);
            graph.Connect(verts[7], verts[9]);
            #endregion

            Vertex<int> newVertex1;
            Vertex<int> newVertex2;
            while (true)
            {
                //Запрашивает Инструменты ввода/вывода
                //предоставить выбор тестируемого модуля
                var input = IOSystem.SafeSimpleChoice("Выберите действие с деревом:", new string[]
                    {
                        "Добавление вершины в граф",
                        "Удаление вершины из графа",
                        "Соединить вершины ребром",
                        "Удалить ребро между вершинами",
                        "Показать все рёбра графа (вывести граф)",
                        "Раскраска графа",
                        "Обход в ширину",
                        "Обход в глубину",
                        "Закончить тестирование"
                    });

                bool endTest = false;

                //В зависимости от запроса запускаем модуль
                //(отсчёт от нуля)
                switch (input)
                {
                    case 0:
                        newVertex1 = new Vertex<int>(IOSystem.GetInt("Введите значение вершины: "));
                        graph.Add(newVertex1);
                        Console.WriteLine("Вершина добавлена");
                        break;

                    case 1:
                        newVertex1 = graph.FindVert(IOSystem.GetInt("Введите значение вершины: "));
                        graph.Remove(newVertex1);
                        Console.WriteLine("Вершина удалена");
                        break;

                    case 2:
                        newVertex1 = graph.FindVert(IOSystem.GetInt("Введите значение вершины №1: "));
                        newVertex2 = graph.FindVert(IOSystem.GetInt("Введите значение вершины №2: "));

                        if(newVertex1 == null || newVertex2 == null)
                        {
                            Console.WriteLine("Вершины не соединены или отсутствуют в графе");
                            break;
                        }

                        graph.Connect(newVertex1, newVertex2);
                        Console.WriteLine("Вершины соединены");
                        break;

                    case 3:
                        newVertex1 = graph.FindVert(IOSystem.GetInt("Введите значение вершины №1: "));
                        newVertex2 = graph.FindVert(IOSystem.GetInt("Введите значение вершины №2: "));

                        if (newVertex1 == null || newVertex2 == null)
                        {
                            Console.WriteLine("Вершины не соединены или отсутствуют в графе");
                            break;
                        }

                        graph.Disconnect(newVertex1, newVertex2);
                        Console.WriteLine("Вершины разъединены");
                        break;

                    case 4:
                        Console.WriteLine("Граф: ");
                        graph.ViewEdges();
                        break;

                    case 5:
                        Console.WriteLine("Раскрашенный граф: ");
                        var colorNum = graph.Paint();
                        graph.ViewEdges();
                        Console.WriteLine("\nКоличество цветов: " + colorNum);
                        graph.ResetColor();
                        break;

                    case 6:
                        Console.WriteLine("________Обход в ширину________");
                        Console.WriteLine(string.Join(" => ", 
                            graph.BypassWidth(graph.FindVert(IOSystem.GetInt("Введите значение вершины: ")))));
                        break;

                    case 7:
                        Console.WriteLine("________Обход в глубину________");
                        Console.WriteLine(string.Join(" => ", 
                            graph.BypassDepth(graph.FindVert(IOSystem.GetInt("Введите значение вершины: ")))));
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
