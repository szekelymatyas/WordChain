using System.Diagnostics.Metrics;
using WordChain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WordChain.Backend;

public class AStarPathFindAlgorithm(IHeuristicCostFunction heuristicCostFunction)
    : IPathFindAlgorithm
{
    public IGenerateResult Generate(WordsGraph wordsGraph, string source, string target)
    {
        wordsGraph.Nodes.TryGetValue(source, out var sourceNode);
        wordsGraph.Nodes.TryGetValue(target, out var targetNode);
        if (sourceNode is null || targetNode is null)
        {
            var message = (sourceNode, targetNode) switch
            {
                (null, null) => "The source and target words do not exist in the graph",
                (null, _) => "The source word do not exist in the graph",
                (_, null) => "The target word do not exist in the graph",
            };

            return new Failed() { Message = message };
        }
        if (sourceNode.GroupId != targetNode.GroupId)
        {
            return new Failed()
            {
                Message = $"No path found from source {source} to target {target}"
            };
        }

        var result = A_Star(sourceNode, targetNode);

        if (result is [])
        {
            return new Failed()
            {
                Message = $"No path found from source {source} to target {target}"
            };
        }

        return new Success() { Path = result };
    }

    public List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
    {
        List<Node> total_path = [current];
        while (cameFrom.TryGetValue(current, out var previous))
        {
            current = previous;
            total_path.Insert(0, current);
        }
        return total_path;
    }

    // A* finds a path from start to goal.
    // h is the heuristic function. h(n) estimates the cost to reach goal from node n.
    List<Node> A_Star(Node start, Node goal)
    {
        // The set of discovered nodes that may need to be (re-)expanded.
        // Initially, only the start node is known.
        // This is usually implemented as a min-heap or priority queue rather than a hash-set.
        var openSet = new PriorityQueue<Node, int>();
        openSet.Enqueue(start, heuristicCostFunction.EstimateCost(start.Word, goal.Word));

        // For node n, cameFrom[n] is the node immediately preceding it on the cheapest path from the start
        // to n currently known.
        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();

        // For node n, gScore[n] is the cost of the cheapest path from start to n currently known.
        Dictionary<Node, int> gScore = new Dictionary<Node, int>();
        gScore[start] = 0;

        // For node n, fScore[n] := gScore[n] + h(n). fScore[n] represents our current best guess as to
        // how cheap a path could be from start to finish if it goes through n.

        Dictionary<Node, int> fScore = new Dictionary<Node, int>();
        fScore[start] = gScore[start] + heuristicCostFunction.EstimateCost(start.Word, goal.Word);

        while (openSet.TryDequeue(out var current, out int priority))
        {
            // This operation can occur in O(Log(N)) time if openSet is a min-heap or a priority queue
            if (current == goal)
                return ReconstructPath(cameFrom, current);

            foreach (var neighbor in current.OutEdges.Select(x => x.Value.To))
            {
                int weighted = 1;
                //for each neighbor of current
                //tentative_gScore is the distance from start to the neighbor through current
                var tentative_gScore = gScore[current] + weighted;

                if (!gScore.ContainsKey(neighbor) || tentative_gScore < gScore[neighbor])
                {
                    // This path to neighbor is better than any previous one.Record it!
                    var dist = heuristicCostFunction.EstimateCost(start.Word, goal.Word);
                    if (!gScore.ContainsKey(neighbor))
                    {
                        openSet.Enqueue(neighbor, dist);
                    }
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentative_gScore;
                    fScore[neighbor] = tentative_gScore + dist;
                }
            }
        }
        // Open set is empty but goal was never reached
        return [];
    }
}
