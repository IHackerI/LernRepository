using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.Graph
{
    class Edge<T>
    {
        public Vertex<T> FirstPoint;
        public Vertex<T> LastPoint;
        public double Length { private set; get; }

        public Edge(Vertex<T> Begin, Vertex<T> End, double distance) // Создание ребра
        {
            FirstPoint = Begin;
            LastPoint = End;
            Length = distance;
        }

        public void View()
        {
            Console.WriteLine(this);
        }

        public override bool Equals(object obj) // Сравнение рёбер
        {
            var tmp = obj as Edge<T>;
            return tmp != null && FirstPoint == tmp.FirstPoint && LastPoint == tmp.LastPoint;
        }
    }
}
