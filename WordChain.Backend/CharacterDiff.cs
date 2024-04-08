namespace WordChain.Backend;

public class CharacterDiff : IHeuristicCostFunction
{
    public int EstimateCost(string source, string target)
    {
        return source.CharDiff(target);
    }
}
