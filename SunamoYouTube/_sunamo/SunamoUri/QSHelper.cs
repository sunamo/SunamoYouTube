namespace SunamoYouTube._sunamo.SunamoUri;

internal class QSHelper
{
    internal static string? GetParameter(string uri, string parameterName)
    {
        var segments = uri.Split(new char[] { '?', '&' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string segment in segments)
        {
            var keyValueParts = segment.Split('=', StringSplitOptions.RemoveEmptyEntries);
            if (keyValueParts[0] == parameterName)
            {
                return keyValueParts[1];
            }
        }

        return null;
    }
}
