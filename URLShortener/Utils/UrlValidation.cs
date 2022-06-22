namespace UrlShortener.Utils;

internal static class UrlValidation
{
    private static readonly HttpClient HttpClient = new();
    internal static bool IsUrlFormatValid(string url) => Uri.TryCreate(url, UriKind.Absolute, out var uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
    internal static async Task<bool> IsUrlAccessibleAsync(string url)
    {
        try
        {
            // Using HttpMethod.Head to avoid actually downloading page content
            var res = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
            return res.IsSuccessStatusCode;
        }
        catch (HttpRequestException)
        {
            return false;
        }
    }
}