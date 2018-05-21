using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.CustomLists
{
    public class Node <T>
    {
        public T Element { get; set; }
        public Node<T> NextNode { get; set; }
        public Node<T> PrevNode { get; set; }
        public Node()
        {
            NextNode = null;
            PrevNode = null;
            Element = default(T);
        }
        
    }
}
