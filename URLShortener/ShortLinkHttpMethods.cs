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

    public static void RedirectToShortCode(string shortCode, HttpContext ctx)
    {
        var db = ctx.RequestServices.GetRequiredService<IShortenerDataContext>();
        var url = db.Find(shortCode);
        // If the specified code wasn't found we'd redirect to a 404 page or the homepage
        ctx.Response.Redirect(url?.RemoteUrl ?? "/");
    }

    public static async Task<IResult> UpdateShortCode(string shortCode, string url, HttpContext ctx)
    {
        if (!UrlValidation.IsUrlFormatValid(url)) return Results.BadRequest("The provided URL was not in the correct format");
        if (!(await UrlValidation.IsUrlAccessibleAsync(url))) return Results.BadRequest("The url provided is offline or not accessible");

        var db = ctx.RequestServices.GetRequiredService<IShortenerDataContext>();
        var storedUrl = db.Find(shortCode);
        if (storedUrl == null) return Results.NotFound("The provided short code doesn't match a known link.");
        storedUrl.RemoteUrl = url;
        storedUrl = db.Update(storedUrl);
        return Results.Ok(storedUrl);
    }

    public static IResult DeleteShortCode(string shortCode, HttpContext ctx)
    {
        var db = ctx.RequestServices.GetRequiredService<IShortenerDataContext>();
        var url = db.Find(shortCode);
        if (url == null) return Results.NotFound("The provided shortcode was not found.");
        db.Delete(shortCode);
        return Results.Ok("Deleted successfully.");
    }
}