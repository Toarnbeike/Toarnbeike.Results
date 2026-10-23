using System.Runtime.CompilerServices;
using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Rules;

/// <summary>
/// Provides extension methods for creating guard targets from <see cref="Result"/> and <see cref="Task{Result}"/> instances.
/// </summary>
public static class ResultEnsureExtensions
{
    extension(Result result)
    {
        /// <summary>
        /// Creates a guard target for the value contained in the <see cref="Result"/> using the specified value selector.
        /// </summary>
        /// <typeparam name="T">The type of the value contained in the result.</typeparam>
        /// <param name="valueSelector">A function to select the value from the result.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the property.</param>
        /// <returns>A <see cref="RuleTarget{T, TProperty}"/> to specify the guard rule for the value.</returns>
        public RuleTarget<T, T> Ensure<T>(
            Func<T> valueSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(valueSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(valueSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);

            return RuleTargetFactory.Create(result.WithValue(valueSelector()), _ => valueSelector(), propertyName, static (name, message) => new GuardFailure(name, message));
        }

        /// <summary>
        /// Creates a guard target for the collection contained in the <see cref="Result"/> using the specified value selector.
        /// </summary>
        /// <typeparam name="TItem">The type of the items in the collection.</typeparam>
        /// <param name="collectionSelector">A function to select the collection from the result.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the property.</param>
        /// <returns>A <see cref="RuleTarget{T, TProperty}"/> to specify the guard rule for the collection.</returns>
        public RuleTarget<IEnumerable<TItem>, IEnumerable<TItem>> EnsureCollection<TItem>(
            Func<IEnumerable<TItem>> collectionSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(collectionSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(collectionSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);

            return RuleTargetFactory.Create(result.WithValue(collectionSelector()), _ => collectionSelector(), propertyName, static (name, message) => new GuardFailure(name, message));
        }
    }

    extension(Task<Result> result)
    {
        /// <summary>
        /// Creates a guard target for the value contained in the <see cref="Result"/> using the specified value selector.
        /// </summary>
        /// <typeparam name="T">The type of the value contained in the result.</typeparam>
        /// <param name="valueSelector">A function to select the value from the result.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the property.</param>
        /// <returns>A <see cref="RuleTarget{T, TProperty}"/> to specify the guard rule for the value.</returns>
        public async Task<RuleTarget<T, T>> EnsureAsync<T>(
            Func<T> valueSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(valueSelector))] string? expression = null)
        {
            var resultValue = await result.ConfigureAwait(false);
            return resultValue.Ensure(valueSelector, propertyName, expression);
        }

        /// <summary>
        /// Creates a guard target for the collection contained in the <see cref="Result"/> using the specified value selector.
        /// </summary>
        /// <typeparam name="TItem">The type of the items in the collection.</typeparam>
        /// <param name="collectionSelector">A function to select the collection from the result.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the property.</param>
        /// <returns>A <see cref="RuleTarget{T, TProperty}"/> to specify the guard rule for the collection.</returns>
        public async Task<RuleTarget<IEnumerable<TItem>, IEnumerable<TItem>>> EnsureCollectionAsync<TItem>(
            Func<IEnumerable<TItem>> collectionSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(collectionSelector))] string? expression = null)
        {
            var resultValue = await result.ConfigureAwait(false);
            return resultValue.EnsureCollection(collectionSelector, propertyName, expression);
        }
    }
}
