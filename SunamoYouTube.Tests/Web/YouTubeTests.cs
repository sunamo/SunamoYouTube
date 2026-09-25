using SunamoYouTube.Web;

/// <summary>
/// Tests for YouTube URL parsing utilities.
/// </summary>
public class YouTubeTests
{
    /// <summary>
    /// Verifies that ParseYtCode extracts the video code from a full YouTube URL.
    /// </summary>
    [Fact]
    public void ParseYtCodeTest()
    {
        var actual = YouTube.ParseYtCode("https://www.youtube.com/watch?v=7JoitjrFLlU");
        var expected = "7JoitjrFLlU";

        Assert.Equal(actual, expected);
    }
}
