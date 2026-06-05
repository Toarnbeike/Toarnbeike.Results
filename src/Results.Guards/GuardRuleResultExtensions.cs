using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Guards.Core;

namespace Toarnbeike.Results.Guards;

public static class GuardRuleResultExtensions
{
    extension<T>(IGuardRuleResult<T> ruleResult)
    {
        /// <summary>
        /// Finalizes the current guard pipeline and converts it into a <see cref="Result"/>.
        /// </summary>
        /// <returns>
        /// A Success result when no failures occured, or, depending on the context either
        /// a <see cref="GuardFailure"/> representing the first failure (Ensure, Fail fast) or
        /// a <see cref="ValidationFailureSummary"/> representing all failures (Validate, Accumulate).
        /// </returns>
        public Result<T> ToResult()
        {
            return ruleResult.GuardContext.ToResult(ruleResult.Value);
        }

        /// <summary>
        /// Finalizes the current guard pipeline and converts it into a <see cref="Result"/>.
        /// </summary>
        /// <param name="value">The value to convert into a result.</param>
        /// <returns>
        /// A Success result when no failures occured, or, depending on the context either
        /// a <see cref="GuardFailure"/> representing the first failure (Ensure, Fail fast) or
        /// a <see cref="ValidationFailureSummary"/> representing all failures (Validate, Accumulate).
        /// </returns>
        public Result<TOther> ToResult<TOther>(TOther value)
        {
            return ruleResult.GuardContext.ToResult(value);
        }

        /// <summary>
        /// Overrides the generated failure message.
        /// </summary>
        /// <param name="message">The custom failure message.</param>
        /// <returns>The updated guard rule result.</returns>
        public IGuardRuleResult<T> WithMessage(FormattableString message)
        {
            return ruleResult.WithCustomMessage(message.ToString(ruleResult.GuardContext.FailureMessageProvider.Culture));
        }

        /// <summary>
        /// Overrides the generated failure message using a custom message factory.
        /// </summary>
        /// <param name="messageBuilder">
        /// A delegate that creates the failure message based on the argument name and guard constraint.
        /// </param>
        /// <returns>The updated guard rule result.</returns>
        public IGuardRuleResult<T> WithMessage(Func<string, RuleContext, FormattableString> messageBuilder)
        {
            var message = messageBuilder.Invoke(ruleResult.Expression, ruleResult.RuleContext);
            return ruleResult.WithMessage(message);
        }

        /// <summary>
        /// Overrides the captured expression used in generated failures.
        /// </summary>
        /// <param name="expression">The custom argument name.</param>
        /// <returns>The updated guard rule result.</returns>
        public IGuardRuleResult<T> WithExpression(string expression)
        {
            return ruleResult.WithCustomExpression(expression);
        }
    }

    extension<T>(Task<IGuardRuleResult<T>> ruleResultTask)
    {
        /// <inheritdoc cref="IGuardContext.ToResult"/>
        public async Task<Result> ToResult() =>
            (await ruleResultTask).ToResult();

        /// <inheritdoc cref="IGuardContext.ToResult{T}"/>
        public async Task<Result<T>> ToResult(T value) =>
            (await ruleResultTask).ToResult(value);

        /// <inheritdoc cref="WithMessage{T}(IGuardRuleResult{T}, FormattableString)"/>
        public async Task<IGuardRuleResult<T>> WithMessage(FormattableString message) =>
            (await ruleResultTask).WithMessage(message);

        /// <inheritdoc cref="WithMessage{T}(IGuardRuleResult{T}, Func{string, RuleContext, FormattableString})"/>
        public async Task<IGuardRuleResult<T>> WithMessage(Func<string, RuleContext, FormattableString> messageBuilder) =>
            (await ruleResultTask).WithMessage(messageBuilder);

        /// <inheritdoc cref="WithExpression{T}(IGuardRuleResult{T}, string)"/>
        public async Task<IGuardRuleResult<T>> WithExpression(string? name) =>
            (await ruleResultTask).WithCustomExpression(name);

        /// <inheritdoc cref="IGuardRuleResult{T}.WithCustomMessage(string?)"/>
        public async Task<IGuardRuleResult<T>> WithCustomMessage(string? message) =>
            (await ruleResultTask).WithCustomMessage(message);

        /// <inheritdoc cref="IGuardRuleResult{T}.WithCustomExpression(string?)"/>
        public async Task<IGuardRuleResult<T>> WithCustomExpression(string? name) =>
            (await ruleResultTask).WithCustomExpression(name);
    }
}