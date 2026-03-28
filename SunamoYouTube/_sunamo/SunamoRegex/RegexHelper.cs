namespace SunamoYouTube._sunamo.SunamoRegex;

/// <summary>
/// Provides compiled regular expressions for common pattern matching operations.
/// </summary>
internal static class RegexHelper
{
    /// <summary>
    /// Matches HTML script tags and their content.
    /// </summary>
    internal static Regex RHtmlScript = new Regex(@"<script[^>]*>[\s\S]*?</script>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    /// <summary>
    /// Matches HTML comments.
    /// </summary>
    internal static Regex RHtmlComment = new Regex(@"<!--[^>]*>[\s\S]*?-->", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    /// <summary>
    /// Matches YouTube video links and captures the video ID.
    /// </summary>
    internal static Regex RYtVideoLink = new Regex("youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.Compiled);

    /// <summary>
    /// Matches HTML br tags (case insensitive).
    /// </summary>
    internal static Regex RBrTagCaseInsensitive = new Regex(@"<br\s*/?>");

    /// <summary>
    /// Matches HTTP and HTTPS URIs.
    /// </summary>
    internal static Regex RUri = new Regex(@"(https?://[^\s]+)");

    /// <summary>
    /// Matches HTML tags.
    /// </summary>
    internal static Regex RHtmlTag = new Regex("<\\s*([A-Za-z])*?[^>]*/?>");

    /// <summary>
    /// Matches 6-character hex color codes.
    /// </summary>
    internal static Regex RColor6 = new Regex(@"^(?:[0-9a-fA-F]{3}){1,2}$");

    /// <summary>
    /// Matches 8-character hex color codes (with alpha).
    /// </summary>
    internal static Regex RColor8 = new Regex(@"^(?:[0-9a-fA-F]{3}){1,2}(?:[0-9a-fA-F]){2}$");

    /// <summary>
    /// Matches HTML pre tags and their content.
    /// </summary>
    internal static Regex RPreTagWithContent = new Regex(@"<\s*pre[^>]*>(.*?)<\s*/\s*pre>", RegexOptions.Multiline);

    /// <summary>
    /// Matches GUID format strings.
    /// </summary>
    internal static readonly Regex IsGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);

    /// <summary>
    /// Matches HTML img tags.
    /// </summary>
    internal static Regex RImgTag = new Regex(@"<img\s+([^>]*)(.*?)[^>]*>");

    /// <summary>
    /// Matches WordPress image thumbnail URLs.
    /// </summary>
    internal static Regex RWpImgThumbnail = new Regex(@"(https?:\/\/([^\s]+)-([0-9]*)x([0-9]*).jpg)");

    /// <summary>
    /// Matches non-pair XML tags (unvalidated).
    /// </summary>
    internal static Regex RNonPairXmlTagsUnvalid = new Regex("<(?:\"[^\"]*\"['\"]*|'[^']*'['\"]*|[^'\">])+>");

    /// <summary>
    /// Matches one or more whitespace characters.
    /// </summary>
    internal static readonly Regex RWhitespace = new Regex(@"\s+");

    /// <summary>
    /// Determines whether the specified text is a valid HTTP or HTTPS URI.
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <returns>True if the text is a valid URI.</returns>
    internal static bool IsUri(string text)
    {
        return RUri.IsMatch(text) && (text.StartsWith("http://") || text.StartsWith("https://"));
    }

    /// <summary>
    /// Stores the last detected telephone number.
    /// </summary>
    internal static string? LastTelephone = null;
}
