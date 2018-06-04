using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.BinTree
{
    class Node
    {
        int key;
        object value;
        Node parent;
        Node left;
        Node right;

        public Node(int key, object value)
        {
            parent = left = right = null;
            this.key = key;
            this.value = value;
        }

        public int Key
        {
            set { key = value; }
            get { return key; }
        }

        public object Value
        {
            set { this.value = value; }
            get { return value; }
        }

        public Node Parent
        {
            set { parent = value; }
            get { return parent; }
        }

        public Node Left
        {
            set { left = value; }
            get { return left; }
        }

        public Node Right
        {
            set { right = value; }
            get { return right; }
        }
        
    }
}
