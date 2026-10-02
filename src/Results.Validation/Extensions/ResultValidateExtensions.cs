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
        var propertyName = ExpressionHelpers.ExtractPropertyName(expression);

        var executor = new ValidationExecutor<T, TProperty>((value, rule, message) =>
            rule(propertySelector(value))
                ? Result.Success()
                : new ValidationFailure(propertyName, message));

        return new ValidationTarget<T, TProperty>(result, executor);
    }

    extension<TItem>(Result<IEnumerable<TItem>> result)
    {
        public ValidationTarget<IEnumerable<TItem>, TProperty> ValidateAll<TProperty>(
            Func<TItem, TProperty> propertySelector,
            [CallerArgumentExpression(nameof(propertySelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(propertySelector);
            var propertyName = ExpressionHelpers.ExtractPropertyName(expression);

            var executor = new ValidationExecutor<IEnumerable<TItem>, TProperty>((collection, rule, message) =>
            {
                var index = 0;
                foreach (var item in collection)
                {
                    if (!rule(propertySelector(item)))
                    {
                        return new ValidationFailure($"{propertyName}[{index}]", message);
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
            var propertyName = ExpressionHelpers.ExtractPropertyName(expression);

            var executor = new ValidationExecutor<IEnumerable<TItem>, TProperty>((collection, rule, message) =>
            {
                return collection.Any(item => rule(propertySelector(item)))
                    ? Result.Success()
                    : new ValidationFailure(propertyName, $"No items in the collection satisfied the rule: {message}");
            });

            return new ValidationTarget<IEnumerable<TItem>, TProperty>(result, executor);
        }
    }
}
