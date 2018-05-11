using System;
namespace ASD.CustomLists
{
    public class Queue<T>
    {
        public int Length { get; private set; }
        private Node<T> _headNode;
        private Node<T> _tailNode;

        public Queue()
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
            var ans = _headNode;
            _headNode = _headNode.Next;
            _headNode.Prev = null;
            Length--;
            return ans.Element;
        }
        
        public Node<T> GetHeadNode()
        {
            return _headNode;
        }

        public void View()
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
