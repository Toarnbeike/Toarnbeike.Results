using Toarnbeike.Results.Guards.Core;

namespace Toarnbeike.Results.Guards;

public interface IGuardRuleResult
{
    /// <summary>
    /// Gets the captured caller argument expression for the guarded value.
    /// </summary>
    internal string CapturedExpression { get; }

    internal RuleContext RuleContext { get; }
    
    internal bool IsValid { get; }

    internal string? CustomMessage { get; set; }
    internal string? CustomExpression { get; set; }
}

public interface IGuardRuleResult<out T> : IGuardRuleResult, IGuardTarget<T>, IGuardContext
{
    internal IGuardRuleResult<T> WithCustomMessage(string? message);
    internal IGuardRuleResult<T> WithCustomExpression(string? expression);

    string IGuardRuleResult.CapturedExpression => Expression;
}