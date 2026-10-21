using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.ValidationRuleBuilderExtensions;

public class PredicateTests
{
    private Func<int, IEnumerable<ValidationFailure>> _registered = null!;
    private readonly ValidationRuleBuilder<int, int> _builder;

    public PredicateTests()
    {
        _builder = new ValidationRuleBuilder<int, int>(
            registerRule: rule => _registered = rule,
            executor: (value, rule, message) =>
            {
                var result = rule(value);
                return result.IsValid
                    ? []
                    : [new ValidationFailure(nameof(Int32), message ?? result.Message)];
            });
    }

    [Test]
    public void Must_ShouldReturnNoFailures_WhenPredicateIsSatisfied()
    {
        _builder.Must(x => x > 0, "Value must be positive");
        var failures = _registered(5);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void Must_ShouldReturnFailure_WhenPredicateIsNotSatisfied()
    {
        _builder.Must(x => x > 0, "Value must be positive");
        var failures = _registered(-1).ToArray();
        failures.Length.ShouldBe(1);
        failures[0].ValidationMessage.ShouldBe("Value must be positive");
    }

    [Test]
    public void MustNot_ShouldReturnNoFailures_WhenPredicateIsNotSatisfied()
    {
        _builder.MustNot(x => x < 0, "Value must not be negative");
        var failures = _registered(5);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void MustNot_ShouldReturnFailure_WhenPredicateIsSatisfied()
    {
        _builder.MustNot(x => x < 0, "Value must not be negative");
        var failures = _registered(-1).ToArray();
        failures.Length.ShouldBe(1);
        failures[0].ValidationMessage.ShouldBe("Value must not be negative");
    }
}
