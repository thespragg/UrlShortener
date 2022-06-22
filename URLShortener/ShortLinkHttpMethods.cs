using UrlShortener.DAL;
using UrlShortener.DAL.Models;
using UrlShortener.Utils;

namespace UrlShortener;

public static class ShortLinkHttpMethods
{
    public static async Task<IResult> CreateShortLink(string url, HttpContext ctx)
    {
        if (!UrlValidation.IsUrlFormatValid(url)) return Results.BadRequest("The provided URL was not in the correct format");
        if (!(await UrlValidation.IsUrlAccessibleAsync(url))) return Results.BadRequest("The url provided is offline or not accessible");
        
        var db = ctx.RequestServices.GetRequiredService<IShortenerDataContext>();
        var shortLink = new ShortLink
        {
            RemoteUrl = url,
            ShortCode = ShortLinkGenerator.GenerateShortLink()
        };

        //Odds of generating same 6 character shortcode twice is 62^6 or about 1 in 56 billion but might happen so collision check is needed
        while (db.Find(shortLink.ShortCode) != null)
        {
            shortLink.ShortCode = ShortLinkGenerator.GenerateShortLink();
        }
        db.Insert(shortLink);
        return Results.Ok(shortLink);
    }
}