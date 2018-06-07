using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.BinTree // data = key
{
    /// <summary>
    /// Бинарное дерево
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinaryTree <T>
    {

        Node<T> head;
        public Node<T> Head { get { return head; } }

        /// <summary>
        /// Вставляет значение в дерево
        /// </summary>
        public void Insert(long key, T value)
        {
            if (head == null || head.Key == key)
            {
                if (head == null)
                    head = new Node<T>();

                head.Key = key;
                head.Value = value;
                return;
            }

            if (head.Key > key)
            {
                if (head.Left == null) head.Left = new Node<T>();
                Insert(key, value, head.Left, head);
            }
            else
            {
                if (head.Right == null) head.Right = new Node<T>();
                Insert(key, value, head.Right, head);
            }

            //Insert(key, value, null, head);
        }

        /// <summary>
        /// Вставляет значение в определённый узел дерева
        /// </summary>
        private void Insert(long data, T value, Node<T> node, Node<T> parent)
        {

            if (node.Key == null || node.Key == data)
            {
                node.Key = data;
                node.Value = value;
                node.Parent = parent;
                return;
            }
            if (node.Key > data)
            {
                if (node.Left == null) node.Left = new Node<T>();
                Insert(data, value, node.Left, node);
            }
            else
            {
                if (node.Right == null) node.Right = new Node<T>();
                Insert(data, value, node.Right, node);
            }
        }

        /// <summary>
        /// Вставляет узел в определённый узел дерева
        /// </summary>
        private void Insert(Node<T> remNode, Node<T> newNode, Node<T> parent)
        {
            if (newNode.Key == null || newNode.Key == remNode.Key)
            {
                newNode.Key = remNode.Key;
                newNode.Value = remNode.Value;
                newNode.Left = remNode.Left;
                newNode.Right = remNode.Right;
                newNode.Parent = parent;
                return;
            }
            if (newNode.Key > remNode.Key)
            {
                if (newNode.Left == null) newNode.Left = new Node<T>();
                Insert(remNode, newNode.Left, newNode);
            }
            else
            {
                if (newNode.Right == null) newNode.Right = new Node<T>();
                Insert(remNode, newNode.Right, newNode);
            }
        }
            
        /// <summary>
        /// Максимальный ключ в дереве
        /// </summary>
        public Node<T> MaxNode()
        {
            var curr = head;
            while (curr.Right != null)
            {
                curr = curr.Right;
            }
            return curr;
        }

        /// <summary>
        /// Минимальный ключ в дереве
        /// </summary>
        /// <returns></returns>
        public Node<T> MinNode()
        {
            var curr = head;
            while (curr.Left != null)
            {
                curr = curr.Left;
            }
            return curr;
        }

        /// <summary>
        /// Удаляет узел из дерева
        /// </summary>
        public void Remove(Node<T> node)
        {
            if (node == null) return;
            var me = node.MeForParent();
            
            if (node.Left == null && node.Right == null)//Если у узла нет дочерних элементов, его можно удалять
            {
                if (me == BinSide.Left)
                {
                    node.Parent.Left = null;
                }
                if (me == BinSide.Right)
                {
                    node.Parent.Right = null;
                }

                if (node == head)
                    head = null;

                return;
            }
            if (node.Left == null) //Если нет левого дочернего, то правый дочерний становится на место удаляемого

            {
                if (me == BinSide.Left)
                {
                    node.Parent.Left = node.Right;
                }
                if (me == BinSide.Right)
                {
                    node.Parent.Right = node.Right;
                }

                node.Right.Parent = node.Parent;
                return;
            }
            if (node.Right == null) //Если нет правого дочернего, то левый дочерний становится на место удаляемого

            {
                if (me == BinSide.Left)
                {
                    node.Parent.Left = node.Left;
                }
                if (me == BinSide.Right)
                {
                    node.Parent.Right = node.Left;
                }

                node.Left.Parent = node.Parent;
                return;
            }

            //Если присутствуют оба дочерних узла
            //то правый ставим на место удаляемого
            //а левый вставляем в правый

            if (me == BinSide.Left)
            {
                node.Parent.Left = node.Right;
            }
            if (me == BinSide.Right)
            {
                node.Parent.Right = node.Right;
            }
            if (me == null)
            {
                var bufLeft = node.Left;
                var bufRightLeft = node.Right.Left;
                var bufRightRight = node.Right.Right;
                node.Key = node.Right.Key;
                node.Value = node.Right.Value;
                node.Right = bufRightRight;
                node.Left = bufRightLeft;
                head = node;
                Insert(bufLeft, node, node);
            }
            else
            {
                node.Right.Parent = node.Parent;
                Insert(node.Left, node.Right, node.Right);
            }
        }

        /// <summary>
        /// Удаляет узел из дерева по ключу
        /// </summary>
        public void Remove(long key)
        {
            var removeNode = Find(key);
            if (removeNode != null)
            {
                Remove(removeNode);
            }
        }

        /// <summary>
        /// Ищет узел с заданным значением
        /// </summary>
        public Node<T> Find(long key)
        {
            if (head == null)
                return null;

            if (head.Key == key) return head;
            if (head.Key > key)
            {
                return Find(key, head.Left);
            }
            return Find(key, head.Right);
        }

        /// <summary>
        /// Ищет значение в определённом узле
        /// </summary>
        public Node<T> Find(long key, Node<T> node)
        {
            if (node == null) return null;

            if (node.Key == key) return node;
            if (node.Key > key)
            {
                return Find(key, node.Left);
            }
            return Find(key, node.Right);
        }

        /// <summary>
        /// Количество элементов в дереве
        /// </summary>
        public long CountElements()
        {
            return CountElements(head);
        }

        /// <summary>
        /// Количество элементов в определённом узле
        /// </summary>
        private long CountElements(Node<T> node)
        {
            if (node == null)
                return 0;

            long count = 1;
            if (node.Right != null)
            {
                count += CountElements(node.Right);
            }
            if (node.Left != null)
            {
                count += CountElements(node.Left);
            }
            return count;
        }
    }

    public class BinaryTreeExtensions<T>
    {
        /// <summary>
        /// Вывод дерева
        /// </summary>
        public static void Print(BinaryTree<T> tree)
        {
            if (tree == null || tree.Head == null)
            {
                return;
            }

            Print(tree.Head);
        }

        /// <summary>
        /// Вывод элементов дерева
        /// </summary>
        public static void Print(Node<T> baseNode)
        {
            if (baseNode == null)
                return;

            if (baseNode.Parent == null)
            {
                Console.WriteLine("ROOT:{0}", baseNode.Key);
            }
            else
            {
                if (baseNode.Parent.Left == baseNode)
                {
                    Console.WriteLine("Left for {1}  --> {0}", baseNode.Key, baseNode.Parent.Key);
                }

                if (baseNode.Parent.Right == baseNode)
                {
                    Console.WriteLine("Right for {1} --> {0}", baseNode.Key, baseNode.Parent.Key);
                }
            }
            if (baseNode.Left != null)
            {
                Print(baseNode.Left);
            }
            if (baseNode.Right != null)
            {
                Print(baseNode.Right);
            }
        }
    }

}
