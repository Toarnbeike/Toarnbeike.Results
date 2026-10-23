using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Extensions;

public static class ValidateUsingExtensions
{
    extension<T, TProperty>(ValidationRuleBuilder<T, TProperty> builder)
    {
        /// <summary>
        /// Validates the selected property using a nested validator.
        /// </summary>
        /// <param name="validator">The validator to apply to the selected property.</param>
        /// <returns>The validation rule builder.</returns>
        public ValidationRuleBuilder<T, TProperty> ValidateUsing(IValidator<TProperty> validator)
        {
            builder.AddValidator(validator);
            return builder;
        }
    }
}