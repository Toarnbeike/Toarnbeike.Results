namespace Toarnbeike.Results.Validation;

internal static class ExpressionHelpers
{
    public static string ExtractPropertyName(string? expression)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(expression);

        // get the complete expression after the first dot, e.g., "x => x.Property.SubProperty" -> "Property.SubProperty"
        var dotIndex = expression.IndexOf('.');
        return dotIndex >= 0 && dotIndex != expression.Length - 1
            ? expression[(dotIndex + 1)..]
            : expression;
    }
}