namespace WordChain.Backend;

public interface IWordChainGenerator
{
    List<string> Generate(string source, string target);
}
