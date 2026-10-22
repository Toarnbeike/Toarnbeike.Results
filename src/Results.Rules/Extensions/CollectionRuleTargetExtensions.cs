using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Extensions;

/// <summary>
/// Provides extension methods for composing validation rules over collection targets.
/// </summary>
/// <remarks>
/// Supports evaluating rules against each item or selected item properties using all-item and any-item
/// semantics with short-circuit evaluation.
/// </remarks>
public static class CollectionRuleTargetExtensions
{
    /// <typeparam name="T">The type of the target.</typeparam>
    /// <typeparam name="TItem">The type of the items in the collection.</typeparam>
    /// <param name="target">A collection target to apply the rules to (created using .EnsureCollection().</param>
    extension<T, TItem>(RuleTarget<T, IEnumerable<TItem>> target)
    {
        /// <summary>
        /// Creates a rule target that applies a rule to each element in the target sequence.
        /// </summary>
        /// <remarks>Elements are evaluated in enumeration order, and evaluation stops at the first failed result.</remarks>
        /// <returns>An <see cref="RuleTarget{T, TItem}"/> that evaluates the rule for each element, returning the first failure result or success when all elements pass.</returns>
        public RuleTarget<T, TItem> All()
        {
            return target.Compose<TItem>((items, rule) =>
            {
                foreach (var item in items)
                {
                    var result = rule(item);
                    if (!result.IsValid)
                    {
                        return result;
                    }
                }
                return RuleResult.Success();
            });
        }

        /// <summary>
        /// Projects each item in the target collection to a property and applies subsequent rules to every projected value.
        /// </summary>
        /// <remarks>Evaluation stops when a rule returns an invalid result.</remarks>
        /// <typeparam name="TProperty">The type of the projected property value to validate for each item.</typeparam>
        /// <param name="propertySelector">A function that selects the property value from each item.</param>
        /// <returns>An <see cref="RuleTarget{T, TProperty}"/> that applies composed validation to all projected values and returns the first failing result, or success when all values are valid.</returns>
        public RuleTarget<T, TProperty> All<TProperty>(Func<TItem, TProperty> propertySelector)
        {
            ArgumentNullException.ThrowIfNull(propertySelector);
            return target.Compose<TProperty>((items, rule) =>
            {
                foreach (var item in items)
                {
                    var result = rule(propertySelector(item));
                    if (!result.IsValid)
                    {
                        return result;
                    }
                }
                return RuleResult.Success();
            });
        }

        /// <summary>
        /// Creates a rule target that evaluates a collection and succeeds when at least one item satisfies the composed item rule.
        /// </summary>
        /// <remarks>Evaluation stops at the first valid item.</remarks>
        /// <returns>An <see cref="RuleTarget{T, TItem}"/> that returns a successful result when any item is valid; otherwise returns a failed result.</returns>
        public RuleTarget<T, TItem> Any()
        {
            return target.Compose<TItem>((items, rule) =>
            {
                foreach (var item in items)
                {
                    var result = rule(item);
                    if (result.IsValid)
                    {
                        return RuleResult.Success();
                    }
                }
                return RuleResult.Failure("Evalutor.Any", "No items satisfied the rule.");
            });
        }

        /// <summary>
        /// Applies a rule to the selected property of each item and succeeds when at least one item satisfies the rule.
        /// </summary>
        /// <remarks>Evaluation stops at the first valid item.</remarks>
        /// <typeparam name="TProperty">The type of the property selected from each item.</typeparam>
        /// <param name="propertySelector">Selects the property value from each item to evaluate.</param>
        /// <returns>An <see cref="RuleTarget{T, TProperty}"/> that evaluates the composed rule for each selected property value and returns success when any evaluation is valid.</returns>
        public RuleTarget<T, TProperty> Any<TProperty>(Func<TItem, TProperty> propertySelector)
        {
            ArgumentNullException.ThrowIfNull(propertySelector);
            return target.Compose<TProperty>((items, rule) =>
            {
                foreach (var item in items)
                {
                    var result = rule(propertySelector(item));
                    if (result.IsValid)
                    {
                        return RuleResult.Success();
                    }
                }
                return RuleResult.Failure("Evalutor.Any", "No items satisfied the rule.");
            });
        }
    }
}