namespace Toarnbeike.Results.Validation.Implementation;

internal static class ExpressionHelpers
{
    public static string ExtractPropertyName(string? expression)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(expression);

        var dotIndex = expression.IndexOf('.');

        // get the complete expression after the first dot, e.g., "x => x.Property.SubProperty" -> "Property.SubProperty"
        if (dotIndex >= 0 && dotIndex != expression.Length - 1)
        {
            expression = expression[(dotIndex + 1)..];
        }

        var lamdbaIndex = expression.IndexOf("=>", StringComparison.Ordinal);

        // get the complete expression after the lambda operator, e.g., "value => value" -> "value"
        return lamdbaIndex >= 0 && lamdbaIndex != expression.Length - 1
            ? expression[(lamdbaIndex + 2)..].Trim()
            : expression;
    }
}