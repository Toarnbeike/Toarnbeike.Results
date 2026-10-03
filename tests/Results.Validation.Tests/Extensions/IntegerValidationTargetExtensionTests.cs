using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Validation.Extensions;

namespace Toarnbeike.Results.Validation.Tests.Extensions;

public class IntegerValidationTargetExtensionTests
{
    [Test]
    public void AtLeast_Should_ReturnFailure_WhenBelow()
    {
        int value = 1;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).AtLeast(2);

        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Value must be at least 2.");
    }

    [Test]
    public void AtLeast_Should_ReturnSuccess_WhenAtLeast()
    {
        int value = 3;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).AtLeast(2);

        result.ShouldBeSuccess();
    }

    [Test]
    public void AtLeast_Should_ReturnFailure_WhenBelow_WithCustomMessage()
    {
        int value = 1;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).AtLeast(2, "Custom message");
        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom message");
    }

    [Test]
    public void AtMost_Should_ReturnFailure_WhenAbove()
    {
        int value = 5;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).AtMost(4);

        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Value must be at most 4.");
    }

    [Test]
    public void AtMost_Should_ReturnSuccess_WhenWithin()
    {
        int value = 4;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).AtMost(4);

        result.ShouldBeSuccess();
    }

    [Test]
    public void AtMost_Should_ReturnFailure_WhenAbove_WithCustomMessage()
    {
        int value = 5;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).AtMost(4, "Custom message");
        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom message");
    }

    [Test]
    public void Between_Should_ReturnFailure_WhenOutside()
    {
        int value = 1;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).Between(2, 3);

        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Value must be between 2 and 3.");
    }

    [Test]
    public void Between_Should_ReturnFailure_WhenOutside_WithCustomMessage()
    {
        int value = 1;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).Between(2, 3, "Custom message");
        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom message");
    }

    [Test]
    public void Between_Should_ReturnSuccess_WhenWithin()
    {
        int value = 2;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).Between(2, 3);

        result.ShouldBeSuccess();
    }

    [Test]
    public void MultipleOf_Should_ReturnFailure_WhenNotMultiple()
    {
        int value = 5;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).MultipleOf(2);

        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Value must be a multiple of 2.");
    }

    [Test]
    public void MultipleOf_Should_ReturnSuccess_WhenMultiple()
    {
        int value = 6;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).MultipleOf(2);

        result.ShouldBeSuccess();
    }

    [Test]
    public void MultipleOf_Should_ReturnFailure_WhenNotMultiple_WithCustomMessage()
    {
        int value = 5;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).MultipleOf(2, "Custom message");
        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom message");
    }
}