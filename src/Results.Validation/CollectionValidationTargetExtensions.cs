using Toarnbeike.Results.Rules;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation;

public static class CollectionValidationTargetExtensions
{
    /// <param name="target">A collection target to apply the rules to (created using .ValidateCollection().</param>
    /// <typeparam name="T">The type of the target.</typeparam>
    /// <typeparam name="TItem">The type of the items in the collection.</typeparam>
    extension<T, TItem>(ValidationTarget<T, IEnumerable<TItem>> target)
    {
        /// <summary>
        /// Creates a composed target that applies a rule to each element in the target sequence.
        /// </summary>
        /// <remarks>Elements are evaluated in enumeration order, and evaluation stops at the first failed result.</remarks>
        /// <returns>A ValidationTarget<T, TItem> that evaluates the rule for each element, returning the first failure result or
        /// success when all elements pass.</returns>
        public ValidationTarget<T, TItem> All()
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
        /// Creates an <see cref="ValidationTarget{T, TProperty}"/> for each item in the collection, allowing you to apply rules to each item's property.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property to apply the rule to.</typeparam>
        /// <param name="propertySelector">A function to select the property from each item in the collection.</param>
        /// <returns>A new <see cref="ValidationTarget{T, TProperty}"/> for the composed property.</returns>
        public ValidationTarget<T, TProperty> All<TProperty>(Func<TItem, TProperty> propertySelector)
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
        /// Builds a collection rule that succeeds when at least one item satisfies the configured item rule.
        /// </summary>
        /// <remarks>Validation fails when no items satisfy the rule, including when the collection is empty.</remarks>
        /// <returns>An <see cref="ValidationTarget{T, TItem}"/> that represents an any-match validation over the collection.</returns>
        public ValidationTarget<T, TItem> Any()
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
                return RuleResult.Failure("Any", "No items in the collection satisfied the specified rule.");
            });
        }

        /// <summary>
        /// Creates an <see cref="EnsureTarget{T, TProperty}"/> that ensures at least one item in the collection satisfies the specified rule.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property to apply the rule to.</typeparam>
        /// <param name="propertySelector">A function to select the property from each item in the collection.</param>
        /// <returns>A new <see cref="ValidationTarget{T, TProperty}"/> that ensures at least one item in the collection satisfies the specified rule.</returns>
        public ValidationTarget<T, TProperty> Any<TProperty>(Func<TItem, TProperty> propertySelector)
        {
            ArgumentNullException.ThrowIfNull(propertySelector);

            return target.Compose<TProperty>((items, rule) =>
            {
                foreach (var item in items)
                {
                    if (rule(propertySelector(item)).IsValid)
                    {
                        return RuleResult.Success();
                    }
                }

                return RuleResult.Failure("Any", "No items in the collection satisfied the specified rule.");
            });
        }
    }
}
