using System.Runtime.CompilerServices;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation;

public static class ResultValidateExtensions
{
    public static ValidationTarget<T, TProperty> Validate<T, TProperty>(
        this Result<T> result,
        Func<T, TProperty> propertySelector,
        [CallerArgumentExpression(nameof(propertySelector))] string? expression = null)
    {
        ArgumentNullException.ThrowIfNull(propertySelector);
        var propertyName = ExtractPropertyName(expression);

        var executor = new ValidationExecutor<T, TProperty>((value, rule) =>
        {
            var ruleResult = rule.Validate(propertySelector(value));
            return ruleResult.IsInvalid(out var errorMessage)
                ? new ValidationFailure(propertyName, errorMessage)
                : Result.Success();
        });

        return new ValidationTarget<T, TProperty>(result, executor);
    }

    extension<TItem>(Result<IEnumerable<TItem>> result)
    {
        public ValidationTarget<IEnumerable<TItem>, TProperty> ValidateAll<TProperty>(
            Func<TItem, TProperty> propertySelector,
            [CallerArgumentExpression(nameof(propertySelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(propertySelector);
            var propertyName = ExtractPropertyName(expression);

            var executor = new ValidationExecutor<IEnumerable<TItem>, TProperty>((collection, rule) =>
            {
                var index = 0;

                foreach (var item in collection)
                {
                    var ruleResult = rule.Validate(propertySelector(item));
                    if (ruleResult.IsInvalid(out var errorMessage))
                    {
                        return new ValidationFailure($"{propertyName}[{index}]", errorMessage);
                    }

                    index++;
                }

                return Result.Success();
            });

            return new ValidationTarget<IEnumerable<TItem>, TProperty>(result, executor);
        }

        public ValidationTarget<IEnumerable<TItem>, TProperty> ValidateAny<TProperty>(
            Func<TItem, TProperty> propertySelector,
            [CallerArgumentExpression(nameof(propertySelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(propertySelector);
            var propertyName = ExtractPropertyName(expression);

            var executor = new ValidationExecutor<IEnumerable<TItem>, TProperty>((collection, rule) =>
            {
                string? errorMessage = null;
                foreach (var item in collection)
                {
                    var ruleResult = rule.Validate(propertySelector(item));
                    if (ruleResult.IsValid) return Result.Success();
                    ruleResult.IsInvalid(out errorMessage);
                }

                return new ValidationFailure(propertyName, $"No items in the collection satisfied the rule: {errorMessage}");
            });

            return new ValidationTarget<IEnumerable<TItem>, TProperty>(result, executor);
        }
    }

    private static string ExtractPropertyName(string? expression)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(expression);

        // get the complete expression after the first dot, e.g., "x => x.Property.SubProperty" -> "Property.SubProperty"
        var dotIndex = expression.IndexOf('.');
        return dotIndex >= 0 && dotIndex != expression.Length - 1
            ? expression[(dotIndex + 1)..]
            : expression;
    }
}
