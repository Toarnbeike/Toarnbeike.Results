using Toarnbeike.Results.Ensure.Extensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class EnsureDateTests
{
    [Test]
    public void After_Should_ReturnFailure_WhenBefore()
    {
        var other = new DateTime(2020, 1, 1);
        var value = other.AddDays(-1);
        var result = Ensure.After(value, other);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("After");
        failure.Expression.ShouldBe(nameof(value));
        failure.Constraint.ShouldBe(other);
        failure.AttemptedValue.ShouldBe(value);
        failure.Message.ShouldBe($"'value' must be after {other}.");
    }

    [Test]
    public void After_Should_ReturnFailure_WhenEqual()
    {
        var date = new DateOnly(2020, 1, 1);
        Ensure.After(date, date).ShouldBeFailure();
    }

    [Test]
    public void After_Should_ReturnSuccess_WhenAfter()
    {
        var other = new DateTimeOffset(2020, 1, 1, 0,0,0,TimeSpan.FromHours(1));
        var value = other.AddDays(1);
        var result = Ensure.After(value, other);
        result.ShouldBeSuccess().ShouldBe(value);
    }

    [Test]
    public void Before_Should_ReturnFailure_WhenBefore()
    {
        var other = new DateOnly(2020, 1, 1);
        var value = other.AddDays(1);
        var result = Ensure.Before(value, other);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("Before");
        failure.Expression.ShouldBe(nameof(value));
        failure.Constraint.ShouldBe(other);
        failure.AttemptedValue.ShouldBe(value);
        failure.Message.ShouldBe($"'value' must be before {other}.");
    }

    [Test]
    public void Before_Should_ReturnFailure_WhenEqual()
    {
        var datetime = new DateTime(2020, 1, 1);
        Ensure.Before(datetime, datetime).ShouldBeFailure();
    }

    [Test]
    public void Before_Should_ReturnSuccess_WhenBefore()
    {
        var other = new DateTime(2020, 1, 1);
        var value = other.AddDays(-1);
        var result = Ensure.Before(value, other);
        result.ShouldBeSuccess().ShouldBe(value);
    }
}