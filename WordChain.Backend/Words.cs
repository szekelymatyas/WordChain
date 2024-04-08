namespace WordChain.Backend;

public class Words(IWordFactory wordFactory) : IWords
{
    private string[]? _words;

    public string[] GetWords()
    {
        _words ??= wordFactory.LoadWords();
        return _words;
    }
}
