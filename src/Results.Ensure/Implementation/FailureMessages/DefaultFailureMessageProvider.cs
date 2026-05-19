using Toarnbeike.Results.Ensure.Abstractions;

namespace Toarnbeike.Results.Ensure.Implementation.FailureMessages;

internal delegate string FailureMessageFactory(FailureMessageContext context);

internal class DefaultFailureMessageProvider : IFailureMessageProvider
{
    private static readonly Dictionary<string, FailureMessageFactory> MessageTemplates = new()
    {
        ["CollectionRules.NotEmpty"] = ctx => $"'{ctx.Expression}' must not be empty, but is.",
        ["CollectionRules.Empty"] = ctx => $"'{ctx.Expression}' must be empty, but contains {ctx.Get<int>("Actual")} item(s).",
        ["CollectionRules.AtLeast"] = ctx => $"'{ctx.Expression}' must contain at least {ctx.Get<int>("Min")} item(s), but contains {ctx.Get<int>("Actual")} item(s).",
        ["CollectionRules.AtMost"] = ctx => $"'{ctx.Expression}' must contain at most {ctx.Get<int>("Max")} item(s), but contains {ctx.Get<int>("Actual")} item(s).",
        ["CollectionRules.Between"] = ctx => $"'{ctx.Expression}' must contain between {ctx.Get<int>("Min")} and {ctx.Get<int>("Max")} items, but contains {ctx.Get<int>("Actual")} item(s).",
        ["CollectionRules.Exactly"] = ctx => $"'{ctx.Expression}' must contain exactly {ctx.Get<int>("Expected")} item(s), but contains {ctx.Get<int>("Actual")} item(s).",
        ["CollectionRules.Single"] = ctx => $"'{ctx.Expression}' must contain exactly 1 item, but contains {ctx.Get<int>("Actual")} item(s).",

        ["DateRules.OnOrAfter"] = ctx => $"'{ctx.Expression}' must be on or after {ctx.Get<object>("Min")}, but is {ctx.AttemptedValue}.",
        ["DateRules.OnOrBefore"] = ctx => $"'{ctx.Expression}' must be on or before {ctx.Get<object>("Max")}, but is {ctx.AttemptedValue}.",
        ["DateRules.Between"] = ctx => $"'{ctx.Expression}' must be between {ctx.Get<object>("Min")} and {ctx.Get<object>("Max")}, but is {ctx.AttemptedValue}.",
        ["DateRules.Future"] = ctx => $"'{ctx.Expression}' must be in the future (on or after {ctx.Get<object>("Now")}), but is {ctx.AttemptedValue}.",
        ["DateRules.Past"] = ctx => $"'{ctx.Expression}' must be in the past (on or before {ctx.Get<object>("Now")}), but is {ctx.AttemptedValue}.",
        ["DateRules.OnDayOfWeek"] = ctx => $"'{ctx.Expression}' must be on a {ctx.Get<DayOfWeek>("ExpectedDay")}, but is {ctx.Get<DayOfWeek>("Actual")}.",
        ["DateRules.OnDaysOfWeek"] = ctx => $"'{ctx.Expression}' must be on one of the following days: {string.Join(", ", ctx.Get<DayOfWeek[]>("Allowed")!)}, but is {ctx.Get<DayOfWeek>("Actual")}.",
        ["DateRules.OnWeekday"] = ctx => $"'{ctx.Expression}' must be on a weekday, but is {ctx.Get<DayOfWeek>("Actual")}.",
        ["DateRules.OnWeekend"] = ctx => $"'{ctx.Expression}' must be on a weekend day, but is {ctx.Get<DayOfWeek>("Actual")}.",

        ["DateOnlyRules.WithinFuture"] = ctx => $"'{ctx.Expression}' must be within the next {ctx.Get<int>("Days")} days (between {ctx.Get<DateOnly>("Today")} and {ctx.Get<DateOnly>("Today").AddDays(ctx.Get<int>("Days"))}), but is {ctx.AttemptedValue}.",
        ["DateOnlyRules.WithinPast"] = ctx => $"'{ctx.Expression}' must be within the last {ctx.Get<int>("Days")} days (between {ctx.Get<DateOnly>("Today").AddDays(-ctx.Get<int>("Days"))} and {ctx.Get<DateOnly>("Today")}), but is {ctx.AttemptedValue}.",

        ["DateTimeRules.WithinFuture"] = ctx => $"'{ctx.Expression}' must be within the next {ctx.Get<TimeSpan>("TimeSpan")} (between {ctx.Get<DateTime>("Now")} and {ctx.Get<DateTime>("Now") + ctx.Get<TimeSpan>("TimeSpan")}), but is {ctx.AttemptedValue}.",
        ["DateTimeRules.WithinPast"] = ctx => $"'{ctx.Expression}' must be within the last {ctx.Get<TimeSpan>("TimeSpan")} (between {ctx.Get<DateTime>("Now") - ctx.Get<TimeSpan>("TimeSpan")} and {ctx.Get<DateTime>("Now")}), but is {ctx.AttemptedValue}.",
        ["DateTimeRules.Around"] = ctx => $"'{ctx.Expression}' must be within {ctx.Get<TimeSpan>("Tolerance")} of {ctx.Get<DateTime>("Comparison")}, but is {ctx.AttemptedValue}.",
        
        ["DateTimeOffsetRules.WithinFuture"] = ctx => $"'{ctx.Expression}' must be within the next {ctx.Get<TimeSpan>("TimeSpan")} (between {ctx.Get<DateTimeOffset>("Now")} and {ctx.Get<DateTimeOffset>("Now") + ctx.Get<TimeSpan>("TimeSpan")}), but is {ctx.AttemptedValue}.",
        ["DateTimeOffsetRules.WithinPast"] = ctx => $"'{ctx.Expression}' must be within the last {ctx.Get<TimeSpan>("TimeSpan")} (between {ctx.Get<DateTimeOffset>("Now") - ctx.Get<TimeSpan>("TimeSpan")} and {ctx.Get<DateTimeOffset>("Now")}), but is {ctx.AttemptedValue}.",
        ["DateTimeOffsetRules.Around"] = ctx => $"'{ctx.Expression}' must be within {ctx.Get<TimeSpan>("Tolerance")} of {ctx.Get<DateTimeOffset>("Comparison")}, but is {ctx.AttemptedValue}.",

        ["EnumRules.IsDefined"] = ctx => $"'{ctx.Expression}' must be a defined value of {ctx.Get<Type>("EnumType")?.Name ?? "'enum'"}, but is {ctx.AttemptedValue}.",
        ["EnumRules.OneOf"] = ctx => $"'{ctx.Expression}' must be one of {string.Join(", ", ctx.Get<IEnumerable<string>>("ValidValues")!)}, but is {ctx.AttemptedValue}.",
        ["EnumRules.NotOneOf"] = ctx => $"'{ctx.Expression}' must not be one of {string.Join(", ", ctx.Get<IEnumerable<string>>("InvalidValues")!)}, but is {ctx.AttemptedValue}.",

        ["FloatingPointNumberRules.AtLeast"] = ctx => $"'{ctx.Expression}' must be at least {ctx.Get<object>("Min")} ± {ctx.Get<object>("Tolerance")}, but is {ctx.AttemptedValue}.",
        ["FloatingPointNumberRules.AtMost"] = ctx => $"'{ctx.Expression}' must be at most {ctx.Get<object>("Max")} ± {ctx.Get<object>("Tolerance")}, but is {ctx.AttemptedValue}.",
        ["FloatingPointNumberRules.Between"] = ctx => $"'{ctx.Expression}' must be between {ctx.Get<object>("Min")} and {ctx.Get<object>("Max")} (± {ctx.Get<object>("Tolerance")}), but is {ctx.AttemptedValue}.",
        ["FloatingPointNumberRules.AtLeastZero"] = ctx => $"'{ctx.Expression}' must be at least zero ± {ctx.Get<object>("Tolerance")}), but is {ctx.AttemptedValue}.",
        ["FloatingPointNumberRules.AtMostZero"] = ctx => $"'{ctx.Expression}' must be at most zero ± {ctx.Get<object>("Tolerance")}), but is {ctx.AttemptedValue}.",
        ["FloatingPointNumberRules.Zero"] = ctx => $"'{ctx.Expression}' must be zero ± {ctx.Get<object>("Tolerance")}), but is {ctx.AttemptedValue}.",
        ["FloatingPointNumberRules.NotZero"] = ctx => $"'{ctx.Expression}' must not be zero ± {ctx.Get<object>("Tolerance")}), but is {ctx.AttemptedValue}.",
        ["FloatingPointNumberRules.MultipleOf"] = ctx => $"'{ctx.Expression}' must be a multiple of {ctx.Get<object>("Factor")} (± {ctx.Get<object>("Tolerance")}), but is {ctx.AttemptedValue}.",
        ["FloatingPointNumberRules.WholeNumber"] = ctx => $"'{ctx.Expression}' must be a whole number (± {ctx.Get<object>("Tolerance")}), but is {ctx.AttemptedValue}.",
        ["FloatingPointNumberRules.MaxDecimalPlaces"] = ctx => $"'{ctx.Expression}' must have at most {ctx.Get<int>("MaxPlaces")} decimal places (± {ctx.Get<object>("Tolerance")}), but is {ctx.AttemptedValue}.",
        ["FloatingPointNumberRules.Finite"] = ctx => $"'{ctx.Expression}' must be a finite number, but is not.",
        ["FloatingPointNumberRules.NotNaN"] = ctx => $"'{ctx.Expression}' must not be NaN, but is.",

        ["GuidRules.NotEmpty"] = ctx => $"'{ctx.Expression}' must not be empty, but is.",
        ["GuidRules.Version4"] = ctx => $"'{ctx.Expression}' must be a version 4 Guid, but is version {ctx.Get<int>("Actual")}.",
        ["GuidRules.Version7"] = ctx => $"'{ctx.Expression}' must be a version 4 Guid, but is version {ctx.Get<int>("Actual")}.",

        ["IntegerNumberRules.AtLeast"] = ctx => $"'{ctx.Expression}' must be at least {ctx.Get<object>("Min")}, but is {ctx.AttemptedValue}.",
        ["IntegerNumberRules.AtMost"] = ctx => $"'{ctx.Expression}' must be at most {ctx.Get<object>("Max")}, but is {ctx.AttemptedValue}.",
        ["IntegerNumberRules.Between"] = ctx => $"'{ctx.Expression}' must be between {ctx.Get<object>("Min")} and {ctx.Get<object>("Max")}, but is {ctx.AttemptedValue}.",
        ["IntegerNumberRules.AtLeastZero"] = ctx => $"'{ctx.Expression}' must be at least zero, but is {ctx.AttemptedValue}.",
        ["IntegerNumberRules.AtMostZero"] = ctx => $"'{ctx.Expression}' must be at most zero, but is {ctx.AttemptedValue}.",
        ["IntegerNumberRules.NotZero"] = ctx => $"'{ctx.Expression}' must not be zero, but is.",
        ["IntegerNumberRules.MultipleOf"] = ctx => $"'{ctx.Expression}' must be a multiple of {ctx.Get<object>("Factor")}, but is {ctx.AttemptedValue}.",
        ["IntegerNumberRules.Even"] = ctx => $"'{ctx.Expression}' must be an even number, but is {ctx.AttemptedValue}.",
        ["IntegerNumberRules.Odd"] = ctx => $"'{ctx.Expression}' must be an odd number, but is {ctx.AttemptedValue}.",
        ["IntegerNumberRules.PowerOf2"] = ctx => $"'{ctx.Expression}' must be a power of 2, but is {ctx.AttemptedValue}.",
        ["IntegerNumberRules.Prime"] = ctx => $"'{ctx.Expression}' must be a prime number, but is {ctx.AttemptedValue}.",
        
        ["NumberRules.GreaterThan"] = ctx => $"'{ctx.Expression}' must be greater than {ctx.Get<object>("Min")}, but is {ctx.AttemptedValue}.",
        ["NumberRules.LessThan"] = ctx => $"'{ctx.Expression}' must be less than {ctx.Get<object>("Max")}, but is {ctx.AttemptedValue}.",
        ["NumberRules.Positive"] = ctx => $"'{ctx.Expression}' must be positive, but is {ctx.AttemptedValue}.",
        ["NumberRules.Negative"] = ctx => $"'{ctx.Expression}' must be negative, but is {ctx.AttemptedValue}.",

        ["PredicateRules.Satisfies"] = ctx => $"'{ctx.Expression}' must satisfy the condition, but does not ({ctx.AttemptedValue}).",
        ["PredicateRules.NotSatisfies"] = ctx => $"'{ctx.Expression}' must not satisfy the condition, but does ({ctx.AttemptedValue}).",

        ["StringRules.NotEmpty"] = ctx => $"'{ctx.Expression}' must not be null or empty, but is.",
        ["StringRules.NotWhiteSpace"] = ctx => $"'{ctx.Expression}' must not be null or whitespace, but is.",
        ["StringRules.MinLength"] = ctx => $"'{ctx.Expression}' must have at least {ctx.Get<int>("MinLength")} characters, but is {ctx.Get<int>("ActualLength")} characters long.",
        ["StringRules.MaxLength"] = ctx => $"'{ctx.Expression}' must have at most {ctx.Get<int>("MaxLength")} characters, but is {ctx.Get<int>("ActualLength")} characters long.",
        ["StringRules.LengthBetween"] = ctx => $"'{ctx.Expression}' must have a length between {ctx.Get<int>("MinLength")} and {ctx.Get<int>("MaxLength")} characters, but is {ctx.Get<int>("ActualLength")} characters long.",
        ["StringRules.Matches"] = ctx => $"'{ctx.Expression}' must match the pattern {ctx.Get<string>("Pattern")}, but does not ({ctx.AttemptedValue}).",
        ["StringRules.Alphabetic"] = ctx => $"'{ctx.Expression}' must contain only alphabetic characters, but is {ctx.AttemptedValue}.",
        ["StringRules.AlphaNumeric"] = ctx => $"'{ctx.Expression}' must contain only alphabetic and numeric characters, but is {ctx.AttemptedValue}.",
        ["StringRules.Numeric"] = ctx => $"'{ctx.Expression}' must contain only digits, but is {ctx.AttemptedValue}.",
        ["StringRules.Ascii"] = ctx => $"'{ctx.Expression}' must contain only valid ASCII characters, but is {ctx.AttemptedValue}.",
        ["StringRules.EmailAddress"] = ctx => $"'{ctx.Expression}' must be a valid email address, but is {ctx.AttemptedValue}.",
        ["StringRules.Uri"] = ctx => $"'{ctx.Expression}' must be a valid URI, but is {ctx.AttemptedValue}.",
        ["StringRules.AbsoluteUri"] = ctx => $"'{ctx.Expression}' must be a valid absolute URI, but is {ctx.AttemptedValue}.",
        ["StringRules.RelativeUri"] = ctx => $"'{ctx.Expression}' must be a valid relative URI, but is {ctx.AttemptedValue}.",
        ["StringRules.IpAddress"] = ctx => $"'{ctx.Expression}' must be a valid IP address, but is {ctx.AttemptedValue}.",
        ["StringRules.Slug"] = ctx => $"'{ctx.Expression}' must be a valid slug, but is {ctx.AttemptedValue}.",
        
        ["TimeSpanRules.AtLeast"] = ctx => $"'{ctx.Expression}' must be at least {ctx.Get<TimeSpan>("Min")}, but is {ctx.AttemptedValue}.",
        ["TimeSpanRules.AtMost"] = ctx => $"'{ctx.Expression}' must be at most {ctx.Get<TimeSpan>("Max")}, but is {ctx.AttemptedValue}.",
        ["TimeSpanRules.Between"] = ctx => $"'{ctx.Expression}' must be between {ctx.Get<TimeSpan>("Min")} and {ctx.Get<TimeSpan>("Max")}, but is {ctx.AttemptedValue}.",
        ["TimeSpanRules.Around"] = ctx => $"'{ctx.Expression}' must be around {ctx.Get<TimeSpan>("Expected")} with a tolerance of {ctx.Get<TimeSpan>("Tolerance")}, but is {ctx.AttemptedValue}.",
        ["TimeSpanRules.AtLeastZero"] = ctx => $"'{ctx.Expression}' must be at least zero, but is {ctx.AttemptedValue}.",
        ["TimeSpanRules.AtMostZero"] = ctx => $"'{ctx.Expression}' must be at most zero, but is {ctx.AttemptedValue}.",
    };

    public string CreateMessage(FailureMessageContext context)
    {
        return MessageTemplates.TryGetValue(context.GuardName, out var template)
            ? template(context)
            : $"{context.GuardName} failed for {context.Expression}.";
    }
}
