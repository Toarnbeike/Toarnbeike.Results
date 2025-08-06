using Microsoft.AspNetCore.Mvc;

namespace Toarnbeike.Results.MinimalApi.Mapping.Failures;

/// <summary>
/// Base class for mapping a specific <see cref="Failure"/> type to an <see cref="IAspNetResult"/>.
/// </summary>
/// <typeparam name="TFailure">The type of <see cref="Failure"/> this mapper can map.</typeparam>
public abstract class FailureResultMapper<TFailure> : IFailureResultMapper<TFailure> where TFailure : Failure
{
    /// <inheritdoc />
    public Type FailureType => typeof(TFailure);

    /// <inheritdoc />
    public abstract ProblemDetails Map(TFailure failure);

    /// <inheritdoc />
    /// <remarks>Delegate the handling of the mapping from the non generic <see cref="IFailureResultMapper"/>
    /// to the type safe Map variant.</remarks>
    ProblemDetails IFailureResultMapper.Map(Failure failure) => Map((TFailure)failure);
}