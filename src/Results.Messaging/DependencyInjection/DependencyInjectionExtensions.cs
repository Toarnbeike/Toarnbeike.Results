using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Reflection;
using Toarnbeike.Results.Messaging.Implementation;
using Toarnbeike.Results.Messaging.Pipeline;
using Toarnbeike.Results.Messaging.Requests;

namespace Toarnbeike.Results.Messaging.DependencyInjection;

/// <summary>
/// Extension methods to register Toarnbeike.Results.Messaging with dependency injection.
/// </summary>
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Add request messaging services to the service collection with 
    /// the default configuration and without assemblies to scan.
    /// </summary>
    public static IServiceCollection AddRequestMessaging(this IServiceCollection services) => services.AddRequestMessaging(_ => { /* use defaults */ });

    /// <summary>
    /// Add request messaging services to the service collection with custom configuration
    /// </summary>
    /// <param name="services">The service collection to register to.</param>
    /// <param name="configure">Delegate to configure the options for the request messaging.</param>
    public static IServiceCollection AddRequestMessaging(this IServiceCollection services, Action<RequestMessagingOptions> configure)
    {
        var options = new RequestMessagingOptions();
        configure(options);

        services.TryAddSingleton<IRequestDispatcher, RequestDispatcher>();

        // Register a fallback no-op logger if none is provided because RequestDispatcher requires an ILogger
        services.TryAddSingleton(typeof(ILogger<>), typeof(NullLogger<>));

        foreach (var assembly in options.HandlerAssemblies)
        {
            services.AddRequestHandlersFromAssembly(assembly);
        }

        foreach (var behaviorType in options.PipelineBehaviours)
        {
            services.AddScoped(typeof(IPipelineBehaviour<,>), behaviorType);
        }

        options.ConfigureAdditionalServices?.Invoke(services);

        return services;
    }

    private static void AddRequestHandlersFromAssembly(this IServiceCollection services, Assembly handlerAssembly)
    {
        var handlerTypes = handlerAssembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .SelectMany(t =>
                t.GetInterfaces()
                 .Where(i => i.IsGenericType && IsRequestHandlerInterface(i))
                 .Select(i => new { Interface = i, Implementation = t }))
            .Distinct();

        foreach (var registration in handlerTypes)
        {
            services.TryAddTransient(registration.Interface, registration.Implementation);
        }
    }

    private static bool IsRequestHandlerInterface(Type interfaceType)
    {
        var definition = interfaceType.GetGenericTypeDefinition();

        if (definition == typeof(IRequestHandler<,>))
            return true;

        // Traverse base interfaces recursively
        return interfaceType.GetInterfaces()
            .Any(baseInterface =>
                baseInterface.IsGenericType &&
                baseInterface.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));
    }
}