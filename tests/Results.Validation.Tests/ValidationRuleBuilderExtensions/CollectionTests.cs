using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.ValidationRuleBuilderExtensions;

public class CollectionTests
{
    private readonly List<int> _collection = new() { 1, 2, 3 };
    private readonly string _customMessage = "Custom message";
    private Func<IEnumerable<int>, IEnumerable<ValidationFailure>> _registered = null!;
    private readonly ValidationRuleBuilder<IEnumerable<int>, IEnumerable<int>> _builder;

    public CollectionTests()
    {
        _builder = new ValidationRuleBuilder<IEnumerable<int>, IEnumerable<int>>(
            registerRule: rule => _registered = rule,
            executor: (value, rule, message) =>
            {
                var result = rule(value);
                return result.IsValid
                    ? []
                    : [new ValidationFailure(nameof(IEnumerable<>), message ?? result.Message)];
            });
    }

    [Test]
    public void Contains_ShouldReturnNoFailures_WhenCollectionContainsItem()
    {
        _builder.Contains(2);
        var failures = _registered(_collection);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void Contains_ShouldReturnFailure_WhenCollectionDoesNotContainItem()
    {
        _builder.Contains(4);
        var failures = _registered(_collection).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void Contains_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.Contains(4, _customMessage);
        var failure = _registered(_collection).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void NotContains_ShouldReturnNoFailures_WhenCollectionDoesNotContainItem()
    {
        _builder.NotContains(4);
        var failures = _registered(_collection);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void NotContains_ShouldReturnFailure_WhenCollectionContainsItem()
    {
        _builder.NotContains(2);
        var failures = _registered(_collection).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void NotContains_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.NotContains(2, _customMessage);
        var failure = _registered(_collection).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void NotEmpty_ShouldReturnNoFailures_WhenCollectionIsNotEmpty()
    {
        _builder.NotEmpty();
        var failures = _registered(_collection);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void NotEmpty_ShouldReturnFailure_WhenCollectionIsEmpty()
    {
        _builder.NotEmpty();
        var failures = _registered(new List<int>()).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void NotEmpty_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.NotEmpty(_customMessage);
        var failure = _registered(new List<int>()).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void Empty_ShouldReturnNoFailures_WhenCollectionIsEmpty()
    {
        _builder.Empty();
        var failures = _registered(new List<int>());
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void Empty_ShouldReturnFailure_WhenCollectionIsNotEmpty()
    {
        _builder.Empty();
        var failures = _registered(_collection).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void Empty_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.Empty(_customMessage);
        var failure = _registered(_collection).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void HasAtLeast_ShouldReturnNoFailures_WhenCollectionHasAtLeastMin()
    {
        _builder.HasAtLeast(2);
        var failures = _registered(_collection);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void HasAtLeast_ShouldReturnFailure_WhenCollectionHasLessThanMin()
    {
        _builder.HasAtLeast(4);
        var failures = _registered(_collection).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void HasAtLeast_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.HasAtLeast(4, _customMessage);
        var failure = _registered(_collection).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void HasAtMost_ShouldReturnNoFailures_WhenCollectionHasAtMostMax()
    {
        _builder.HasAtMost(4);
        var failures = _registered(_collection);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void HasAtMost_ShouldReturnFailure_WhenCollectionHasMoreThanMax()
    {
        _builder.HasAtMost(2);
        var failures = _registered(_collection).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void HasAtMost_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.HasAtMost(2, _customMessage);
        var failure = _registered(_collection).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void HasBetween_ShouldReturnNoFailures_WhenCollectionHasBetweenMinAndMax()
    {
        _builder.HasBetween(2, 4);
        var failures = _registered(_collection);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void HasBetween_ShouldReturnFailure_WhenCollectionHasLessThanMin()
    {
        _builder.HasBetween(4, 5);
        var failures = _registered(_collection).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void HasBetween_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.HasBetween(4, 5, _customMessage);
        var failure = _registered(_collection).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void HasExactly_ShouldReturnNoFailures_WhenCollectionHasExactlyExpected()
    {
        _builder.HasExactly(3);
        var failures = _registered(_collection);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void HasExactly_ShouldReturnFailure_WhenCollectionDoesNotHaveExactlyExpected()
    {
        _builder.HasExactly(2);
        var failures = _registered(_collection).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void HasExactly_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.HasExactly(2, _customMessage);
        var failure = _registered(_collection).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }
}
