namespace WordChain.Backend;

public class WordFactory(IConfiguration configuration) : IWordFactory
{
    public string[] LoadWords()
    {
        var filePath = configuration.GetValue<string>("words");
        if (filePath is null)
            throw new InvalidOperationException("Configuration words not provided!");

        var text = File.ReadAllText(filePath);

        var words = text.Split('\r', '\n');

        return words;
    }
}
