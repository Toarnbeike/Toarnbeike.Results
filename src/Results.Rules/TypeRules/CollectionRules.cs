using Toarnbeike.Results.Rules.Evaluations.Collections;
using Toarnbeike.Results.Rules.Evaluations.Comparisons;

namespace Toarnbeike.Results.Rules.TypeRules;

public static class CollectionRules
{
    /// <summary>
    /// Verifies that the collection contains the expected item.
    /// </summary>
    /// <typeparam name="TItem">The type of the items in the collection.</typeparam>
    /// <param name="expected">The item that is expected to be in the collection.</param>
    /// <returns>A RuleDelegate that evaluates the Collection value.</returns>
    public static RuleDelegate<IEnumerable<TItem>> Contains<TItem>(TItem expected) => value =>
        RuleResult.Create("Collection.Contains", value, new ContainsEvaluator<TItem>(expected));

    /// <summary>
    /// Verifies that the collection does not contain the unexpected item.
    /// </summary>
    /// <typeparam name="TItem">The type of the items in the collection.</typeparam>
    /// <param name="unexpected">The item that is unexpected to be in the collection.</param>
    /// <returns>A RuleDelegate that evaluates the Collection value.</returns>
    public static RuleDelegate<IEnumerable<TItem>> NotContains<TItem>(TItem unexpected) => value =>
        RuleResult.Create("Collection.NotContains", value, new DoesNotContainEvaluator<TItem>(unexpected));

    /// <summary>
    /// Verifies that the collection has the expected number of items.
    /// </summary>
    /// <typeparam name="TItem">The type of the items in the collection.</typeparam>
    /// <param name="expectedCount">The expected number of items in the collection.</param>
    /// <returns>A RuleDelegate that evaluates the Collection value.</returns>
    public static RuleDelegate<IEnumerable<TItem>> HasExactly<TItem>(int expectedCount) => value =>
        RuleResult.Create("Collection.HasExactly", value, value => value.Count(), new EqualToEvaluator<int>(expectedCount));

    /// <summary>
    /// Verifies that the collection has at least the specified number of items.
    /// </summary>
    /// <typeparam name="TItem">The type of the items in the collection.</typeparam>
    /// <param name="minimumCount">The minimum number of items that the collection should have.</param>
    /// <returns>A RuleDelegate that evaluates the Collection value.</returns>
    public static RuleDelegate<IEnumerable<TItem>> HasAtLeast<TItem>(int minimumCount) => value =>
        RuleResult.Create("Collection.HasAtLeast", value, value => value.Count(), new AtLeastEvaluator<int>(minimumCount));

    /// <summary>
    /// Verifies that the collection has at most the specified number of items.
    /// </summary>
    /// <typeparam name="TItem">The type of the items in the collection.</typeparam>
    /// <param name="maximumCount">The maximum number of items that the collection should have.</param>
    /// <returns>A RuleDelegate that evaluates the Collection value.</returns>
    public static RuleDelegate<IEnumerable<TItem>> HasAtMost<TItem>(int maximumCount) => value =>
        RuleResult.Create("Collection.HasAtMost", value, value => value.Count(), new AtMostEvaluator<int>(maximumCount));

    /// <summary>
    /// Verifies that the collection has a number of items between the specified minimum and maximum counts.
    /// </summary>
    /// <typeparam name="TItem">The type of the items in the collection.</typeparam>
    /// <param name="minimumCount">The minimum number of items that the collection should have.</param>
    /// <param name="maximumCount">The maximum number of items that the collection should have.</param>
    /// <returns>A RuleDelegate that evaluates the Collection value.</returns>
    public static RuleDelegate<IEnumerable<TItem>> HasBetween<TItem>(int minimumCount, int maximumCount) => value =>
        RuleResult.Create("Collection.HasBetween", value, value => value.Count(), new BetweenEvaluator<int>(minimumCount, maximumCount));
}