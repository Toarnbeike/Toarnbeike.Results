using System.Runtime.CompilerServices;
using Toarnbeike.Results.Guards.Core.Targets;

namespace Toarnbeike.Results.Guards;

public static class GuardContextExtensions
{
    /// <param name="context"> The context that is extended with this method. </param>
    extension(IGuardContext context)
    {
        /// <summary>
        /// Begins evaluation of a new value within the current guard pipeline.
        /// </summary>
        /// <typeparam name="T">The type of the value being guarded.</typeparam>
        /// <param name="value">The value to guard.</param>
        /// <param name="expr"> Automatically captured caller argument expression representing the guarded value. </param>
        /// <returns> A guard target that exposes the available guard extensions for the value. </returns>
        public IGuardTarget<T> That<T>(T value,
            [CallerArgumentExpression(nameof(value))]
            string? expr = null)
        {
            return context.ShouldContinueExecution
                ? new GuardTarget<T>(context, value, expr ?? "<unknown>")
                : new ShortCircuitedGuardTarget<T>(context);
        }

        /// <summary>
        /// Begins evaluation of a new collection of values within the current guard pipeline.
        /// Each of the values must satisfy the guard rules for the guard pipeline to continue execution.
        /// </summary>
        /// <typeparam name="T">The type of the value being guarded.</typeparam>
        /// <param name="values">The collection of values to guard.</param>
        /// <param name="expr"> Automatically captured caller argument expression representing the guarded collection. </param>
        /// <returns> A collection guard target that exposes the available guard extensions for the value. </returns>
        public ICollectionGuardTarget<T> ThatEach<T>(IEnumerable<T> values,
            [CallerArgumentExpression(nameof(values))]
            string? expr = null)
        {
            return context.ShouldContinueExecution
                ? new CollectionGuardTarget<T>(context, values, expr ?? "<unknown>")
                : new ShortCircuitedCollectionGuardTarget<T>(context);
        }
    }
}