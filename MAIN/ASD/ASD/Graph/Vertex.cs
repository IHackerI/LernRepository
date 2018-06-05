using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.Graph
{


    /// <summary>
    /// Цвет вершины графа
    /// </summary>
    enum Color
    {
        None,
        White,
        Black,
        Red,
        Green,
        Blue,
        Orange,
        Yellow,
        Purple
    }

    /// <summary>
    /// Вершина графа
    /// </summary>
    class Vertex<T>
    {
        public Vertex(T value, string label) // Создание вершины
        {
            Label = label;
            Value = value;
            Color = null;
        }

        public string Label { private set; get; }
        public T Value { private set; get; }
        public string  Color{ private set; get; }
        public int Length = -1;
        public Vertex<T> PrevVertex;



        public override string ToString()
        {
            var res = string.Empty;
            var vertex = this;

            while(vertex != null)
            {
                res += $" {vertex.Value}";
                vertex = vertex.PrevVertex;
            }
            return res;
        }



    }
}
