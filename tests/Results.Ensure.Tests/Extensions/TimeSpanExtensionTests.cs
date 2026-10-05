using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Ensure.Extensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class TimeSpanExtensionTests
{
    [Test]
    public void AtLeast_Should_ReturnFailure_WhenShorter()
    {
        var min = TimeSpan.FromMinutes(5);
        var value = TimeSpan.FromMinutes(1);
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).AtLeast(min);

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe($"TimeSpan must be at least {min}.");
    }

    [Test]
    public void AtLeast_Should_ReturnSuccess_WhenAtLeast()
    {
        var min = TimeSpan.FromMinutes(5);
        var value = TimeSpan.FromMinutes(5);
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).AtLeast(min);

        result.ShouldBeSuccess();
    }

    [Test]
    public void AtLeast_Should_UseCustomMessage_WhenProvided()
    {
        var min = TimeSpan.FromMinutes(5);
        var value = TimeSpan.FromMinutes(1);
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).AtLeast(min, "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void AtMost_Should_ReturnFailure_WhenLonger()
    {
        var max = TimeSpan.FromMinutes(10);
        var value = TimeSpan.FromMinutes(15);
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).AtMost(max);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe($"TimeSpan must be at most {max}.");
    }

    [Test]
    public void AtMost_Should_ReturnSuccess_WhenWithin()
    {
        var max = TimeSpan.FromMinutes(10);
        var value = TimeSpan.FromMinutes(5);
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).AtMost(max);
        result.ShouldBeSuccess();
    }

    [Test]
    public void AtMost_Should_UseCustomMessage_WhenProvided()
    {
        var max = TimeSpan.FromMinutes(10);
        var value = TimeSpan.FromMinutes(15);
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).AtMost(max, "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void Between_Should_ReturnFailure_WhenOutsideRange()
    {
        var min = TimeSpan.FromMinutes(5);
        var max = TimeSpan.FromMinutes(10);
        var value = TimeSpan.FromMinutes(12);
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).Between(min, max);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe($"TimeSpan must be between {min} and {max}.");
    }

    [Test]
    public void Between_Should_ReturnSuccess_WhenWithinRange()
    {
        var min = TimeSpan.FromMinutes(5);
        var max = TimeSpan.FromMinutes(10);
        var value = TimeSpan.FromMinutes(7);
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).Between(min, max);
        result.ShouldBeSuccess();
    }

    [Test]
    public void Between_Should_UseCustomMessage_WhenProvided()
    {
        var min = TimeSpan.FromMinutes(5);
        var max = TimeSpan.FromMinutes(10);
        var value = TimeSpan.FromMinutes(12);
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).Between(min, max, "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void Around_Should_ReturnFailure_WhenOutsideTolerance()
    {
        var expected = TimeSpan.FromMinutes(10);
        var tolerance = TimeSpan.FromMinutes(1);
        var value = TimeSpan.FromMinutes(12);
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).Around(expected, tolerance);

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe($"TimeSpan must be around {expected} with a tolerance of {tolerance}.");
    }

    [Test]
    public void Around_Should_ReturnSuccess_WhenWithinTolerance()
    {
        var expected = TimeSpan.FromMinutes(10);
        var tolerance = TimeSpan.FromMinutes(2);
        var value = TimeSpan.FromMinutes(11);
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).Around(expected, tolerance);

        result.ShouldBeSuccess();
    }

    [Test]
    public void Around_Should_UseCustomMessage_WhenProvided()
    {
        var expected = TimeSpan.FromMinutes(10);
        var tolerance = TimeSpan.FromMinutes(1);
        var value = TimeSpan.FromMinutes(12);
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).Around(expected, tolerance, "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }
}
