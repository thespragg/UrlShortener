using UrlShortener;
using UrlShortener.DAL;
using UrlShortener.DAL.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IShortenerDataContext, SQLiteShortenerDataContext>();
var app = builder.Build();

app.MapPost("create", ShortLinkHttpMethods.CreateShortLink);
app.MapGet("{shortCode}", ShortLinkHttpMethods.RedirectToShortCode);
//Would ideally use PATCH here, but it's not supported in minimal api's yet
app.MapPost("{shortCode}/update", ShortLinkHttpMethods.UpdateShortCode);
app.MapDelete("{shortCode}/delete", ShortLinkHttpMethods.DeleteShortCode);
app.MapGet("{shortCode}/stats", ShortLinkHttpMethods.GetAccessStats);
app.Run();
