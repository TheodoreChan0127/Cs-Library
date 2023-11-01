namespace Graphs;

public class PathNodeInfo<T>
{
    private GraphNode<T> previous;

    public PathNodeInfo(GraphNode<T> previous)
    {
        this.previous = previous;
    }

    public GraphNode<T> Previous => previous;
}