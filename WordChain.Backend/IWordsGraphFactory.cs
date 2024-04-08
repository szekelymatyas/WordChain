namespace WordChain.Backend;

public interface IWordsGraphFactory
{
    WordsGraph Create(string[] words);
}
