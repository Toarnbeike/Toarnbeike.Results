using System.Collections;
using Toarnbeike.Results.Guards.Core.Guards;

namespace Toarnbeike.Results.Guards.Rules;

public static class CollectionRules
{
    private const string RuleCategory = "Collection";

    private static int GetCount(IEnumerable collection)
    {
        return collection switch
        {
            null => 0,
            ICollection coll => coll.Count,
            _ => collection.Cast<object?>().ToArray().Length
        };
    }

    extension<TCollection>(IGuardTarget<TCollection> target) where TCollection : IEnumerable
    {
        /// <summary>
        /// Rule that the targeted collection must not be empty.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TCollection> NotEmpty() =>
            target.Evaluate(value =>
            {
                var count = GetCount(value);
                return new RuleEvaluation(
                    ComparisonGuards.GreaterThan(count, 0),
                    $"{RuleCategory}.{nameof(NotEmpty)}");
            });

        /// <summary>
        /// Rule that the targeted collection must be empty.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TCollection> Empty() =>
            target.Evaluate(value =>
            {
                var count = GetCount(value);
                return new RuleEvaluation(
                    ComparisonGuards.Equal(count, 0),
                    $"{RuleCategory}.{nameof(Empty)}",
                    ("Actual", count));
            });

        /// <summary>
        /// Rule that the targeted collection must have at least the specified number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements.</param>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TCollection> AtLeast(int min) =>
            target.Evaluate(value =>
            {
                var count = GetCount(value);
                return new RuleEvaluation(
                    ComparisonGuards.AtLeast(count, min),
                    $"{RuleCategory}.{nameof(AtLeast)}",
                    ("Actual", count),
                    ("Min", min));
            });

        /// <summary>
        /// Rule that the targeted collection must have at most the specified number of elements.
        /// </summary>
        /// <param name="max">The maximum number of elements.</param>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TCollection> AtMost(int max) =>
            target.Evaluate(value =>
            {
                var count = GetCount(value);
                return new RuleEvaluation(
                    ComparisonGuards.AtMost(count, max),
                    $"{RuleCategory}.{nameof(AtMost)}",
                    ("Actual", count),
                    ("Max", max));
            });

        /// <summary>
        /// Rule that the targeted collection must have at between the specified minimum and maximum number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements.</param>
        /// <param name="max">The maximum number of elements.</param>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TCollection> Between(int min, int max) =>
            target.Evaluate(value =>
            {
                var count = GetCount(value);
                return new RuleEvaluation(
                    ComparisonGuards.AtLeast(count, min) && ComparisonGuards.AtMost(count, max),
                    $"{RuleCategory}.{nameof(Between)}",
                    ("Actual", count),
                    ("Min", min),
                    ("Max", max));
            });

        /// <summary>
        /// Rule that the targeted collection must have at exactly the specified number of elements.
        /// </summary>
        /// <param name="expected">The exact number of elements.</param>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TCollection> Exactly(int expected) =>
            target.Evaluate(value =>
            {
                var count = GetCount(value);
                return new RuleEvaluation(
                    ComparisonGuards.Equal(count, expected),
                    $"{RuleCategory}.{nameof(Exactly)}",
                    ("Actual", count),
                    ("Expected", expected));
            });


        /// <summary>
        /// Rule that the targeted collection must have exactly one element.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TCollection> Single() =>
            target.Evaluate(value =>
            {
                var count = GetCount(value);
                return new RuleEvaluation(
                    ComparisonGuards.Equal(count, 1),
                    $"{RuleCategory}.{nameof(Single)}",
                    ("Actual", count));
            });
    }
}