using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Tests.Failures;

/// <summary>
/// Tests for the <see cref="ValidationFailure"/> record.
/// </summary>
public class ValidationFailureTests
{
    [Test]
    public void ValidationFailure_Should_BeCreatedFromAPropertyAndAMessage()
    {
        var failure = new ValidationFailure("Property", "Something is wrong with this property");
        failure.Property.ShouldBe("Property");
        failure.ValidationMessage.ShouldBe("Something is wrong with this property");
    }

    [Test]
    public void ValidationFailure_Should_PopulateCode_WithValidationProperty()
    {
        var failure = new ValidationFailure("Property", "Something is wrong with this property");
        failure.Category.ShouldBe(FailureCategory.Validation);
    }

    [Test]
    public void ValidationFailure_Should_PopulateBaseMessage_WithPropertyAndMessage()
    {
        var failure = new ValidationFailure("Property", "Something is wrong with this property");
        var baseFailure = failure as Failure;
        baseFailure.Message.ShouldBe($"Validation failed for Property with message Something is wrong with this property");
    }
}

