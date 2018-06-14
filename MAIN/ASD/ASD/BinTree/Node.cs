using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.BinTree
{
    public enum BinSide
    {
        Left,
        Right
    }

    /// <summary>
    /// Элемент бинарного дерева
    /// </summary>
    public class Node<T>
    {
        long? key;
        T value;
        Node<T> left { get; set; }
        Node<T> right { get; set; }
        Node<T> parent { get; set; }

        public Node() { }

        public Node(int key, T value)
        {
            parent = left = right = null;
            this.key = key;
            this.value = value;
        }

        public long? Key
        {
            set { key = value; }
            get { return key; }
        }

        public T Value
        {
            set { this.value = value; }
            get { return value; }
        }

        public Node<T> Parent
        {
            set { parent = value; }
            get { return parent; }
        }

        public Node<T> Left
        {
            set { left = value; }
            get { return left; }
        }

        public Node<T> Right
        {
            set { right = value; }
            get { return right; }
        }

        /// <summary>
        /// Возвращает уровень текущего узла
        /// </summary>
        public int Level()
        {
            var level = 0;
            var curr = parent;
            while (curr.Parent != null)
            {
                level++;
                curr = curr.Parent;
            }
            return level;
        }

        /// <summary>
        /// Следующий узел
        /// </summary>
        public Node<T> NextNode()
        {
            var val = this;
            if (val.Right == null && val.Parent != null)
            {
                if (val == val.Parent.Left)
                    return val.Parent;
                else
                {
                    while (val != val.Parent.Left)
                    {
                        val = val.Parent;
                        if (val.Parent == null)
                            return null;
                    }
                    return val.Parent;
                }
            }
            else
            {
                val = val.Right;
                while (val.Left != null)
                    val = val.Left;
                return val;
            }
        }

        /// <summary>
        /// Предыдущий узел
        /// </summary>
        public Node<T> PrevNode()
        {
            var val = this;
            if (val.Left == null && val.Parent != null)
            {
                if (val == val.Parent.Right)
                    return val.Parent;
                else
                {
                    while (val != val.Parent.Right)
                    {
                        val = val.Parent;
                        if (val.Parent == null)
                            return null;
                    }
                    return val.Parent;
                }
            }
            else
            {
                val = val.Left;
                while (val.Right != null)
                    val = val.Right;
                return val;
            }
        }

        /// <summary>
        /// Определяет, в какой ветви для родительского лежит данный узел
        /// </summary>
        public BinSide? MeForParent()

        {
            if (Parent == null) return null;
            if (Parent.Left == this) return BinSide.Left;
            if (Parent.Right == this) return BinSide.Right;
            return null;
        }
    }
}
