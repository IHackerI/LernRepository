using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD.BinTree
{
    public class BinaryTreeNode <TNode>: IComparable <TNode>
    where TNode : IComparable
    {
        public BinaryTreeNode(TNode value)
        {
            Value = value;
        }

        public BinaryTreeNode <TNode> Left { get; set; }
        public BinaryTreeNode <TNode> Right { get; set; }
        public TNode Value { get; private set; }

        /// 
        /// Сравнивает текущий узел с данным.
        /// 
        /// Сравнение производится по полю Value.
        /// Метод возвращает 1, если значение текущего узла больше,
        /// чем переданного методу, -1, если меньше и 0, если они равны
        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }
    }
}
