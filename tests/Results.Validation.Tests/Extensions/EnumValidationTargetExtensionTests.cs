using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Validation.Extensions;

namespace Toarnbeike.Results.Validation.Tests.Extensions;

public class EnumValidationTargetExtensionTests
{
    private enum TestEnum
    {
        A = 0,
        B = 1,
        C = 2
    }

    [Test]
    public void IsDefined_Should_ReturnFailure_WhenNotDefined()
    {
        var value = (TestEnum)99;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).IsDefined();

        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe($"Value must be a defined member of the {nameof(TestEnum)} enum.");
    }

    [Test]
    public void IsDefined_Should_ReturnSuccess_WhenValid()
    {
        var value = TestEnum.A;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).IsDefined();

        result.ShouldBeSuccess();
    }

    [Test]
    public void IsDefined_Should_UseCustomMessage_WhenProvided()
    {
        var value = (TestEnum)99;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).IsDefined("Custom message");

        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom message");
    }

    [Test]
    public void OneOf_Should_ReturnFailure_WhenNotAccepted()
    {
        var value = TestEnum.C;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).OneOf([TestEnum.A, TestEnum.B]);

        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Value must be one of [A, B].");
    }

    [Test]
    public void OneOf_Should_ReturnSuccess_WhenAccepted()
    {
        var value = TestEnum.A;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).OneOf([TestEnum.A, TestEnum.B]);

        result.ShouldBeSuccess();
    }

    [Test]
    public void OneOf_Should_UseCustomMessage_WhenProvided()
    {
        var value = TestEnum.C;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).OneOf([TestEnum.A], "Custom message");

        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom message");
    }

    [Test]
    public void NotOneOf_Should_ReturnFailure_WhenRejected()
    {
        var value = TestEnum.A;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).NotOneOf([TestEnum.A, TestEnum.B]);

        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Value must not be one of [A, B].");
    }

    [Test]
    public void NotOneOf_Should_ReturnSuccess_WhenNotRejected()
    {
        var value = TestEnum.C;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).NotOneOf([TestEnum.A, TestEnum.B]);

        result.ShouldBeSuccess();
    }

    [Test]
    public void NotOneOf_Should_UseCustomMessage_WhenProvided()
    {
        var value = TestEnum.A;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).NotOneOf([TestEnum.A], "Custom message");

        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom message");
    }
}
