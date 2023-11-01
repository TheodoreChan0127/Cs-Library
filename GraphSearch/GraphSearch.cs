using System.Text;

namespace Graphs;
    
public static class GraphSearch
{
    public enum SearchType
    {
        DepthFirst,
        WidthFirst
    }
    
    static void BuildTree(){}

    public static string Search<T>(T src, T des, Graph<T> graph, SearchType type)
    {
        LinkedList<GraphNode<T>> searchList = new LinkedList<GraphNode<T>>();

        if (src.Equals(des))
        {
            return src.ToString();
        }
        else if (graph.Find(src)==null || graph.Find(des)==null)
        {
            return "No Such Route";
        }
        else
        {
            GraphNode<T> startNode = graph.Find(src);
            Dictionary<GraphNode<T>, PathNodeInfo<T>> path = new Dictionary<GraphNode<T>, PathNodeInfo<T>>();
            path.Add(startNode,new PathNodeInfo<T>(null));

            searchList.AddFirst(startNode);

            while (searchList.Count>0)
            {
                GraphNode<T> currentNode = searchList.First.Value;
                searchList.RemoveFirst();

                foreach (var neighbor in currentNode.Neighbors)
                {
                    if (neighbor.Value.Equals(des))
                    {
                        path.Add(neighbor,new PathNodeInfo<T>(currentNode));
                        return PathToString(neighbor,path);
                    }
                    else if (path.ContainsKey(neighbor))
                    {
                        continue; //circle
                    }
                    else
                    {
                        path.Add(neighbor,new PathNodeInfo<T>(currentNode));

                        if (type == SearchType.DepthFirst)
                        {
                            searchList.AddFirst(neighbor);
                        }
                        else if (type == SearchType.WidthFirst)
                        {
                            searchList.AddLast(neighbor);
                        }
                        {
                            
                        }
                    }
                }
            }
        }

        return "Error";
    }

    static string PathToString<T>(GraphNode<T> endNode,Dictionary<GraphNode<T>,PathNodeInfo<T>> pathNodes)
    {
        LinkedList<GraphNode<T>> path = new LinkedList<GraphNode<T>>();
        path.AddFirst(endNode);
        GraphNode<T> preNode = pathNodes[endNode].Previous;
        while (preNode!=null)
        {
            path.AddFirst(preNode);
            preNode = pathNodes[preNode].Previous;
        }

        StringBuilder builder = new StringBuilder();

        LinkedListNode<GraphNode<T>> currentNode = path.First;
        int nodeCount = 0;
        while (currentNode!=null)
        {
            ++nodeCount;
            builder.Append(currentNode.Value.Value);
            if (nodeCount<path.Count)
            {
                builder.Append("->");
            }

            currentNode = currentNode.Next;
        }

        return builder.ToString();
    }
}