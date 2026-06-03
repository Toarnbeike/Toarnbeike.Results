using System.Globalization;

namespace Toarnbeike.Results.Guards.Messages;

internal class DefaultFailureMessageProvider : IFailureMessageProvider
{
    private readonly Dictionary<string, FailureMessageTemplateCollection> _collections;

    private static readonly FailureMessageTemplateCollection CollectionRulesTemplates = new("Collection")
    {
        ["NotEmpty"] = ctx => $"'{ctx.Expression}' must not be empty, but is.",
        ["Empty"] = ctx => $"'{ctx.Expression}' must be empty, but contains {ctx.GetActualItems()}.",
        ["AtLeast"] = ctx =>
            $"'{ctx.Expression}' must contain at least {ctx.GetItems("Min")}, but contains {ctx.GetActualItems()}.",
        ["AtMost"] = ctx =>
            $"'{ctx.Expression}' must contain at most {ctx.GetItems("Max")}, but contains {ctx.GetActualItems()}.",
        ["Between"] = ctx =>
            $"'{ctx.Expression}' must contain between {ctx.Get<int>("Min")} and {ctx.GetItems("Max")}, but contains {ctx.GetActualItems()}.",
        ["Exactly"] = ctx =>
            $"'{ctx.Expression}' must contain exactly {ctx.GetItems("Expected")}, but contains {ctx.GetActualItems()}.",
        ["Single"] = ctx => $"'{ctx.Expression}' must contain exactly 1 item, but contains {ctx.GetActualItems()}.",
    };

    private static readonly FailureMessageTemplateCollection EnumRulesTemplates = new("Enum")
    {
        ["IsDefined"] = ctx =>
            $"'{ctx.Expression}' must be a defined value of '{ctx.Get<Type>("EnumType")?.Name}', but is not (value '{ctx.AttemptedValue}' is unknown).",
        ["OneOf"] = ctx =>
            $"'{ctx.Expression}' must be one of the following values: [{string.Join(", ", ctx.Get<IEnumerable<string>>("ValidValues")!)}], but is {ctx.AttemptedValue}.",
        ["NotOneOf"] = ctx =>
            $"'{ctx.Expression}' must not be one of the following values: [{string.Join(", ", ctx.Get<IEnumerable<string>>("InvalidValues")!)}], but is {ctx.AttemptedValue}.",
    };

    private static readonly FailureMessageTemplateCollection GuidRulesTemplates = new("Guid")
    {
        ["NotEmpty"] = ctx => $"'{ctx.Expression}' must not be an empty Guid, but is.",
        ["Version4"] = ctx => $"'{ctx.Expression}' must be a version 4 Guid, but is version {ctx.Get<int>("Actual")}.",
        ["Version7"] = ctx => $"'{ctx.Expression}' must be a version 7 Guid, but is version {ctx.Get<int>("Actual")}.",
    };

    private static readonly FailureMessageTemplateCollection DateRulesTemplates = new("Date")
    {
        ["OnOrAfter"] = ctx =>
            $"'{ctx.Expression}' must be on or after {ctx.Get<object>("Min")}, but is {ctx.AttemptedValue}.",
        ["OnOrBefore"] = ctx =>
            $"'{ctx.Expression}' must be on or before {ctx.Get<object>("Max")}, but is {ctx.AttemptedValue}.",
        ["Between"] = ctx =>
            $"'{ctx.Expression}' must be between {ctx.Get<object>("Min")} and {ctx.Get<object>("Max")}, but is {ctx.AttemptedValue}.",
        ["Future"] = ctx =>
            $"'{ctx.Expression}' must be in the future (on or after {ctx.Get<object>("Now")}), but is {ctx.AttemptedValue}.",
        ["Past"] = ctx =>
            $"'{ctx.Expression}' must be in the past (on or before {ctx.Get<object>("Now")}), but is {ctx.AttemptedValue}.",
        ["OnDayOfWeek"] = ctx =>
            $"'{ctx.Expression}' must be on a {ctx.Get<DayOfWeek>("ExpectedDay")}, but is a {ctx.Get<DayOfWeek>("Actual")}.",
        ["OnDaysOfWeek"] = ctx =>
            $"'{ctx.Expression}' must be on one of the following days: [{string.Join(", ", ctx.Get<DayOfWeek[]>("Allowed")!)}], but is a {ctx.Get<DayOfWeek>("Actual")}.",
        ["OnWeekday"] = ctx => $"'{ctx.Expression}' must be on a weekday, but is a {ctx.Get<DayOfWeek>("Actual")}.",
        ["OnWeekend"] = ctx => $"'{ctx.Expression}' must be on a weekend day, but is a {ctx.Get<DayOfWeek>("Actual")}.",

        // Specific for DateOnly
        ["DateOnly.WithinFuture"] = ctx =>
            $"'{ctx.Expression}' must be within the next {ctx.Get<int>("Days")} days (between {ctx.Get<DateOnly>("Today")} and {ctx.Get<DateOnly>("Today").AddDays(ctx.Get<int>("Days"))}), but is {ctx.AttemptedValue}.",
        ["DateOnly.WithinPast"] = ctx =>
            $"'{ctx.Expression}' must be within the last {ctx.Get<int>("Days")} days (between {ctx.Get<DateOnly>("Today").AddDays(-ctx.Get<int>("Days"))} and {ctx.Get<DateOnly>("Today")}), but is {ctx.AttemptedValue}.",

        // Specific for DateTime
        ["DateTime.WithinFuture"] = ctx =>
            $"'{ctx.Expression}' must be within the next {ctx.Get<TimeSpan>("TimeSpan")} (between {ctx.Get<DateTime>("Now")} and {ctx.Get<DateTime>("Now") + ctx.Get<TimeSpan>("TimeSpan")}), but is {ctx.AttemptedValue}.",
        ["DateTime.WithinPast"] = ctx =>
            $"'{ctx.Expression}' must be within the last {ctx.Get<TimeSpan>("TimeSpan")} (between {ctx.Get<DateTime>("Now") - ctx.Get<TimeSpan>("TimeSpan")} and {ctx.Get<DateTime>("Now")}), but is {ctx.AttemptedValue}.",
        ["DateTime.Around"] = ctx =>
            $"'{ctx.Expression}' must be within {ctx.Get<TimeSpan>("Tolerance")} of {ctx.Get<DateTime>("Comparison")}, but is {ctx.AttemptedValue}.",

        // Specific for DateTimeOffset
        ["DateTimeOffset.WithinFuture"] = ctx =>
            $"'{ctx.Expression}' must be within the next {ctx.Get<TimeSpan>("TimeSpan")} (between {ctx.Get<DateTimeOffset>("Now")} and {ctx.Get<DateTimeOffset>("Now") + ctx.Get<TimeSpan>("TimeSpan")}), but is {ctx.AttemptedValue}.",
        ["DateTimeOffset.WithinPast"] = ctx =>
            $"'{ctx.Expression}' must be within the last {ctx.Get<TimeSpan>("TimeSpan")} (between {ctx.Get<DateTimeOffset>("Now") - ctx.Get<TimeSpan>("TimeSpan")} and {ctx.Get<DateTimeOffset>("Now")}), but is {ctx.AttemptedValue}.",
        ["DateTimeOffset.Around"] = ctx =>
            $"'{ctx.Expression}' must be within {ctx.Get<TimeSpan>("Tolerance")} of {ctx.Get<DateTimeOffset>("Comparison")}, but is {ctx.AttemptedValue}.",
    };

    private static readonly FailureMessageTemplateCollection NumberRulesTemplates = new("Number")
    {
        ["GreaterThan"] = ctx => $"'{ctx.Expression}' must be greater than {ctx.Get<object>("Min")}, but is {ctx.AttemptedValue}.",
        ["LessThan"] = ctx => $"'{ctx.Expression}' must be less than {ctx.Get<object>("Max")}, but is {ctx.AttemptedValue}.",
        ["Positive"] = ctx => $"'{ctx.Expression}' must be positive, but is {ctx.AttemptedValue}.",
        ["Negative"] = ctx => $"'{ctx.Expression}' must be negative, but is {ctx.AttemptedValue}.",

        ["Floating.AtLeast"] = ctx => $"'{ctx.Expression}' must be at least {ctx.Get<object>("Min")} ± {ctx.Get<object>("Tolerance")}, but is {ctx.AttemptedValue}.",
        ["Floating.AtMost"] = ctx => $"'{ctx.Expression}' must be at most {ctx.Get<object>("Max")} ± {ctx.Get<object>("Tolerance")}, but is {ctx.AttemptedValue}.",
        ["Floating.Between"] = ctx => $"'{ctx.Expression}' must be between {ctx.Get<object>("Min")} and {ctx.Get<object>("Max")} (± {ctx.Get<object>("Tolerance")}), but is {ctx.AttemptedValue}.",
        ["Floating.AtLeastZero"] = ctx => $"'{ctx.Expression}' must be at least zero (± {ctx.Get<object>("Tolerance")}), but is {ctx.AttemptedValue}.",
        ["Floating.AtMostZero"] = ctx => $"'{ctx.Expression}' must be at most zero (± {ctx.Get<object>("Tolerance")}), but is {ctx.AttemptedValue}.",
        ["Floating.Zero"] = ctx => $"'{ctx.Expression}' must be zero (± {ctx.Get<object>("Tolerance")}), but is {ctx.AttemptedValue}.",
        ["Floating.NotZero"] = ctx => $"'{ctx.Expression}' must not be zero (± {ctx.Get<object>("Tolerance")}), but is {ctx.AttemptedValue}.",
        ["Floating.MultipleOf"] = ctx => $"'{ctx.Expression}' must be a multiple of {ctx.Get<object>("Factor")} (± {ctx.Get<object>("Tolerance")}), but is {ctx.AttemptedValue}.",
        ["Floating.WholeNumber"] = ctx => $"'{ctx.Expression}' must be a whole number (± {ctx.Get<object>("Tolerance")}), but is {ctx.AttemptedValue}.",
        ["Floating.MaxDecimalPlaces"] = ctx => $"'{ctx.Expression}' must have at most {ctx.Get<int>("MaxPlaces")} decimal places (± {ctx.Get<object>("Tolerance")}), but is {ctx.AttemptedValue}.",
        ["Floating.Finite"] = ctx => $"'{ctx.Expression}' must be a finite number, but is not.",
        ["Floating.NotNaN"] = ctx => $"'{ctx.Expression}' must not be NaN, but is.",

        ["Integer.AtLeast"] = ctx => $"'{ctx.Expression}' must be at least {ctx.Get<object>("Min")}, but is {ctx.AttemptedValue}.",
        ["Integer.AtMost"] = ctx => $"'{ctx.Expression}' must be at most {ctx.Get<object>("Max")}, but is {ctx.AttemptedValue}.",
        ["Integer.Between"] = ctx => $"'{ctx.Expression}' must be between {ctx.Get<object>("Min")} and {ctx.Get<object>("Max")}, but is {ctx.AttemptedValue}.",
        ["Integer.AtLeastZero"] = ctx => $"'{ctx.Expression}' must be at least zero, but is {ctx.AttemptedValue}.",
        ["Integer.AtMostZero"] = ctx => $"'{ctx.Expression}' must be at most zero, but is {ctx.AttemptedValue}.",
        ["Integer.NotZero"] = ctx => $"'{ctx.Expression}' must not be zero, but is.",
        ["Integer.MultipleOf"] = ctx => $"'{ctx.Expression}' must be a multiple of {ctx.Get<object>("Factor")}, but is {ctx.AttemptedValue}.",
        ["Integer.Even"] = ctx => $"'{ctx.Expression}' must be an even number, but is {ctx.AttemptedValue}.",
        ["Integer.Odd"] = ctx => $"'{ctx.Expression}' must be an odd number, but is {ctx.AttemptedValue}.",
        ["Integer.PowerOf2"] = ctx => $"'{ctx.Expression}' must be a power of 2, but is {ctx.AttemptedValue}.",
        ["Integer.Prime"] = ctx => $"'{ctx.Expression}' must be a prime number, but is {ctx.AttemptedValue}.",
    };

    private static readonly FailureMessageTemplateCollection PredicateRulesTemplates = new("Predicate")
    {
        ["Satisfies"] = ctx => $"'{ctx.Expression}' must satisfy a given condition, but does not.",
        ["NotSatisfies"] = ctx => $"'{ctx.Expression}' must not satisfy a given condition, but does.",
    };

    private static readonly FailureMessageTemplateCollection TimeSpanRulesTemplates = new("TimeSpan")
    {
        ["AtLeast"] = ctx =>
            $"'{ctx.Expression}' must be at least {ctx.Get<TimeSpan>("Min")}, but is {ctx.AttemptedValue}.",
        ["AtMost"] = ctx =>
            $"'{ctx.Expression}' must be at most {ctx.Get<TimeSpan>("Max")}, but is {ctx.AttemptedValue}.",
        ["Between"] = ctx =>
            $"'{ctx.Expression}' must be between {ctx.Get<TimeSpan>("Min")} and {ctx.Get<TimeSpan>("Max")}, but is {ctx.AttemptedValue}.",
        ["Around"] = ctx =>
            $"'{ctx.Expression}' must be around {ctx.Get<TimeSpan>("Expected")} ± {ctx.Get<TimeSpan>("Tolerance")}, but is {ctx.AttemptedValue}.",
        ["AtLeastZero"] = ctx => $"'{ctx.Expression}' must be at least zero, but is {ctx.AttemptedValue}.",
        ["AtMostZero"] = ctx => $"'{ctx.Expression}' must be at most zero, but is {ctx.AttemptedValue}.",
    };

    private static readonly FailureMessageTemplateCollection StringRulesTemplates = new("String")
    {
        ["NotEmpty"] = ctx => $"'{ctx.Expression}' must not be null or empty, but is.",
        ["NotWhiteSpace"] = ctx => $"'{ctx.Expression}' must not be null or whitespace, but is.",
        ["MinLength"] = ctx =>
            $"'{ctx.Expression}' must have at least {ctx.Get<int>("MinLength")} characters, but is {ctx.Get<int>("ActualLength")} characters long.",
        ["MaxLength"] = ctx =>
            $"'{ctx.Expression}' must have at most {ctx.Get<int>("MaxLength")} characters, but is {ctx.Get<int>("ActualLength")} characters long.",
        ["LengthBetween"] = ctx =>
            $"'{ctx.Expression}' must have a length between {ctx.Get<int>("MinLength")} and {ctx.Get<int>("MaxLength")} characters, but is {ctx.Get<int>("ActualLength")} characters long.",
        ["Matches"] = ctx =>
            $"'{ctx.Expression}' must match the pattern '{ctx.Get<string>("Pattern")}', but does not.",
        
        ["Alphabetic"] = ctx =>
            $"'{ctx.Expression}' must contain only alphabetic characters, but is {ctx.AttemptedValue}.",
        ["AlphaNumeric"] = ctx =>
            $"'{ctx.Expression}' must contain only alphabetic and numeric characters, but is {ctx.AttemptedValue}.",
        ["Numeric"] = ctx => $"'{ctx.Expression}' must contain only digits, but is {ctx.AttemptedValue}.",
        ["Ascii"] = ctx => $"'{ctx.Expression}' must contain only valid ASCII characters, but is {ctx.AttemptedValue}.",
        
        ["EmailAddress"] = ctx => $"'{ctx.Expression}' must be a valid email address, but is {ctx.AttemptedValue}.",
        ["Uri"] = ctx => $"'{ctx.Expression}' must be a valid URI, but is {ctx.AttemptedValue}.",
        ["AbsoluteUri"] = ctx => $"'{ctx.Expression}' must be a valid absolute URI, but is {ctx.AttemptedValue}.",
        ["RelativeUri"] = ctx => $"'{ctx.Expression}' must be a valid relative URI, but is {ctx.AttemptedValue}.",
        ["IpAddress"] = ctx => $"'{ctx.Expression}' must be a valid IP address, but is {ctx.AttemptedValue}.",
        ["Slug"] = ctx => $"'{ctx.Expression}' must be a valid slug, but is {ctx.AttemptedValue}.",
    };

    public CultureInfo Culture { get; }

    internal DefaultFailureMessageProvider(CultureInfo culture)
    {
        _collections = new[]
        {
            CollectionRulesTemplates,
            EnumRulesTemplates,
            GuidRulesTemplates,
            DateRulesTemplates,
            NumberRulesTemplates,
            PredicateRulesTemplates,
            TimeSpanRulesTemplates,
            StringRulesTemplates
        }.ToDictionary(x => x.RuleName);
        Culture = culture;
    }

    public string CreateMessage(FailureMessageContext context)
    {
        var guardName = context.GuardName;
        var separatorIndex = guardName.IndexOf('.');

        var category = guardName[..separatorIndex];
        var rule = guardName[(separatorIndex + 1)..];

        return _collections.TryGetValue(category, out var templateCollection) &&
               templateCollection.TryGetMessage(rule, context, out var message)
            ? message.ToString(Culture)
            : $"{context.GuardName} failed for {context.Expression}.";
    }
}
