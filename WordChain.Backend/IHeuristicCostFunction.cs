namespace WordChain.Backend;

public interface IHeuristicCostFunction
{
    public int EstimateCost(string source, string target);
}
