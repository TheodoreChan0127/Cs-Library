using System.Text;

namespace Trees
{
    public enum ChildType
    {
        Left,
        Right,
        Null
    } 
    public class BinaryTreeNode<T>
    {
        #region Fields

        private T _value;
        private BinaryTreeNode<T> _parent;
        private BinaryTreeNode<T> rightChild, leftChild;
        private ChildType _type;

        #endregion

        #region Constructors

        public BinaryTreeNode(T value, BinaryTreeNode<T> parent,ChildType type)
        {
            _value = value;
            _parent = parent;
            _type = type;
            parent.SetChild(this, type);
            
            rightChild = null;
            leftChild = null;
        }
        
        #endregion

        #region Properties

        public T Value => _value;

        public ChildType Type => _type;
        public BinaryTreeNode<T> Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public BinaryTreeNode<T> LeftChild => leftChild;

        public BinaryTreeNode<T> RightChild => rightChild;

        #endregion

        #region Methods

        public bool SetChild(BinaryTreeNode<T> child,ChildType type)
        {
            if (leftChild==child || rightChild==child || this == child)
            {
                return false;
            }
            else if(child.Parent == this)
            {
                if (type==ChildType.Left)
                {
                    leftChild = child;
                    return true;
                }
                else
                {
                    rightChild = child;
                    return true;
                }
            }

            return false;
        }

        public bool RemoveChild(ChildType type)
        {
            BinaryTreeNode<T> child = type == ChildType.Left ? leftChild : rightChild;

            if (child == null)
            {
                return false;
            }
            
            if (type==ChildType.Left)
            {
                leftChild = null;
            }
            else
            {
                rightChild = null;
            }
            
            child.Parent = null;
            child.RemoveAllChild();
            return true;
        }

        public bool RemoveAllChild()
        {
            if (leftChild!=null)
            { 
                RemoveChild(ChildType.Left);
            }

            if (rightChild!=null)
            {
                RemoveChild(ChildType.Right);
            }

            return true;
        }

        public bool IfHasChild(BinaryTreeNode<T> node,ChildType type)
        {
            BinaryTreeNode<T> child = type == ChildType.Left ? leftChild : rightChild;
            if (child == null)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder nodeString = new StringBuilder();

            nodeString.Append($"[Node Value: {_value} Parent: ");

            if (_parent!=null)
            {
                nodeString.Append(_parent.Value);
            }
            else
            {
                nodeString.Append("null");
            }

            nodeString.Append($" LeftChildren: {leftChild.Value} RightChild: {rightChild.Value}]");

            return nodeString.ToString();
        }

        #endregion
    }
}

