using SQLite;
using UrlShortener.DAL.Models;

namespace UrlShortener.DAL;

public class SQLiteShortenerDataContext : IShortenerDataContext
{
    private readonly string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ShortenerDatabase.db");
    private readonly SQLiteConnection _db;

    public SQLiteShortenerDataContext()
    {
        _db = new SQLiteConnection(DatabasePath);
        _db.CreateTable<ShortLink>();
    }

    public ShortLink Insert(ShortLink link)
    {
        _db.Insert(link);
        return link;
    }

    public ShortLink Update(ShortLink link)
    {
        _db.Update(link);
        return link;
    }

    public void Delete(string shortCode)
    {
        var link = Find(shortCode);
        if (link == null) return;
        _db.Delete(link);
    }

    public ShortLink? Find(string shortCode) => _db.Table<ShortLink>().FirstOrDefault(x => x.ShortCode == shortCode);
}