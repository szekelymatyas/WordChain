namespace WordChain.Backend;

public class WordsGraphFactory : IWordsGraphFactory
{
    public WordsGraph Create(string[] words)
    {
        WordsGraph graph = new WordsGraph()
        {
            Nodes = words
                .Select(x => new KeyValuePair<string, Node>(x, new Node() { Word = x }))
                .ToDictionary()
        };

        foreach (var first in graph.Nodes)
        {
            foreach (var second in graph.Nodes)
            {
                if (first.Key == second.Key)
                {
                    continue;
                }
                if (first.Key.CharDiff(second.Key) > 1)
                {
                    continue;
                }

                var edge = new Edge() { From = first.Value, To = second.Value };
                first.Value.OutEdges.Add(second.Key, edge);
                second.Value.InEdges.Add(first.Key, edge);
            }
        }

        var allNode = graph.Nodes.ToDictionary();
        var touched = new Dictionary<string, Node>();
        while (allNode.Any())
        {
            var node = allNode.First();
            allNode.Remove(node.Key);
            touched.Add(node.Key, node.Value);

            SetGroupIdForReachableNodes(allNode, touched, node);
        }
        return graph;
    }

    private static void SetGroupIdForReachableNodes(
        Dictionary<string, Node> allNode,
        Dictionary<string, Node> touched,
        KeyValuePair<string, Node> node
    )
    {
        var temp = new Dictionary<string, Node>();
        temp.Add(node.Key, node.Value);
        while (temp.Any())
        {
            var currant = temp.First();
            foreach (var neighbor in currant.Value.OutEdges)
            {
                if (neighbor.Value.From.GroupId == neighbor.Value.To.GroupId)
                {
                    continue;
                }

                if (touched.ContainsKey(neighbor.Key))
                {
                    continue;
                }

                neighbor.Value.To.GroupId = neighbor.Value.From.GroupId;
                temp.Add(neighbor.Key, neighbor.Value.To);
                allNode.Remove(neighbor.Key);
                touched.Add(neighbor.Key, neighbor.Value.To);
            }
            temp.Remove(currant.Key);
        }
    }
}
