using System.Collections;
using Toarnbeike.Results.Guards.Implementations.Guards;

namespace Toarnbeike.Results.Guards.Rules;

public static class CollectionRules
{
    private const string RuleCategory = "Collection";

    extension<TCollection>(IGuardTarget<TCollection> target) where TCollection : IEnumerable
    {
        /// <summary>
        /// Rule that the targeted collection must not be empty.
        /// </summary>
        public IGuardRuleResult<TCollection> NotEmpty()
        {
            var actualCount = target.GetCount();
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.GreaterThan(actualCount, 0),
                    $"{RuleCategory}.{nameof(NotEmpty)}");
        }

        /// <summary>
        /// Rule that the targeted collection must be empty.
        /// </summary>
        public IGuardRuleResult<TCollection> Empty()
        {
            var actualCount = target.GetCount();
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.Equal(actualCount, 0),
                    $"{RuleCategory}.{nameof(Empty)}", 
                    ("Actual", actualCount));
        }

        /// <summary>
        /// Rule that the targeted collection must have at least the specified number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements.</param>
        public IGuardRuleResult<TCollection> AtLeast(int min)
        {
            var actualCount = target.GetCount();
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.AtLeast(actualCount, min),
                    $"{RuleCategory}.{nameof(AtLeast)}",
                    ("Actual", actualCount), ("Min", min));
        }

        /// <summary>
        /// Rule that the targeted collection must have at most the specified number of elements.
        /// </summary>
        /// <param name="max">The maximum number of elements.</param>
        public IGuardRuleResult<TCollection> AtMost(int max)
        {
            var actualCount = target.GetCount();
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.AtMost(actualCount, max),
                    $"{RuleCategory}.{nameof(AtMost)}",
                    ("Actual", actualCount), ("Max", max));
        }

        /// <summary>
        /// Rule that the targeted collection must have at between the specified minimum and maximum number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements.</param>
        /// <param name="max">The maximum number of elements.</param>
        public IGuardRuleResult<TCollection> Between(int min, int max)
        {
            var actualCount = target.GetCount();
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.AtLeast(actualCount, min) &&
                                   ComparisonGuards.AtMost(actualCount, max),
                    $"{RuleCategory}.{nameof(Between)}",
                    ("Actual", actualCount), ("Min", min), ("Max", max));
        }

        /// <summary>
        /// Rule that the targeted collection must have at exactly the specified number of elements.
        /// </summary>
        /// <param name="expected">The exact number of elements.</param>
        public IGuardRuleResult<TCollection> Exactly(int expected)
        {
            var actualCount = target.GetCount();
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.Equal(actualCount, expected),
                    $"{RuleCategory}.{nameof(Exactly)}",
                    ("Actual", actualCount),
                    ("Expected", expected));
        }

        /// <summary>
        /// Rule that the targeted collection must have exactly one element.
        /// </summary>
        public IGuardRuleResult<TCollection> Single()
        {
            var actualCount = target.GetCount();
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.Equal(actualCount, 1),
                    $"{RuleCategory}.{nameof(Single)}",
                    ("Actual", actualCount));
        }

        private int GetCount()
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            // Justification: Null collection can be created from the ShortCircuitingTarget.
            if (target.Value is null)
                return 0;
            if (target.Value is ICollection collection)
            {
                return collection.Count;
            }

            return target.Value.Cast<object?>().ToArray().Length;
        }
    }
}