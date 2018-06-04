using System;
namespace ASD.SetDeckQueueStack
{
    public class Queue<T> // Очередь
    {
        public int Length { get; private set; }
        private Node<T> _headNode;
        private Node<T> _tailNode;

        public Queue()
        {
            _tailNode = _headNode = null;
            Length = 0;
        }
        
        public void Push(T element) // Добавление элемента в конец очереди
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
        
        public T Pop() // Изъятие первого элемента
        {
            var ans = _headNode;
            if (_headNode == null)
                return default(T);
            _headNode = _headNode.Next;
            if (_headNode != null)
                _headNode.Prev = null;
            else
                _tailNode = null;

            Length--;
            return ans.Element;

        }

        public Node<T> GetHeadNode() // Получение элемента из начала очереди
        {
            return _headNode;
        }

        public void View() // Вывод очереди
        {
            var curNode = GetHeadNode();

            if (curNode == null)
                return;

            while (curNode != null)
            {
                Console.Write(curNode.Element + " ");
                curNode = curNode.Next;
            }
            
            Console.WriteLine();
        }
    }
}
