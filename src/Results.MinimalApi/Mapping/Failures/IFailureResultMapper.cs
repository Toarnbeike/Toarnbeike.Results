using Microsoft.AspNetCore.Mvc;

namespace Toarnbeike.Results.MinimalApi.Mapping.Failures;

public interface IFailureResultMapper
{
    /// <summary>
    /// The type of <see cref="Failure"/> that this mapper can handle.
    /// </summary>
    Type FailureType { get; }

    /// <summary>
    /// Mapping function that maps the specified <paramref name="failure"/> to an <see cref="IAspNetResult"/>.
    /// </summary>
    /// <param name="failure">The failure that must be mapped.</param>
    /// <returns>A new <see cref="Microsoft.AspNetCore.Http.IResult"/> with the details from this specific failure.</returns>
    ProblemDetails Map(Failure failure);
}

public interface IFailureResultMapper<TFailure> : IFailureResultMapper where TFailure : Failure
{
    /// <summary>
    /// Mapping function that maps the specified <paramref name="failure"/> to an <see cref="IAspNetResult"/>.
    /// </summary>
    /// <param name="failure">The failure that must be mapped.</param>
    /// <returns>A new <see cref="Microsoft.AspNetCore.Http.IResult"/> with the details from this specific failure.</returns>
    ProblemDetails Map(TFailure failure);
}
