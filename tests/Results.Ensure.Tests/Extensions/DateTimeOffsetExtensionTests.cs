using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class DateTimeOffsetExtensionTests
{
    [Test]
    public void OnOrAfter_Should_ReturnFailure_WhenBeforeMin()
    {
        var date = new DateTimeOffset(2023, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var comparison = new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).OnOrAfter(comparison);
        _ = result.ShouldBeFailureOfType<GuardFailure>();
    }

    [Test]
    public void OnOrAfter_Should_ReturnSuccess_WhenOnOrAfterMin()
    {
        var date = new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).OnOrAfter(new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero));
        result.ShouldBeSuccess();
    }

    [Test]
    public void OnOrAfter_Should_UseCustomMessage_WhenProvided()
    {
        var date = new DateTimeOffset(2023, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).OnOrAfter(new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero), "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void OnOrBefore_Should_ReturnFailure_WhenAfterMax()
    {
        var date = new DateTimeOffset(2023, 1, 3, 12, 0, 0, TimeSpan.Zero);
        var comparison = new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).OnOrBefore(comparison);
        _ = result.ShouldBeFailureOfType<GuardFailure>();
    }

    [Test]
    public void OnOrBefore_Should_ReturnSuccess_WhenOnOrBeforeMax()
    {
        var date = new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).OnOrBefore(new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero));
        result.ShouldBeSuccess();
    }

    [Test]
    public void OnOrBefore_Should_UseCustomMessage_WhenProvided()
    {
        var date = new DateTimeOffset(2023, 1, 3, 12, 0, 0, TimeSpan.Zero);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).OnOrBefore(new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero), "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void MustBeBetween_Should_ReturnFailure_WhenOutsideRange()
    {
        var date = new DateTimeOffset(2023, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var min = new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero);
        var max = new DateTimeOffset(2023, 1, 3, 12, 0, 0, TimeSpan.Zero);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).Between(min, max);
        _ = result.ShouldBeFailureOfType<GuardFailure>();
    }

    [Test]
    public void MustBeBetween_Should_ReturnSuccess_WhenWithinRange()
    {
        var date = new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).Between(new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero), new DateTimeOffset(2023, 1, 3, 12, 0, 0, TimeSpan.Zero));
        result.ShouldBeSuccess();
    }

    [Test]
    public void MustBeBetween_Should_UseCustomMessage_WhenProvided()
    {
        var date = new DateTimeOffset(2023, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var result =
            Result.Success()
            .WithValue(date)
            .Ensure(x => x).Between(new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero), new DateTimeOffset(2023, 1, 3, 12, 0, 0, TimeSpan.Zero), "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }
}

