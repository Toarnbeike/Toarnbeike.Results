using System.Runtime.CompilerServices;
using Toarnbeike.Results.Guards.Core.Targets;

namespace Toarnbeike.Results.Guards;

public static class GuardContextExtensions
{
    /// <summary>
    /// Begins evaluation of a new value within the current guard pipeline.
    /// </summary>
    /// <typeparam name="T">The type of the value being guarded.</typeparam>
    /// <param name="context"> The context that is extended with this method. </param>
    /// <param name="value">The value to guard.</param>
    /// <param name="expr"> Automatically captured caller argument expression representing the guarded value. </param>
    /// <returns> A guard target that exposes the available guard extensions for the value. </returns>
    public static IGuardTarget<T> That<T>(this IGuardContext context, T value,
        [CallerArgumentExpression(nameof(value))]
        string? expr = null)
    {
        return context.ShouldContinueExecution
            ? new GuardTarget<T>(context, value, expr ?? "<unknown>")
            : new ShortCircuitedGuardTarget<T>(context);
    }
}