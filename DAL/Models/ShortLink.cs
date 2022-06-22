using SQLite;

namespace UrlShortener.DAL.Models;

public class ShortLink
{
    [PrimaryKey, AutoIncrement]
    public long Id { get; set; }
    public string RemoteUrl { get; set; } = null!;
    [Indexed] 
    public string ShortCode { get; set; } = null!;
}