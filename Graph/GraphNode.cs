using System.Text;

namespace Graphs
{
    public class GraphNode<T>
    {
        private T value;
        private List<GraphNode<T>> neighbors;

        public GraphNode(T value)
        {
            this.value = value;
            neighbors = new List<GraphNode<T>>();
        }

        public T Value => value;

        public IList<GraphNode<T>> Neighbors => neighbors.AsReadOnly();

        public bool AddNeighbor(GraphNode<T> neighbor)
        {
            if (neighbors.Contains(neighbor))
            {
                return false;
            }
            
            neighbors.Add(neighbor);
            return true;
        }

        public bool RemoveNeighbor(GraphNode<T> neighbor)
        {
            return neighbors.Remove(neighbor);
        }

        public bool RemoveAllNeighbors()
        {
            for (int i = neighbors.Count-1; i >= 0; --i)
            {
                neighbors.RemoveAt(i);
            }

            return true;
        }
        
        public override string ToString()
        {
            StringBuilder nodeString = new StringBuilder();

            nodeString.Append($"[Node Value: {value} Neighbors: ");
            
            for (int i = 0; i < neighbors.Count; ++i)
            {
                nodeString.Append($"{neighbors[i].Value} ");
            }

            nodeString.Append("]");

            return nodeString.ToString();
        }
    }
}