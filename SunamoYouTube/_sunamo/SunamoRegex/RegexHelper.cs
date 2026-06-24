namespace SunamoYouTube._sunamo.SunamoRegex;

internal static class RegexHelper
{
    internal static Regex RHtmlScript = new Regex(@"<script[^>]*>[\s\S]*?</script>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    internal static Regex RHtmlComment = new Regex(@"<!--[^>]*>[\s\S]*?-->", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    internal static Regex RYtVideoLink = new Regex("youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.Compiled);

    internal static Regex RBrTagCaseInsensitive = new Regex(@"<br\s*/?>");

    internal static Regex RUri = new Regex(@"(https?://[^\s]+)");

    internal static Regex RHtmlTag = new Regex("<\\s*([A-Za-z])*?[^>]*/?>");

    internal static Regex RColor6 = new Regex(@"^(?:[0-9a-fA-F]{3}){1,2}$");

    internal static Regex RColor8 = new Regex(@"^(?:[0-9a-fA-F]{3}){1,2}(?:[0-9a-fA-F]){2}$");

    internal static Regex RPreTagWithContent = new Regex(@"<\s*pre[^>]*>(.*?)<\s*/\s*pre>", RegexOptions.Multiline);

    internal static readonly Regex IsGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);

    internal static Regex RImgTag = new Regex(@"<img\s+([^>]*)(.*?)[^>]*>");

    internal static Regex RWpImgThumbnail = new Regex(@"(https?:\/\/([^\s]+)-([0-9]*)x([0-9]*).jpg)");

    internal static Regex RNonPairXmlTagsUnvalid = new Regex("<(?:\"[^\"]*\"['\"]*|'[^']*'['\"]*|[^'\">])+>");

    internal static readonly Regex RWhitespace = new Regex(@"\s+");

    internal static bool IsUri(string text)
        => RUri.IsMatch(text) && (text.StartsWith("http://") || text.StartsWith("https://"));

    internal static string? LastTelephone = null;
}
