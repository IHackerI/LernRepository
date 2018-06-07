using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.Graph
{
    /// <summary>
    /// Ребро графа
    /// </summary>
    public class Edge<T>
    {
        public Vertex<T> FirstPoint;
        public Vertex<T> LastPoint;
        public double Length { private set; get; }

        /// <summary>
        /// Создание ребра
        /// </summary>
        public Edge(Vertex<T> Begin, Vertex<T> End, double distance)
        {
            FirstPoint = Begin;
            LastPoint = End;
            Length = distance;
        }

        public override string ToString()
        {
            return FirstPoint + "-" + LastPoint;
        }
    }
}
