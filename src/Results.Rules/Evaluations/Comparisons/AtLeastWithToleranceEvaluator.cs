using System.Numerics;
using Toarnbeike.Results.Rules.Tolerance;

namespace Toarnbeike.Results.Rules.Evaluations.Comparisons;

/// <summary>
/// Checks if a given value is at least a specified minimum value, considering a tolerance.
/// </summary>
/// <typeparam name="TValue">The type of the value being compared.</typeparam>
/// <param name="min">The minimum value to compare against.</param>
/// <param name="tolerance">The tolerance to consider when comparing values. If no tolerance is provided, a default tolerance for the type from the rule context will be used.</param>
internal class AtLeastWithToleranceEvaluator<TValue>(TValue min, TValue? tolerance) : IEvaluation<TValue>
     where TValue : struct, INumber<TValue>, IMinMaxValue<TValue>
{
    public EvaluationResult Evaluate(TValue value)
    {
        tolerance ??= RuleContext.Tolerance<TValue>();
        ArgumentOutOfRangeException.ThrowIfNegative(tolerance.Value);

        var adjustedMin = ToleranceMath<TValue>.GetAdjustedMin(min, tolerance.Value);

        return EvaluationResult.Create(
            attemptedValue: value,
            isValid: value.CompareTo(adjustedMin) >= 0,
            evaluatorName: "AtLeastWithToleranceEvaluator",
            details: [
                ("type", typeof(TValue).Name), 
                ("min", min), 
                ("tolerance", tolerance.Value), 
                ("adjustedMin", adjustedMin)
                ]);
    }
}