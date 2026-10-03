using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Validation;

/// <inheritdoc/>
public abstract class Validator<T> : IValidator<T>
{
    /// <inheritdoc/>
    public Result<T> Validate(T value)
    {
        var failures = IterateRules(value);
        return failures.Any() 
            ? new ValidationFailureSummary(failures)
            : value;
    }

    /// <summary>
    /// Registers the validation rules for the validator.
    /// </summary>
    /// <param name="rules">The collection of validation rules to register.</param>
    protected abstract void RegisterRules(ValidationRuleCollection<T> rules);

    private readonly Lazy<IReadOnlyList<Func<T, IEnumerable<ValidationFailure>>>> _rules;

    protected Validator()
    {
        _rules = new(CreateRuleCollection, LazyThreadSafetyMode.ExecutionAndPublication);
    }

    private IReadOnlyList<Func<T, IEnumerable<ValidationFailure>>> CreateRuleCollection()
    {
        var rules = new ValidationRuleCollection<T>();
        RegisterRules(rules);
        return rules.Build();
    }

    private IEnumerable<ValidationFailure> IterateRules(T value)
    {
        foreach (var rule in _rules.Value)
        {
            foreach (var failure in rule(value))
            {
                yield return failure;
            }
        }
    }
}