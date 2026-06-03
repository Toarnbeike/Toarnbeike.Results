using Toarnbeike.Results.Guards.Implementations;
using Toarnbeike.Results.Guards.Implementations.Results;

namespace Toarnbeike.Results.Guards;

public static class GuardTargetExtensions
{
    extension<T>(IGuardTarget<T> target)
    {
        public IGuardRuleResult<T> SkipEvaluation()
        {
            var ruleContext = new RuleContext("Skipped", target.Value);
            return new GuardRuleResult<T>(target.GuardContext, target.Expression, true, target.Value, ruleContext);
        }

        /// <summary>
        /// Evaluates a guard condition for the current value.
        /// </summary>
        /// <param name="isValid"> Indicates whether the guard condition succeeded. </param>
        /// <param name="guardName"> The name of the guard that performed the evaluation. </param>
        /// <param name="context"> Optional rule context associated with the guard. </param>
        /// <returns>
        /// A configurable rule result representing the outcome of the evaluation.
        /// </returns>
        public IGuardRuleResult<T> Evaluate(bool isValid, string guardName,
            params (string key, object? value)[] context)
        {
            var ruleContext = new RuleContext(guardName, target.Value, context);
            var result = new GuardRuleResult<T>(target.GuardContext, target.Expression, isValid, target.Value,
                ruleContext);
            target.GuardContext.Results.Add(result);
            return result;
        }

        /// <summary>
        /// Evaluates a guard condition for the current value.
        /// </summary>
        /// <param name="newValue"> The new value for the guard condition. </param>
        /// <param name="isValid"> Indicates whether the guard condition succeeded. </param>
        /// <param name="guardName"> The name of the guard that performed the evaluation. </param>
        /// <param name="context"> Optional rule context associated with the guard. </param>
        /// <returns>
        /// A configurable rule result representing the outcome of the evaluation.
        /// </returns>
        public IGuardRuleResult<TOut> EvaluateAs<TOut>(TOut newValue, bool isValid, string guardName,
            params (string key, object? value)[] context)
        {
            var ruleContext = new RuleContext(guardName, target.Value, context);
            var result =
                new GuardRuleResult<TOut>(target.GuardContext, target.Expression, isValid, newValue, ruleContext);
            target.GuardContext.Results.Add(result);
            return result;
        }
    }
}