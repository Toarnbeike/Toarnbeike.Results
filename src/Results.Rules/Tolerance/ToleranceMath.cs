using System.Numerics;

namespace Toarnbeike.Results.Rules.Tolerance;

/// <summary>
/// A helper struct for performing mathematical operations with tolerance on a value of type T.
/// </summary>
/// <typeparam name="T">The type of the value being operated on.</typeparam>
internal static class ToleranceMath<T> where T : INumber<T>, IMinMaxValue<T>
{
    public static T GetAdjustedMin(T min, T tolerance)
    {
        // If the provided minimum value would become less than the minimum value of type T after subtracting the tolerance, return the minimum value of type T instead.
        return min.CompareTo(T.MinValue + tolerance) < 0 ? T.MinValue : min - tolerance;
    }

    public static T GetAdjustedMax(T max, T tolerance)
    {
        // If the provided maximum value would become greater than the maximum value of type T after adding the tolerance, return the maximum value of type T instead.
        return max.CompareTo(T.MaxValue - tolerance) > 0 ? T.MaxValue : max + tolerance;
    }
}