using System.Numerics;
using Toarnbeike.Results.Rules.Evaluations.Comparisons;
using Toarnbeike.Results.Rules.Evaluations.Numbers;

namespace Toarnbeike.Results.Rules;

public static class FloatingPointRules
{
    /// <summary>
    /// Verifies that the floating point value is at least the specified minimum value, with an optional tolerance.
    /// </summary>
    /// <typeparam name="TFloat">The type of the floating point value.</typeparam>
    /// <param name="min">The minimum floating point value.</param>
    /// <param name="tolerance">The optional tolerance value.</param>
    /// <returns>A rule delegate that evaluates the floating point value.</returns>
    public static RuleDelegate<TFloat> AtLeast<TFloat>(TFloat min, TFloat? tolerance)
        where TFloat : struct, IFloatingPoint<TFloat>, IMinMaxValue<TFloat> => value =>
        RuleResult.Create("Comparison.AtLeastWithTolerance", value, new AtLeastWithToleranceEvaluator<TFloat>(min, tolerance));

    /// <summary>
    /// Verifies that the floating point value is at most the specified maximum value, with an optional tolerance.
    /// </summary>
    /// <typeparam name="TFloat">The type of the floating point value.</typeparam>
    /// <param name="max">The maximum floating point value.</param>
    /// <param name="tolerance">The optional tolerance value.</param>
    /// <returns>A rule delegate that evaluates the floating point value.</returns>
    public static RuleDelegate<TFloat> AtMost<TFloat>(TFloat max, TFloat? tolerance)
        where TFloat : struct, IFloatingPoint<TFloat>, IMinMaxValue<TFloat> => value =>
        RuleResult.Create("Comparison.AtMostWithTolerance", value, new AtMostWithToleranceEvaluator<TFloat>(max, tolerance));

    /// <summary>
    /// Verifies that the floating point value is between the specified minimum and maximum values, with an optional tolerance.
    /// </summary>
    /// <typeparam name="TFloat">The type of the floating point value.</typeparam>
    /// <param name="min">The minimum floating point value.</param>
    /// <param name="max">The maximum floating point value.</param>
    /// <param name="tolerance">The optional tolerance value.</param>
    /// <returns>A rule delegate that evaluates the floating point value.</returns>
    public static RuleDelegate<TFloat> Between<TFloat>(TFloat min, TFloat max, TFloat? tolerance)
        where TFloat : struct, IFloatingPoint<TFloat>, IMinMaxValue<TFloat> => value =>
        RuleResult.Create("Comparison.BetweenWithTolerance", value, new BetweenWithToleranceEvaluator<TFloat>(min, max, tolerance));

    /// <summary>
    /// Verifies that the floating point value is equal to the specified target value, with an optional tolerance.
    /// </summary>
    /// <typeparam name="TFloat">The type of the floating point value.</typeparam>
    /// <param name="target">The target floating point value.</param>
    /// <param name="tolerance">The optional tolerance value.</param>
    /// <returns>A rule delegate that evaluates the floating point value.</returns>
    public static RuleDelegate<TFloat> EqualTo<TFloat>(TFloat target, TFloat? tolerance)
        where TFloat : struct, IFloatingPoint<TFloat>, IMinMaxValue<TFloat> => value =>
        RuleResult.Create("Comparison.EqualToWithTolerance", value, new EqualToWithToleranceEvaluator<TFloat>(target, tolerance));

    /// <summary>
    /// Verifies that the floating point value is not equal to the specified target value.
    /// </summary>
    /// <typeparam name="TFloat">The type of the floating point value.</typeparam>
    /// <param name="target">The target floating point value.</param>
    /// <param name="tolerance">The optional tolerance value.</param>
    /// <returns>A rule delegate that evaluates the floating point value.</returns>
    public static RuleDelegate<TFloat> NotEqualTo<TFloat>(TFloat target, TFloat? tolerance)
        where TFloat : struct, IFloatingPoint<TFloat>, IMinMaxValue<TFloat> => value =>
        RuleResult.Create("Comparison.NotEqualToWithTolerance", value, new NotEqualToWithToleranceEvaluator<TFloat>(target, tolerance));

    /// <summary>
    /// Verifies that the floating point value is a multiple of the specified factor, with an optional tolerance.
    /// </summary>
    /// <typeparam name="TFloat">The type of the floating point value.</typeparam>
    /// <param name="factor">The factor to check for multiples of.</param>
    /// <param name="tolerance">The optional tolerance value.</param>
    /// <returns>A rule delegate that evaluates the floating point value.</returns>
    public static RuleDelegate<TFloat> MultipleOf<TFloat>(TFloat factor, TFloat? tolerance)
        where TFloat : struct, IFloatingPoint<TFloat>, IMinMaxValue<TFloat> => value =>
        RuleResult.Create("Float.MultipleOfWithTolerance", value, new MultipleOfWithToleranceEvaluator<TFloat>(factor, tolerance));

    /// <summary>
    /// Verifies that the floating point value is finite.
    /// </summary>
    /// <typeparam name="TFloat">The type of the floating point value.</typeparam>
    /// <returns>A rule delegate that evaluates the floating point value.</returns>
    public static RuleDelegate<TFloat> Finite<TFloat>()
        where TFloat : struct, IFloatingPoint<TFloat>, IMinMaxValue<TFloat> => value =>
        RuleResult.Create("Float.Finite", value, new FiniteEvaluator<TFloat>());
}

