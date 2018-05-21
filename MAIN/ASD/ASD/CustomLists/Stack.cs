using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.CustomLists
{
    public class Stack<T>
    {
        public int Length { get; private set; }
        private Node<T> _headNode;
        private Node<T> _tailNode;

        public Stack()
        {
            _tailNode = _headNode = null;
            Length = 0;
        }

        public void Push(T element)
        {
            if (_headNode == null)
            {
                Node<T> _current = new Node<T>();
                _current.Next = null;
                _tailNode = _current;
                _current.Element = element;
                _current.Prev = null;
                _headNode = _current;
                Length++;
            }
            else
            {
                Node<T> newNode = new Node<T>();
                _tailNode.Next = newNode;
                newNode.Prev = _tailNode;
                _tailNode = newNode;
                _tailNode.Element = element;
                _tailNode.Next = null;
                Length++;
            }
        }

        public T Pop()
        {
            var ans = _tailNode;
            _tailNode = _tailNode.Prev;
            _tailNode.Next = null;
            Length--;
            return ans.Element;
        }

        public Node<T> GetTailNode()
        {
            return _tailNode;
        }

        public void View()
        {
            var curNode = GetTailNode();

            if (curNode == null)
                return;

            while (curNode != null)
            {
                Console.Write(curNode.Element + " ");
                curNode = curNode.Prev;
            }

            Console.WriteLine();
        }
    }
}
