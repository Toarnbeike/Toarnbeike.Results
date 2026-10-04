using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Validation.Extensions;

namespace Toarnbeike.Results.Validation.Tests.Extensions;

public class DateTimeOffsetValidationTargetExtensionTests
{
    [Test]
    public void OnOrAfter_Should_ReturnFailure_WhenBeforeMin()
    {
        var date = new DateTimeOffset(2023, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var comparison = new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero);
        var result =
            Result.Success()
            .WithValue(date)
            .Validate(x => x).OnOrAfter(comparison);
        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe($"Date must be on or after {comparison}.");
    }

    [Test]
    public void OnOrAfter_Should_ReturnSuccess_WhenOnOrAfterMin()
    {
        var date = new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero);
        var result =
            Result.Success()
            .WithValue(date)
            .Validate(x => x).OnOrAfter(new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero));
        result.ShouldBeSuccess();
    }

    [Test]
    public void OnOrAfter_Should_UseCustomMessage_WhenProvided()
    {
        var date = new DateTimeOffset(2023, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var result =
            Result.Success()
            .WithValue(date)
            .Validate(x => x).OnOrAfter(new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero), "Custom message");
        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom message");
    }

    [Test]
    public void OnOrBefore_Should_ReturnFailure_WhenAfterMax()
    {
        var date = new DateTimeOffset(2023, 1, 3, 12, 0, 0, TimeSpan.Zero);
        var comparison = new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero);
        var result =
            Result.Success()
            .WithValue(date)
            .Validate(x => x).OnOrBefore(comparison);
        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe($"Date must be on or before {comparison}.");
    }

    [Test]
    public void OnOrBefore_Should_ReturnSuccess_WhenOnOrBeforeMax()
    {
        var date = new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero);
        var result =
            Result.Success()
            .WithValue(date)
            .Validate(x => x).OnOrBefore(new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero));
        result.ShouldBeSuccess();
    }

    [Test]
    public void OnOrBefore_Should_UseCustomMessage_WhenProvided()
    {
        var date = new DateTimeOffset(2023, 1, 3, 12, 0, 0, TimeSpan.Zero);
        var result =
            Result.Success()
            .WithValue(date)
            .Validate(x => x).OnOrBefore(new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero), "Custom message");
        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom message");
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
            .Validate(x => x).MustBeBetween(min, max);
        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe($"Date must be between {min} and {max}.");
    }

    [Test]
    public void MustBeBetween_Should_ReturnSuccess_WhenWithinRange()
    {
        var date = new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero);
        var result =
            Result.Success()
            .WithValue(date)
            .Validate(x => x).MustBeBetween(new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero), new DateTimeOffset(2023, 1, 3, 12, 0, 0, TimeSpan.Zero));
        result.ShouldBeSuccess();
    }

    [Test]
    public void MustBeBetween_Should_UseCustomMessage_WhenProvided()
    {
        var date = new DateTimeOffset(2023, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var result =
            Result.Success()
            .WithValue(date)
            .Validate(x => x).MustBeBetween(new DateTimeOffset(2023, 1, 2, 12, 0, 0, TimeSpan.Zero), new DateTimeOffset(2023, 1, 3, 12, 0, 0, TimeSpan.Zero), "Custom message");
        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom message");
    }
}

