using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules;

internal static class RuleTargetFactory
{
    internal static RuleTarget<T, TProperty> Create<T, TProperty>(
        Result<T> result,
        Func<T, TProperty> propertySelector,
        string propertyName,
        Func<string, string, Failure> failureFactory)
    {
        ArgumentNullException.ThrowIfNull(propertySelector);
        ArgumentNullException.ThrowIfNull(failureFactory);

        var executor = new RuleExecutor<T, TProperty>((value, rule, message) =>
        {
            var ruleResult = rule(propertySelector(value));
            return MapResult(ruleResult, propertyName, message, failureFactory);
        });

        var asyncExecutor = new AsyncRuleExecutor<T, TProperty>(async (value, rule, message) =>
        {
            var ruleResult = await rule(propertySelector(value)).ConfigureAwait(false);
            return MapResult(ruleResult, propertyName, message, failureFactory);
        });

        return new RuleTarget<T, TProperty>(result, executor, asyncExecutor);
    }

    internal static Result MapResult(
        RuleResult ruleResult,
        string propertyName,
        string? message,
        Func<string, string, Failure> failureFactory) =>
        ruleResult.IsValid
            ? Result.Success()
            : Result.Failure(failureFactory(ruleResult.GetPropertyName(propertyName), message ?? ruleResult.Message));
}
