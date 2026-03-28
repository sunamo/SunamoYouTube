namespace SunamoYouTube._sunamo.SunamoExceptions;

/// <summary>
/// Helper class for throwing validated exceptions with detailed context.
/// </summary>
internal partial class ThrowEx
{
    /// <summary>
    /// Validates that a string argument is not null or empty.
    /// </summary>
    /// <param name="argumentName">Name of the argument to validate.</param>
    /// <param name="argumentValue">Value of the argument to validate.</param>
    /// <returns>True if the validation failed and an exception was thrown.</returns>
    internal static bool IsNullOrEmpty(string argumentName, string argumentValue)
    { return ThrowIsNotNull(Exceptions.IsNullOrWhitespace(FullNameOfExecutedCode(), argumentName, argumentValue, true)); }

    #region Other
    /// <summary>
    /// Gets the fully qualified name of the currently executed code location.
    /// </summary>
    /// <returns>The fully qualified type and method name.</returns>
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    /// <summary>
    /// Gets the fully qualified name from type and method name.
    /// </summary>
    /// <param name="type">The type object, Type, MethodBase, or string representation.</param>
    /// <param name="methodName">The method name.</param>
    /// <param name="isFromThrowEx">Whether the call originates from ThrowEx class.</param>
    /// <returns>The fully qualified type.method name.</returns>
    private static string FullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (isFromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (type is Type resolvedType)
        {
            typeFullName = resolvedType.FullName ?? "Type cannot be get via type is Type resolvedType";
        }
        else if (type is MethodBase methodBase)
        {
            typeFullName = methodBase.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase methodBase";
            methodName = methodBase.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type objectType = type.GetType();
            typeFullName = objectType.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    /// <summary>
    /// Throws an exception if the provided exception message is not null.
    /// </summary>
    /// <param name="exceptionMessage">The exception message to check. If not null, an exception is thrown.</param>
    /// <param name="isReallyThrowing">Whether to actually throw the exception or just return the result.</param>
    /// <returns>True if the exception message was not null.</returns>
    internal static bool ThrowIsNotNull(string? exceptionMessage, bool isReallyThrowing = true)
    {
        if (exceptionMessage != null)
        {
            Debugger.Break();
            if (isReallyThrowing)
            {
                throw new Exception(exceptionMessage);
            }
            return true;
        }
        return false;
    }
    #endregion
}
