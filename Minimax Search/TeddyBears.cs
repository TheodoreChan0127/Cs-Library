namespace MinimaxSearch
{
    public class TeddyBears
    {
        private static List<int> binContents = new List<int>();
        private static List<Configuration> newConfigurations = new List<Configuration>();

        static MinimaxTree<Configuration> BuildTree()
        {
            binContents.Clear();
            binContents.Add(2);
            binContents.Add(1);
            Configuration rootConf = new Configuration(binContents);

            MinimaxTree<Configuration> tree = new MinimaxTree<Configuration>(rootConf);
            LinkedList<MinimaxTreeNode<Configuration>> nodeList = new LinkedList<MinimaxTreeNode<Configuration>>();

            nodeList.AddLast(tree.Root);

            while (nodeList.Count>0)
            {
                MinimaxTreeNode<Configuration> currentNode = nodeList.First.Value;
                nodeList.RemoveFirst();
                List<Configuration> chilren = GetNextConfigurations(currentNode.Value);

                foreach (var child in chilren)
                {
                    MinimaxTreeNode<Configuration> childNode = new MinimaxTreeNode<Configuration>(child, currentNode);
                    tree.AddNode(childNode);
                    nodeList.AddLast(childNode);
                }
            }

            return tree;
        }

        static List<Configuration> GetNextConfigurations(Configuration currentConf)
        {
            newConfigurations.Clear();
            IList<int> currentBins = currentConf.Bins;
            for (int i = 0; i < currentBins.Count; ++i)
            {
                int currentBinCount = currentBins[i];

                while (currentBinCount>0)
                {
                    --currentBinCount;
                    
                    binContents.Clear();
                    binContents.AddRange(currentBins);
                    binContents[i] = currentBinCount;
                    newConfigurations.Add(new Configuration(binContents));
                }
            }

            return newConfigurations;
        }

        static void Minimax(MinimaxTreeNode<Configuration> tree, bool maxmizing)
        {
            IList<MinimaxTreeNode<Configuration>> children = tree.Children;
            if (children.Count>0)
            {
                foreach (var child in children)
                {
                    Minimax(child,!maxmizing);
                }

                if (maxmizing)
                {
                    tree.MinimaxScore = int.MinValue;
                }
                else
                {
                    tree.MinimaxScore = int.MaxValue;
                }

                foreach (var child in children)
                {
                    if (maxmizing)
                    {
                        if (child.MinimaxScore>tree.MinimaxScore)
                        {
                            tree.MinimaxScore = child.MinimaxScore;
                        }
                    }
                    else
                    {
                        if (child.MinimaxScore<tree.MinimaxScore)
                        {
                            tree.MinimaxScore = child.MinimaxScore;
                        }
                    }
                }
            }
            else
            {
                AssignMinimaxScore(tree, maxmizing);
            }
        }

        static void AssignMinimaxScore(MinimaxTreeNode<Configuration> node, bool maxmizing)
        {
            if (node.Value.Empty)
            {
                if (maxmizing)
                {
                    node.MinimaxScore = 1;
                }
                else
                {
                    node.MinimaxScore = 0;
                }
            }
        }
    }
}
