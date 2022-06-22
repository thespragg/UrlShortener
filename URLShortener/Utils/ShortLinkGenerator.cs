namespace UrlShortener.Utils;

internal static class ShortLinkGenerator
{
    private static readonly Random Rnd = new();
    internal static string GenerateShortLink(int len = 6)
    {
        return new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", len)
            .Select(s => s[Rnd.Next(s.Length)]).ToArray());
    }
}