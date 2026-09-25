namespace SunamoYouTube.Web;

public static class YouTube
{
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

    public static string GetLinkToVideo(string videoCode)
        => "http://www.youtube.com/watch?v=" + videoCode;

    public static string GetHtmlAnchor(string videoCode)
        => "<a href='" + GetLinkToVideo(videoCode) + "'>" + videoCode + "</a>";

    public static string GetLinkToSearch(string query)
        => "http://www.youtube.com/results?search_query=" + UH.UrlEncode(query);

    public static string ReplaceAll(string text, List<string> what, string replacement)
    {
        foreach (var item in what)
        {
            text = text.Replace(item, replacement);
        }

        return text;
    }
}
