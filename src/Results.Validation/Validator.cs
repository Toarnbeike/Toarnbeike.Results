using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Validation;

public abstract class Validator<T> : IValidator<T>
{
    public Result<T> Validate(T value)
    {
        var failures = IterateRules(value).ToList();
        return failures.Count == 0 
            ? value 
            : new ValidationFailureSummary(failures);
    }
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

    protected abstract void RegisterRules(ValidationRuleCollection<T> rules);

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