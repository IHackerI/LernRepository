using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.CustomLists
{
    public class Deck <T>
    {
        public int Length { get; private set; }
        private Node<T> _headNode;
        private Node<T> _tailNode;

        public Deck()
        {
            _tailNode = _headNode = null;
            Length = 0;
        }

        public void AddHead(T element)
        {
            Node<T> _current = new Node<T>();
            if (_headNode == null)
            {
                _current.NextNode = null;
                _tailNode = _current;
            }
            else
            {
                _current.NextNode = _headNode;
                _headNode.PrevNode = _current;
            }
            _current.Element = element;
            _current.PrevNode = null;
            _headNode = _current;
            Length++;
        }

        public void AddTail(T element)
        {
            if (_headNode == null)
                AddHead(element);
            else
            {
                Node<T> newNode = new Node<T>();
                _tailNode.NextNode = newNode;
                newNode.PrevNode = _tailNode;
                _tailNode = newNode;
                _tailNode.Element = element;
                _tailNode.NextNode = null;
                Length++;
            }
        }

        public void InsertBefore(T beforeEl, T addElement)
        {
            if (Contains(beforeEl))
            {
                Node<T> current = _headNode;
                Node<T> element = new Node<T>();
                element.Element = addElement;
                while (current != null)
                {
                    if (current.Element.Equals(beforeEl))
                    {
                        if (current.PrevNode == null)
                        {
                            AddHead(addElement);
                            current = null;
                        }
                        else
                        {
                            element.NextNode = current;
                            element.PrevNode = current.PrevNode;
                            current.PrevNode = element;
                            element.PrevNode.NextNode = element;
                            current = element.NextNode.NextNode;
                            Length++;
                        }
                    }
                    else
                        current = current.NextNode;
                }
            }
            else
            {
                AddHead(addElement);
            }

        }

        public void InsertAfter(T afterEl, T addEl)
        {
            if (Contains(afterEl))
            {
                Node<T> current = _headNode;
                Node<T> element = new Node<T>();
                element.Element = addEl;
                while (current != null)
                {
                    if (current.NextNode == null)
                    {
                        AddTail(addEl);
                        current = null;
                    }
                    else
                    if (current.Element.Equals(afterEl))
                    {
                        element.NextNode = current.NextNode;
                        current.NextNode = element;
                        element.PrevNode = current;
                        element.NextNode.PrevNode = element;
                        current = element.NextNode;
                        Length++;
                        current = null;
                    }
                    else
                        current = current.NextNode;
                }
            }
            else
            {
                AddTail(addEl);
            }
        }

        
        public void Remove(T removeEl)
        {
            Node<T> current = _headNode;
            while (current != null)
            {
                if (current.Element.Equals(removeEl))
                    break;
                current = current.NextNode;
            }
            if (current != null)
            {
                if (current.NextNode != null)
                    current.NextNode.PrevNode = current.PrevNode;
                else
                    _tailNode = current.PrevNode;
                if (current.PrevNode != null)
                    current.PrevNode.NextNode = current.NextNode;
                else
                    _headNode = current.NextNode;
                Length--;
            }
        }
        public T this[int _position]
        {
            get
            {
                Node<T> tempNode = _headNode;
                for (int i = 0; i < _position; ++i)
                    // переходим к следующему узлу списка
                    tempNode = tempNode.NextNode;
                return tempNode.Element;
            }
        }

        public void RemoveHead()
        {
            _headNode = _headNode.NextNode;
            _headNode.PrevNode = null;
            Length--;
        }

        public void RemoveTail()
        {
            _tailNode = _tailNode.PrevNode;
            _tailNode.NextNode = null;
            Length--;
        }

        public void RemoveByIndex(int i)
        {
            Node<T> current = _headNode;
            int j = -1;
            while (current != null)
            {
                j++;
                if (i == 0)
                {
                    RemoveHead();
                    current = null;
                }
                else
                if (i == Length - 1)
                {
                    RemoveTail();
                    current = null;
                }
                else
                if (j == i)
                {
                    current.PrevNode.NextNode = current.NextNode;
                    current = current.NextNode;
                    current.PrevNode = current.PrevNode.PrevNode;
                    current.NextNode.PrevNode = current;
                    Length--;
                }
                else
                    current = current.NextNode;
            }
        }


        public bool Contains(T _element)
        {
            Node<T> current = _headNode;
            while (current != null)
            {
                if (current.Element.Equals(_element))
                    return true;
                else
                    current = current.NextNode;
            }
            return false;
        }

        public int GetIndex(T _element)
        {
            Node<T> current = _headNode;
            int i = -1;
            while (current != null)
            {
                i++;
                if (current.Element.Equals(_element))
                    return i;
                else
                    current = current.NextNode;
            }
            return i;
        }

        public void ViewHead()
        {
            for (int i = 0; i < Length; i++)
            {
                Console.Write(this[i] + " ");
            }
            Console.WriteLine();
        }

        public void ViewTail()
        {
            for (int i = Length - 1; i > -1; i--)
                Console.Write(this[i] + " ");
            Console.WriteLine();
        }

    }
}
