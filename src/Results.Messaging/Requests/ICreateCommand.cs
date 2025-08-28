namespace Toarnbeike.Results.Messaging.Requests;

/// <summary>
/// Represents a request to create a new object in the system.
/// The handler produces a <see cref="Result{TCreated}"/> containing the created object.
/// </summary>
/// <remarks>
/// This allows the caller to access information about the newly created object
/// (such as its identifier) to compose an appropriate response.
/// </remarks>
/// <typeparam name="TCreated">The type of the object being created.</typeparam>
public interface ICreateCommand<TCreated> : IRequest<Result<TCreated>>;