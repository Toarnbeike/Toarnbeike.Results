using Toarnbeike.Results.Guards.Core;
using Toarnbeike.Results.Guards.Core.Results;

namespace Toarnbeike.Results.Guards;

/// <param name="isValid"> Indicates whether the guard condition succeeded. </param>
/// <param name="guardName"> The name of the guard that performed the evaluation. </param>
/// <param name="context"> Optional rule context associated with the guard. </param>
public class RuleEvaluation(bool isValid, string guardName, params (string key, object? value)[] context)
{
    public bool IsValid => isValid;
    public string GuardName => guardName;
    public (string key, object? value)[] Context => context;

    public static async Task<RuleEvaluation> FromTask(Task<bool> isValidTask, string guardName, params (string key, object? value)[] context)
    {
        var isValid = await isValidTask;
        return new RuleEvaluation(isValid, guardName, context);
    }

    public static async Task<RuleEvaluation> FromTaskInverted(Task<bool> isInvalidTask, string guardName, params (string key, object? value)[] context)
    {
        var isValid = !await isInvalidTask;
        return new RuleEvaluation(isValid, guardName, context);
    }
}

public static class GuardTargetExtensions
{
    extension<T>(IGuardTarget<T> target)
    {
        /// <summary>
        /// Evaluates a guard condition for the current value.
        /// </summary>
        /// <returns>
        /// A configurable rule result representing the outcome of the evaluation.
        /// </returns>
        public IGuardRuleResult<T> Evaluate(Func<T, RuleEvaluation> evaluation)
        {
            if (!target.GuardContext.ShouldContinueExecution)
            {
                return new GuardRuleResult<T>(target.GuardContext, target.Expression, true, target.Value,
                    new RuleContext("Skipped", target.Value));
            }

            var ruleEvaluation = evaluation(target.Value);
            var ruleContext = new RuleContext(ruleEvaluation.GuardName, target.Value, ruleEvaluation.Context);
            var result = new GuardRuleResult<T>(target.GuardContext, target.Expression, ruleEvaluation.IsValid, target.Value, ruleContext);
            target.GuardContext.Results.Add(result);
            return result;
        }

        public async Task<IGuardRuleResult<T>> EvaluateAsync(Func<T, Task<RuleEvaluation>> evaluation)
        {
            if (!target.GuardContext.ShouldContinueExecution)
            {
                return new GuardRuleResult<T>(target.GuardContext, target.Expression, true, target.Value,
                    new RuleContext("Skipped", target.Value));
            }

            var ruleEvaluation = await evaluation(target.Value);
            var ruleContext = new RuleContext(ruleEvaluation.GuardName, target.Value, ruleEvaluation.Context);
            var result = new GuardRuleResult<T>(target.GuardContext, target.Expression, ruleEvaluation.IsValid, target.Value, ruleContext);
            target.GuardContext.Results.Add(result);
            return result;
        }

        /// <summary>
        /// Evaluates a guard condition for the current value, and convert to a new type after successful evaluation.
        /// </summary>
        /// <returns>
        /// A configurable rule result representing the outcome of the evaluation.
        /// </returns>
        public IGuardRuleResult<TNew> EvaluateThenConvert<TNew>(Func<T, RuleEvaluation> evaluation, Func<T, TNew> conversion)
        {
            if (!target.GuardContext.ShouldContinueExecution)
            {
                return new GuardRuleResult<TNew>(target.GuardContext, target.Expression, true, default!,
                    new RuleContext("Skipped", target.Value));
            }

            var ruleEvaluation = evaluation(target.Value);
            var ruleContext = new RuleContext(ruleEvaluation.GuardName, target.Value, ruleEvaluation.Context);
            var result = new GuardRuleResult<TNew>(target.GuardContext, target.Expression, ruleEvaluation.IsValid, conversion(target.Value), ruleContext);
            target.GuardContext.Results.Add(result);
            return result;
        }
    }
}