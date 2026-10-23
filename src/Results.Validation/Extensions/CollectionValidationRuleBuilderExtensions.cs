using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class CollectionValidationRuleBuilderExtensions
{
    extension<T, TItem>(ValidationRuleBuilder<T, IEnumerable<TItem>> builder)
    {
        /// <summary>
        /// Validates the rule against every item in the collection.
        /// </summary>
        /// <returns>A validation rule builder for each item in the collection.</returns>
        public ValidationRuleBuilder<T, TItem> All()
        {
            ValidatorExecutor<T, TItem> executor = (value, rule, message) =>
            {
                var failures = new List<ValidationFailure>();
                var index = 0;
                foreach (var item in builder.Select(value))
                {
                    var result = rule(item);
                    if (!result.IsValid)
                    {
                        failures.Add(new ValidationFailure($"{builder.PropertyName}[{index}]", message ?? result.Message));
                    }

                    index++;
                }

                return failures;
            };

            return builder.Create(executor, validator => builder.RegisterRule(value =>
            {
                var failures = new List<ValidationFailure>();
                var index = 0;
                foreach (var item in builder.Select(value))
                {
                    failures.AddRange(NestedValidation.Validate(
                        validator,
                        item,
                        $"{builder.PropertyName}[{index}]"));
                    index++;
                }

                return failures;
            }));
        }

        /// <summary>
        /// Validates that at least one item in the collection passes the rule.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if no item passes.</param>
        /// <returns>A validation rule builder for the collection items.</returns>
        public ValidationRuleBuilder<T, TItem> Any(string? message = null)
        {
            ValidatorExecutor<T, TItem> executor = (T value, RuleDelegate<TItem> rule, string? _) =>
            {
                foreach (var item in builder.Select(value))
                {
                    if (rule(item).IsValid)
                    {
                        return [];
                    }
                }

                return [new ValidationFailure(
                    builder.PropertyName!,
                    message ?? "No items satisfied the validation rule.")];
            };

            return builder.Create(executor);
        }
    }
}
