using Toarnbeike.Results.Rules.Evaluations.Comparisons;

namespace Toarnbeike.Results.Rules.TypeRules;

/// <summary>
/// Set of Rules for Date/Time types 
/// (<see cref="DateTime"/>,<see cref="DateTimeOffset"/>, <see cref="DateOnly"/> and <see cref="TimeOnly"/>).
/// </summary>
public static class DateRules
{
    /// <summary>
    /// Verifies that the value is on or after the specified minimum value.
    /// </summary>
    /// <typeparam name="TDate">The type of the date.</typeparam>
    /// <param name="min">The minimum date value.</param>
    /// <returns>A rule delegate that verifies the date value.</returns>
    public static RuleDelegate<TDate> OnOrAfter<TDate>(TDate min)
        where TDate : IComparable<TDate> => value =>
        RuleResult.Create("Date.OnOrAfter", value, new AtLeastEvaluator<TDate>(min));

    /// <summary>
    /// Verifies that the value is on or before the specified maximum value.
    /// </summary>
    /// <typeparam name="TDate">The type of the date.</typeparam>
    /// <param name="max">The maximum date value.</param>
    /// <returns>A rule delegate that verifies the date value.</returns>
    public static RuleDelegate<TDate> OnOrBefore<TDate>(TDate max)
        where TDate : IComparable<TDate> => value =>
        RuleResult.Create("Date.OnOrBefore", value, new AtMostEvaluator<TDate>(max));

    /// <summary>
    /// Verifies that the value is between the specified minimum and maximum values (inclusive).
    /// </summary>
    /// <typeparam name="TDate">The type of the date.</typeparam>
    /// <param name="min">The minimum date value.</param>
    /// <param name="max">The maximum date value.</param>
    /// <returns>A rule delegate that verifies the date value.</returns>
    public static RuleDelegate<TDate> Between<TDate>(TDate min, TDate max)
        where TDate : IComparable<TDate> => value =>
        RuleResult.Create("Date.Between", value, new BetweenEvaluator<TDate>(min, max));

    /// <summary>
    /// Verifies that the value is in the future relative to the current UTC date/time.
    /// </summary>
    /// <typeparam name="TDate">The type of the date.</typeparam>
    /// <returns>A rule delegate that verifies the date value.</returns>
    public static RuleDelegate<TDate> Future<TDate>() where TDate : IComparable<TDate> => value =>
        RuleResult.Create("Date.Future", value, new AtLeastEvaluator<TDate>(RuleContext.UtcNow<TDate>()));

    /// <summary>
    /// Verifies that the value is in the past relative to the current UTC date/time.
    /// </summary>
    /// <typeparam name="TDate">The type of the date.</typeparam>
    /// <returns>A rule delegate that verifies the date value.</returns>
    public static RuleDelegate<TDate> Past<TDate>() where TDate : IComparable<TDate> => value =>
        RuleResult.Create("Date.Past", value, new AtMostEvaluator<TDate>(RuleContext.UtcNow<TDate>()));

    /// <summary>
    /// Verifies that the value is within the specified number of days in the future relative to the current UTC date.
    /// </summary>
    /// <param name="days">The number of days in the future.</param>
    /// <returns>A rule delegate that verifies the date value.</returns>
    public static RuleDelegate<DateOnly> WithinFuture(int days) => value =>
    {
        var utcNow = RuleContext.UtcNow<DateOnly>();
        return RuleResult.Create("Date.WithinFuture", value, new BetweenEvaluator<DateOnly>(utcNow, utcNow.AddDays(days)));
    };

    /// <summary>
    /// Verifies that the value is within the specified time span in the future relative to the current UTC date/time.
    /// </summary>
    /// <param name="timeSpan">The time span in the future.</param>
    /// <returns>A rule delegate that verifies the date value.</returns>
    public static RuleDelegate<DateTimeOffset> WithinFuture(TimeSpan timeSpan) => value =>
    {
        var utcNow = RuleContext.UtcNow<DateTimeOffset>();
        return RuleResult.Create("Date.WithinFuture", value, new BetweenEvaluator<DateTimeOffset>(utcNow, utcNow.Add(timeSpan)));
    };

    /// <summary>
    /// Verifies that the value is within the specified number of days in the past relative to the current UTC date.
    /// </summary>
    /// <param name="days">The number of days in the past.</param>
    /// <returns>A rule delegate that verifies the date value.</returns>
    public static RuleDelegate<DateOnly> WithinPast(int days) => value =>
    {
        var utcNow = RuleContext.UtcNow<DateOnly>();
        return RuleResult.Create("Date.WithinPast", value, new BetweenEvaluator<DateOnly>(utcNow.AddDays(-days), utcNow));
    };

    /// <summary>
    /// Verifies that the value is within the specified time span in the past relative to the current UTC date/time.
    /// </summary>
    /// <param name="timeSpan">The time span in the past.</param>
    /// <returns>A rule delegate that verifies the date value.</returns>
    public static RuleDelegate<DateTimeOffset> WithinPast(TimeSpan timeSpan) => value =>
    {
        var utcNow = RuleContext.UtcNow<DateTimeOffset>();
        return RuleResult.Create("Date.WithinPast", value, new BetweenEvaluator<DateTimeOffset>(utcNow.Subtract(timeSpan), utcNow));
    };

}
