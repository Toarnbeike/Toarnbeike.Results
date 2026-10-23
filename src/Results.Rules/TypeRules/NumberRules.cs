using System.Numerics;
using Toarnbeike.Results.Rules.Evaluations.Comparisons;
using Toarnbeike.Results.Rules.Evaluations.Numbers;

namespace Toarnbeike.Results.Rules.TypeRules;

public static class NumberRules
{
    /// <summary>
    /// Verifies that the Number value is strictly greater than the specified minimum value.
    /// </summary>
    /// <typeparam name="TNumber">The type of the Number value.</typeparam>
    /// <param name="min">The minimum Number value.</param>
    /// <returns>A RuleDelegate that evaluates the Number value.</returns>
    public static RuleDelegate<TNumber> GreaterThan<TNumber>(TNumber min)
        where TNumber : INumber<TNumber> => value =>
        RuleResult.Create("Comparison.GreaterThan", value, new GreaterThanEvaluator<TNumber>(min));

    /// <summary>
    /// Verifies that the Number value is at least the specified minimum value.
    /// </summary>
    /// <typeparam name="TNumber">The type of the Number value.</typeparam>
    /// <param name="min">The minimum Number value.</param>
    /// <returns>A RuleDelegate that evaluates the Number value.</returns>
    public static RuleDelegate<TNumber> AtLeast<TNumber>(TNumber min)
        where TNumber : INumber<TNumber> => value =>
        RuleResult.Create("Comparison.AtLeast", value, new AtLeastEvaluator<TNumber>(min));

    /// <summary>
    /// Verifies that the Number value is at most the specified maximum value.
    /// </summary>
    /// <typeparam name="TNumber">The type of the Number value.</typeparam>
    /// <param name="max">The maximum Number value.</param>
    /// <returns>A RuleDelegate that evaluates the Number value.</returns>
    public static RuleDelegate<TNumber> AtMost<TNumber>(TNumber max)
        where TNumber : INumber<TNumber> => value =>
        RuleResult.Create("Comparison.AtMost", value, new AtMostEvaluator<TNumber>(max));

    /// <summary>
    /// Verifies that the Number value is strictly less than the specified maximum value.
    /// </summary>
    /// <typeparam name="TNumber">The type of the Number value.</typeparam>
    /// <param name="max">The maximum Number value.</param>
    /// <returns>A RuleDelegate that evaluates the Number value.</returns>
    public static RuleDelegate<TNumber> LessThan<TNumber>(TNumber max)
        where TNumber : INumber<TNumber> => value =>
        RuleResult.Create("Comparison.LessThan", value, new LessThanEvaluator<TNumber>(max));

    /// <summary>
    /// Verifies that the Number value is between the specified minimum and maximum values.
    /// </summary>
    /// <typeparam name="TNumber">The type of the Number value.</typeparam>
    /// <param name="min">The minimum Number value.</param>
    /// <param name="max">The maximum Number value.</param>
    /// <returns>A RuleDelegate that evaluates the Number value.</returns>
    public static RuleDelegate<TNumber> Between<TNumber>(TNumber min, TNumber max)
        where TNumber : INumber<TNumber> => value =>
        RuleResult.Create("Comparison.Between", value, new BetweenEvaluator<TNumber>(min, max));

    /// <summary>
    /// Verifies that the Number value is equal to the specified value.
    /// </summary>
    /// <typeparam name="TNumber">The type of the Number value.</typeparam>
    /// <param name="target">The value to compare against.</param>
    /// <returns>A RuleDelegate that evaluates the Number value.</returns>
    public static RuleDelegate<TNumber> EqualTo<TNumber>(TNumber target) 
        where TNumber : INumber<TNumber> => value =>
        RuleResult.Create("Comparison.EqualTo", value, new EqualToEvaluator<TNumber>(target));

    /// <summary>
    /// Verifies that the Number value is not equal to the specified value.
    /// </summary>
    /// <typeparam name="TNumber">The type of the Number value.</typeparam>
    /// <param name="target">The value to compare against.</param>
    /// <returns>A RuleDelegate that evaluates the Number value.</returns>
    public static RuleDelegate<TNumber> NotEqualTo<TNumber>(TNumber target)
        where TNumber : INumber<TNumber> => value =>
        RuleResult.Create("Comparison.NotEqualTo", value, new NotEqualToEvaluator<TNumber>(target));

    /// <summary>
    /// Verifies that the integer value is a multiple of the specified factor.
    /// </summary>
    /// <typeparam name="TInt">The type of the integer value.</typeparam>
    /// <param name="factor">The factor to check for multiples of.</param>
    /// <returns>A RuleDelegate that evaluates the integer value.</returns>
    public static RuleDelegate<TInt> MultipleOf<TInt>(TInt factor)
        where TInt : struct, IBinaryInteger<TInt> => value =>
        RuleResult.Create("Int.MultipleOf", value, new MultipleOfEvaluator<TInt>(factor));

    /// <summary>
    /// Verifies that the integer value is even.
    /// </summary>
    /// <typeparam name="TInt">The type of the integer value.</typeparam>
    /// <returns>A RuleDelegate that evaluates the integer value.</returns>
    public static RuleDelegate<TInt> Even<TInt>()
        where TInt : struct, IBinaryInteger<TInt> => value =>
        RuleResult.Create("Int.Even", value, new EvenEvaluator<TInt>());

    /// <summary>
    /// Verifies that the integer value is odd.
    /// </summary>
    /// <typeparam name="TInt">The type of the integer value.</typeparam>
    /// <returns>A RuleDelegate that evaluates the integer value.</returns>
    public static RuleDelegate<TInt> Odd<TInt>()
        where TInt : struct, IBinaryInteger<TInt> => value =>
        RuleResult.Create("Int.Odd", value, new OddEvaluator<TInt>());

    /// <summary>
    /// Verifies that the integer value is a power of 2.
    /// </summary>
    /// <typeparam name="TInt">The type of the integer value.</typeparam>
    /// <returns>A RuleDelegate that evaluates the integer value.</returns>
    public static RuleDelegate<TInt> PowerOf2<TInt>()
        where TInt : struct, IBinaryInteger<TInt> => value =>
        RuleResult.Create("Int.PowerOf2", value, new PowerOf2Evaluator<TInt>());

}
