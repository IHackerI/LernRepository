using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.Graph
{
    /// <summary>
    /// Граф
    /// </summary>
    public class Graph<T>
    {
        private List<Vertex<T>> _vertexes = new List<Vertex<T>>();

        /// <summary>
        /// Добавление вершины в граф
        /// </summary>
        public void Add(Vertex<T> vertex)
        {
            if (_vertexes.Contains(vertex) || vertex == null) return;
            _vertexes.Add(vertex);

        }

        /// <summary>
        /// Соединение двух вершин ребром
        /// </summary>
        public void Connect(Vertex<T> vertex1, Vertex<T> vertex2)
        {
            if (!_vertexes.Contains(vertex1) || !_vertexes.Contains(vertex2))
            {
                Console.WriteLine("Какой-то из вершин нет в графе!");
                return;
            }

            vertex1.Add(vertex2);
            vertex2.Add(vertex1);

            vertex1._edges.Add(new Edge<T>(vertex1, vertex2, 1.0));
            vertex2._edges.Add(new Edge<T>(vertex1, vertex2, 1.0));
        }

        /// <summary>
        /// Поиск вершины в графе
        /// </summary>
        public Vertex<T> FindVert(T value)
        {
            foreach(var vert in _vertexes)
            {
                if (vert.Value.Equals(value)) return vert;
            }
            return null;
        }

        /// <summary>
        /// Удаление ребра между вершинами
        /// </summary>
        public void Disconnect(Vertex<T> vertex1, Vertex<T> vertex2)
        {
            if (!_vertexes.Contains(vertex1) || !_vertexes.Contains(vertex2))
            {
                Console.WriteLine("Какой-то из вершин нет в графе!");
                return;
            }

            vertex1.Remove(vertex2);
            vertex2.Remove(vertex1);

            #region Добавление ребра в список рёбер
            for (int i = 0; i < vertex1._edges.Count; i++)
            {
                if (vertex1.Equals(vertex1._edges[i].FirstPoint) &&
                    vertex2.Equals(vertex1._edges[i].LastPoint))
                {
                    vertex1._edges.Remove(vertex1._edges[i]);
                    break;
                }
            }
            for (int i = 0; i < vertex1._edges.Count; i++)
            {
                if (vertex1.Equals(vertex2._edges[i].FirstPoint) &&
                    vertex2.Equals(vertex2._edges[i].LastPoint))
                {
                    vertex2._edges.Remove(vertex2._edges[i]);
                    break;
                }
            }
            #endregion
        }

        /// <summary>
        /// Вывод графа в консоль
        /// </summary>
        public void ViewEdges()
        {
            foreach (var vertex in _vertexes)
            {
                vertex.ViewNeighbours();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Удаление вершины из графа
        /// </summary>
        public void Remove(Vertex<T> vertex)
        {
            _vertexes.Remove(vertex);
        }

        /// <summary>
        /// Раскраска графа
        /// </summary>
        public int Paint()
        {
            ResetColor();

            int MaxColor = 0;
            
            foreach (var vertex in _vertexes)
            {
                int myColor = 1;
                MaxColor = Math.Max(myColor, MaxColor);

                while (true)
                {
                    var enumer = vertex.GetNeghboursEnumer();
                    bool badIter = false;

                    while (enumer.MoveNext())
                    {
                        if ((int)enumer.Current.Color == myColor)
                        {
                            badIter = true;
                            myColor++;
                            MaxColor = Math.Max(myColor, MaxColor);
                            break;
                        }
                    }

                    if (!badIter)
                    {
                        break;
                    }
                }

                vertex.Color = (ConsoleColor)myColor;
            }
            return MaxColor;
        }

        /// <summary>
        /// Сброс цвета
        /// </summary>
        public void ResetColor()
        {
            foreach (var v in _vertexes)
            {
                v.Color = ConsoleColor.Black;
            }
        }

        /// <summary>
        /// Обход в ширину
        /// </summary>
        public HashSet<Vertex<T>> BypassWidth(Vertex<T> primaryVertex)// обход в ширину
        {
            var visitedVertexes = new HashSet<Vertex<T>>();// Отслеживает уже посещенные вершины
            if (primaryVertex == null)
            {
                Console.WriteLine("Такой вершины не существует!");
                return visitedVertexes;
            }

            if (!_vertexes.Contains(primaryVertex))
                return visitedVertexes;

            var queue = new Queue<Vertex<T>>();//отслеживает  найденные вершины  ,  которые не посетили. Первоначально "queue"   содержит начальную вершину
            queue.Enqueue(primaryVertex);

            while (queue.Count > 0)
            {
                var currentVertex = queue.Dequeue();
                if (visitedVertexes.Contains(currentVertex))
                    continue;
                visitedVertexes.Add(currentVertex);

                var enumer = currentVertex.GetNeghboursEnumer();
                while (enumer.MoveNext())
                {
                    if (!visitedVertexes.Contains(enumer.Current))
                        queue.Enqueue(enumer.Current);
                }
            }

            return visitedVertexes;
        }

        /// <summary>
        /// Обход в глубину
        /// </summary>
        public HashSet<Vertex<T>> BypassDepth(Vertex<T> primaryVertex)
        {

            var visitedVertexes = new HashSet<Vertex<T>>();// Отслеживает уже посещенные вершины
            if (primaryVertex == null)
            {
                Console.WriteLine("Такой вершины не существует!");
                return visitedVertexes;
            }
            
            if (!_vertexes.Contains(primaryVertex))
                return visitedVertexes;

            var stackVertexes = new Stack<Vertex<T>>();//Отслеживает найденные, но еще не посещаемые вершины. Первоначально стек содержит начальную вершину
            stackVertexes.Push(primaryVertex);

            while (stackVertexes.Count > 0)
            {
                var currentVertex = stackVertexes.Pop();

                if (visitedVertexes.Contains(currentVertex))
                    continue;



                visitedVertexes.Add(currentVertex);
                var enumer = currentVertex.GetNeghboursEnumer();
                while (enumer.MoveNext())
                {
                    if (!visitedVertexes.Contains(enumer.Current))
                    {
                        stackVertexes.Push(enumer.Current);
                    }
                }
            }

            return visitedVertexes;
        }
    }
}
