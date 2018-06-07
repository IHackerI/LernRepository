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
        public int Distance = int.MaxValue;
        public bool IsChecked;
        public Vertex<T> PrevVert;

        private T _value;
        private ConsoleColor _color;
        public ConsoleColor Color { get { return (ConsoleColor)(15 - (int)_color); }
                                    set { _color = (ConsoleColor)(15 - (int)value); }
                                  }
        public List<Edge<T>> _edges = new List<Edge<T>>();
        

        public IEnumerable<Vertex<T>> GetNeghboursEnumer()
        {
            return _connectedVertexes;
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
                Console.ForegroundColor = _color;
                Console.Write((this.ToString()).PadLeft(12));
                Console.ForegroundColor = tmpColor;
                Console.Write(" alone");
            }
            
            foreach (var neighbour in _connectedVertexes)
            {
                Console.ForegroundColor = _color;
                Console.Write((this.ToString()).PadLeft(12));
                Console.ForegroundColor = tmpColor;
                Console.Write(" - ");
                Console.ForegroundColor = neighbour._color;
                Console.WriteLine(neighbour);
                Console.ForegroundColor = tmpColor;
                // + " - " + neighbour

                Console.WriteLine((_color.ToString()).PadLeft(12) + " - " + neighbour._color);
            }
            Console.ForegroundColor = tmpColor;
        }

        public void ViewHimSelf()
        {
            var tmpColor = Console.ForegroundColor;

            Console.ForegroundColor = _color;
            Console.Write(this);
            Console.ForegroundColor = tmpColor;
            Console.Write(" ({0})", _color.ToString());

            Console.ForegroundColor = tmpColor;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
