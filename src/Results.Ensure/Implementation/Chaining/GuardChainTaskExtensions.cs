using Toarnbeike.Results.Ensure.Implementation.RuleResults;

namespace Toarnbeike.Results.Ensure.Implementation.Chaining;

public static class GuardChainTaskExtensions
{
    extension(Task<IGuardChain> chainTask)
    {
        /// <inheritdoc cref="IGuardChain.ToResult"/>
        public async Task<Result> ToResult() =>
            (await chainTask).ToResult();

        /// <inheritdoc cref="IGuardChain.ToResult{T}"/>
        public async Task<Result<T>> ToResult<T>(T value) =>
            (await chainTask).ToResult(value);
    }

    extension(Task<IGuardRuleResult> ruleResultTask)
    {
        /// <inheritdoc cref="IGuardChain.ToResult"/>
        public async Task<Result> ToResult() => 
            (await ruleResultTask).ToResult();

        /// <inheritdoc cref="IGuardChain.ToResult{T}"/>
        public async Task<Result<T>> ToResult<T>(T value) => 
            (await ruleResultTask).ToResult(value);
    }
}