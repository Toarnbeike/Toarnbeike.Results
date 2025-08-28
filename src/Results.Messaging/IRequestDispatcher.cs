using Toarnbeike.Results.Messaging.Requests;

namespace Toarnbeike.Results.Messaging;

/// <summary>
/// Defines the entrypoint for dispatching requests within the messaging system.
/// Handles any <see cref="IRequest{TResponse}"/> and returns a response of type <c>TResponse</c>.
/// </summary>
/// <remarks>
/// This interface is intended for consumers of the library to send requests without
/// needing to know which handler processes them. Implementations are provided internally
/// and registered via dependency injection (<see cref="DependencyInjectionExtensions.AddRequestMessaging()"/>.
/// </remarks>
public interface IRequestDispatcher
{
    /// <summary>
    /// Dispatches the specified request to the appropriate handler and returns its result.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected from the handler. Must implement <see cref="IResult"/>.</typeparam>
    /// <param name="request">The request to handle. Must implement <see cref="IRequest{TResponse}"/>.</param>
    /// <param name="cancellationToken">Optional token to cancel the asynchronous handling.</param>
    /// <returns>
    /// A <typeparamref name="TResponse"/> representing the outcome of the request.
    /// Always returns a <see cref="IResult"/>-based response, encapsulating success or failure.
    /// </returns>
    Task<TResponse> DispatchAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
        where TResponse : IResult;
}
