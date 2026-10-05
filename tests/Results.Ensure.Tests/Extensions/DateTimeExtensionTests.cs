using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Ensure.Extensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class DateTimeExtensionTests
{
    [Test]
    public void OnOrAfter_Should_ReturnFailure_WhenBeforeMin()
    {
        var date = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Unspecified);
        var comparison = new DateTime(2023, 1, 2, 12, 0, 0, DateTimeKind.Unspecified);
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
        var date = new DateTime(2023, 1, 2, 12, 0, 0, DateTimeKind.Unspecified);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).OnOrAfter(new DateTime(2023, 1, 2, 12, 0, 0, DateTimeKind.Unspecified));
        result.ShouldBeSuccess();
    }

    [Test]
    public void OnOrAfter_Should_UseCustomMessage_WhenProvided()
    {
        var date = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Unspecified);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).OnOrAfter(new DateTime(2023, 1, 2, 12, 0, 0, DateTimeKind.Unspecified), "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void OnOrBefore_Should_ReturnFailure_WhenAfterMax()
    {
        var date = new DateTime(2023, 1, 3, 12, 0, 0, DateTimeKind.Unspecified);
        var comparison = new DateTime(2023, 1, 2, 12, 0, 0, DateTimeKind.Unspecified);
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
        var date = new DateTime(2023, 1, 2, 12, 0, 0, DateTimeKind.Unspecified);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).OnOrBefore(new DateTime(2023, 1, 2, 12, 0, 0, DateTimeKind.Unspecified));
        result.ShouldBeSuccess();
    }

    [Test]
    public void OnOrBefore_Should_UseCustomMessage_WhenProvided()
    {
        var date = new DateTime(2023, 1, 3, 12, 0, 0, DateTimeKind.Unspecified);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).OnOrBefore(new DateTime(2023, 1, 2, 12, 0, 0, DateTimeKind.Unspecified), "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void MustBeBetween_Should_ReturnFailure_WhenOutsideRange()
    {
        var date = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Unspecified);
        var min = new DateTime(2023, 1, 2, 12, 0, 0, DateTimeKind.Unspecified);
        var max = new DateTime(2023, 1, 3, 12, 0, 0, DateTimeKind.Unspecified);
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
        var date = new DateTime(2023, 1, 2, 12, 0, 0, DateTimeKind.Unspecified);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).MustBeBetween(new DateTime(2023, 1, 2, 12, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 3, 12, 0, 0, DateTimeKind.Unspecified));
        result.ShouldBeSuccess();
    }

    [Test]
    public void MustBeBetween_Should_UseCustomMessage_WhenProvided()
    {
        var date = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Unspecified);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).MustBeBetween(new DateTime(2023, 1, 2, 12, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 3, 12, 0, 0, DateTimeKind.Unspecified), "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }
}

