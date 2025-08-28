using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Toarnbeike.Results.Messaging.Pipeline;
using Toarnbeike.Results.Messaging.Pipeline.PerformanceLogging;
using Toarnbeike.Results.Messaging.Pipeline.Validation;

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

    /// <summary>
    /// Add <see cref="PerformanceLoggingBehaviour{TRequest, TResponse}" /> as pipeline behaviour.
    /// The config can be manipulated via the action delegate, but it can also be read from config.
    /// </summary>
    public RequestMessagingOptions AddPerformanceLoggingBehavior(Action<PerformanceLoggingOptions>? configure = null)
    {
        PipelineBehaviours.Add(typeof(PerformanceLoggingBehaviour<,>));

        ConfigureAdditionalServices += services =>
        {
            services.AddOptions<PerformanceLoggingOptions>()
                .BindConfiguration(PerformanceLoggingOptions.ConfigSectionName);

            if (configure is not null)
            {
                services.Configure(configure);
            }
        };

        return this;
    }

    /// <summary>
    /// Add a pipeline behaviour that validates requests using FluentValidation.
    /// This will register all validators found in the specified handler assemblies.
    /// </summary>
    /// <returns>The RequestMessagingOptions for fluent configuration.</returns>
    public RequestMessagingOptions AddValidationBehaviour()
    {
        ConfigureAdditionalServices += services =>
        {
            services.AddValidatorsFromAssemblies(HandlerAssemblies, includeInternalTypes: true); // registreert IValidator<T>
            services.AddScoped(typeof(IPipelineBehaviour<,>), typeof(FluentValidationPipelineBehaviour<,>));
        };

        return this;
    }
}