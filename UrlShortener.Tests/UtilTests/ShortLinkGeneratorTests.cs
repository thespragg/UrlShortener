namespace UrlShortener.Tests.UtilTests;
using UrlShortener.Utils;

public class ShortLinkGeneratorTests
{
    [Fact]
    public void GenerateShortLink_GeneratesCorrectLength()
    {
        for (var i = 0; i < 10; i++) {
            Assert.Equal(i, ShortLinkGenerator.GenerateShortLink(i).Length);
        }
    }
}