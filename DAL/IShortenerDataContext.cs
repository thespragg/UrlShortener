using System.Linq.Expressions;
using UrlShortener.DAL.Models;

namespace UrlShortener.DAL;

public interface IShortenerDataContext
{
    public ShortLink Insert(ShortLink link);
    public ShortLink Update(ShortLink link);
    public void Delete(string shortCode);
    public ShortLink? Find(string shortCode);
}