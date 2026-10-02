namespace Toarnbeike.Results.Validation.Implementation;

public readonly record struct ValidationRule<TProperty>(Func<TProperty, bool> Predicate, string Message);
