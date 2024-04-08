namespace WordChain.Backend;

public class WordsGraphInstance(IWordsGraphFactory wordsGraphFactory, IWords words)
    : IWordsGraphInstance
{
    private WordsGraph? _value;

    public WordsGraph GetValue()
    {
        _value ??= wordsGraphFactory.Create(words.GetWords());
        return _value;
    }
}
