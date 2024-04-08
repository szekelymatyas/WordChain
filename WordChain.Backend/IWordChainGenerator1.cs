namespace WordChain.Backend;

public interface IWordChainGenerator
{
    IGenerateResult Generate(string source, string target);
}
