using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.Extensions;

public class CollectionValidationRuleBuilderExtensionTests
{
    private readonly string _customMessage = "Custom message";
    private Func<IEnumerable<int>, IEnumerable<ValidationFailure>>? _registered = null;
    private readonly ValidationRuleBuilder<IEnumerable<int>, IEnumerable<int>> _builder;
    public CollectionValidationRuleBuilderExtensionTests()
    {
        _builder = new ValidationRuleBuilder<IEnumerable<int>, IEnumerable<int>>(
            r => _registered = r,
            (value, rule) => rule.Predicate(value)
                ? []
                : [new ValidationFailure("value", rule.Message)]);
    }

    [Test]
    public void NotEmpty_ShouldSucceed()
    {
        _builder.NotEmpty();
        ShouldSucceed([1, 2, 3]);
    }

    [Test]
    public void NotEmpty_ShouldFail()
    {
        _builder.NotEmpty();
        ShouldFail([], "Collection must not be empty.");
    }

    [Test]
    public void NotEmpty_CustomMessage()
    {
        _builder.NotEmpty(_customMessage);
        ShouldFail([], _customMessage);
    }

    [Test]
    public void Empty_ShouldSucceed()
    {
        _builder.Empty();
        ShouldSucceed([]);
    }

    [Test]
    public void Empty_ShouldFail()
    {
        _builder.Empty();
        ShouldFail([1, 2, 3], "Collection must be empty.");
    }

    [Test]
    public void Empty_CustomMessage()
    {
        _builder.Empty(_customMessage);
        ShouldFail([1, 2, 3], _customMessage);
    }

    [Test]
    public void HasAtLeast_ShouldSucceed()
    {
        _builder.HasAtLeast(2);
        ShouldSucceed([1, 2, 3]);
    }

    [Test]
    public void HasAtLeast_ShouldFail()
    {
        _builder.HasAtLeast(4);
        ShouldFail([1, 2, 3], "Collection must contain at least 4 element(s).");
    }

    [Test]
    public void HasAtLeast_CustomMessage()
    {
        _builder.HasAtLeast(4, _customMessage);
        ShouldFail([1, 2, 3], _customMessage);
    }

    [Test]
    public void HasAtMost_ShouldSucceed()
    {
        _builder.HasAtMost(4);
        ShouldSucceed([1, 2, 3]);
    }

    [Test]
    public void HasAtMost_ShouldFail()
    {
        _builder.HasAtMost(2);
        ShouldFail([1, 2, 3], "Collection must contain at most 2 element(s).");
    }

    [Test]
    public void HasAtMost_CustomMessage()
    {
        _builder.HasAtMost(2, _customMessage);
        ShouldFail([1, 2, 3], _customMessage);
    }

    [Test]
    public void HasBetween_ShouldSucceed()
    {
        _builder.HasBetween(2, 4);
        ShouldSucceed([1, 2, 3]);
    }

    [Test]
    public void HasBetween_ShouldFail()
    {
        _builder.HasBetween(4, 6);
        ShouldFail([1, 2, 3], "Collection must contain between 4 and 6 element(s).");
    }

    [Test]
    public void HasBetween_CustomMessage()
    {
        _builder.HasBetween(4, 6, _customMessage);
        ShouldFail([1, 2, 3], _customMessage);
    }

    [Test]
    public void HasExactly_ShouldSucceed()
    {
        _builder.HasExactly(3);
        ShouldSucceed([1, 2, 3]);
    }

    [Test]
    public void HasExactly_ShouldFail()
    {
        _builder.HasExactly(2);
        ShouldFail([1, 2, 3], "Collection must contain exactly 2 element(s).");
    }

    [Test]
    public void HasExactly_CustomMessage()
    {
        _builder.HasExactly(2, _customMessage);
        ShouldFail([1, 2, 3], _customMessage);
    }

    private void ShouldSucceed(IEnumerable<int> value)
    {
        _registered.ShouldNotBeNull();
        var failures = _registered(value).ToArray();
        failures.ShouldBeEmpty();
    }

    private void ShouldFail(IEnumerable<int> value, string expectedMessage)
    {
        _registered.ShouldNotBeNull();
        var failures = _registered(value).ToArray();
        failures.Length.ShouldBe(1);
        failures[0].ValidationMessage.ShouldBe(expectedMessage);
    }
}
