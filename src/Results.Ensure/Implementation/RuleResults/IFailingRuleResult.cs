namespace Toarnbeike.Results.Ensure.Implementation.RuleResults;

internal interface IFailingRuleResult
{
    string Message { get; }
    string ArgumentName { get; }
    object? Constraint { get; }
    string GuardName { get; }
    object? AttemptedValueAsObject { get; }
}