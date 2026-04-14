using Toarnbeike.Results.Ensure.PrimitiveExtensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests.PrimitiveExtensions;

public class DateTimeExtensionTests
{
    private readonly DateTime _comparison = new(2020, 1, 1);

    [Test]
    public void After_Should_ReturnFailure_WhenBefore()
    {
        var value = _comparison.AddDays(-1);
        var result = value.After(_comparison);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("After");
        failure.Expression.ShouldBe(nameof(value));
        failure.Constraint.ShouldBe(_comparison);
        failure.AttemptedValue.ShouldBe(value);
        failure.Message.ShouldBe($"'value' must be after {_comparison}.");
    }

    [Test]
    public void After_Should_ReturnFailure_WhenEqual()
    {
        _comparison.After(_comparison).ShouldBeFailure();
    }

    [Test]
    public void After_Should_ReturnSuccess_WhenAfter()
    {
        var value = _comparison.AddDays(1);
        var result = value.After(_comparison);
        result.ShouldBeSuccess().ShouldBe(value);
    }

    [Test]
    public void Before_Should_ReturnFailure_WhenBefore()
    {
        var value = _comparison.AddDays(1);
        var result = value.Before(_comparison);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("Before");
        failure.Expression.ShouldBe(nameof(value));
        failure.Constraint.ShouldBe(_comparison);
        failure.AttemptedValue.ShouldBe(value);
        failure.Message.ShouldBe($"'value' must be before {_comparison}.");
    }

    [Test]
    public void Before_Should_ReturnFailure_WhenEqual()
    {
        _comparison.Before(_comparison).ShouldBeFailure();
    }

    [Test]
    public void Before_Should_ReturnSuccess_WhenBefore()
    {
        var value = _comparison.AddDays(-1);
        var result = value.Before(_comparison);
        result.ShouldBeSuccess().ShouldBe(value);
    }
}
