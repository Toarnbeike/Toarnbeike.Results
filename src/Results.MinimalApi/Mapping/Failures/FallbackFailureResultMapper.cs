using Microsoft.AspNetCore.Mvc;

namespace Toarnbeike.Results.MinimalApi.Mapping.Failures;

/// <summary>
/// Fallback mapper for failures that do not have a specific mapper registered.
/// </summary>
internal class FallbackFailureResultMapper : FailureResultMapper<Failure>, IFallbackFailureResultMapper
{
    /// <summary>
    /// Fallback mapper for failures that do not have a specific mapper registered.
    /// </summary>
    /// <param name="failure">The failure to map.</param>
    /// <returns>A <see cref="ProblemDetails"/> with the failure message and a 400 status code.</returns>
    public override ProblemDetails Map(Failure failure)
    {
        return new ProblemDetails
        {
            Title = "Unmapped Failure occured",
            Detail = failure.Message,
            Status = 400,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Extensions = { ["code"] = failure.Code }
        };
    }
}