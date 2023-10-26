using System.Collections;
using System.Text;

namespace Trees
{
    public class BinaryTree<T>:IEnumerable
    {
        #region Fields

        private BinaryTreeNode<T> _root = null;
        private List<BinaryTreeNode<T>> _nodes = new List<BinaryTreeNode<T>>();
        private List<BinaryTreeNode<T>> _traversal = new List<BinaryTreeNode<T>>();

        #endregion

        #region Constuctors

        public BinaryTree(T value)
        {
            _root = new BinaryTreeNode<T>(value,null,ChildType.Null);
            _nodes.Add(_root);
        }
            
        #endregion

        #region Properties

        public int Count => _nodes.Count;

        public BinaryTreeNode<T> Root => _root;

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

        public bool AddNode(BinaryTreeNode<T> node)
        {
            if ( node==null || node.Parent==null || !_nodes.Contains(node.Parent))
            {
                return false;
            }
            else if (node.Parent.IfHasChild(node))
            {
                return false;
            }
            else
            {
                _nodes.Add(node);
                return node.Parent.SetChild(node);
            }
        }

        public bool RemoveNode(BinaryTreeNode<T> node)
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
                bool success = node.Parent.RemoveChild(node.Type);
                if (!success)
                {
                    return false;
                }

                success = _nodes.Remove(node);
                if (!success)
                {
                    return false;
                }

                node.RemoveAllChild();

                return true;
            }
        }

        public BinaryTreeNode<T> Find(T value)
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

            builder.Append("\nNodes: \n");

            for (int i = 0; i < _nodes.Count; ++i)
            {
                builder.Append(_nodes[i].ToString());
                if (i<_nodes.Count-1)
                {
                    builder.Append("\n");
                }
            }
            return builder.ToString();
        }
        
        public static void PreOrderTraversal(BinaryTreeNode<T> node, List<BinaryTreeNode<T>> traversal)
        {
            if (node == null || traversal==null)
            {
                return;
            }

            traversal.Add(node);
            
            if (node.LeftChild != null)
            {
                PreOrderTraversal(node.LeftChild,traversal);
            }

            if (node.RightChild != null)
            {
                PreOrderTraversal(node.RightChild,traversal);
            }
        }

        public static void InOrderTraversal(BinaryTreeNode<T> node,List<BinaryTreeNode<T>> traversal)
        {
            if (node == null || traversal==null)
            {
                return;
            }
            
            if (node.LeftChild != null)
            {
                InOrderTraversal(node.LeftChild,traversal);
            }
            traversal.Add(node);
            if (node.RightChild != null)
            {
                InOrderTraversal(node.RightChild,traversal);
            }
        }

        public static void PostOrderTraversal(BinaryTreeNode<T> node, List<BinaryTreeNode<T>> traversal)
        {
            if (node == null || traversal == null)
            {
                return;
            }

            if (node.LeftChild != null)
            {
                PostOrderTraversal(node.LeftChild, traversal);
            }

            if (node.RightChild != null)
            {
                PostOrderTraversal(node.RightChild, traversal);
            }

            traversal.Add(node);
        }

        public static void BreadthFirstTraversal(BinaryTreeNode<T> root, List<BinaryTreeNode<T>> traversal)
        {
            if (root == null || traversal==null)
            {
                return;
            }

            Queue<BinaryTreeNode<T>> searchList = new Queue<BinaryTreeNode<T>>();
            searchList.Enqueue(root);

            while (searchList.Count>0)
            {
                BinaryTreeNode<T> node = searchList.First();
                searchList.Dequeue();
                traversal.Add(node);

                if (node.LeftChild!=null)
                {
                    searchList.Enqueue(node.LeftChild);
                }
                if (node.RightChild!=null)
                {
                    searchList.Enqueue(node.RightChild);
                }
            }
        }

        public IEnumerator GetEnumerator() => new BinaryTreeEnumerator<T>(_traversal);

        #endregion
    }

    public class BinaryTreeEnumerator<T> : IEnumerator
    {
        private List<BinaryTreeNode<T>> _traversal;
        private int position = 1;

        public BinaryTreeEnumerator(List<BinaryTreeNode<T>> traversal)
        {
            _traversal = traversal;
        }

        public object Current
        {
            get
            {
                try
                {
                    return _traversal[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            ++position;
            return position < _traversal.Count;
        }

        public void Reset()
        {
            position = -1;
        }

    }
}

