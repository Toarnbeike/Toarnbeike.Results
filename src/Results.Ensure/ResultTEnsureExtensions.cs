using System.Runtime.CompilerServices;
using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Rules.Extensions;

/// <summary>
/// Provides extension methods for creating guard targets from <see cref="Result{T}"/> instances.
/// </summary>
public static class ResultTEnsureExtensions
{
    /// <typeparam name="T">The type of the value contained in the result.</typeparam>
    extension<T>(Result<T> result)
    {
        /// <summary>
        /// Creates an ensure target for the current value and associates validation failures with the specified property name.
        /// </summary>
        /// <param name="propertyName">Property name to include in guard failures. If null, a fallback name is extracted.</param>
        /// <returns>An ensure target that evaluates rules against the current value and returns either success or a guard failure.</returns>
        public RuleTarget<T, T> Ensure(string propertyName)
        {
            propertyName ??= ExpressionHelpers.ExtractPropertyName("");

            var executor = new RuleExecutor<T, T>((value, rule, message) =>
            {
                var guardResult = rule(value);
                return guardResult.IsValid
                    ? Result.Success(value)
                    : new GuardFailure(propertyName, message ?? guardResult.Message);
            });

            return new RuleTarget<T, T>(result, executor);
        }

        /// <summary>
        /// Creates a guard target for the value contained in the <see cref="Result{T}"/> using the specified value selector.
        /// </summary>
        /// <param name="valueSelector">A function to select the value from the result.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the property.</param>
        /// <returns>A <see cref="RuleTarget{T, TProperty}"/> to specify the guard rule for the value.</returns>
        public RuleTarget<T, TProperty> Ensure<TProperty>(
            Func<T, TProperty> valueSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(valueSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(valueSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);

            var executor = new RuleExecutor<T, TProperty>((value, rule, message) =>
            {
                var guardResult = rule(valueSelector(value));
                return guardResult.IsValid
                    ? Result.Success(value)
                    : new GuardFailure(propertyName, message ?? guardResult.Message);
            });

            return new RuleTarget<T, TProperty>(result, executor);
        }

        /// <summary>
        /// Creates a guard target for the collection contained in the <see cref="Result{T}"/> using the specified value selector.
        /// </summary>
        /// <param name="valueSelector">A function to select the collection from the result.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the property.</param>
        /// <returns>A <see cref="RuleTarget{T, IEnumerable{T}}"/> to specify the guard rule for the collection.</returns>
        public RuleTarget<T, IEnumerable<TItem>> EnsureCollection<TItem>(
            Func<T, IEnumerable<TItem>> valueSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(valueSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(valueSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);
            var executor = new RuleExecutor<T, IEnumerable<TItem>>((value, rule, message) =>
            {
                var guardResult = rule(valueSelector(value));
                return guardResult.IsValid
                    ? Result.Success(value)
                    : new GuardFailure(propertyName, message ?? guardResult.Message);
            });
            return new RuleTarget<T, IEnumerable<TItem>>(result, executor);
        }
    }
}
