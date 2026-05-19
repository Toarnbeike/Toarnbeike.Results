namespace Toarnbeike.Results.Ensure.Implementation.RuleResults;

internal interface IFailingRuleResult
{
    string Message { get; }
    string ArgumentName { get; }
    RuleContext Context { get; }
    string GuardName { get; }
    object? AttemptedValueAsObject { get; }
}