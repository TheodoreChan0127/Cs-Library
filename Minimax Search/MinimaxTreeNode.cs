
using System.Text;
using Trees;

namespace MinimaxSearch
{
    public class MinimaxTreeNode<T>
    {
         #region Fields

         T _value;
         MinimaxTreeNode<T> _parent;
         List<MinimaxTreeNode<T>> _children;
         private int minimaxScore = 0;
        
        #endregion

        #region Constructors

        public MinimaxTreeNode(T value, MinimaxTreeNode<T> parent)
        {
            _value = value;
            _parent = parent;
            _children = new List<MinimaxTreeNode<T>>();
        }
        
        #endregion

        #region Properties

        public T Value => _value;

        public MinimaxTreeNode<T> Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public IList<MinimaxTreeNode<T>> Children => _children.AsReadOnly();

        public int MinimaxScore
        {
            get { return minimaxScore; }
            set { minimaxScore = value; }
        }

        #endregion

        #region Methods

        public bool AddChild(MinimaxTreeNode<T> child)
        {
            if (_children.Contains(child) || this == child)
            {
                return false;
            }
            else
            {
                _children.Add(child);
                child.Parent = this;
                return true;
            }
        }

        public bool RemoveChild(MinimaxTreeNode<T> child)
        {
            if (_children.Contains(child))
            {
                child.Parent = null;
                return _children.Remove(child);
            }
            else
            {
                return false;
            }
        }

        public bool RemoveAllChild()
        {
            for (int i = _children.Count-1; i >= 0; --i)
            {
                _children[i].Parent = null;
                _children.RemoveAt(i);
            }

            return true;
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

            nodeString.Append(" Children: ");

            for (int i = 0; i < _children.Count; ++i)
            {
                nodeString.Append($"{_children[i].Value} ");
            }

            nodeString.Append("]");

            return nodeString.ToString();
        }

        #endregion

    }
}
