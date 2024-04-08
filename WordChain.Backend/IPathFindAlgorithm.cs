namespace WordChain.Backend;

public interface IGenerateResult { }

public class Failed : IGenerateResult
{
    public required string Message { get; init; }
}

public class Success : IGenerateResult
{
    public required List<Node> Path { get; init; }
}

public interface IPathFindAlgorithm
{
    IGenerateResult Generate(WordsGraph wordsGraph, string source, string target);
}
