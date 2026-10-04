namespace Toarnbeike.Results.Rules;

public readonly record struct Rule<TProperty>(Func<TProperty, bool> Predicate, string Message);
