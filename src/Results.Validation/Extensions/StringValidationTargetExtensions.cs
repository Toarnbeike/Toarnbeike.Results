using System.Text.RegularExpressions;
using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class StringExtensions
{
    extension<T>(ValidationRuleBuilder<T, string> builder)
    {
        public ValidationRuleBuilder<T, string> NotEmpty(string? message = null) =>
            builder.Add(value => StringRules.NotEmpty(value), message ?? "must not be empty.");

        public ValidationRuleBuilder<T, string> NotNullOrWhiteSpace(string? message = null) =>
            builder.Add(value => StringRules.NotWhiteSpace(value), message ?? "must not be null or whitespace.");

        public ValidationRuleBuilder<T, string> MinLength(int minLength, string? message = null) =>
            builder.Add(value => ComparisonRules.AtLeast(value.Length, minLength),
                message ?? $"must have a minimum length of {minLength}.");

        public ValidationRuleBuilder<T, string> MaxLength(int maxLength, string? message = null) =>
            builder.Add(value => ComparisonRules.AtMost(value.Length, maxLength),
                message ?? $"must have a maximum length of {maxLength}.");

        public ValidationRuleBuilder<T, string> LengthBetween(int minLength, int maxLength, string? message = null) =>
            builder.Add(value => ComparisonRules.AtLeast(value.Length, minLength) &&
                ComparisonRules.AtMost(value.Length, maxLength),
                message ?? $"must have a length between {minLength} and {maxLength}.");

        public ValidationRuleBuilder<T, string> MatchesRegex(Regex regex, string? message = null)
        {
            ArgumentNullException.ThrowIfNull(regex);
            return builder.Add(value => StringRules.Matches(value, regex),
                message ?? $"must match the provided regex pattern.");
        }

        public ValidationRuleBuilder<T, string> MatchesRegex(string pattern, string? message = null)
        {
            ArgumentNullException.ThrowIfNull(pattern);
            return builder.Add(value => StringRules.Matches(value, new Regex(pattern, options: RegexOptions.None, matchTimeout: TimeSpan.FromSeconds(1))),
                message ?? $"must match the regex pattern: {pattern}.");
        }
    }

    extension<T>(ValidationTarget<T, string> target)
    {
        public Result<T> NotEmpty(string? message = null) =>
            target.Apply(value => StringRules.NotEmpty(value), message ?? "must not be empty.");

        public Result<T> NotNullOrWhiteSpace(string? message = null) =>
            target.Apply(value => StringRules.NotWhiteSpace(value), message ?? "must not be null or whitespace.");

        public Result<T> MinLength(int minLength, string? message = null) =>
            target.Apply(value => ComparisonRules.AtLeast(value.Length, minLength),
                message ?? $"must have a minimum length of {minLength}.");

        public Result<T> MaxLength(int maxLength, string? message = null) =>
            target.Apply(value => ComparisonRules.AtMost(value.Length, maxLength),
                message ?? $"must have a maximum length of {maxLength}.");

        public Result<T> LengthBetween(int minLength, int maxLength, string? message = null) =>
            target.Apply(value => ComparisonRules.AtLeast(value.Length, minLength) &&
                ComparisonRules.AtMost(value.Length, maxLength),
                message ?? $"must have a length between {minLength} and {maxLength}.");

        public Result<T> MatchesRegex(Regex regex, string? message = null)
        {
            ArgumentNullException.ThrowIfNull(regex);
            return target.Apply(value => StringRules.Matches(value, regex),
                message ?? $"must match the provided regex pattern.");
        }

        public Result<T> MatchesRegex(string pattern, string? message = null)
        {
            ArgumentNullException.ThrowIfNull(pattern);
            return target.Apply(value => StringRules.Matches(value, new Regex(pattern, options: RegexOptions.None, matchTimeout: TimeSpan.FromSeconds(1))),
                message ?? $"must match the regex pattern: {pattern}.");
        }
    }
}
