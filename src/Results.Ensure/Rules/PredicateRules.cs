namespace Toarnbeike.Results.Ensure.Rules;

public static class PredicateRules
{
    extension<T>(IGuardTarget<T> target)
    {
        /// <summary>
        /// Rule that the targeted value must satisfy the provided predicate.
        /// </summary>
        public IGuardRuleResult Satisfies(Func<T, bool> predicate) =>
            target.Evaluate(predicate(target.Value),
                $"{nameof(PredicateRules)}.{nameof(Satisfies)}",
                ("Predicate", predicate)
            );

        /// <summary>
        /// Rule that the targeted value must not satisfy the provided predicate.
        /// </summary>
        public IGuardRuleResult NotSatisfies(Func<T, bool> predicate) =>
            target.Evaluate(!predicate(target.Value),
                $"{nameof(PredicateRules)}.{nameof(NotSatisfies)}",
                ("Predicate", predicate)
            );

        /// <summary>
        /// Rule that the targeted value must satisfy the provided predicate.
        /// </summary>
        public async Task<IGuardRuleResult> SatisfiesAsync(Func<T, Task<bool>> predicate) =>
            target.Evaluate(await predicate(target.Value),
                $"{nameof(PredicateRules)}.{nameof(Satisfies)}",
                ("Predicate", predicate)
            );

        /// <summary>
        /// Rule that the targeted value must not satisfy the provided predicate.
        /// </summary>
        public async Task<IGuardRuleResult> NotSatisfiesAsync(Func<T, Task<bool>> predicate) =>
            target.Evaluate(!await predicate(target.Value),
                $"{nameof(PredicateRules)}.{nameof(NotSatisfies)}",
                ("Predicate", predicate)
            );
    }
}