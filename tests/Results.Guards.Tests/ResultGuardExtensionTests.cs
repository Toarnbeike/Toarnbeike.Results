using System.Globalization;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Guards.Rules;
using Toarnbeike.Results.Guards.Tolerances;
using Toarnbeike.Results.TestExtensions;
using TUnit.Assertions.Exceptions;

namespace Toarnbeike.Results.Guards.Tests;

public class ResultGuardExtensionTests
{
    [Test]
    public void Ensure_Should_ReturnSuccess_WithOriginalValue()
    {
        var guid = Guid.CreateVersion7();
        var result = Result.Ensure()
            .That(guid).NotEmpty().Version7()
            .ToResult();

        var actual = result.ShouldBeSuccess();
        actual.ShouldBe(guid);
    }

    [Test]
    public void Ensure_Should_ReturnSuccess_WithProvidedValue()
    {
        var guid = Guid.CreateVersion7();
        var result = Result.Ensure()
            .That(guid).NotEmpty().Version7()
            .ToResult("Hello");

        var actual = result.ShouldBeSuccess();
        actual.ShouldBe("Hello");
    }

    [Test]
    public void Ensure_Should_ReturnGuardFailure()
    {
        var guid = Guid.CreateVersion7();
        var result = Result.Ensure()
            .That(guid).NotEmpty().Version4()
            .ToResult();

        var guardFailure = result.ShouldBeFailureOfType<GuardFailure>();
        guardFailure.Category.ShouldBe(FailureCategory.Business);
        guardFailure.Message.ShouldContain("version 4");
        guardFailure.Expression.ShouldBe("guid");
    }

    [Test]
    public void Ensure_Should_ShortCircuit_ReturnFirstFailure()
    {
        var value = "Hello";
        var result = Result.Ensure()
            .That(value).MinLength(6)
            .That(value).Satisfies(_ => throw new AssertionException("Ensure did not short circuit after first failure"))
            .ToResult();

        var failure = result.ShouldBeFailure();
        failure.Message.ShouldContain("6");
    }

    [Test]
    public void Ensure_Should_ApplyCultureInfo_WhenProvided()
    {
        var value = 1.15d;
        var result = Result.Ensure(new CultureInfo("nl-NL"))
            .That(value).GreaterThan(2)
            .ToResult();

        var failure = result.ShouldBeFailure();
        failure.Message.ShouldContain("1,15"); // with a comma as the decimal separator, as per Dutch culture
    }

    [Test]
    public void Ensure_Should_ApplyToleranceProvider_WhenProvided()
    {
        var value = 1.0d + 1e-10; // Add less than the default tolerance to ensure it would be considered equal without custom tolerance provider
        var result = Result.Ensure(toleranceProvider: new ZeroToleranceProvider())
            .That(value).AtMost(1.0d)
            .ToResult();

        result.ShouldBeFailure();

        // to show that the failure is due to the custom tolerance provider:
        Result.Ensure()
            .That(value).AtMost(1.0d)
            .ToResult()
            .ShouldBeSuccess();
    }

    [Test]
    public void Ensure_Should_ApplyCustomTimeProvider_WhenProvided()
    {
        // The FakeTimeProvider returns a fixed time of 2000-01-01T00:00:00Z,
        // so any time value that is not within the past 2 hours of that should fail.
        var timeProvider = new FakeTimeProvider();

        var value = DateTime.UtcNow.AddHours(-1);
        var result = Result.Ensure(timeProvider: timeProvider)
            .That(value).WithinPast(TimeSpan.FromHours(2))
            .ToResult();

        var failure = result.ShouldBeFailure();
        failure.Message.ShouldContain("01/01/2000");
    }

    [Test]
    public void Validate_Should_ReturnSuccess_WithOriginalValue()
    {
        var guid = Guid.CreateVersion7();
        var result = Result.Validate()
            .That(guid).NotEmpty().Version7()
            .ToResult();

        var actual = result.ShouldBeSuccess();
        actual.ShouldBe(guid);
    }

    [Test]
    public void Validate_Should_ReturnSuccess_WithProvidedValue()
    {
        var guid = Guid.CreateVersion7();
        var result = Result.Validate()
            .That(guid).NotEmpty().Version7()
            .ToResult("Hello");

        var actual = result.ShouldBeSuccess();
        actual.ShouldBe("Hello");
    }

    [Test]
    public void Validate_Should_ReturnValidationFailureSummary()
    {
        var guid = Guid.CreateVersion7();
        var result = Result.Validate()
            .That(guid).NotEmpty().Version4()
            .ToResult();

        var summaryFailure = result.ShouldBeFailureOfType<ValidationFailureSummary>();
        summaryFailure.Category.ShouldBe(FailureCategory.Validation);
        var validationFailure = summaryFailure.FailureMessages["guid"];
        validationFailure.Length.ShouldBe(1);
        validationFailure.Single().ShouldContain("version 4");
    }

    [Test]
    public void Validate_Should_Accumulate_MultipleFailures()
    {
        var value = "Hello";
        var result = Result.Validate()
            .That(value).MinLength(6)
            .That(value).Satisfies(_ => false)
            .ToResult();

        var summaryFailure = result.ShouldBeFailureOfType<ValidationFailureSummary>();
        var valueFailures = summaryFailure.FailureMessages["value"];
        valueFailures.Length.ShouldBe(2);
        valueFailures[0].ShouldContain("at least 6 characters");
        valueFailures[1].ShouldContain("satisfy a given condition");
    }

    [Test]
    public void Validate_Should_ApplyCultureInfo_WhenProvided()
    {
        var value = 1.15d;
        var result = Result.Validate(new CultureInfo("nl-NL"))
            .That(value).GreaterThan(2)
            .ToResult();

        var summaryFailure = result.ShouldBeFailureOfType<ValidationFailureSummary>();
        summaryFailure.FailureMessages["value"].Single().ShouldContain("1,15"); // with a comma as the decimal separator, as per Dutch culture
    }

    [Test]
    public void Validate_Should_ApplyToleranceProvider_WhenProvided()
    {
        var value = 1.0d + 1e-10; // Add less than the default tolerance to Validate it would be considered equal without custom tolerance provider
        var result = Result.Validate(toleranceProvider: new ZeroToleranceProvider())
            .That(value).AtMost(1.0d)
            .ToResult();

        result.ShouldBeFailure();

        // to show that the failure is due to the custom tolerance provider:
        Result.Validate()
            .That(value).AtMost(1.0d)
            .ToResult()
            .ShouldBeSuccess();
    }

    [Test]
    public void Validate_Should_ApplyCustomTimeProvider_WhenProvided()
    {
        // The FakeTimeProvider returns a fixed time of 2000-01-01T00:00:00Z,
        // so any time value that is not within the past 2 hours of that should fail.
        var timeProvider = new FakeTimeProvider();

        var value = DateTime.UtcNow.AddHours(-1);
        var result = Result.Validate(timeProvider: timeProvider)
            .That(value).WithinPast(TimeSpan.FromHours(2))
            .ToResult();

        var summaryFailure = result.ShouldBeFailureOfType<ValidationFailureSummary>();
        summaryFailure.FailureMessages["value"].Single().ShouldContain("01/01/2000");
    }

    private class FakeTimeProvider : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() =>
            new(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);
    }
}

//public class GuardResultExtensionsTests
//{
//    [Test]
//    public void WithMessage_Should_
//}