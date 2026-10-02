using System.Text.RegularExpressions;
using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class StringExtensions
{
    extension<T>(ValidationRuleBuilder<T, string> builder)
    {
        public ValidationRuleBuilder<T, string> NotEmpty(string? message = null) =>
            builder.Add(StringRules.NotEmpty(message));

        public ValidationRuleBuilder<T, string> NotNullOrWhiteSpace(string? message = null) =>
            builder.Add(StringRules.NotWhiteSpace(message));

        public ValidationRuleBuilder<T, string> MinLength(int minLength, string? message = null) =>
            builder.Add(StringRules.MinLength(minLength, message));

        public ValidationRuleBuilder<T, string> MaxLength(int maxLength, string? message = null) =>
            builder.Add(StringRules.MaxLength(maxLength, message));

        public ValidationRuleBuilder<T, string> LengthBetween(int minLength, int maxLength, string? message = null) =>
            builder.Add(StringRules.LengthBetween(minLength, maxLength, message));

        public ValidationRuleBuilder<T, string> MatchesRegex(Regex regex, string? message = null) =>
            builder.Add(StringRules.Matches(regex, message));

        public ValidationRuleBuilder<T, string> MatchesRegex(string pattern, string? message = null) => 
            builder.Add(StringRules.Matches(pattern, message));
    }

    extension<T>(ValidationTarget<T, string> target)
    {
        public Result<T> NotEmpty(string? message = null) =>
            target.Apply(StringRules.NotEmpty(message));

        public Result<T> NotNullOrWhiteSpace(string? message = null) =>
            target.Apply(StringRules.NotWhiteSpace(message));

        public Result<T> MinLength(int minLength, string? message = null) =>
            target.Apply(StringRules.MinLength(minLength, message));

        public Result<T> MaxLength(int maxLength, string? message = null) =>
            target.Apply(StringRules.MaxLength(maxLength, message));

        public Result<T> LengthBetween(int minLength, int maxLength, string? message = null) =>
            target.Apply(StringRules.LengthBetween(minLength, maxLength, message));

        public Result<T> MatchesRegex(Regex regex, string? message = null) =>
            target.Apply(StringRules.Matches(regex, message));
        
        public Result<T> MatchesRegex(string pattern, string? message = null) =>
            target.Apply(StringRules.Matches(pattern, message));
    }
}