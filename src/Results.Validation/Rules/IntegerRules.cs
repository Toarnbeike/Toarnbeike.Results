using System.Numerics;

namespace Toarnbeike.Results.Validation.Rules;

internal static class IntegerRules
{
    public static bool MultipleOf<T>(T value, T factor) where T : struct, IBinaryInteger<T>
    {
        if (factor == T.Zero)
            throw new ArgumentOutOfRangeException(nameof(factor), factor, "The provided factor can't be zero.");
        return value % factor == T.Zero;
    }
}
