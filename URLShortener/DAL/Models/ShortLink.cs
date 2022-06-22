using System.Text.Json;
using SQLite;

namespace UrlShortener.DAL.Models;

public class ShortLink
{
    [PrimaryKey, AutoIncrement]
    public long Id { get; set; }
    public string RemoteUrl { get; set; } = null!;
    [Indexed] 
    public string ShortCode { get; set; } = null!;
    public string? AccessedSerialised { get; set; }
    [Ignore] 
    public List<DateTime> Accessed => AccessedSerialised == null ? new List<DateTime>() : JsonSerializer.Deserialize<List<DateTime>>(AccessedSerialised)!;
}