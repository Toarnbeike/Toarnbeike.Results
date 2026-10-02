using System.Collections;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Rules;

internal static class CollectionRules
{
    public static ValidationRule<TEnumerable> NotEmpty<TEnumerable>(string? message)
        where TEnumerable : IEnumerable =>
        new(value => value is not null && GetCount(value) > 0,
        message ?? "Collection must not be empty."
    );

    public static ValidationRule<TEnumerable> Empty<TEnumerable>(string? message)
        where TEnumerable : IEnumerable =>
        new(value => value is not null && GetCount(value) == 0,
        message ?? "Collection must be empty."
    );

    public static ValidationRule<TEnumerable> HasAtLeast<TEnumerable>(int min, string? message)
        where TEnumerable : IEnumerable =>
        new(value => value is not null && GetCount(value) >= min,
        message ?? $"Collection must contain at least {min} element(s)."
    );

    public static ValidationRule<TEnumerable> HasAtMost<TEnumerable>(int max, string? message)
        where TEnumerable : IEnumerable =>
        new(value => value is not null && GetCount(value) <= max,
        message ?? $"Collection must contain at most {max} element(s)."
    );

    public static ValidationRule<TEnumerable> HasBetween<TEnumerable>(int min, int max, string? message)
        where TEnumerable : IEnumerable =>
        new(value => value is not null && GetCount(value) >= min && GetCount(value) <= max,
        message ?? $"Collection must contain between {min} and {max} element(s)."
    );

    public static ValidationRule<TEnumerable> HasExactly<TEnumerable>(int expected, string? message)
        where TEnumerable : IEnumerable =>
        new(value => value is not null && GetCount(value) == expected,
        message ?? $"Collection must contain exactly {expected} element(s)."
    );

    private static int GetCount(IEnumerable target)
    {
        if (target is ICollection collection)
        {
            return collection.Count;
        }

        return target.Cast<object?>().ToArray().Length;
    }
}
