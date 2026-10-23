using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class BoolExtensions
{
    extension<T>(ValidationRuleBuilder<T, bool> builder)
    {
        /// <summary>
        /// Validates that the boolean value is true.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, bool> IsTrue(string? message = null) =>
            builder.Add(BoolRules.IsTrue(), message);

        /// <summary>
        /// Validates that the boolean value is false.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, bool> IsFalse(string? message = null) =>
            builder.Add(BoolRules.IsFalse(), message);
    }
}
