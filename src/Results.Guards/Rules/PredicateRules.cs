namespace Toarnbeike.Results.Guards.Rules;

public static class PredicateRules
{
    private const string RuleCategory = "Predicate";

    extension<T>(IGuardTarget<T> target)
    {
        /// <summary>
        /// Rule that the targeted value must satisfy the provided predicate.
        /// </summary>
        public IGuardRuleResult<T> Satisfies(Func<T, bool> predicate)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(predicate(target.Value),
                    $"{RuleCategory}.{nameof(Satisfies)}",
                    ("Predicate", predicate)
                );
        }

        /// <summary>
        /// Rule that the targeted value must not satisfy the provided predicate.
        /// </summary>
        public IGuardRuleResult<T> NotSatisfies(Func<T, bool> predicate)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(!predicate(target.Value),
                    $"{RuleCategory}.{nameof(NotSatisfies)}",
                    ("Predicate", predicate)
                );
        }

        /// <summary>
        /// Rule that the targeted value must satisfy the provided predicate.
        /// </summary>
        public async Task<IGuardRuleResult<T>> SatisfiesAsync(Func<T, Task<bool>> predicate)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(await predicate(target.Value),
                    $"{RuleCategory}.{nameof(Satisfies)}",
                    ("Predicate", predicate)
                );
        }

        /// <summary>
        /// Rule that the targeted value must not satisfy the provided predicate.
        /// </summary>
        public async Task<IGuardRuleResult<T>> NotSatisfiesAsync(Func<T, Task<bool>> predicate)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(!await predicate(target.Value),
                    $"{RuleCategory}.{nameof(NotSatisfies)}",
                    ("Predicate", predicate)
                );
        }
    }
}