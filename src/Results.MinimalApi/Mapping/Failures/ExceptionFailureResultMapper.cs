using Microsoft.AspNetCore.Mvc;
using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.MinimalApi.Mapping.Failures;

/// <summary>
/// Maps an <see cref="ExceptionFailure"/> to a <see cref="ProblemDetails"/> response suitable for API clients.
/// </summary>
/// <remarks>
/// This default mapper returns a generic 500 Internal Server Error response with minimal information,
/// avoiding exposure of sensitive exception details. It is intended to be secure and safe for production use.
/// <para>
/// For more detailed error information, such as stack traces, consider implementing and registering
/// a custom mapper, for example a <c>DebugExceptionFailureResultMapper</c>.
/// </para>
/// </remarks>
internal sealed class ExceptionFailureResultMapper : FailureResultMapper<ExceptionFailure>
{
    /// <summary>
    /// Converts the specified <paramref name="failure"/> into a <see cref="ProblemDetails"/> instance.
    /// </summary>
    /// <param name="failure">The exception failure to convert.</param>
    /// <returns>
    /// A <see cref="ProblemDetails"/> representing the exception, with status code 500 and a generic error detail.
    /// </returns>
    public override ProblemDetails Map(ExceptionFailure failure)
    {
        return new ProblemDetails
        {
            Title = "Internal Server Error",
            Status = 500,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Detail = "An unexpected error occurred.",
            Extensions =
            {
                ["code"] = "exception"
            }
        };
    }
}
