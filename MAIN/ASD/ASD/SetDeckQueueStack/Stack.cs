using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.SetDeckQueueStack
{
    /// <summary>
    /// Стек
    /// </summary>
    public class Stack<T> 
    {
        public int Length { get; private set; }
        //private Node<T> _headNode;
        private Node<T> _top;

        public Stack()
        {
            _top = null;
            Length = 0;
        }

        /// <summary>
        /// Добавление элемента в конец стека
        /// </summary>
        public void Push(T element)
        {
            if (_top == null)
            {
                _top = new Node<T>();
                _top.Next = null;
                _top.Element = element;
                _top.Prev = null;
                Length++;
            }
            else
            {
                _top.Next = new Node<T>();
                _top.Next.Prev = _top;
                _top = _top.Next;
                _top.Element = element;
                _top.Next = null;
                Length++;
            }
        }

        /// <summary>
        /// Изъятие элемента из конца стека
        /// </summary>
        public Node<T> Pop()
        {
            var ans = _top;

            if (_top == null)
                return null;

            _top = _top.Prev;

            if (_top != null)
                _top.Next = null;
            else
                _top = null;
            
            Length--;
            return ans;
        }

        /// <summary>
        /// Получение последнего элемента
        /// </summary>
        public Node<T> Peek()
        {
            return _top;
        }

        /// <summary>
        /// Вывод стека
        /// </summary>
        public void View()
        {
            var curNode = Peek();

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
