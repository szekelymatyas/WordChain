namespace WordChain.Backend;

public interface IPathFindAlgorithm
{
    List<Node> Generate(WordsGraph wordsGraph, string source, string target);
}
