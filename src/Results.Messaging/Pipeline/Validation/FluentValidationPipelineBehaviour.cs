using FluentValidation;
using Microsoft.Extensions.Logging;
using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.FluentValidation;
using Toarnbeike.Results.Messaging.Extensions;
using Toarnbeike.Results.Messaging.Requests;

namespace Toarnbeike.Results.Messaging.Pipeline.Validation;

/// <summary>
/// Validation behaviour that validates an incoming request using the configured validators.
/// It will return a <see cref="Result"/> or <see cref="Result{TResponse}"/> with a <see cref="ValidationFailures"/> if validation fails.
/// </summary>
/// <typeparam name="TRequest">The type of the request</typeparam>
/// <typeparam name="TResponse">The type of the response that matches the request</typeparam>
/// <param name="validators">Collection of <see cref="IValidator"/> that are registered via dependency injection.</param>
internal sealed class FluentValidationPipelineBehaviour<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators,
    ILogger<FluentValidationPipelineBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehaviour<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse> where TResponse : IResult
{
    public async Task<Result> PreHandleAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        var requestName = typeof(TRequest).PrettyName();
        
        if (!validators.Any())
        {
            logger.LogDebug("Validated {Request} - No validators registered.", requestName);
            return Result.Success();
        }

        return await Result.Success(request)
            .ValidateAsync(validators)
            .Tap(_ => logger.LogDebug("Validated {Request} - Valid request.", requestName))
            .TapFailure(failure => logger.LogInformation("Validated {Request} - Invalid request, {Count} failure(s)", requestName, (failure as ValidationFailures)!.Failures.Count));
    }

    public Task<Result> PostHandleAsync(TRequest request, TResponse response, CancellationToken cancellationToken = default)
    {
        return Result.SuccessTask();
    }
}
