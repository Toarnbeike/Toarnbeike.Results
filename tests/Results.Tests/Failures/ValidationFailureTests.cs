using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Tests.Failures;

/// <summary>
/// Tests for the <see cref="ValidationFailure"/> record.
/// </summary>
public class ValidationFailureTests
{
    [Fact]
    public void ValidationFailure_Should_BeCreatedFromAPropertyAndAMessage()
    {
        var failure = new ValidationFailure("Property", "Something is wrong with this property");
        failure.Property.ShouldBe("Property");
        failure.ValidationMessage.ShouldBe("Something is wrong with this property");
    }

    [Fact]
    public void ValidationFailure_Should_PopulateCode_WithValidationProperty()
    {
        var failure = new ValidationFailure("Property", "Something is wrong with this property");
        failure.Code.ShouldBe("validation_Property");
    }

    [Fact]
    public void ValidationFailure_Should_PopulateBaseMessage_WithPropertyAndMessage()
    {
        var failure = new ValidationFailure("Property", "Something is wrong with this property");
        var baseFailure = failure as Failure;
        baseFailure.Message.ShouldBe($"Property: Something is wrong with this property");
    }
}

