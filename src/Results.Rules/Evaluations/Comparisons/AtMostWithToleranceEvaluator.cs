using System.Numerics;
using Toarnbeike.Results.Rules.Tolerance;

namespace Toarnbeike.Results.Rules.Evaluations.Comparisons;

/// <summary>
/// Checks if a given value is at most a specified maximum value, considering a tolerance.
/// </summary>
/// <typeparam name="TValue">The type of the value being compared.</typeparam>
/// <param name="max">The maximum value to compare against.</param>
/// <param name="tolerance">The tolerance to consider when comparing values. If no tolerance is provided, a default tolerance for the type from the rule context will be used.</param>
internal class AtMostWithToleranceEvaluator<TValue>(TValue max, TValue? tolerance) : IEvaluation<TValue>
     where TValue : struct, INumber<TValue>, IMinMaxValue<TValue>
{
    public EvaluationResult Evaluate(TValue value)
    {
        tolerance ??= RuleContext.Tolerance<TValue>();
        ArgumentOutOfRangeException.ThrowIfNegative(tolerance.Value);
        var adjustedMax = ToleranceMath<TValue>.GetAdjustedMax(max, tolerance.Value);

        return EvaluationResult.Create(
            attemptedValue: value,
            isValid: value.CompareTo(adjustedMax) <= 0,
            evaluatorName: "AtMostWithToleranceEvaluator",
            details: [
                ("type", typeof(TValue).Name),
                ("max", max),
                ("tolerance", tolerance.Value),
                ("adjustedMax", adjustedMax)
                ]);
    }
}
