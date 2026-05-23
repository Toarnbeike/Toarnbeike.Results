namespace Toarnbeike.Results.Ensure.Implementation.RuleResults;

internal interface IFailingRuleResult
{
    string? CustomMessage { get; }
    string ArgumentName { get; }
    RuleContext Context { get; }
    string GuardName { get; }
    object? AttemptedValueAsObject { get; }
}