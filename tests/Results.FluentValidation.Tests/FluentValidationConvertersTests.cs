using FluentValidation.Results;

namespace Toarnbeike.Results.FluentValidation.Tests;

/// <summary>
/// Tests for <see cref="FluentValidationConverters"/>
/// </summary>
public class FluentValidationConvertersTests
{
    [Fact]
    public void ToValidationFailure_Should_Map_PropertyName_And_ErrorMessage()
    {
        var fluentFailure = new ValidationFailure("Username", "Username is required");

        var result = fluentFailure.ToValidationFailure();

        result.Property.ShouldBe("Username");
        result.ValidationMessage.ShouldBe("Username is required");
    }

    [Fact]
    public void ToValidationFailures_FromValidationResult_Should_Map_AllErrors()
    {
        var fluentResult = new ValidationResult(new List<ValidationFailure>
        {
            new("Username", "Required"),
            new("Password", "Too short")
        });

        var result = fluentResult.ToValidationFailures();

        result.Failures.ShouldContainKey("Username");
        result.Failures["Username"].ShouldContain("Required");

        result.Failures.ShouldContainKey("Password");
        result.Failures["Password"].ShouldContain("Too short");
    }

    [Fact]
    public void ToValidationFailures_FromEnumerable_Should_Map_AllFailures()
    {
        var fluentFailures = new List<ValidationFailure>
        {
            new("Email", "Invalid format"),
            new("Email", "Must be unique"),
            new("Password", "Required")
        };

        var result = fluentFailures.ToValidationFailures();

        result.Failures.ShouldContainKey("Email");
        result.Failures["Email"].ShouldContain("Invalid format");
        result.Failures["Email"].ShouldContain("Must be unique");

        result.Failures.ShouldContainKey("Password");
        result.Failures["Password"].ShouldContain("Required");
    }
}