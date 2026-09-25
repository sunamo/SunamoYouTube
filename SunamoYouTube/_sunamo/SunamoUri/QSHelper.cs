namespace SunamoYouTube._sunamo.SunamoUri;

/// <summary>
/// Helper class for parsing query string parameters from URIs.
/// </summary>
internal class QSHelper
{
    /// <summary>
    /// Gets a query string parameter value from a URI.
    /// </summary>
    /// <param name="uri">The URI to parse.</param>
    /// <param name="parameterName">The name of the parameter to retrieve.</param>
    /// <returns>The parameter value, or null if not found.</returns>
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
