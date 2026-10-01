using System.Text.RegularExpressions;
using Toarnbeike.Results.Guards.Attributes;
using Toarnbeike.Results.Guards.Core.Guards;

namespace Toarnbeike.Results.Guards.Rules;

public static class StringRules
{
    private const string RuleCategory = "String";

    extension(IGuardTarget<string?> target)
    {
        /// <summary>
        /// Rule that the targeted string must not be empty.
        /// </summary>
        public IGuardRuleResult<string> NotEmpty() =>
            target.EvaluateThenConvert(value =>
                    new RuleEvaluation(
                        StringGuards.NotEmpty(value),
                        $"{RuleCategory}.{nameof(NotEmpty)}"),
                value => value!);

        /// <summary>
        /// Rule that the targeted string must not be whiteSpace.
        /// </summary>
        public IGuardRuleResult<string> NotWhiteSpace() =>
            target.EvaluateThenConvert(value =>
                new RuleEvaluation(
                    StringGuards.NotWhiteSpace(value),
                    $"{RuleCategory}.{nameof(NotWhiteSpace)}"),
                value => value!);
    }

    extension(IGuardTarget<string> target)
    {
        /// <summary>
        /// Rule that the targeted string must have at least the specified minimum length.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<string> MinLength(int minLength) =>
            target.Evaluate(value =>
                    new RuleEvaluation(
                        ComparisonGuards.AtLeast(value.Length, minLength),
                        $"{RuleCategory}.{nameof(MinLength)}",
                        ("MinLength", minLength),
                        ("ActualLength", value.Length)));

        /// <summary>
        /// Rule that the targeted string must have at most the specified maximum length.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<string> MaxLength(int maxLength) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.AtMost(value.Length, maxLength),
                    $"{RuleCategory}.{nameof(MaxLength)}",
                    ("MaxLength", maxLength),
                    ("ActualLength", value.Length)));

        /// <summary>
        /// Rule that the targeted string must have a length between the specified minimum and maximum, inclusive.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<string> LengthBetween(int minLength, int maxLength) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.AtLeast(value.Length, minLength) && ComparisonGuards.AtMost(value.Length, maxLength),
                    $"{RuleCategory}.{nameof(LengthBetween)}",
                    ("MinLength", minLength),
                    ("MaxLength", maxLength),
                    ("ActualLength", value.Length)));

        /// <summary>
        /// Rule that the targeted string matches the specified pattern.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<string> Matches(string pattern) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    StringGuards.Matches(value, new Regex(pattern, default)),
                    $"{RuleCategory}.{nameof(Matches)}",
                    ("Pattern", pattern),
                    ("Options", default(RegexOptions))));

        /// <summary>
        /// Rule that the targeted string matches the specified pattern.
        /// </summary>
        public IGuardRuleResult<string> Matches(string pattern, RegexOptions options) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    StringGuards.Matches(value, new Regex(pattern, options)),
                    $"{RuleCategory}.{nameof(Matches)}",
                    ("Pattern", pattern),
                    ("Options", options)));

        /// <summary>
        /// Rule that the targeted string contains only alphabetic characters.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<string> Alphabetic() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    StringGuards.Alphabetic(value),
                    $"{RuleCategory}.{nameof(Alphabetic)}"));

        /// <summary>
        /// Rule that the targeted string contains only alphabetic characters and number.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<string> AlphaNumeric() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    StringGuards.AlphaNumeric(value),
                    $"{RuleCategory}.{nameof(AlphaNumeric)}"));

        /// <summary>
        /// Rule that the targeted string contains only digits.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<string> Numeric() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    StringGuards.Numeric(value),
                    $"{RuleCategory}.{nameof(Numeric)}"));

        /// <summary>
        /// Rule that the targeted string contains only valid ASCII characters.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<string> Ascii() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    StringGuards.Ascii(value),
                    $"{RuleCategory}.{nameof(Ascii)}"));

        /// <summary>
        /// Rule that the targeted string is a valid Email address.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<string> EmailAddress() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    StringGuards.EmailAddress(value),
                    $"{RuleCategory}.{nameof(EmailAddress)}"));

        /// <summary>
        /// Rule that the targeted string is a valid Uri.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<string> Uri() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    StringGuards.Uri(value),
                    $"{RuleCategory}.{nameof(Uri)}"));

        /// <summary>
        /// Rule that the targeted string is a valid absolute Uri.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<string> AbsoluteUri() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    StringGuards.AbsoluteUri(value),
                    $"{RuleCategory}.{nameof(AbsoluteUri)}"));

        /// <summary>
        /// Rule that the targeted string is a valid relative Uri.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<string> RelativeUri() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    StringGuards.RelativeUri(value),
                    $"{RuleCategory}.{nameof(RelativeUri)}"));

        /// <summary>
        /// Rule that the targeted string is a valid relative IP Address.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<string> IpAddress() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    StringGuards.IpAddress(value),
                    $"{RuleCategory}.{nameof(IpAddress)}"));

        /// <summary>
        /// Rule that the targeted string is a valid slug.
        /// </summary>
        /// <remarks>
        /// This is a common format for URL slugs, which typically consist of lowercase letters, numbers, and hyphens,
        /// and do not contain spaces or special characters.
        /// The exact definition of a "slug" can vary depending on the context,
        /// so the implementation of this rule may not fit specific requirements.
        /// </remarks>
        public IGuardRuleResult<string> Slug() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    StringGuards.Slug(value),
                    $"{RuleCategory}.{nameof(Slug)}"));
    }
}