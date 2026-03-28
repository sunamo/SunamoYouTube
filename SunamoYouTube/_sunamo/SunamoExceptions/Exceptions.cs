namespace SunamoYouTube._sunamo.SunamoExceptions;

/// <summary>
/// Exception helper class for formatting and creating exception messages.
/// </summary>
internal sealed partial class Exceptions
{
    #region Other
    /// <summary>
    /// Checks the prefix string and appends a colon separator if not empty.
    /// </summary>
    /// <param name="prefix">The prefix string to check.</param>
    /// <returns>The prefix with colon separator, or empty string if prefix is null or whitespace.</returns>
    internal static string CheckBefore(string prefix)
    {
        return string.IsNullOrWhiteSpace(prefix) ? string.Empty : prefix + ": ";
    }

    /// <summary>
    /// Gets the place of exception from the current stack trace.
    /// </summary>
    /// <param name="isFillingFirstTwo">Whether to also fill the type and method name from the first non-ThrowEx frame.</param>
    /// <returns>A tuple containing type name, method name, and formatted stack trace.</returns>
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

    /// <summary>
    /// Extracts type name and method name from a stack trace line.
    /// </summary>
    /// <param name="line">The stack trace line to parse.</param>
    /// <param name="typeName">The extracted type name.</param>
    /// <param name="methodName">The extracted method name.</param>
    internal static void TypeAndMethodName(string line, out string typeName, out string methodName)
    {
        var afterAt = line.Split("at ")[1].Trim();
        var beforeParenthesis = afterAt.Split('(')[0];
        var nameParts = beforeParenthesis.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = nameParts[^1];
        nameParts.RemoveAt(nameParts.Count - 1);
        typeName = string.Join(".", nameParts);
    }

    /// <summary>
    /// Gets the name of the calling method from the stack trace.
    /// </summary>
    /// <param name="depth">The stack frame depth to retrieve.</param>
    /// <returns>The name of the calling method.</returns>
    internal static string CallingMethod(int depth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(depth)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }
    #endregion

    #region IsNullOrWhitespace
    /// <summary>
    /// Checks if a string argument is null, empty, or whitespace and returns an error message.
    /// </summary>
    /// <param name="prefix">Prefix for the error message.</param>
    /// <param name="argumentName">Name of the argument being checked.</param>
    /// <param name="argumentValue">Value of the argument to check.</param>
    /// <param name="isNotAllowingOnlyWhitespace">Whether to disallow strings containing only whitespace.</param>
    /// <returns>Error message if validation fails, null otherwise.</returns>
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

    /// <summary>
    /// Formats and returns accumulated additional parameter information.
    /// </summary>
    /// <returns>Formatted string with outer and inner additional parameter details.</returns>
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
