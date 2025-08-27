using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Toarnbeike.Results.Messaging.DependencyInjection;

/// <summary>
/// Provides configuration options for setting up request messaging services.
/// </summary>
/// <remarks>
/// This class allows customization of the request messaging system by specifying assemblies
/// to scan for request handlers, adding pipeline behaviors, and configuring additional services.
/// </remarks>
public sealed class RequestMessagingOptions
{
    internal List<Assembly> HandlerAssemblies { get; } = [];
    internal List<Type> PipelineBehaviours { get; } = [];

    public Action<IServiceCollection>? ConfigureAdditionalServices { get; set; }

    /// <summary>
    /// Add a single assembly to scan for request handlers.
    /// </summary>
    /// <returns>The RequestMessagingOptions for fluent configuration.</returns>
    public RequestMessagingOptions FromAssembly(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));
        HandlerAssemblies.Add(assembly);
        return this;
    }

    /// <summary>
    /// Add multiple assemblies to scan for request handlers.
    /// </summary>
    /// <returns>The RequestMessagingOptions for fluent configuration.</returns>
    public RequestMessagingOptions FromAssemblies(params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(assemblies, nameof(assemblies));
        HandlerAssemblies.AddRange(assemblies);
        return this;
    }

    /// <summary>
    /// Add the assembly that contains the specified type to scan for request handlers.
    /// </summary>
    /// <returns>The RequestMessagingOptions for fluent configuration.</returns>
    public RequestMessagingOptions FromAssemblyContaining<T>()
    {
        ArgumentNullException.ThrowIfNull(typeof(T), nameof(T));
        return FromAssembly(typeof(T).Assembly);
    }

    /// <summary>
    /// Add a pipeline behavior to be executed in the specified order.
    /// </summary>
    public RequestMessagingOptions AddPipelineBehaviour(Type behaviorType)
    {
        ArgumentNullException.ThrowIfNull(behaviorType);
        PipelineBehaviours.Add(behaviorType);
        return this;
    }
}