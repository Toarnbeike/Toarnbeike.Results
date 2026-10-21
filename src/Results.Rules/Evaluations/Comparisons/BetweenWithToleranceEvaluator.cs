using System.Numerics;
using Toarnbeike.Results.Rules.Tolerance;

namespace Toarnbeike.Results.Rules.Evaluations.Comparisons;

/// <summary>
/// Checks if a given value is between a specified minimum and maximum value (inclusive), considering a tolerance.
/// </summary>
/// <typeparam name="TValue">The type of the value being compared.</typeparam>
/// <param name="min">The minimum value to compare against.</param>
/// <param name="max">The maximum value to compare against.</param>
/// <param name="tolerance">The tolerance to consider when comparing values. If no tolerance is provided, a default tolerance for the type from the rule context will be used.</param>
internal class BetweenWithToleranceEvaluator<TValue>(TValue min, TValue max, TValue? tolerance) : IEvaluation<TValue>
     where TValue : struct, INumber<TValue>, IMinMaxValue<TValue>
{
    public EvaluationResult Evaluate(TValue value)
    {
        tolerance ??= RuleContext.Tolerance<TValue>();
        ArgumentOutOfRangeException.ThrowIfNegative(tolerance.Value);

        var adjustedMin = ToleranceMath<TValue>.GetAdjustedMin(min, tolerance.Value);
        var adjustedMax = ToleranceMath<TValue>.GetAdjustedMax(max, tolerance.Value);

        return EvaluationResult.Create(
            attemptedValue: value,
            isValid: value.CompareTo(adjustedMin) >= 0 && value.CompareTo(adjustedMax) <= 0,
            evaluatorName: "BetweenWithToleranceEvaluator",
            details: [
                ("type", typeof(TValue).Name),
                ("min", min),
                ("max", max),
                ("tolerance", tolerance.Value),
                ("adjustedMin", adjustedMin),
                ("adjustedMax", adjustedMax)
                ]);
    }
}
