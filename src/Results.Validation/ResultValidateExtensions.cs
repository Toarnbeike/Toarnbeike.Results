using System.Runtime.CompilerServices;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation;

public static class ResultValidateExtensions
{
    extension<T>(Result<T> result)
    {
        public ValidationTarget<T, TProperty> Validate<TProperty>(
        Func<T, TProperty> propertySelector,
        string? propertyName = null,
        [CallerArgumentExpression(nameof(propertySelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(propertySelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);

            var executor = new ValidationExecutor<T, TProperty>((value, rule) =>
                rule.Predicate(propertySelector(value))
                    ? Result.Success()
                    : new ValidationFailure(propertyName, rule.Message));

            return new ValidationTarget<T, TProperty>(result, executor);
        }

        public ValidationTarget<T, TItem> ValidateAll<TItem>(
            Func<T, IEnumerable<TItem>> collectionSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(collectionSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(collectionSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);

            var executor = new ValidationExecutor<T, TItem>((value, rule) =>
            {
                var collection = collectionSelector(value);
                var index = 0;
                foreach (var item in collection)
                {
                    if (!rule.Predicate(item))
                    {
                        return new ValidationFailure($"{propertyName}[{index}]", rule.Message);
                    }
                    index++;
                }
                return Result.Success();
            });

            return new ValidationTarget<T, TItem>(result, executor);
        }

        public ValidationTarget<T, TItem> ValidateAny<TItem>(
            Func<T, IEnumerable<TItem>> collectionSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(collectionSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(collectionSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);
            var executor = new ValidationExecutor<T, TItem>((value, rule) =>
            {
                var collection = collectionSelector(value);
                return collection.Any(item => rule.Predicate(item))
                    ? Result.Success()
                    : new ValidationFailure(propertyName, $"No items in the collection satisfied the rule: {rule.Message}");
            });
            return new ValidationTarget<T, TItem>(result, executor);
        }
    }
}
