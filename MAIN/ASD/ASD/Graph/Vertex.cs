using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.Graph
{
    /// <summary>
    /// Вершина графа
    /// </summary>
    public class Vertex<T>
    {
        private T _value;
        public ConsoleColor Color;
        public List<Edge<T>> _edges = new List<Edge<T>>();
        

        public IEnumerator<Vertex<T>> GetNeghboursEnumer()
        {
            return _connectedVertexes.GetEnumerator();
        }
        private List<Vertex<T>> _connectedVertexes = new List<Vertex<T>>();

        public T Value
        {
            get { return _value; }
        }

        public Vertex(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Добавление связи со смежной вершиной
        /// </summary>
        public void Add(Vertex<T> vertex)
        {
            if (_connectedVertexes.Contains(vertex)) return;
            _connectedVertexes.Add(vertex);
        }

        /// <summary>
        /// Удаление связи со смежной вершиной
        /// </summary>
        public void Remove(Vertex<T> vertex)
        {
            _connectedVertexes.Remove(vertex);
        }

        /// <summary>
        /// Вывод раскраски
        /// </summary>
        public void ViewNeighbours()
        {
            var tmpColor = Console.ForegroundColor;

            if (_connectedVertexes.Count < 1)
            {
                Console.ForegroundColor = (ConsoleColor)(15 - (int)Color);
                Console.Write(this);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" alone");
            }
            
            foreach (var neighbour in _connectedVertexes)
            {
                Console.ForegroundColor = (ConsoleColor)(15 - (int)Color);
                Console.Write(this);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("-");
                Console.ForegroundColor = (ConsoleColor)(15 - (int)neighbour.Color);
                Console.WriteLine(neighbour);
            }
            Console.ForegroundColor = tmpColor;
        }

        public override string ToString()
        {
            return _value.ToString();
        }


    }
}
