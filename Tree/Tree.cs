using System.Text;

namespace Trees
{
    public class Tree<T>
    {
        #region Fields

        private TreeNode<T> _root = null;
        private List<TreeNode<T>> _nodes = new List<TreeNode<T>>();

        #endregion

        #region Constuctors

        public Tree(T value)
        {
            _root = new TreeNode<T>(value,null);
            _nodes.Add(_root);
        }
            
        #endregion

        #region Properties

        public int Count => _nodes.Count;

        public TreeNode<T> Root => _root;

        #endregion

        #region Methods

        public void Clear()
        {
            foreach (var node in _nodes)
            {
                node.Parent = null;
                node.RemoveAllChild();
            }

            for (int i = _nodes.Count-1; i >= 0; --i)
            {
                _nodes.RemoveAt(i);
            }

            _root = null;
        }

        public bool AddNode(TreeNode<T> node)
        {
            if ( node==null || node.Parent==null || !_nodes.Contains(node.Parent))
            {
                return false;
            }
            else if (node.Parent.Children.Contains(node))
            {
                return false;
            }
            else
            {
                _nodes.Add(node);
                return node.Parent.AddChild(node);
            }
        }

        public bool RemoveNode(TreeNode<T> node)
        {
            if (node == null)
            {
                return false;
            }
            else if (node == _root)
            {
                Clear();
                return false;
            }
            else
            {
                bool success = node.Parent.RemoveChild(node);
                if (!success)
                {
                    return false;
                }

                success = _nodes.Remove(node);
                if (!success)
                {
                    return false;
                }

                if (node.Children.Count>0)
                {
                    IList<TreeNode<T>> children = node.Children;
                    for (int i = _nodes.Count-1; i >= 0; --i)
                    {
                        RemoveNode(node);
                    }
                }

                return true;
            }
        }

        public TreeNode<T> Find(T value)
        {
            foreach (var node in _nodes)
            {
                if (node.Value.Equals(value))
                {
                    return node;
                }
            }
            return null;
        }
        
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append($"Root:  ");

            if (_root!=null)
            {
                builder.Append(_root.Value);
            }
            else
            {
                builder.Append("null");
            }

            builder.Append(" Nodes: ");

            for (int i = 0; i < _nodes.Count; ++i)
            {
                _nodes[i].ToString();
                if (i<_nodes.Count-1)
                {
                    builder.Append(" , ");
                }
            }
            return builder.ToString();
        }

        #endregion
    }
}

