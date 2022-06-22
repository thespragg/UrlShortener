using UrlShortener;
using UrlShortener.DAL;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IShortenerDataContext, SQLiteShortenerDataContext>();
var app = builder.Build();

app.MapPost("create", ShortLinkHttpMethods.CreateShortLink);
app.MapGet("{shortCode}", ShortLinkHttpMethods.RedirectToShortCode);
app.Run();
