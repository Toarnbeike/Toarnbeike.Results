using Toarnbeike.Results.Messaging.Pipeline;
using Toarnbeike.Results.Messaging.Requests;

namespace Toarnbeike.Results.Messaging.Implementation;

/// <summary>
/// Executes a request pipeline by chaining multiple behaviours around a handler.
/// Behaviours are executed in the order they are registered.
/// </summary>
/// <typeparam name="TRequest">The type of the request being handled.</typeparam>
/// <typeparam name="TResponse">The type of response returned by the handler.</typeparam>
internal sealed class RequestPipelineExecutor<TRequest, TResponse>(
    IEnumerable<IPipelineBehaviour<TRequest, TResponse>> behaviours,
    IRequestHandler<TRequest, TResponse> handler)
    where TRequest : class, IRequest<TResponse>
    where TResponse : IResult
{
    /// <summary>
    /// Executes the request through the full pipeline, invoking each behaviour's PreHandleAsync
    /// and PostHandleAsync methods around the final handler.
    /// PreHandleAsync can short-circuit the pipeline by returning a Failure.
    /// PostHandleAsync can optionally return a Failure to override the response.
    /// </summary>
    public async Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        RequestHandlerDelegate<TResponse> handlerDelegate = () => handler.HandleAsync(request, cancellationToken);

        foreach (var behaviour in behaviours.Reverse())
        {
            var next = handlerDelegate;
            handlerDelegate = async () =>
            {
                var preResult = await behaviour.PreHandleAsync(request, cancellationToken);
                if (preResult.TryGetFailure(out var failure))
                {
                    return FailureToTResponse(failure);
                }

                var response = await next();
                var postResult = await behaviour.PostHandleAsync(request, response, cancellationToken);
                return postResult.TryGetFailure(out failure)
                    ? FailureToTResponse(failure)
                    : response;
            };
        }

        return await handlerDelegate();
    }

    /// <summary>
    /// Converts a <see cref="Failure"/> into the appropriate <typeparamref name="TResponse"/> type,
    /// either <see cref="Result"/> (for commands) or <see cref="Result{TValue}"/> (for queries).
    /// </summary>
    /// <param name="failure">The <see cref="Failure"/> to convert.</param>
    /// <returns>A <typeparamref name="TResponse"/> instance representing the failure.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <typeparamref name="TResponse"/> is not <see cref="Result"/> or <see cref="Result{TValue}"/>.
    /// </exception>
    private static TResponse FailureToTResponse(Failure failure)
    {
        // Determine the concrete type of TResponse at runtime
        var responseType = typeof(TResponse);

        // Case 1: TResponse is a plain Result (typically for commands)
        if (responseType == typeof(Result))
        {
            // Convert Failure to Result using the existing factory method
            return (TResponse)(IResult)Result.Failure(failure);
        }

        // Case 2: TResponse is a Result<TValue> (typically for queries)
        if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(Result<>))
        {
            // Extract the generic type parameter (TValue)
            var valueType = responseType.GetGenericArguments()[0];

            // Build the closed generic Result<TValue> type
            var closedResultType = typeof(Result<>).MakeGenericType(valueType);

            // Get the static Failure(Failure) method on Result<TValue>
            var failureMethod = closedResultType.GetMethod(
                nameof(Result.Failure),
                [typeof(Failure)]
            );

            if (failureMethod != null)
            {
                // Invoke the static method: Result<TValue>.Failure(failure)
                return (TResponse)failureMethod.Invoke(null, [failure])!;
            }
        }

        // If TResponse is neither Result nor Result<T>, we cannot convert
        throw new InvalidOperationException($"Cannot convert Failure to {typeof(TResponse)}");
    }
}