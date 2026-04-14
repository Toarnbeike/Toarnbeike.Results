using Toarnbeike.Results.Ensure.PrimitiveExtensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests.PrimitiveExtensions;

public class NumberExtensionsTests
{
    [Test]
    public void GreaterThan_Should_ReturnFailure_WhenSmaller()
    {
        var min = 0;
        var value = -1;
        var result = value.GreaterThan(min);
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
        0.GreaterThan(0).ShouldBeFailure();
    }

    [Test]
    public void GreaterThan_Should_ReturnSuccess_WhenValid()
    {
        var value = 1;
        var result = value.GreaterThan(0);
        result.ShouldBeSuccess().ShouldBe(value);
    }

    [Test]
    public void LessThan_Should_ReturnFailure_WhenGreater()
    {
        var max = 1d;
        var value = 1.5d;
        var result = value.LessThan(max);
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
        0.LessThan( 0).ShouldBeFailure();
    }

    [Test]
    public void LessThan_Should_ReturnSuccess_WhenValid()
    {
        var value = 0;
        var result = value.LessThan(1);
        result.ShouldBeSuccess().ShouldBe(value);
    }

    [Test]
    public void InRange_Should_ReturnFailure_WhenAboveMax()
    {
        var min = 0;
        var max = 100;
        var value = 200;
        var result = value.InRange(min, max);
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
        var result = value.InRange(0, 100);
        result.ShouldBeSuccess().ShouldBe(value);
    }

    [Test]
    public void InRange_Should_ReturnSuccess_WhenEqualToMin()
    {
        0.InRange(0, 100).ShouldBeSuccess();
    }

    [Test]
    public void InRange_Should_ReturnFailure_WhenBelowMin()
    {
        (-10).InRange(0, 100).ShouldBeFailure();
    }
}