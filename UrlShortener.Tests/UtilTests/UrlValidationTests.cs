using UrlShortener.Utils;

namespace UrlShortener.Tests.UtilTests;

public class UrlValidationTests
{
    private readonly string[] _validUrLs = new string[] { "https://www.google.com", "https://google.com", "https://ads.google.com" };
    private readonly string[] _invalidUrLs = new string[] { "www.google.com", "ads.google.com", "google" };
    private readonly string[] _unreachableUrls = new string[] { "https://google", "https://www.google" };

    [Fact]
    public void IsUrlFormatValid_IsTrueForRealURL()
    {
        foreach (var validUrl in _validUrLs)
        {
            var isValid = UrlValidation.IsUrlFormatValid(validUrl);
            Assert.True(isValid);
        }
    }

    [Fact]
    public void IsUrlFormatValid_IsFalseForInvalidUrl()
    {
        foreach (var invalidUrl in _invalidUrLs)
        {
            var isValid = UrlValidation.IsUrlFormatValid(invalidUrl);
            Assert.False(isValid);
        }
    }

    [Fact]
    public async Task IsUrlAccessibleAsync_ReachesAccessibleSites()
    {
        foreach (var validUrl in _validUrLs)
        {
            var isAccessible = await UrlValidation.IsUrlAccessibleAsync(validUrl);
            Assert.True(isAccessible);
        }
    }

    [Fact]
    public async Task IsUrlAccessibleAsync_CannotReachUnreachableSites()
    {
        foreach (var unreachableUrl in _unreachableUrls)
        {
            var isAccessible = await UrlValidation.IsUrlAccessibleAsync(unreachableUrl);
            Assert.False(isAccessible);
        }
    }
}