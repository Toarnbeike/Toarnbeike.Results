using Microsoft.AspNetCore.Mvc;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.MinimalApi.Mapping.Failures;

namespace Toarnbeike.Results.MinimalApi.Tests.Mapping.Failures;

public class ValidationFailuresResultMapperTests
{
    [Fact]
    public void Map_MapsValidationFailureToProblemDetails()
    {
        // Arrange
        var failure1 = new ValidationFailure("Name", "Invalid input");
        var failure2 = new ValidationFailure("Email", "Email is required");

        var failures = new ValidationFailures(new[] { failure1, failure2 });

        var mapper = new ValidationFailuresResultMapper();

        var result = mapper.Map(failures);

        result.ShouldBeOfType<ValidationProblemDetails>();
        var validationDetails = (ValidationProblemDetails)result;
        validationDetails.Title.ShouldBe("Validation Errors");
        validationDetails.Detail.ShouldBe("One or more validations failed:");
        validationDetails.Status.ShouldBe(400);
        validationDetails.Type.ShouldBe("https://tools.ietf.org/html/rfc7231#section-6.5.1");
        validationDetails.Extensions["code"].ShouldBe("validation_failures");

        validationDetails.Errors.ShouldContainKey("Name");
        validationDetails.Errors["Name"].ShouldBe(["Invalid input"]);
        validationDetails.Errors.ShouldContainKey("Email");
        validationDetails.Errors["Email"].ShouldBe(["Email is required"]);
    }
}