namespace Toarnbeike.Results.Messaging.Requests;

/// <summary>
/// Represents a request to change the state of the system without returning a value.
/// The handler produces a <see cref="Result"/> indicating success or failure.
/// </summary>
/// <remarks>
/// For commands that create new entities and need to return the created object,
/// use <see cref="ICreateCommand{TCreated}"/>.
/// </remarks>
public interface ICommand : IRequest<Result>;