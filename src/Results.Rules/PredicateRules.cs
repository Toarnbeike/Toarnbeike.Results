namespace Toarnbeike.Results.Rules;

public static class PredicateRules
{
    public static Rule<T> Must<T>(Func<T, bool> predicate, string message)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return new(value => value is not null && predicate(value), message);
    }

    public static Rule<T> MustNot<T>(Func<T, bool> predicate, string message)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return new(value => value is not null && !predicate(value), message);
    }
}