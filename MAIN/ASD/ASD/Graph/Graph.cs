using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.Graph
{
    class Graph<T>
    {
        
        public Graph(List<Vertex<T>> Vertex, List<int> Edge)
        {
            foreach (var v in Vertex) AddVertex(v);
            for (int i = 0; i < Edge.Count; i += 2) AddEdge(Edge[i], Edge[i+1]);
        }

        public List<Vertex<T>> Vertexes = new List<Vertex<T>>();
        public Dictionary<int, HashSet<int>> ConnectVertexes { get; } = new Dictionary<int, HashSet<int>>();

        public void AddVertex(Vertex<T> Vertex)
        {
            Vertexes.Add(Vertex);

            ConnectVertexes[Vertexes.Count-1] = new HashSet<int>();
        }

        public void AddEdge(int a, int b)
        {
            if (ConnectVertexes.ContainsKey(a) && ConnectVertexes.ContainsKey(b))
            {
                ConnectVertexes[a].Add(b);
                ConnectVertexes[b].Add(a);
            }
        }



        public HashSet<Vertex<T>> BypassWidth(Graph<T> graph, Vertex<T> primaryVertex)// обход в ширину
        {
            var visitedVertexes = new HashSet<Vertex<T>>();

            if (!graph.ConnectVertexes.ContainsKey(Vertexes.FindIndex(x => x == primaryVertex)))
                return visitedVertexes;

            var queue = new Queue<Vertex<T>>();//отслеживает  найденные вершины  ,  которые не посетили. Первоначально "queue"   содержит начальную вершину
            queue.Enqueue(primaryVertex);

            while (queue.Count > 0)
            {
                var currentVertex = queue.Dequeue();
                if (visitedVertexes.Contains(currentVertex))
                    continue;
                visitedVertexes.Add(currentVertex);

                foreach (var neighbor in graph.ConnectVertexes[Vertexes.FindIndex(x => x == currentVertex)])
                    if (!visitedVertexes.Contains(Vertexes[neighbor]))
                        queue.Enqueue(Vertexes[neighbor]);
            }

            return visitedVertexes;
        }


        public HashSet<Vertex<T>> BypassDepth(Graph<T> graph, Vertex<T> primaryVertex)
        {
            var visitedVertexes = new HashSet<Vertex<T>>();// Отслеживает уже посещенные вершины

            if (!graph.ConnectVertexes.ContainsKey(Vertexes.FindIndex(x => x == primaryVertex)))
                return visitedVertexes;

            var stackVertexes = new Stack<Vertex<T>>();//Отслеживает найденные, но еще не посещаемые вершины. Первоначально стек содержит начальную вершину
            stackVertexes.Push(primaryVertex);

            while (stackVertexes.Count > 0)
            {
                var currentVertex = stackVertexes.Pop();

                if (visitedVertexes.Contains(currentVertex))
                    continue;
                
                visitedVertexes.Add(currentVertex);

                foreach (var neighbor in graph.ConnectVertexes[Vertexes.FindIndex(x => x == currentVertex)])
                    if (!visitedVertexes.Contains(Vertexes[neighbor]))
                    {
                        //Vertexes[neighbor].PrevVertex = currentVertex;
                        stackVertexes.Push(Vertexes[neighbor]);
                    }
            }

            return visitedVertexes;
        }
    }
}
