using System.Numerics;

namespace Toarnbeike.Results.Ensure.Guards;

internal static class FloatingPointGuards
{
    public static bool Finite<T>(T value) where T : IFloatingPointIeee754<T> => T.IsFinite(value);
    public static bool NotNaN<T>(T value) where T : IFloatingPointIeee754<T> => !T.IsNaN(value);
}