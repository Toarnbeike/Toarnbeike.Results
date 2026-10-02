using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation;

/// <summary>
/// It's a shame this is neccesary, but conversion from Result{TCollection{TItem}} to Result{IEnumerable{TItem}} is not implicit, so we need these extension methods to make it possible to validate collections.
/// </summary>
public static class ResultValidateCollectionExtensions
{
    extension<TItem>(Result<List<TItem>> result)
    {
        public ValidationTarget<IEnumerable<TItem>, TProperty> ValidateAll<TProperty>(
            Func<TItem, TProperty> propertySelector,
            [CallerArgumentExpression(nameof(propertySelector))] string? expression = null) =>
                result.AsEnumerable<List<TItem>, TItem>().ValidateAll(propertySelector, expression);

        public ValidationTarget<IEnumerable<TItem>, TProperty> ValidateAny<TProperty>(
            Func<TItem, TProperty> propertySelector,
            [CallerArgumentExpression(nameof(propertySelector))] string? expression = null) =>
                result.AsEnumerable<List<TItem>, TItem>().ValidateAny(propertySelector, expression);
    }

    extension<TItem>(Result<TItem[]> result)
    {
        public ValidationTarget<IEnumerable<TItem>, TProperty> ValidateAll<TProperty>(
            Func<TItem, TProperty> propertySelector,
            [CallerArgumentExpression(nameof(propertySelector))] string? expression = null) =>
                result.AsEnumerable<TItem[], TItem>().ValidateAll(propertySelector, expression);

        public ValidationTarget<IEnumerable<TItem>, TProperty> ValidateAny<TProperty>(
            Func<TItem, TProperty> propertySelector,
            [CallerArgumentExpression(nameof(propertySelector))] string? expression = null) =>
                result.AsEnumerable<TItem[], TItem>().ValidateAny(propertySelector, expression);
    }

    extension<TItem>(Result<ImmutableList<TItem>> result)
    {
        public ValidationTarget<IEnumerable<TItem>, TProperty> ValidateAll<TProperty>(
            Func<TItem, TProperty> propertySelector,
            [CallerArgumentExpression(nameof(propertySelector))] string? expression = null) =>
                result.AsEnumerable<ImmutableList<TItem>, TItem>().ValidateAll(propertySelector, expression);

        public ValidationTarget<IEnumerable<TItem>, TProperty> ValidateAny<TProperty>(
            Func<TItem, TProperty> propertySelector,
            [CallerArgumentExpression(nameof(propertySelector))] string? expression = null) =>
                result.AsEnumerable<ImmutableList<TItem>, TItem>().ValidateAny(propertySelector, expression);
    }

    extension<TItem>(Result<ICollection<TItem>> result)
    {
        public ValidationTarget<IEnumerable<TItem>, TProperty> ValidateAll<TProperty>(
            Func<TItem, TProperty> propertySelector,
            [CallerArgumentExpression(nameof(propertySelector))] string? expression = null) =>
                result.AsEnumerable<ICollection<TItem>, TItem>().ValidateAll(propertySelector, expression);

        public ValidationTarget<IEnumerable<TItem>, TProperty> ValidateAny<TProperty>(
            Func<TItem, TProperty> propertySelector,
            [CallerArgumentExpression(nameof(propertySelector))] string? expression = null) =>
                result.AsEnumerable<ICollection<TItem>, TItem>().ValidateAny(propertySelector, expression);
    }

    extension<TItem>(Result<IReadOnlyCollection<TItem>> result)
    {
        public ValidationTarget<IEnumerable<TItem>, TProperty> ValidateAll<TProperty>(
            Func<TItem, TProperty> propertySelector,
            [CallerArgumentExpression(nameof(propertySelector))] string? expression = null) =>
                result.AsEnumerable<IReadOnlyCollection<TItem>, TItem>().ValidateAll(propertySelector, expression);

        public ValidationTarget<IEnumerable<TItem>, TProperty> ValidateAny<TProperty>(
            Func<TItem, TProperty> propertySelector,
            [CallerArgumentExpression(nameof(propertySelector))] string? expression = null) =>
                result.AsEnumerable<IReadOnlyCollection<TItem>, TItem>().ValidateAny(propertySelector, expression);
    }

    extension<TItem>(Result<IReadOnlyList<TItem>> result)
    {
        public ValidationTarget<IEnumerable<TItem>, TProperty> ValidateAll<TProperty>(
            Func<TItem, TProperty> propertySelector,
            [CallerArgumentExpression(nameof(propertySelector))] string? expression = null) =>
                result.AsEnumerable<IReadOnlyList<TItem>, TItem>().ValidateAll(propertySelector, expression);

        public ValidationTarget<IEnumerable<TItem>, TProperty> ValidateAny<TProperty>(
            Func<TItem, TProperty> propertySelector,
            [CallerArgumentExpression(nameof(propertySelector))] string? expression = null) =>
                result.AsEnumerable<IReadOnlyList<TItem>, TItem>().ValidateAny(propertySelector, expression);
    }

    private static Result<IEnumerable<TItem>> AsEnumerable<TCollection, TItem>(this Result<TCollection> result) 
        where TCollection : IEnumerable<TItem> =>
        result.Map(collection => (IEnumerable<TItem>)collection);
}