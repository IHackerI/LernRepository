using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.BinTree // data = key
{
    public enum BinSide
    {
        Left,
        Right
    }
    
    public class BinaryTree <T>
    {

        public long? Key { get; private set; }
        public T Value;
        public BinaryTree<T> Left { get; set; }
        public BinaryTree<T> Right { get; set; }
        public BinaryTree<T> Parent { get; set; }

        
        public void Insert(long key, T value)// Вставляет целочисленное значение в дерево
        {
            if (Key == null || Key == key)
            {
                Key = key;
                Value = value;
                return;
            }
            if (Key > key)
            {
                if (Left == null) Left = new BinaryTree<T>();
                Insert(key, value,  Left, this);
            }
            else
            {
                if (Right == null) Right = new BinaryTree<T>();
                Insert(key, value, Right, this);
            }
        }

        private void Insert(long data, T value, BinaryTree<T> node, BinaryTree<T> parent)
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
                if (node.Left == null) node.Left = new BinaryTree<T>();
                Insert(data, value, node.Left, node);
            }
            else
            {
                if (node.Right == null) node.Right = new BinaryTree<T>();
                Insert(data, value, node.Right, node);
            }
        } // Вставляет значение в определённый узел дерева

     
        private void Insert(BinaryTree<T> data, BinaryTree<T> node, BinaryTree<T> parent)
        {

            if (node.Key == null || node.Key == data.Key)
            {
                node.Key = data.Key;
                node.Value = data.Value;
                node.Left = data.Left;
                node.Right = data.Right;
                node.Parent = parent;
                return;
            }
            if (node.Key > data.Key)
            {
                if (node.Left == null) node.Left = new BinaryTree<T>();
                Insert(data, node.Left, node);
            }
            else
            {
                if (node.Right == null) node.Right = new BinaryTree<T>();
                Insert(data, node.Right, node);
            }
        } // Вставляет узел в определённый узел дерева

        private BinSide? MeForParent(BinaryTree<T> node) // Определяет, в какой ветви для родительского лежит данный узел

        {
            if (node.Parent == null) return null;
            if (node.Parent.Left == node) return BinSide.Left;
            if (node.Parent.Right == node) return BinSide.Right;
            return null;
        }

        public int Level() // Возвращает уровень текущего узла
        {
            var level = 0;
            var curr = this;
            while(curr.Parent != null)
            {
                level++;
                curr = curr.Parent;
            }
            return level;
        }

        public BinaryTree<T> MaxNode()
        {
            
            var curr = this;
            while (curr.Right != null)
            {
                curr = curr.Right;
            }
            return curr;
        }

        public BinaryTree<T> MinNode()
        {

            var curr = this;
            while (curr.Left != null)
            {
                curr = curr.Left;
            }
            return curr;
        }

        
        public void Remove(BinaryTree<T> node) // Удаляет узел из дерева

        {
            if (node == null) return;
            var me = MeForParent(node);
            
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
                Insert(bufLeft, node, node);
            }
            else
            {
                node.Right.Parent = node.Parent;
                Insert(node.Left, node.Right, node.Right);
            }
        }

        public void Remove(long key)
        {
            var removeNode = Find(key);
            if (removeNode != null)
            {
                Remove(removeNode);
            }
        } // Удаляет значение из дерева
        
        public BinaryTree<T> Find(long key)
        {
            if (Key == key) return this;
            if (Key > key)
            {
                return Find(key, Left);
            }
            return Find(key, Right);
        } // Ищет узел с заданным значением

        
        public BinaryTree<T> Find(long key, BinaryTree<T> node)
        {
            if (node == null) return null;

            if (node.Key == key) return node;
            if (node.Key > key)
            {
                return Find(key, node.Left);
            }
            return Find(key, node.Right);
        } // Ищет значение в определённом узле

        public long CountElements()
        {
            return CountElements(this);
        } // Количество элементов в дереве
        
        private long CountElements(BinaryTree<T> node)
        {
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
        } // Количество элементов в определённом узле
        
        public BinaryTree<T> NextNode()
        {
            var val = this;
            if (val.Right == null)
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
    }

    public class BinaryTreeExtensions<T>
    {
        public static void Print(BinaryTree<T> node)
        {
            if (node != null)
            {
                if (node.Parent == null)
                {
                    Console.WriteLine("ROOT:{0}", node.Key);
                }
                else
                {
                    if (node.Parent.Left == node)
                    {
                        Console.WriteLine("Left for {1}  --> {0}", node.Key, node.Parent.Key);
                    }

                    if (node.Parent.Right == node)
                    {
                        Console.WriteLine("Right for {1} --> {0}", node.Key, node.Parent.Key);
                    }
                }
                if (node.Left != null)
                {
                    Print(node.Left);
                }
                if (node.Right != null)
                {
                    Print(node.Right);
                }
            }
        }
    }

}
