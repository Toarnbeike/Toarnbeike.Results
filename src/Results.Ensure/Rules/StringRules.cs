using System.Text.RegularExpressions;
using Toarnbeike.Results.Ensure.Guards;

namespace Toarnbeike.Results.Ensure.Rules;

public static class StringRules
{
    private const string RuleCategory = "String";

    extension(IGuardTarget<string?> target)
    {
        /// <summary>
        /// Rule that the targeted string must not be empty.
        /// </summary>
        public IGuardRuleResult NotEmpty() =>
            target.Evaluate(StringGuards.NotEmpty(target.Value),
                $"{RuleCategory}.{nameof(NotEmpty)}");

        /// <summary>
        /// Rule that the targeted string must not be whiteSpace.
        /// </summary>
        public IGuardRuleResult NotWhiteSpace() =>
            target.Evaluate(StringGuards.NotWhiteSpace(target.Value),
                $"{RuleCategory}.{nameof(NotWhiteSpace)}");
    }

    extension(IGuardTarget<string> target)
    {
        /// <summary>
        /// Rule that the targeted string must have at least the specified minimum length.
        /// </summary>
        public IGuardRuleResult MinLength(int minLength) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value.Length, minLength),
                $"{RuleCategory}.{nameof(MinLength)}", 
                ("MinLength", minLength),
                ("ActualLength", target.Value.Length));

        /// <summary>
        /// Rule that the targeted string must have at most the specified maximum length.
        /// </summary>
        public IGuardRuleResult MaxLength(int maxLength) =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value.Length, maxLength),
                $"{RuleCategory}.{nameof(MaxLength)}", 
                ("MaxLength", maxLength),
                ("ActualLength", target.Value.Length));

        /// <summary>
        /// Rule that the targeted string must have a length between the specified minimum and maximum, inclusive.
        /// </summary>
        public IGuardRuleResult LengthBetween(int minLength, int maxLength) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value.Length, minLength) &&
                            ComparisonGuards.AtMost(target.Value.Length, maxLength),
                $"{RuleCategory}.{nameof(LengthBetween)}", 
                ("MinLength", minLength),
                ("MaxLength", maxLength),
                ("ActualLength", target.Value.Length));

        /// <summary>
        /// Rule that the targeted string matches the specified pattern.
        /// </summary>
        public IGuardRuleResult Matches(string pattern, RegexOptions options = default) =>
            target.Evaluate(StringGuards.Matches(target.Value, new Regex(pattern, options)),
                $"{RuleCategory}.{nameof(Matches)}", 
                ("Pattern", pattern),
                ("Options", options));

        /// <summary>
        /// Rule that the targeted string contains only alphabetic characters.
        /// </summary>
        public IGuardRuleResult Alphabetic() =>
            target.Evaluate(StringGuards.Alphabetic(target.Value),
                $"{RuleCategory}.{nameof(Alphabetic)}");

        /// <summary>
        /// Rule that the targeted string contains only alphabetic characters and number.
        /// </summary>
        public IGuardRuleResult AlphaNumeric() =>
            target.Evaluate(StringGuards.AlphaNumeric(target.Value),
                $"{RuleCategory}.{nameof(AlphaNumeric)}");

        /// <summary>
        /// Rule that the targeted string contains only digits.
        /// </summary>
        public IGuardRuleResult Numeric() =>
            target.Evaluate(StringGuards.Numeric(target.Value),
                $"{RuleCategory}.{nameof(Numeric)}");

        /// <summary>
        /// Rule that the targeted string contains only valid ASCII characters.
        /// </summary>
        public IGuardRuleResult Ascii() =>
            target.Evaluate(StringGuards.Ascii(target.Value),
                $"{RuleCategory}.{nameof(Ascii)}");

        /// <summary>
        /// Rule that the targeted string is a valid Email address.
        /// </summary>
        public IGuardRuleResult EmailAddress() =>
            target.Evaluate(StringGuards.EmailAddress(target.Value),
                $"{RuleCategory}.{nameof(EmailAddress)}");

        /// <summary>
        /// Rule that the targeted string is a valid Uri.
        /// </summary>
        public IGuardRuleResult Uri() =>
            target.Evaluate(StringGuards.Uri(target.Value),
                $"{RuleCategory}.{nameof(Uri)}");

        /// <summary>
        /// Rule that the targeted string is a valid absolute Uri.
        /// </summary>
        public IGuardRuleResult AbsoluteUri() =>
            target.Evaluate(StringGuards.AbsoluteUri(target.Value),
                $"{RuleCategory}.{nameof(AbsoluteUri)}");

        /// <summary>
        /// Rule that the targeted string is a valid relative Uri.
        /// </summary>
        public IGuardRuleResult RelativeUri() =>
            target.Evaluate(StringGuards.RelativeUri(target.Value),
                $"{RuleCategory}.{nameof(RelativeUri)}");

        /// <summary>
        /// Rule that the targeted string is a valid relative IP Address.
        /// </summary>
        public IGuardRuleResult IpAddress() =>
            target.Evaluate(StringGuards.IpAddress(target.Value),
                $"{RuleCategory}.{nameof(IpAddress)}");

        /// <summary>
        /// Rule that the targeted string is a valid slug.
        /// </summary>
        /// <remarks>
        /// This is a common format for URL slugs, which typically consist of lowercase letters, numbers, and hyphens,
        /// and do not contain spaces or special characters.
        /// The exact definition of a "slug" can vary depending on the context,
        /// so the implementation of this rule may not fit specific requirements.
        /// </remarks>
        public IGuardRuleResult Slug() =>
            target.Evaluate(StringGuards.Slug(target.Value),
                $"{RuleCategory}.{nameof(Slug)}");
    }
}