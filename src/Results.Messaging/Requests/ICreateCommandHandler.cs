namespace Toarnbeike.Results.Messaging.Requests;

/// <summary>
/// Defines a handler for an <see cref="ICreateCommand{TCreated}"/>.
/// </summary>
/// <typeparam name="TRequest">The type of create command request being handled.</typeparam>
/// <typeparam name="TCreated">The type of the object being created.</typeparam>
public interface ICreateCommandHandler<in TRequest, TCreated> : IRequestHandler<TRequest, Result<TCreated>>
    where TRequest : ICreateCommand<TCreated>;