using System.Runtime.CompilerServices;
using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Rules.Helpers;

namespace Toarnbeike.Results.Ensure;

/// <summary>
/// Extensions for guarding against invalid domain values of a <see cref="Result{T}"/> using a fluent API.
/// </summary>
public static class ResultEnsureExtensions
{
    extension(Result result)
    {
        /// <summary>
        /// Creates a guard target for the value contained in the <see cref="Result{T}"/> using the specified value selector.
        /// </summary>
        /// <typeparam name="T">The type of the value contained in the result.</typeparam>
        /// <param name="valueSelector">A function to select the value from the result.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the property.</param>
        /// <returns>A <see cref="EnsureTarget{T, T}"/> to specify the guard rule for the value.</returns>
        public EnsureTarget<T, T> Ensure<T>(
            Func<T> valueSelector, 
            string? propertyName = null, 
            [CallerArgumentExpression(nameof(valueSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(valueSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);

            var executor = new EnsureExecutor<T, T>((_, rule) =>
                rule.Predicate(valueSelector())
                    ? Result.Success()
                    : new GuardFailure(propertyName, rule.Message));

            return new EnsureTarget<T, T>(result.WithValue(valueSelector()), executor);
        }

        /// <summary>
        /// Creates a guard target for all items in a collection using the specified collection selector.
        /// </summary>
        /// <typeparam name="TItem">The type of the items in the collection.</typeparam>
        /// <param name="collectionSelector">A function to select the collection from the result.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the property.</param>
        /// <returns>A <see cref="EnsureTarget{TItem, TItem}"/> to specify the guard rule for all items in the collection.</returns>
        public EnsureTarget<IEnumerable<TItem>, TItem> EnsureAll<TItem>(
            Func<IEnumerable<TItem>> collectionSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(collectionSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(collectionSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);
            var executor = new EnsureExecutor<IEnumerable<TItem>, TItem>((_, rule) =>
            {
                var collection = collectionSelector();
                var index = 0;
                foreach (var item in collection)
                {
                    if (!rule.Predicate(item))
                    {
                        return new GuardFailure($"{propertyName}[{index}]", rule.Message);
                    }
                    index++;
                }
                return Result.Success();
            });
            return new EnsureTarget<IEnumerable<TItem>, TItem>(result.WithValue(collectionSelector()), executor);
        }

        /// <summary>
        /// Creates a guard target for all items in a collection using the specified collection selector.
        /// </summary>
        /// <typeparam name="TItem">The type of the items in the collection.</typeparam>
        /// <param name="collectionSelector">A function to select the collection from the result.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the property.</param>
        /// <returns>A <see cref="EnsureTarget{TItem, TItem}"/> to specify the guard rule for any item in the collection.</returns>
        public EnsureTarget<IEnumerable<TItem>, TItem> EnsureAny<TItem>(
            Func<IEnumerable<TItem>> collectionSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(collectionSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(collectionSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);
            var executor = new EnsureExecutor<IEnumerable<TItem>, TItem>((_, rule) =>
            {
                var collection = collectionSelector();
                return collection.Any(item => rule.Predicate(item))
                    ? Result.Success()
                    : new GuardFailure(propertyName, $"No items in the collection satisfied the rule: {rule.Message}");
            });
            return new EnsureTarget<IEnumerable<TItem>, TItem>(result.WithValue(collectionSelector()), executor);
        }
    }

    extension<T>(Result<T> result)
    {
        /// <summary>
        /// Creates a guard target for the value contained in the <see cref="Result{T}"/> using the specified value selector.
        /// </summary>
        /// <typeparam name="T">The type of the value contained in the result.</typeparam>
        /// <param name="valueSelector">A function to select the value from the result.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the property.</param>
        /// <returns>A <see cref="EnsureTarget{TProperty, T}"/> to specify the guard rule for the value.</returns>
        public EnsureTarget<TProperty, TProperty> Ensure<TProperty>(
            Func<TProperty> valueSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(valueSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(valueSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);

            var executor = new EnsureExecutor<TProperty, TProperty>((_, rule) =>
                rule.Predicate(valueSelector())
                    ? Result.Success()
                    : new GuardFailure(propertyName, rule.Message));

            return new EnsureTarget<TProperty, TProperty>(result.Map(_ => valueSelector()), executor);
        }

        /// <summary>
        /// Creates a guard target for all items in a collection using the specified collection selector.
        /// </summary>
        /// <typeparam name="TItem">The type of the items in the collection.</typeparam>
        /// <param name="collectionSelector">A function to select the collection from the result.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the property.</param>
        /// <returns>A <see cref="EnsureTarget{TItem, TItem}"/> to specify the guard rule for all items in the collection.</returns>
        public EnsureTarget<IEnumerable<TItem>, TItem> EnsureAll<TItem>(
            Func<IEnumerable<TItem>> collectionSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(collectionSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(collectionSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);
            var executor = new EnsureExecutor<IEnumerable<TItem>, TItem>((_, rule) =>
            {
                var collection = collectionSelector();
                var index = 0;
                foreach (var item in collection)
                {
                    if (!rule.Predicate(item))
                    {
                        return new GuardFailure($"{propertyName}[{index}]", rule.Message);
                    }
                    index++;
                }
                return Result.Success();
            });
            return new EnsureTarget<IEnumerable<TItem>, TItem>(result.Map(_ => collectionSelector()), executor);
        }

        /// <summary>
        /// Creates a guard target for all items in a collection using the specified collection selector.
        /// </summary>
        /// <typeparam name="TItem">The type of the items in the collection.</typeparam>
        /// <param name="collectionSelector">A function to select the collection from the result.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the property.</param>
        /// <returns>A <see cref="EnsureTarget{TItem, TItem}"/> to specify the guard rule for any item in the collection.</returns>
        public EnsureTarget<IEnumerable<TItem>, TItem> EnsureAny<TItem>(
            Func<IEnumerable<TItem>> collectionSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(collectionSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(collectionSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);
            var executor = new EnsureExecutor<IEnumerable<TItem>, TItem>((_, rule) =>
            {
                var collection = collectionSelector();
                return collection.Any(item => rule.Predicate(item))
                    ? Result.Success()
                    : new GuardFailure(propertyName, $"No items in the collection satisfied the rule: {rule.Message}");
            });
            return new EnsureTarget<IEnumerable<TItem>, TItem>(result.Map(_ => collectionSelector()), executor);
        }

        /// <summary>
        /// Creates a guard target for  a property of the value contained in the <see cref="Result{T}"/> using the specified property selector.
        /// This guard target can then be used to specify the guard rule for the property.
        /// All guard rules are short-circuiting, meaning that if a rule fails, subsequent rules will not be evaluated.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property to guard.</typeparam>
        /// <param name="propertySelector">A function to select the property from the value.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the property.</param>
        /// <returns>A <see cref="EnsureTarget{T, TProperty}"/> to specify the guard rule for the property.</returns>
        public EnsureTarget<T, TProperty> Ensure<TProperty>(
        Func<T, TProperty> propertySelector,
        string? propertyName = null,
        [CallerArgumentExpression(nameof(propertySelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(propertySelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);

            var executor = new EnsureExecutor<T, TProperty>((value, rule) =>
                rule.Predicate(propertySelector(value))
                    ? Result.Success()
                    : new GuardFailure(propertyName, rule.Message));

            return new EnsureTarget<T, TProperty>(result, executor);
        }

        /// <summary>
        /// Creates a guard target for all items in a collection property of the value contained in the <see cref="Result{T}"/> using the specified collection selector.
        /// This guard target can then be used to specify the guard rule for the items in the collection.
        /// All guard rules are short-circuiting, meaning that if a rule fails, subsequent rules will not be evaluated.
        /// Also when a single item in the collection fails the guard, the guard will return with the first encountered failure.
        /// </summary>
        /// <typeparam name="TItem">The type of the items in the collection to guard.</typeparam>
        /// <param name="collectionSelector">A function to select the collection from the value.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the collection.</param>
        /// <returns>A <see cref="EnsureTarget{T, TItem}"/> to specify the guard rule for the items in the collection.</returns>
        public EnsureTarget<T, TItem> EnsureAll<TItem>(
            Func<T, IEnumerable<TItem>> collectionSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(collectionSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(collectionSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);

            var executor = new EnsureExecutor<T, TItem>((value, rule) =>
            {
                var collection = collectionSelector(value);
                var index = 0;
                foreach (var item in collection)
                {
                    if (!rule.Predicate(item))
                    {
                        return new GuardFailure($"{propertyName}[{index}]", rule.Message);
                    }
                    index++;
                }
                return Result.Success();
            });

            return new EnsureTarget<T, TItem>(result, executor);
        }

        /// <summary>
        /// Creates a guard target for all items in a collection property of the value contained in the <see cref="Result{T}"/> using the specified collection selector.
        /// The guard target will determine if at least one item in the collection satisfies the specified guard rule.
        /// If no items in the collection satisfy the rule, the guard will return a failure with the specified message.
        /// </summary>
        /// <typeparam name="TItem">The type of the items in the collection to guard.</typeparam>
        /// <param name="collectionSelector">A function to select the collection from the value.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the collection.</param>
        /// <returns>A <see cref="EnsureTarget{T, TItem}"/> representing the validation target for any item in the collection.</returns>
        public EnsureTarget<T, TItem> EnsureAny<TItem>(
            Func<T, IEnumerable<TItem>> collectionSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(collectionSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(collectionSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);
            var executor = new EnsureExecutor<T, TItem>((value, rule) =>
            {
                var collection = collectionSelector(value);
                return collection.Any(item => rule.Predicate(item))
                    ? Result.Success()
                    : new GuardFailure(propertyName, $"No items in the collection satisfied the rule: {rule.Message}");
            });
            return new EnsureTarget<T, TItem>(result, executor);
        }
    }
}