using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.BinaryTree.Base
{
    class BinNode
    {
        Node head, val;

        public BinNode()
        {
            head = val = null;
        }
        public Node Root()
        {
            return head;
        }


        public void Add(int key, object value) //Добавление
        {
            Node node = new Node(key, value);
            if (head == null)
            {
                val = node;
                head = val;
            }
            else
            {
                val = head;
                bool flag = true;
                while (flag == true)
                {
                    if (val.Key < node.Key)
                    {
                        if (val.Right == null)
                        {
                            val.Right = node;
                            node.Parent = val;
                            flag = false;
                        }
                        else val = val.Right;
                    }
                    else
                    {
                        if (val.Left == null)
                        {
                            val.Left = node;
                            node.Parent = val;
                            flag = false;
                        }
                        else val = val.Left;
                    }
                }
            }
        }


        public Node Value(int key) //Поиск по ключу 
        {
            val = search(key);
            return val;
        }




        public int Level(Node t) //Определение уровня по ключу
        {
            if (t != null)
            {
                int key = t.Key;
                val = head;
                int x = 1;
                while (1 == 1)
                {
                    if (val.Key == key)
                        return x;
                    else if (val.Key < key)
                    { val = val.Right; x++; }
                    else { val = val.Left; x++; }
                }
            }
            return -9999999;
        }


        public Node MaxNode(Node t) //Поиск максимального элемента узла
        {
            int key = t.Key;
            val = search(key);
            while (val.Right != null)
                val = val.Right;
            return val;
        }


        public Node MinNode(Node t) //Поиск Минимального элемента узла
        {
            int key = t.Key;
            try
            {
                val = search(key);
                if (val.Left != null)
                {
                    while (val.Left != null)
                        val = val.Left;
                    return val;
                }
                else
                {
                    while (val.Left == null)
                        val = val.Right;
                    while (val.Left != null)
                        val = val.Left;
                    return val;
                }
            }
            catch
            {
                val = search(key); return val;
            }
        }

        public void DellNode(int key) //Удаление узла дерева
        {
            val = search(key);
            Dell(val);
        }
        public void Dell_node_Ur() //Удаление с проверкой
        {
            Node max = MaxNode(head);
            val = MinNode(head);
            int ur = Level(val);
            Node Dell_nod = val;
            while (val != max)
            {
                val = NextNode(val);
                if (Level(val) >= ur)
                {
                    ur = Level(val);
                    Dell_nod = val;
                }
            }
            Dell(Dell_nod);
        }

        public void Dell(Node val) //Удаление!!!
        {
            if (val.Right == null && val.Left == null)
            {
                if (val == val.Parent.Left)
                    val.Parent.Left = null;
                else val.Parent.Right = null;
            }
            else if (val.Right == null && val.Left != null)
            {
                val.Left.Parent = val.Parent;
                if (val == val.Parent.Right)
                    val.Parent.Right = val.Left;
                else val.Parent.Left = val.Left;
            }
            else if (val.Right != null && val.Left == null)
            {
                val.Right.Parent = val.Parent;
                if (val == val.Parent.Right)
                    val.Parent.Right = val.Right;
                else val.Parent.Left = val.Right;
            }
            else if (val.Right != null && val.Left != null)
            {
                if (val.Right.Left != null)
                {
                    Node node = val.Right.Left;
                    val.Key = node.Key;
                    val.Value = node.Value;
                    Dell(node);
                }
                else
                {
                    Node node = val.Right;
                    val.Left.Parent = node;
                    node.Left = val.Left;
                    node.Parent = val.Parent;
                    if (val == val.Parent.Right)
                        val.Parent.Right = node;
                    else val.Parent.Left = node;
                }
            }
        }
        public Node NextNode(Node t) //Поиск следующего элемента по индексу
        {
            int key = t.Key;
            val = search(key);
            if (val.Right == null)
            {
                if (val == val.Parent.Left)
                    return val.Parent;
                else
                {
                    while (val != val.Parent.Left)
                    {
                        val = val.Parent;
                        if (val == head)
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


        public string View(Node t) //Вывод дерева
        {
            Node node = MaxNode(t);
            Node val = MinNode(t);
            string s = Convert.ToString(val.Key);
            while (val != node)
            {
                val = NextNode(val);
                s = s + "  " + Convert.ToString(val.Key);
            }
            return s;
        }

        public void recInOrder(Node t) //рекурсивный вывод дерева
        {
            if (t.Left != null) recInOrder(t.Left);
            Console.WriteLine("Key={0}  Value={1}",t.Key,t.Value);
            if (t.Right != null) recInOrder(t.Right);
        }



        private Node search(int key) //Поиск
        {
            val = head;
            while (1 == 1)
            {
                if (val.Key == key)
                    return val;
                else if (val.Key < key)
                    val = val.Right;
                else val = val.Left;
            }
        }

    }

}
