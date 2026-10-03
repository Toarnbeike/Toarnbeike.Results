using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Validation.Extensions;

namespace Toarnbeike.Results.Validation.Tests.Extensions;

public class NumericValidationTargetExtensionTests
{
    [Test]
    public void GreaterThan_Should_ReturnFailure_WhenNotGreater()
    {
        int value = 1;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).GreaterThan(2);

        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Value must be greater than 2.");
    }

    [Test]
    public void GreaterThan_Should_ReturnSuccess_WhenGreater()
    {
        int value = 3;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).GreaterThan(2);

        result.ShouldBeSuccess();
    }

    [Test]
    public void GreaterThan_Should_ReturnFailure_WhenNotGreater_WithCustomMessage()
    {
        int value = 1;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).GreaterThan(2, "Custom message");
        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom message");
    }

    [Test]
    public void LessThan_Should_ReturnFailure_WhenNotLess()
    {
        int value = 5;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).LessThan(4);

        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Value must be less than 4.");
    }

    [Test]
    public void LessThan_Should_ReturnSuccess_WhenLess()
    {
        int value = 3;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).LessThan(4);

        result.ShouldBeSuccess();
    }

    [Test]
    public void LessThan_Should_ReturnFailure_WhenNotLess_WithCustomMessage()
    {
        int value = 5;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).LessThan(4, "Custom message");
        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom message");
    }

    [Test]
    public void Positive_Should_ReturnFailure_WhenNotPositive()
    {
        int value = 0;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).Positive();

        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Value must be positive.");
    }

    [Test]
    public void Positive_Should_ReturnSuccess_WhenPositive()
    {
        int value = 1;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).Positive();

        result.ShouldBeSuccess();
    }

    [Test]
    public void Positive_Should_ReturnFailure_WhenNotPositive_WithCustomMessage()
    {
        int value = 0;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).Positive("Custom message");
        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom message");
    }
}
