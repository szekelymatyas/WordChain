namespace WordChain.Backend;

public static class StringExtensions
{
    public static int CharDiff(this string first, string second)
    {
        int diff = 0;
        for (var i = 0; i < first.Length && i < second.Length; i++)
        {
            if (first[i] != second[i])
            {
                diff++;
            }
        }
        return diff;
    }
}
