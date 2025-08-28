namespace Toarnbeike.Results.Messaging.Implementation;

/// <summary>
/// Delegate representing the remainder of the pipeline, including the final handler.
/// </summary>
/// <typeparam name="TResponse">The type of response produced.</typeparam>
internal delegate Task<TResponse> RequestHandlerDelegate<TResponse>();