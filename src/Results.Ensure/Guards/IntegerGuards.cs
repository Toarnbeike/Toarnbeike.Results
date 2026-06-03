using System.Numerics;

namespace Toarnbeike.Results.Ensure.Guards;

internal static class IntegerGuards
{
    public static bool MultipleOf<T>(T value, T factor) where T : struct, IBinaryInteger<T>
    {
        if (factor == T.Zero)
            throw new ArgumentOutOfRangeException(nameof(factor), factor, "The provided factor can't be zero.");
        return value % factor == T.Zero;
    }

    public static bool Even<T>(T value) where T : IBinaryInteger<T> => (value & T.One) == T.Zero;
    public static bool Odd<T>(T value) where T : IBinaryInteger<T> => (value & T.One) != T.Zero;
    public static bool PowerOf2<T>(T value) where T : IBinaryInteger<T> => 
        value > T.Zero && (value & (value - T.One)) == T.Zero;
    public static bool Prime<T>(T value) where T : IBinaryInteger<T>
    {
        if (value < T.Zero)
            throw new ArgumentOutOfRangeException(nameof(value), value,
                "Can't determine a prime of a non positive number");

        var two = T.CreateChecked(2);
        if (value == two) return true;

        if (value < two || Even(value))
        {
            return false;
        }

        // Loop over every odd number starting from 3 up to the square root of the value
        // If any of these numbers divides the value evenly, then it's not prime
        // This is not the most efficient way to check if a number is prime,
        // since it checks odd composite numbers (9, 15 etc.) unnecessarily.
        // However, it's simple and works for common values,
        // without building a cache of primes or using more complex algorithms like Miller-Rabin.
        for (var i = T.CreateChecked(3); i * i <= value; i += two)
        {
            if (value % i == T.Zero)
            {
                return false;
            }
        }
        return true;
    }
}