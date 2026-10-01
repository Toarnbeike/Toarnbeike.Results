using Toarnbeike.Results.Guards.Core;
using Toarnbeike.Results.Guards.Core.Results;
using Toarnbeike.Results.Guards.Core.Targets;

namespace Toarnbeike.Results.Guards;

public static class CollectionGuardTargetExtensions
{
    extension<T>(ICollectionGuardTarget<T> target)
    {
        /// <summary>
        /// Evaluates a guard condition for the current collection value.
        /// </summary>
        /// <returns>
        /// A configurable rule result representing the outcome of the evaluation.
        /// </returns>
        public ICollectionGuardRuleResult<T> EvaluateCollection(Func<IGuardTarget<T>, IGuardRuleResult<T>> evaluation)
        {
            var collectionResult = new CollectionGuardRuleResult<T>(target.GuardContext, target.Expression, target.Values);
            for (var i = 0; i < target.Values.Count; i++)
            {
                var value = target.Values[i];
                var ruleResult = evaluation(new GuardTarget<T>(
                    target.GuardContext, value, target.Expression + $"[{i}]"));

                collectionResult.RuleSpecificResults.Add(ruleResult);
            }
            return collectionResult;
        }
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