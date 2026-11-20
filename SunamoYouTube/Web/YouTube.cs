namespace SunamoYouTube.Web;

/// <summary>
/// Represents the you tube type.
/// </summary>
public static class YouTube
{
    /// <summary>
    /// Parse yt code.
    /// </summary>
    /// <param name="uri">The uri.</param>
    /// <returns>The parse yt code result.</returns>
    public static string ParseYtCode(string uri)
    {
        var regex = new Regex("youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");
        var match = regex.Match(uri);
        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        return null!;
    }

    /// <summary>
    /// Get link to video.
    /// </summary>
    /// <param name="videoCode">The video code.</param>
    /// <returns>The get link to video result.</returns>
    public static string GetLinkToVideo(string videoCode)
        => "http://www.youtube.com/watch?v=" + videoCode;

    /// <summary>
    /// Get html anchor.
    /// </summary>
    /// <param name="videoCode">The video code.</param>
    /// <returns>The get html anchor result.</returns>
    public static string GetHtmlAnchor(string videoCode)
        => "<a href='" + GetLinkToVideo(videoCode) + "'>" + videoCode + "</a>";

    /// <summary>
    /// Get link to search.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>The get link to search result.</returns>
    public static string GetLinkToSearch(string query)
        => "http://www.youtube.com/results?search_query=" + UH.UrlEncode(query);

    /// <summary>
    /// Replace all.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="what">The what.</param>
    /// <param name="replacement">The replacement.</param>
    /// <returns>The replace all result.</returns>
    public static string ReplaceAll(string text, List<string> what, string replacement)
    {
        foreach (var item in what)
        {
            text = text.Replace(item, replacement);
        }

        return text;
    }
}
