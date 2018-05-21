using System;
using System.Collections.Generic;
using ASD.Graph;

namespace ASD.WorkTesters
{
    public static class GraphTester
    {
        public static void TEST()
        {
            Vertex<int> v1 = new Vertex<int>(1, "A");
            Vertex<int> v2 = new Vertex<int>(2, "B");
            Vertex<int> v3 = new Vertex<int>(3, "C");
            Vertex<int> v4 = new Vertex<int>(4, "D");
            Vertex<int> v5 = new Vertex<int>(5, "E");
            Vertex<int> v6 = new Vertex<int>(6, "F");
            Vertex<int> v7 = new Vertex<int>(7, "G");
            Vertex<int> v8 = new Vertex<int>(8, "H");
            Vertex<int> v9 = new Vertex<int>(9, "I");
            Vertex<int> v10 = new Vertex<int>(10, "J");
            var vertexes = new List<Vertex<int>> { v1, v2, v3, v4, v5, v6, v7, v8, v9, v10 };//создаются вершины

            var edges = new List<Tuple<Vertex<int>, Vertex<int>>>{Tuple.Create(v1,v2), Tuple.Create(v1,v3), //создаём ребра(объекты кортежи) (списки смежности)

            Tuple.Create(v2,v4), Tuple.Create(v3,v5), Tuple.Create(v3,v6),
            Tuple.Create(v4,v7), Tuple.Create(v5,v7), Tuple.Create(v5,v8),
            Tuple.Create(v5,v6), Tuple.Create(v8,v9), Tuple.Create(v9,v10)};


            var graph = new Graph<int>(vertexes, edges);



            Console.WriteLine("________Обход в ширину_______ ");
            Console.WriteLine(string.Join("=> ", graph.BypassWidth(graph, v1)));


            Console.WriteLine("______Обход в глубину______");
            Console.WriteLine(string.Join(" \n", graph.BypassDepth(graph, v1)));
            
            Console.ReadKey();
        }
    }
}
