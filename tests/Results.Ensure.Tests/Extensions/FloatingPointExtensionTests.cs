using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Ensure.Extensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class FloatingPointExtensionTests
{
    [Test]
    public void AtLeast_Should_ReturnFailure_WhenBelow()
    {
        double value = 0.5;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).AtLeast(1.0);

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Value must be at least 1.");
    }

    [Test]
    public void AtLeast_Should_ReturnSuccess_WhenAtLeast()
    {
        double value = 1.5;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).AtLeast(1.0);

        result.ShouldBeSuccess();
    }

    [Test]
    public void AtLeast_Should_UseCustomMessage_WhenProvided()
    {
        double value = 0.5;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).AtLeast(1.0, null, "Custom message");

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void AtMost_Should_ReturnFailure_WhenAbove()
    {
        double value = 2.0;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).AtMost(1.0);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Value must be at most 1.");
    }

    [Test]
    public void AtMost_Should_ReturnSuccess_WhenWithin()
    {
        double value = 0.5;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).AtMost(1.0);
        result.ShouldBeSuccess();
    }

    [Test]
    public void AtMost_Should_UseCustomMessage_WhenProvided()
    {
        double value = 2.0;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).AtMost(1.0, null, "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void Between_Should_ReturnFailure_WhenOutside()
    {
        double value = 0.5;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).Between(1.0, 2.0);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Value must be between 1 and 2.");
    }

    [Test]
    public void Between_Should_ReturnSuccess_WhenWithin()
    {
        double value = 1.5;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).Between(1.0, 2.0);
        result.ShouldBeSuccess();
    }

    [Test]
    public void Between_Should_UseCustomMessage_WhenProvided()
    {
        double value = 0.5;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).Between(1.0, 2.0, null, "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void MultipleOf_Should_ReturnFailure_WhenNotMultiple()
    {
        double value = 3.0;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).MultipleOf(2.0);

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Value must be a multiple of 2.");
    }

    [Test]
    public void MultipleOf_Should_ReturnSuccess_WhenMultiple()
    {
        double value = 4.0;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).MultipleOf(2.0);

        result.ShouldBeSuccess();
    }

    [Test]
    public void Finite_Should_ReturnFailure_WhenInfinite()
    {
        double value = double.PositiveInfinity;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).Finite();

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Value must be a finite Double.");
    }

    [Test]
    public void Finite_Should_ReturnSuccess_WhenFinite()
    {
        double value = 1.0;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).Finite();

        result.ShouldBeSuccess();
    }

    [Test]
    public void NotNaN_Should_ReturnFailure_WhenNaN()
    {
        double value = double.NaN;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).NotNaN();

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Value must not be NaN.");
    }

    [Test]
    public void NotNaN_Should_ReturnSuccess_WhenNotNaN()
    {
        double value = 1.0;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).NotNaN();

        result.ShouldBeSuccess();
    }
}
