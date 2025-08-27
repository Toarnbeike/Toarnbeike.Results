namespace Toarnbeike.Results.Messaging.Requests;

/// <summary>
/// Defines a handler for an <see cref="ICommand"/>.
/// </summary>
/// <typeparam name="TRequest">The type of command request being handled.</typeparam>
public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest, Result>
    where TRequest : ICommand;