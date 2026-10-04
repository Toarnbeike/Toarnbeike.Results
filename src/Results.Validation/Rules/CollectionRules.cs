using System.Collections;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Rules;

internal static class CollectionRules
{
    public static ValidationRule<TEnumerable> NotEmpty<TEnumerable>(string? message)
        where TEnumerable : IEnumerable =>
        new(value => value is not null && !IsCollectionEmpty(value),
            message ?? "Collection must not be empty."
    );

    public static ValidationRule<TEnumerable> Empty<TEnumerable>(string? message)
        where TEnumerable : IEnumerable =>
        new(value => value is not null && IsCollectionEmpty(value),
            message ?? "Collection must be empty."
    );

    public static ValidationRule<TEnumerable> HasAtLeast<TEnumerable>(int min, string? message)
        where TEnumerable : IEnumerable =>
        new(value => value is not null && !HasCollectionLessThan(value, min),
            message ?? $"Collection must contain at least {min} element(s)."
    );

    public static ValidationRule<TEnumerable> HasAtMost<TEnumerable>(int max, string? message)
        where TEnumerable : IEnumerable =>
        new(value => value is not null && HasCollectionLessThan(value, max + 1),
            message ?? $"Collection must contain at most {max} element(s)."
    );

    public static ValidationRule<TEnumerable> HasBetween<TEnumerable>(int min, int max, string? message)
        where TEnumerable : IEnumerable
    {
        return new(value => value is not null && HasCollectionBetween(value, min, max),
        message ?? $"Collection must contain between {min} and {max} element(s).");
    }

    public static ValidationRule<TEnumerable> HasExactly<TEnumerable>(int expected, string? message)
        where TEnumerable : IEnumerable =>
        new(value => value is not null && HasCollectionExactly(value, expected),
        message ?? $"Collection must contain exactly {expected} element(s)."
    );


    private static bool IsCollectionEmpty(IEnumerable target)
    {
        if (target is ICollection collection)
        {
            return collection.Count == 0;
        }
        return !target.GetEnumerator().MoveNext();
    }

    private static bool HasCollectionLessThan(IEnumerable target, int count)
    {
        if (target is ICollection collection)
        {
            return collection.Count < count;
        }

        int currentCount = 0;
        foreach (var _ in target)
        {
            currentCount++;
            if (currentCount >= count)
            {
                return false;
            }
        }
        return true;
    }

    private static bool HasCollectionExactly(IEnumerable target, int expectedCount)
    {
        if (target is ICollection collection)
        {
            return collection.Count == expectedCount;
        }

        int currentCount = 0;
        foreach (var _ in target)
        {
            currentCount++;
            if (currentCount > expectedCount)
            {
                return false;
            }
        }
        return currentCount == expectedCount;
    }

    private static bool HasCollectionBetween(IEnumerable target, int min, int max)
    {
        if (target is ICollection collection)
        {
            return collection.Count >= min && collection.Count <= max;
        }
        int currentCount = 0;
        foreach (var _ in target)
        {
            currentCount++;
            if (currentCount > max)
            {
                return false;
            }
        }
        return currentCount >= min;
    }
}
