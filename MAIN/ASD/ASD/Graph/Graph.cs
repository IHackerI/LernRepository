using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.Graph
{
    class Graph<T>
    {
        
        public Graph(List<Vertex<T>> Vertex, List<Tuple<Vertex<T>, Vertex<T>>> Edge)
        {
            foreach (var v in Vertex) AddVertex(v);
            foreach (var edge in Edge) AddEdge(edge);
        }

        public Dictionary<Vertex<T>, HashSet<Vertex<T>>> ConnectVertexes { get; } = new Dictionary<Vertex<T>, HashSet<Vertex<T>>>();

        public void AddVertex(Vertex<T> Vertex)
        {
            ConnectVertexes[Vertex] = new HashSet<Vertex<T>>();
        }

        public void AddEdge(Tuple<Vertex<T>, Vertex<T>> Edge)
        {
            if (ConnectVertexes.ContainsKey(Edge.Item1) && ConnectVertexes.ContainsKey(Edge.Item2))
            {
                ConnectVertexes[Edge.Item1].Add(Edge.Item2);
                ConnectVertexes[Edge.Item2].Add(Edge.Item1);
            }
        }



        public HashSet<Vertex<T>> BypassWidth(Graph<T> graph, Vertex<T> primaryVertex)// обход в ширину
        {

            var visitedVertexes = new HashSet<Vertex<T>>();

            if (!graph.ConnectVertexes.ContainsKey(primaryVertex))
                return visitedVertexes;

            var queue = new Queue<Vertex<T>>();//отслеживает  найденные вершины  ,  которые не посетили. Первоначально "queue"   содержит начальную вершину
            queue.Enqueue(primaryVertex);

            while (queue.Count > 0)
            {
                var currentVertex = queue.Dequeue();
                if (visitedVertexes.Contains(currentVertex))
                    continue;
                visitedVertexes.Add(currentVertex);

                foreach (var neighbor in graph.ConnectVertexes[currentVertex])
                    if (!visitedVertexes.Contains(neighbor))
                        queue.Enqueue(neighbor);
            }

            return visitedVertexes;
        }


        public HashSet<Vertex<T>> BypassDepth(Graph<T> graph, Vertex<T> primaryVertex)
        {
            var visitedVertexes = new HashSet<Vertex<T>>();// Отслеживает уже посещенные вершины

            if (!graph.ConnectVertexes.ContainsKey(primaryVertex))
                return visitedVertexes;

            var stackVertexes = new Stack<Vertex<T>>();//Отслеживает найденные, но еще не посещаемые вершины. Первоначально стек содержит начальную вершину
            stackVertexes.Push(primaryVertex);

            while (stackVertexes.Count > 0)
            {
                var currentVertex = stackVertexes.Pop();

                if (visitedVertexes.Contains(currentVertex))
                    continue;



                visitedVertexes.Add(currentVertex);

                foreach (var neighbor in graph.ConnectVertexes[currentVertex])
                    if (!visitedVertexes.Contains(neighbor))
                    {
                        neighbor.PrevVertex = currentVertex;
                        stackVertexes.Push(neighbor);
                    }
            }

            return visitedVertexes;
        }
    }
}
