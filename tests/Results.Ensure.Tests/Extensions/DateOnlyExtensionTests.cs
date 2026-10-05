using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Ensure.Extensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class DateOnlyExtensionTests
{
    [Test]
    public void OnOrAfter_Should_ReturnFailure_WhenBeforeMin()
    {
        var date = new DateOnly(2023, 1, 1);
        var comparison = new DateOnly(2023, 1, 2);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).OnOrAfter(comparison);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe($"Date must be on or after {comparison}.");
    }

    [Test]
    public void OnOrAfter_Should_ReturnSuccess_WhenOnOrAfterMin()
    {
        var date = new DateOnly(2023, 1, 2);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).OnOrAfter(new DateOnly(2023, 1, 2));
        result.ShouldBeSuccess();
    }

    [Test]
    public void OnOrAfter_Should_UseCustomMessage_WhenProvided()
    {
        var date = new DateOnly(2023, 1, 1);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).OnOrAfter(new DateOnly(2023, 1, 2), "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void OnOrBefore_Should_ReturnFailure_WhenAfterMax()
    {
        var date = new DateOnly(2023, 1, 3);
        var comparison = new DateOnly(2023, 1, 2);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).OnOrBefore(comparison);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe($"Date must be on or before {comparison}.");
    }

    [Test]
    public void OnOrBefore_Should_ReturnSuccess_WhenOnOrBeforeMax()
    {
        var date = new DateOnly(2023, 1, 2);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).OnOrBefore(new DateOnly(2023, 1, 2));
        result.ShouldBeSuccess();
    }

    [Test]
    public void OnOrBefore_Should_UseCustomMessage_WhenProvided()
    {
        var date = new DateOnly(2023, 1, 3);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).OnOrBefore(new DateOnly(2023, 1, 2), "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void MustBeBetween_Should_ReturnFailure_WhenOutsideRange()
    {
        var date = new DateOnly(2023, 1, 1);
        var min = new DateOnly(2023, 1, 2);
        var max = new DateOnly(2023, 1, 3);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).MustBeBetween(min, max);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe($"Date must be between {min} and {max}.");
    }

    [Test]
    public void MustBeBetween_Should_ReturnSuccess_WhenWithinRange()
    {
        var date = new DateOnly(2023, 1, 2);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).MustBeBetween(new DateOnly(2023, 1, 2), new DateOnly(2023, 1, 3));
        result.ShouldBeSuccess();
    }

    [Test]
    public void MustBeBetween_Should_UseCustomMessage_WhenProvided()
    {
        var date = new DateOnly(2023, 1, 1);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).MustBeBetween(new DateOnly(2023, 1, 2), new DateOnly(2023, 1, 3), "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }
}

