using System.Text;

namespace Graphs;

public class Graph<T>
{
    private List<GraphNode<T>> nodes = new List<GraphNode<T>>();

    public int Count => nodes.Count;
    public IList<GraphNode<T>> Nodes => nodes.AsReadOnly();

    public void Clear()
    {
        foreach (var node in nodes)
        {
            node.RemoveAllNeighbors();
        }

        for (int i = Count-1; i>=0 ; i++)
        {
            nodes.RemoveAt(i);
        }
    }

    public bool AddNode(T value)
    {
        if (Find(value) != null)
        {
            return false;
        }
        else
        {
            nodes.Add(new GraphNode<T>(value));
            return true;
        }
    }

    public bool AddEdge(T value1, T value2)
    {
        GraphNode<T> node1 = Find(value1);
        GraphNode<T> node2 = Find(value2);

        if (node1==null || node2==null)
        {
            return false;
        }

        if (node1.Neighbors.Contains(node2))
        {
            return false;
        }
        else
        {
            node1.AddNeighbor(node2);
            node2.AddNeighbor(node1);
            return true;
        }
    }
    
    public GraphNode<T> Find(T value)
    {
        foreach (var node in nodes)
        {
            if (node.Value.Equals(value))
            {
                return node;
            }
        }

        return null;
    }

    public bool RemoveNode(T value)
    {
        GraphNode<T> node = Find(value);

        if (node == null)
        {
            return false;
        }

        nodes.Remove(node);
        foreach (var neighbor in node.Neighbors)
        {
            neighbor.RemoveNeighbor(node);
        }
        node.RemoveAllNeighbors();
        
        return true;
    }

    public bool RemoveEdge(T value1, T value2)
    {
        GraphNode<T> node1 = Find(value1);
        GraphNode<T> node2 = Find(value2);

        if (node1==null || node2==null)
        {
            return false;
        }

        if (!node1.Neighbors.Contains(node2))
        {
            return false;
        }
        else
        {
            node1.RemoveNeighbor(node2);
            node2.RemoveNeighbor(node1);
            return true;
        }
    }
    
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < Count; ++i)
        {
            builder.Append(nodes[i].ToString());
            if (i<Count-1)
            {
                builder.Append(",");
            }
        }

        return builder.ToString();
    }
}