using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Validation.Extensions;

namespace Toarnbeike.Results.Validation.Tests.Extensions;

public class GuidValidationTargetExtensionTests
{
    [Test]
    public void NotEmpty_Should_ReturnFailure_WhenEmpty()
    {
        var value = Guid.Empty;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).NotEmpty();

        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Guid must not be empty.");
    }

    [Test]
    public void NotEmpty_Should_ReturnSuccess_WhenNotEmpty()
    {
        var value = Guid.NewGuid();
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).NotEmpty();

        result.ShouldBeSuccess();
    }

    [Test]
    public void Version4_Should_ReturnSuccess_WhenVersion4()
    {
        var value = Guid.NewGuid();
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).Version4();

        result.ShouldBeSuccess();
    }

    [Test]
    public void Version4_Should_ReturnFailure_WhenNotVersion4()
    {
        var value = Guid.Empty;
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).Version4();

        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Guid must be version 4.");
    }

    [Test]
    public void Version7_Should_ReturnSuccess_WhenVersion7()
    {
        var value = Guid.CreateVersion7();
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).Version7();

        result.ShouldBeSuccess();
    }

    [Test]
    public void Version7_Should_ReturnFailure_WhenNotVersion7()
    {
        var value = Guid.NewGuid();
        var result =
            Result.Success()
            .WithValue(value)
            .Validate(x => x).Version7();

        var failure = result.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Guid must be version 7.");
    }
}
