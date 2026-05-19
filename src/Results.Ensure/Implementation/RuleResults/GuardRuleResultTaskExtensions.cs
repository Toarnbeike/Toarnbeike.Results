namespace Toarnbeike.Results.Ensure.Implementation.RuleResults;

public static class GuardRuleResultTaskExtensions
{
    extension(Task<IGuardRuleResult> resultTask)
    {
        /// <inheritdoc cref="IGuardRuleResult.WithMessage(string)"/>
        public async Task<IGuardRuleResult> WithMessage(string? message) => 
            (await resultTask).WithMessage(message);

        /// <inheritdoc cref="IGuardRuleResult.WithMessage(Func{string, RuleContext, string})"/>
        public async Task<IGuardRuleResult> WithMessage(Func<string, RuleContext, string> messageBuilder) => 
            (await resultTask).WithMessage(messageBuilder);

        /// <inheritdoc cref="IGuardRuleResult.WithArgumentName(string)"/>
        public async Task<IGuardRuleResult> WithArgumentName(string? name) => 
            (await resultTask).WithArgumentName(name);
    }
}