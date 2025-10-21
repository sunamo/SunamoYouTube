// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoYouTube._sunamo.SunamoUri;

internal class QSHelper
{
    internal static string GetParameter(string uri, string nameParam)
    {
        var main = uri.Split(new string[] { "?", "&" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string var in main)
        {
            var value = var.Split(new String[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
            if (value[0] == nameParam)
            {
                return value[1];
            }
        }

        return null;
    }
}
