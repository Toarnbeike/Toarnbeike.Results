using Toarnbeike.Results.Ensure.Extensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class EnsureNumberTests
{
    [Test]
    public void GreaterThan_Should_ReturnFailure_WhenSmaller()
    {
        var min = 0;
        var value = -1;
        var result = Ensure.GreaterThan(value, min);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("GreaterThan");
        failure.Expression.ShouldBe(nameof(value));
        failure.Constraint.ShouldBe(min);
        failure.AttemptedValue.ShouldBe(value);
        failure.Message.ShouldBe("'value' must be greater than 0.");
    }

    [Test]
    public void GreaterThan_Should_ReturnFailure_WhenEqual()
    {
        Ensure.GreaterThan(0, 0).ShouldBeFailure();
    }

    [Test]
    public void GreaterThan_Should_ReturnSuccess_WhenValid()
    {
        var value = 1;
        var result = Ensure.GreaterThan(value, 0);
        result.ShouldBeSuccess().ShouldBe(value);
    }

    [Test]
    public void LessThan_Should_ReturnFailure_WhenGreater()
    {
        var max = 1d;
        var value = 1.5d;
        var result = Ensure.LessThan(value, max);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("LessThan");
        failure.Expression.ShouldBe(nameof(value));
        failure.Constraint.ShouldBe(max);
        failure.AttemptedValue.ShouldBe(value);
        failure.Message.ShouldBe("'value' must be less than 1.");
    }

    [Test]
    public void LessThan_Should_ReturnFailure_WhenEqual()
    {
        Ensure.LessThan(0, 0).ShouldBeFailure();
    }

    [Test]
    public void LessThan_Should_ReturnSuccess_WhenValid()
    {
        var value = 0;
        var result = Ensure.LessThan(value, 1);
        result.ShouldBeSuccess().ShouldBe(value);
    }

    [Test]
    public void InRange_Should_ReturnFailure_WhenAboveMax()
    {
        var min = 0;
        var max = 100;
        var value = 200;
        var result = Ensure.InRange(value, min, max);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("InRange");
        failure.Expression.ShouldBe(nameof(value));
        failure.Constraint.ShouldBe((min, max));
        failure.AttemptedValue.ShouldBe(value);
        failure.Message.ShouldBe("'value' must be between 0 and 100.");
    }

    [Test]
    public void InRange_Should_ReturnSuccess_WhenEqualToMax()
    {
        var value = 100;
        var result = Ensure.InRange(value, 0, 100);
        result.ShouldBeSuccess().ShouldBe(value);
    }

    [Test]
    public void InRange_Should_ReturnSuccess_WhenEqualToMin()
    {
        Ensure.InRange(0, 0, 100).ShouldBeSuccess();
    }

    [Test]
    public void InRange_Should_ReturnFailure_WhenBelowMin()
    {
        Ensure.InRange(-10, 0, 100).ShouldBeFailure();
    }
}