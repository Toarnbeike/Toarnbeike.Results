using System.Numerics;
using Toarnbeike.Results.Rules.Tolerance;

namespace Toarnbeike.Results.Rules.Evaluations.Comparisons;

/// <summary>
/// Checks if a given value is equal to a specified target value, considering a tolerance.
/// </summary>
/// <typeparam name="TValue">The type of the value being compared.</typeparam>
/// <param name="target">The target value to compare against.</param>
/// <param name="tolerance">The tolerance to consider when comparing values. If no tolerance is provided, a default tolerance for the type from the rule context will be used.</param>
internal class EqualToWithToleranceEvaluator<TValue>(TValue target, TValue? tolerance) : IEvaluation<TValue>
     where TValue : struct, INumber<TValue>, IMinMaxValue<TValue>
{
    public EvaluationResult Evaluate(TValue value)
    {
        tolerance ??= RuleContext.Tolerance<TValue>();
        ArgumentOutOfRangeException.ThrowIfNegative(tolerance.Value);

        var adjustedMin = ToleranceMath<TValue>.GetAdjustedMin(target, tolerance.Value);
        var adjustedMax = ToleranceMath<TValue>.GetAdjustedMax(target, tolerance.Value);
        
        return EvaluationResult.Create(
            attemptedValue: value,
            isValid: value.CompareTo(adjustedMin) >= 0 && value.CompareTo(adjustedMax) <= 0,
            evaluatorName: "EqualToWithToleranceEvaluator",
            details: [
                ("type", typeof(TValue).Name),
                ("target", target),
                ("tolerance", tolerance.Value),
                ("adjustedMin", adjustedMin),
                ("adjustedMax", adjustedMax)
                ]);
    }
}
