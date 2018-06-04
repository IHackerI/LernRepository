using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.SetDeckQueueStack
{
    public class Deck <T> // Двусвязный список
    {
        public int Length { get; private set; }
        private Node<T> _headNode;
        private Node<T> _tailNode;

        public Deck()
        {
            _tailNode = _headNode = null;
            Length = 0;
        }

        public void AddHead(T element) // Добавить элемент в наччало списка
        {
            Node<T> _current = new Node<T>();
            if (_headNode == null)
            {
                _current.Next = null;
                _tailNode = _current;
            }
            else
            {
                _current.Next = _headNode;
                _headNode.Prev = _current;
            }
            _current.Element = element;
            _current.Prev = null;
            _headNode = _current;
            Length++;
        }

        public void AddTail(T element) // Добавить элемент в конец списка
        {
            if (_headNode == null)
                AddHead(element);
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

        public void InsertBefore(T beforeEl, T addElement) // Добавить элемент ДО определённого элемента в списке
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
                        if (current.Prev == null)
                        {
                            AddHead(addElement);
                            current = null;
                        }
                        else
                        {
                            element.Next = current;
                            element.Prev = current.Prev;
                            current.Prev = element;
                            element.Prev.Next = element;
                            current = element.Next.Next;
                            Length++;
                        }
                    }
                    else
                        current = current.Next;
                }
            }
            else
            {
                AddHead(addElement);
            }

        }

        public void InsertAfter(T afterEl, T addEl) // Добавить элемент ПОСЛЕ определённого элемента в списке
        {
            if (Contains(afterEl))
            {
                Node<T> current = _headNode;
                Node<T> element = new Node<T>();
                element.Element = addEl;
                while (current != null)
                {
                    if (current.Next == null)
                    {
                        AddTail(addEl);
                        current = null;
                    }
                    else
                    if (current.Element.Equals(afterEl))
                    {
                        element.Next = current.Next;
                        current.Next = element;
                        element.Prev = current;
                        element.Next.Prev = element;
                        current = element.Next;
                        Length++;
                        current = null;
                    }
                    else
                        current = current.Next;
                }
            }
            else
            {
                AddTail(addEl);
            }
        }

        public T this[int _position] 
        {
            get
            {
                Node<T> tempNode = _headNode;
                for (int i = 0; i < _position; ++i) 
                    tempNode = tempNode.Next; // переходим к следующему узлу списка
                return tempNode.Element;
            }
        }

        public void Remove(T removeEl) // Удаление элемента
        {
            Node<T> current = _headNode;
            while (current != null)
            {
                if (current.Element.Equals(removeEl))
                    break;
                current = current.Next;
            }
            if (current != null)
            {
                if (current.Next != null)
                    current.Next.Prev = current.Prev;
                else
                    _tailNode = current.Prev;
                if (current.Prev != null)
                    current.Prev.Next = current.Next;
                else
                    _headNode = current.Next;
                Length--;
            }
        }

        public void RemoveHead() // Удаление элемента в верху списка
        {
            Length--;
            if (_headNode != null)
            {
                Length--;
                _headNode = _headNode.Next;
            }

            if (_headNode != null)
                _headNode.Prev = null;
            else
                _tailNode = null;

        }

        public void RemoveTail() // Удаление эелемента в конце списка
        {
            if (_tailNode != null)
            {
                Length--;
                _tailNode = _tailNode.Prev;
            }

            if (_tailNode != null)
                _tailNode.Next = null;
            else
                _headNode = null;

        }

        public void RemoveByIndex(int i) // Удаление элемента по индексу
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
                    current.Prev.Next = current.Next;
                    current = current.Next;
                    current.Prev = current.Prev.Prev;
                    current.Next.Prev = current;
                    Length--;
                }
                else
                    current = current.Next;
            }
        }


        public bool Contains(T _element) // Проверяет, содержит ли список данный элемент
        {
            Node<T> current = _headNode;
            while (current != null)
            {
                if (current.Element.Equals(_element))
                    return true;
                else
                    current = current.Next;
            }
            return false;
        }

        public int GetIndex(T _element) // Получение индекса переданного элемента
        {
            Node<T> current = _headNode;
            int i = -1;
            while (current != null)
            {
                i++;
                if (current.Element.Equals(_element))
                    return i;
                else
                    current = current.Next;
            }
            return i;
        }

        public void ViewHead() // Вывод элемента верха списка
        {
            for (int i = 0; i < Length; i++)
            {
                Console.Write(this[i] + " ");
            }
            Console.WriteLine();
        }

        public void ViewTail() // Вывод элемента низа списка
        {
            for (int i = Length - 1; i > -1; i--)
                Console.Write(this[i] + " ");
            Console.WriteLine();
        }

    }
}
