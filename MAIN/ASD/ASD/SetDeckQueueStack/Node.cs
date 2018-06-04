using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.SetDeckQueueStack
{
    public class Node <T>
    {
        public Node()
        {
            Next = null;
            Prev = null;
            Element = default(T);
        }
        public T Element { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Prev { get; set; }
    }
}
