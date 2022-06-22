namespace UrlShortener.Models;

public class ShortLinkStats
{
    public long LastHour { get; set; }
    public long LastDay { get; set; }
    public long LastWeek { get; set; }
    public long AllTime { get; set; }
    private ShortLinkStats() { }

    public static ShortLinkStats FromDateList(List<DateTime> dates)
    {
        var week = dates.Where(x => x >= DateTime.Now.AddDays(-7)).ToList();
        var day = week.Where(x => x >= DateTime.Now.AddDays(-1)).ToList();
        var hour = day.Where(x => x >= DateTime.Now.AddHours(-1)).ToList();

        return new ShortLinkStats
        {
            LastHour = hour.Count,
            LastDay = day.Count,
            LastWeek = week.Count,
            AllTime = dates.Count
        };
    }
}