namespace WordChain.Backend;

public class WordChainGenerator(
    IWords words,
    IWordsGraphFactory wordsGraphFactory,
    IPathFindAlgorithm pathFindAlgorithm
) : IWordChainGenerator
{
    private WordsGraph? _wordsGraph = null;

    public List<string> Generate(string source, string target)
    {
        _wordsGraph ??= wordsGraphFactory.Create(words.GetWords());

        return pathFindAlgorithm.Generate(_wordsGraph, source, target).Select(x => x.Word).ToList();
    }
}
