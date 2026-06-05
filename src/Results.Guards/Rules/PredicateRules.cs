namespace Toarnbeike.Results.Guards.Rules;

public static class PredicateRules
{
    private const string RuleCategory = "Predicate";

    extension<T>(IGuardTarget<T> target)
    {
        /// <summary>
        /// Rule that the targeted value must satisfy the provided predicate.
        /// </summary>
        public IGuardRuleResult<T> Satisfies(Func<T, bool> predicate) =>
            target.Evaluate(value => 
                new RuleEvaluation(
                    predicate(value),
                    $"{RuleCategory}.{nameof(Satisfies)}",
                    ("Predicate", predicate)));

        /// <summary>
        /// Rule that the targeted value must not satisfy the provided predicate.
        /// </summary>
        public IGuardRuleResult<T> NotSatisfies(Func<T, bool> predicate) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    !predicate(value),
                    $"{RuleCategory}.{nameof(NotSatisfies)}",
                    ("Predicate", predicate)));

        /// <summary>
        /// Rule that the targeted value must satisfy the provided predicate.
        /// </summary>
        public Task<IGuardRuleResult<T>> SatisfiesAsync(Func<T, Task<bool>> predicate) =>
            target.EvaluateAsync(value => 
                RuleEvaluation.FromTask(
                    predicate(value),
                    $"{RuleCategory}.{nameof(Satisfies)}",
                    ("Predicate", predicate)));

        /// <summary>
        /// Rule that the targeted value must not satisfy the provided predicate.
        /// </summary>
        public Task<IGuardRuleResult<T>> NotSatisfiesAsync(Func<T, Task<bool>> predicate) =>
            target.EvaluateAsync(value =>
                RuleEvaluation.FromTaskInverted(
                    predicate(value),
                    $"{RuleCategory}.{nameof(NotSatisfies)}",
                    ("Predicate", predicate)));
    }
}