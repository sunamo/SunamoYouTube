

namespace SunamoYouTube._sunamo;
internal class QSHelper
{
    internal static string GetParameter(string uri, string nameParam)
    {
        var main = uri.Split(new string[] { AllStrings.q, "&" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string var in main)
        {
            var v = var.Split(new String[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
            if (v[0] == nameParam)
            {
                return v[1];
            }
        }

        return null;
    }
}
