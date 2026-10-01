using FluentValidation.Results;

namespace Toarnbeike.Results.FluentValidation.Tests;

/// <summary>
/// Tests for <see cref="FluentValidationConverters"/>
/// </summary>
public class FluentValidationConvertersTests
{
    [Test]
    public void ToValidationFailure_Should_Map_PropertyName_And_ErrorMessage()
    {
        var fluentFailure = new ValidationFailure("Username", "Username is required");

        var result = fluentFailure.ToValidationFailure();

        result.Property.ShouldBe("Username");
        result.ValidationMessage.ShouldBe("Username is required");
    }

    [Test]
    public void ToValidationFailures_FromValidationResult_Should_Map_AllErrors()
    {
        var fluentResult = new ValidationResult(new List<ValidationFailure>
        {
            new("Username", "Required"),
            new("Password", "Too short")
        });

        var result = fluentResult.ToValidationFailures();

        result.FailureMessages.ShouldContainKey("Username");
        result.FailureMessages["Username"].ShouldContain("Required");

        result.FailureMessages.ShouldContainKey("Password");
        result.FailureMessages["Password"].ShouldContain("Too short");
    }

    [Test]
    public void ToValidationFailures_FromEnumerable_Should_Map_AllFailures()
    {
        var fluentFailures = new List<ValidationFailure>
        {
            new("Email", "Invalid format"),
            new("Email", "Must be unique"),
            new("Password", "Required")
        };

        var result = fluentFailures.ToValidationFailures();

        result.FailureMessages.ShouldContainKey("Email");
        result.FailureMessages["Email"].ShouldContain("Invalid format");
        result.FailureMessages["Email"].ShouldContain("Must be unique");

        result.FailureMessages.ShouldContainKey("Password");
        result.FailureMessages["Password"].ShouldContain("Required");
    }
}