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

        Node<T> root;
        public Node<T> Root { get { return root; } }

        /// <summary>
        /// Вставляет значение в дерево
        /// </summary>
        public void Insert(long key, T value)
        {
            if (root == null || root.Key == key)
            {
                if (root == null)
                    root = new Node<T>();

                root.Key = key;
                root.Value = value;
                return;
            }

            if (root.Key > key)
            {
                if (root.Left == null) root.Left = new Node<T>();
                Insert(key, value, root.Left, root);
            }
            else
            {
                if (root.Right == null) root.Right = new Node<T>();
                Insert(key, value, root.Right, root);
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
            var curr = root;
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
            var curr = root;
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

                if (node == root)
                    root = null;

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
                var newRoot = node.Right;
                var oldRoot = node;
                
                root = newRoot;
                root.Parent = null;

                oldRoot.Left.Parent = root.Left;
                if (root.Left == null)
                    root.Left = oldRoot.Left;
                else
                    root.Left.Left = oldRoot.Left;
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

        public void SmallRightRotate(Node<T> baseNode)
        {
            if (baseNode == null) return;

            var a = baseNode;
            var b = a.Left;     if (b == null) return;
          //var l = b.Left;
            var c = b.Right;
          //var r = a.Right;
          
            if (a.Parent == null)
                root = b;
            else
                if (a.MeForParent() == BinSide.Left)
                    a.Parent.Left = b;
                else
                    a.Parent.Right = b;

            b.Parent = a.Parent;
            a.Parent = b;

            a.Left = c;     if (c != null)
            c.Parent = a;

            b.Right = a;
        }

        public void SmallLeftRotate(Node<T> baseNode)
        {
            if (baseNode == null) return;

            var a = baseNode;
            var b = a.Right;    if (b == null) return;
          //var l = a.Left;
            var c = b.Left;
          //var r = b.Right;
          
            if (a.Parent == null)
                root = b;
            else
                if (a.MeForParent() == BinSide.Left)
                    a.Parent.Left = b;
                else
                    a.Parent.Right = b;

            b.Parent = a.Parent;
            a.Parent = b;
            
            a.Right = c; if (c != null)
            c.Parent = a;

            b.Left = a;
        }

        public void BigRightRotate(Node<T> baseNode)
        {
            if (baseNode == null) return;

            var a = baseNode;
            var b = a.Left;     if (b == null) return;
            var c = b.Right;    if (c == null) return;
          //var l = b.Left;
            var m = c.Left;
            var n = c.Right;
          //var r = a.Right;
            
            if (a.Parent == null)
                root = c;
            else
                if (a.MeForParent() == BinSide.Left)
                    a.Parent.Left = c;
                else
                    a.Parent.Right = c;

            c.Parent = a.Parent;

            c.Left = b;
            b.Parent = c;

            c.Right = a;
            a.Parent = c;

            b.Right = m;    if (m != null)
            m.Parent = b;

            a.Left = n;     if (n != null)
            n.Parent = a;
        }

        public void BigLeftRotate(Node<T> baseNode)
        {
            if (baseNode == null) return;

            var a = baseNode;
            var b = a.Right;    if (b == null) return;
            var c = b.Left;     if (c == null) return;
          //var l = a.Left;
            var m = c.Left;
            var n = c.Right;
          //var r = b.Right;
            
            if (a.Parent == null)
                root = c;
            else
                if (a.MeForParent() == BinSide.Left)
                    a.Parent.Left = c;
                else
                    a.Parent.Right = c;

            c.Parent = a.Parent;

            c.Left = a;
            a.Parent = c;

            c.Right = b;
            b.Parent = c;

            a.Right = m;    if (m != null)
            m.Parent = a;

            b.Left = n;     if (n != null)
            n.Parent = b;
        }

        /// <summary>
        /// Ищет узел с заданным значением
        /// </summary>
        public Node<T> Find(long key)
        {
            if (root == null)
                return null;

            if (root.Key == key) return root;
            if (root.Key > key)
            {
                return Find(key, root.Left);
            }
            return Find(key, root.Right);
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
            if (root == null)
                return 0;
            return root.CountElements();
        }
    }

    public class BinaryTreeExtensions<T>
    {
        /// <summary>
        /// Вывод дерева
        /// </summary>
        public static void RecursPrint(BinaryTree<T> tree)
        {
            if (tree == null || tree.Root == null)
            {
                return;
            }

            var views = new List<List<string>>();

            RecursPrint(tree.Root, 0, 0, views);

            Console.WriteLine();

            int maxRow = 0;

            foreach (var col in views)
            {
                if (maxRow < col.Count)
                    maxRow = col.Count;
            }
            
            int width = (int)Math.Pow(2, views.Count-1);

            for (int i = 0; i < views.Count; i++)
            {
                int step = Math.Max(1, width);
                
                foreach (var view in views[i])
                {
                    Console.Write(view.PadLeft(step).PadRight(step+step));
                }

                width >>= 1;
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Вывод элементов дерева
        /// </summary>
        public static void RecursPrint(Node<T> baseNode, int level, int ind, List<List<string>> views)
        {
            if (views.Count <= level)
                views.Add(new List<string>());

            while (views[level].Count < ind)
            {
                views[level].Add("_");
            }

            if (baseNode == null)
            {
                views[level].Add("_");
                return;
            }
            
            views[level].Add(baseNode.Key.ToString());
            
            RecursPrint(baseNode.Left , level + 1, (ind<<1)  , views);
            RecursPrint(baseNode.Right, level + 1, (ind<<1)+1, views);
        }

        public static void UpNoRecursPrint(BinaryTree<T> tree)
        {
            var t = tree.MinNode();
            while (t != null)
            {
                Console.WriteLine(" Key: {0}", t.Key);
                t = t.NextNode();
            }
        }

        public static void DownNoRecursPrint(BinaryTree<T> tree)
        {
            var t = tree.MaxNode();
            while (t != null)
            {
                Console.WriteLine(" Key: {0}", t.Key);
                t = t.PrevNode();
            }
        }
    }
}
