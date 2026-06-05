using System.Globalization;
using Toarnbeike.Results.Guards.Core.GuardContext;
using Toarnbeike.Results.Guards.Tolerances;

namespace Toarnbeike.Results.Guards;

public static class ResultGuardExtensions
{
    extension(Result)
    {
        public static IGuardContext Ensure(
            CultureInfo? culture = null,
            IToleranceProvider? toleranceProvider = null,
            TimeProvider? timeProvider = null) =>
            new EnsureGuardContext(culture, toleranceProvider, timeProvider);

        public static IGuardContext Validate(
            CultureInfo? culture = null,
            IToleranceProvider? toleranceProvider = null,
            TimeProvider? timeProvider = null) =>
            new ValidatingGuardContext(culture, toleranceProvider, timeProvider);
    }
}