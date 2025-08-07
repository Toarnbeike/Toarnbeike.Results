using Microsoft.AspNetCore.Mvc;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.MinimalApi.Mapping.Failures;

namespace Toarnbeike.Results.MinimalApi.Tests.Mapping.Failures;

public class ValidationFailureResultMapperTests
{
    [Fact]
    public void Map_ShouldReturnProblemDetails_WhenValidationFailureIsMapped()
    {
        var validationFailure = new ValidationFailure("Name", "Invalid input");
        var mapper = new ValidationFailureResultMapper();
        
        var response = mapper.Map(validationFailure);
        
        response.Title.ShouldBe("Validation Error");
        response.Status.ShouldBe(400);
        response.Detail.ShouldBe("Name: Invalid input");
        response.Type.ShouldBe("https://tools.ietf.org/html/rfc7231#section-6.5.1");
        response.Extensions["code"].ShouldBe("validation_Name");

        var errors = response.ShouldBeOfType<ValidationProblemDetails>().Errors;
        errors.ShouldContainKey("Name");
    }
}