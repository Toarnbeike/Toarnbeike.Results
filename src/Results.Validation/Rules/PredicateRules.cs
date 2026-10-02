using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Rules;

internal static class PredicateRules
{
    public static ValidationRule<T> MustSatisfy<T>(Func<T, bool> predicate, string message)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return new(value => value is not null && predicate(value), message);
    }

    public static ValidationRule<T> MustNotSatisfy<T>(Func<T, bool> predicate, string message)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return new(value => value is not null && !predicate(value), message);
    }
}