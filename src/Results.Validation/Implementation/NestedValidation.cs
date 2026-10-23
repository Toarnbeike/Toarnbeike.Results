using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Validation.Implementation;

internal static class NestedValidation
{
    internal static IEnumerable<ValidationFailure> Validate<T>(
        IValidator<T> validator,
        T value,
        string parentPath)
    {
        var result = validator.Validate(value);
        if (!result.TryGetFailure(out var failure))
        {
            return [];
        }

        if (failure is not ValidationFailureSummary summary)
        {
            return [new ValidationFailure(parentPath, failure.Message)];
        }

        return summary.Failures.SelectMany(group => group.Value.Select(message =>
            new ValidationFailure(Join(parentPath, group.Key), message)));
    }

    private static string Join(string parentPath, string childPath) =>
        childPath.StartsWith("[", StringComparison.Ordinal)
            ? $"{parentPath}{childPath}"
            : $"{parentPath}.{childPath}";
}