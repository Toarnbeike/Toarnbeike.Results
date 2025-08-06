using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.MinimalApi.Mapping.Failures;

/// <summary>
/// Maps a single <see cref="ValidationFailure"/> to an HTTP <see cref="ValidationProblemDetails"/> response.
/// </summary>
/// <remarks>
/// This mapper converts a single validation failure into a standardized <c>400 Bad Request</c>
/// response using the <see cref="ValidationProblemDetails"/> format. The error is placed into the
/// <see cref="ValidationProblemDetails.Errors"/> dictionary, keyed by the property name.
/// </remarks>
internal sealed class ValidationFailureResultMapper : FailureResultMapper<ValidationFailure>
{
    /// <summary>
    /// Converts the specified <paramref name="failure"/> into a <see cref="ValidationProblemDetails"/> instance.
    /// </summary>
    /// <param name="failure">The validation failure to convert.</param>
    /// <returns>
    /// A <see cref="ValidationProblemDetails"/> representing the validation error, with status code 400 and additional
    /// metadata including the validation <c>code</c> in the <see cref="ProblemDetails.Extensions"/> dictionary.
    /// </returns>
    public override ProblemDetails Map(ValidationFailure failure)
    {
        var failureDictionary = new Dictionary<string, string[]>
        { 
            [failure.Property] = [failure.ValidationMessage] 
        };

        return new ValidationProblemDetails(failureDictionary)
        {
            Title = "Validation Error",
            Detail = failure.Message,
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Extensions =
            {
                ["code"] = failure.Code
            }
        };
    }
}
