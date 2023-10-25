using System.Text;

namespace Trees
{
    public class TreeNode<T>
    {
        #region Fields

        private T _value;
        private TreeNode<T> _parent;
        private List<TreeNode<T>> _children;
        
        #endregion

        #region Constructors

        public TreeNode(T value, TreeNode<T> parent)
        {
            _value = value;
            _parent = parent;
            _children = new List<TreeNode<T>>();
        }
        
        #endregion

        #region Properties

        public T Value => _value;

        public TreeNode<T> Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public IList<TreeNode<T>> Children => _children.AsReadOnly();

        #endregion

        #region Methods

        public bool AddChild(TreeNode<T> child)
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

        public bool RemoveChild(TreeNode<T> child)
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

