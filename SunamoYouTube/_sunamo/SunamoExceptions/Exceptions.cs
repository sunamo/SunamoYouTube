namespace SunamoYouTube._sunamo.SunamoExceptions;

internal sealed partial class Exceptions
{
    #region Other
    internal static string CheckBefore(string prefix)
        => string.IsNullOrWhiteSpace(prefix) ? string.Empty : prefix + ": ";

    internal static Tuple<string, string, string> PlaceOfException(bool isFillingFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var lines = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        string typeName = string.Empty;
        string methodName = string.Empty;
        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            if (isFillingFirstTwo)
                if (!line.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(line, out typeName, out methodName);
                    isFillingFirstTwo = false;
                }
            if (line.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(typeName, methodName, string.Join(Environment.NewLine, lines));
    }

    internal static void TypeAndMethodName(string line, out string typeName, out string methodName)
    {
        var afterAt = line.Split("at ")[1].Trim();
        var beforeParenthesis = afterAt.Split('(')[0];
        var nameParts = beforeParenthesis.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = nameParts[^1];
        nameParts.RemoveAt(nameParts.Count - 1);
        typeName = string.Join(".", nameParts);
    }

    internal static string CallingMethod(int depth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(depth)?.GetMethod();
        if (methodBase is null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }
    #endregion

    #region IsNullOrWhitespace
    internal static string? IsNullOrWhitespace(string prefix, string argumentName, string argumentValue, bool isNotAllowingOnlyWhitespace)
    {
        string additionalParamsText;
        if (argumentValue == null)
        {
            additionalParamsText = AddParams();
            return CheckBefore(prefix) + argumentName + " is null" + additionalParamsText;
        }
        if (argumentValue == string.Empty)
        {
            additionalParamsText = AddParams();
            return CheckBefore(prefix) + argumentName + " is empty (without trim)" + additionalParamsText;
        }
        if (isNotAllowingOnlyWhitespace && argumentValue.Trim() == string.Empty)
        {
            additionalParamsText = AddParams();
            return CheckBefore(prefix) + argumentName + " is empty (with trim)" + additionalParamsText;
        }
        return null;
    }

    private readonly static StringBuilder additionalInfoInnerStringBuilder = new();
    private readonly static StringBuilder additionalInfoStringBuilder = new();

    internal static string AddParams()
    {
        additionalInfoStringBuilder.Insert(0, Environment.NewLine);
        additionalInfoStringBuilder.Insert(0, "Outer:");
        additionalInfoStringBuilder.Insert(0, Environment.NewLine);
        additionalInfoInnerStringBuilder.Insert(0, Environment.NewLine);
        additionalInfoInnerStringBuilder.Insert(0, "Inner:");
        additionalInfoInnerStringBuilder.Insert(0, Environment.NewLine);
        var additionalParamsText = additionalInfoStringBuilder.ToString();
        var additionalParamsInnerText = additionalInfoInnerStringBuilder.ToString();
        return additionalParamsText + additionalParamsInnerText;
    }
    #endregion
}
