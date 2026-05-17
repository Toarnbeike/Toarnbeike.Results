using System.Collections;
using Toarnbeike.Results.Ensure.Guards;
using Toarnbeike.Results.Ensure.Implementation.RuleResults;

namespace Toarnbeike.Results.Ensure.Rules;

public static class CollectionRules
{
    extension(IGuardTarget<ICollection> target)
    {
        /// <summary>
        /// Rule that the targeted collection must not be empty.
        /// </summary>
        public IGuardRuleResult NotEmpty() =>
            target.Evaluate(ComparisonGuards.GreaterThan(target.Value.Count, 0),
                $"{nameof(CollectionRules)}.{nameof(NotEmpty)}");

        /// <summary>
        /// Rule that the targeted collection must be empty.
        /// </summary>
        public IGuardRuleResult Empty() =>
            target.Evaluate(ComparisonGuards.Equal(target.Value.Count, 0),
                $"{nameof(CollectionRules)}.{nameof(Empty)}");

        /// <summary>
        /// Rule that the targeted collection must have at least the specified number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements.</param>
        public IGuardRuleResult AtLeast(int min) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value.Count, min),
                $"{nameof(CollectionRules)}.{nameof(AtLeast)}", min);

        /// <summary>
        /// Rule that the targeted collection must have at most the specified number of elements.
        /// </summary>
        /// <param name="max">The maximum number of elements.</param>
        public IGuardRuleResult AtMost(int max) =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value.Count, max),
                $"{nameof(CollectionRules)}.{nameof(AtMost)}", max);

        /// <summary>
        /// Rule that the targeted collection must have at between the specified minimum and maximum number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements.</param>
        /// <param name="max">The maximum number of elements.</param>
        public IGuardRuleResult Between(int min, int max) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value.Count, min) &&
                            ComparisonGuards.AtMost(target.Value.Count, max),
                $"{nameof(CollectionRules)}.{nameof(Between)}", (min, max));

        /// <summary>
        /// Rule that the targeted collection must have at exactly the specified number of elements.
        /// </summary>
        /// <param name="count">The exact number of elements.</param>
        public IGuardRuleResult Exactly(int count) =>
            target.Evaluate(ComparisonGuards.Equal(target.Value.Count, count),
                $"{nameof(CollectionRules)}.{nameof(Exactly)}", count);

        /// <summary>
        /// Rule that the targeted collection must have exactly one element.
        /// </summary>
        public IGuardRuleResult Single() =>
            target.Evaluate(ComparisonGuards.Equal(target.Value.Count, 1),
                $"{nameof(CollectionRules)}.{nameof(Single)}");
    }

    extension<TEnumerable>(IGuardTarget<TEnumerable> target) where TEnumerable : IEnumerable
    {
        /// <summary>
        /// Rule that the targeted collection must not be empty.
        /// </summary>
        public IGuardRuleResult NotEmpty() => target.Enumerated().NotEmpty();

        /// <summary>
        /// Rule that the targeted collection must be empty.
        /// </summary>
        public IGuardRuleResult Empty() => target.Enumerated().Empty();

        /// <summary>
        /// Rule that the targeted collection must have at least the specified number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements.</param>
        public IGuardRuleResult AtLeast(int min) => target.Enumerated().AtLeast(min);

        /// <summary>
        /// Rule that the targeted collection must have at most the specified number of elements.
        /// </summary>
        /// <param name="max">The maximum number of elements.</param>
        public IGuardRuleResult AtMost(int max) => target.Enumerated().AtMost(max);

        /// <summary>
        /// Rule that the targeted collection must have at between the specified minimum and maximum number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements.</param>
        /// <param name="max">The maximum number of elements.</param>
        public IGuardRuleResult Between(int min, int max) => target.Enumerated().Between(min, max);

        /// <summary>
        /// Rule that the targeted collection must have at exactly the specified number of elements.
        /// </summary>
        /// <param name="count">The exact number of elements.</param>
        public IGuardRuleResult Exactly(int count) => target.Enumerated().Exactly(count);

        /// <summary>
        /// Rule that the targeted collection must have exactly one element.
        /// </summary>
        public IGuardRuleResult Single() => target.Enumerated().Single();

        private IGuardTarget<ICollection> Enumerated() => target.As(t => t.Cast<object?>().ToArray());
    }
}