using System;
using System.Collections.Generic;
using ASD.Graph;

namespace ASD.WorkTesters
{
    public static class GraphTester
    {
        public static void TEST()
        {
            Vertex<int> v0 = new Vertex<int>(1, "A");
            Vertex<int> v1 = new Vertex<int>(2, "B");
            Vertex<int> v2 = new Vertex<int>(3, "C");
            Vertex<int> v3 = new Vertex<int>(4, "D");
            Vertex<int> v4 = new Vertex<int>(5, "E");
            Vertex<int> v5 = new Vertex<int>(6, "F");
            Vertex<int> v6 = new Vertex<int>(7, "G");
            Vertex<int> v7 = new Vertex<int>(8, "H");
            Vertex<int> v8 = new Vertex<int>(9, "I");
            Vertex<int> v9 = new Vertex<int>(10, "J");
            var vertexes = new List<Vertex<int>> { v0, v1, v2, v3, v4, v5, v6, v7, v8, v9 };//создаются вершины

            var edges = new List<int>{ 
                //создаём ребра(объекты кортежи) (списки смежности)

                0,1,  0,2, 
                1,3,  2,4,  2,5,
                3,6,  4,6,  4,7,
                4,5,  7,8,  8,9
            };


            var graph = new Graph<int>(vertexes, edges);



            Console.WriteLine("________Обход в ширину_______ ");
            Console.WriteLine(string.Join(" => ", graph.BypassWidth(graph, v0)));


            Console.WriteLine("______Обход в глубину______");
            Console.WriteLine(string.Join(" => ", graph.BypassDepth(graph, v0)));
        }
    }
}
