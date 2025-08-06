using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using Toarnbeike.Results.MinimalApi.Mapping.Failures;

namespace Toarnbeike.Results.MinimalApi.DependencyInjection;

/// <summary>
/// Builder for configuring result mapping in the ASP.NET Core dependency injection container.
/// </summary>
public sealed class ResultMappingBuilder
{
    private readonly IServiceCollection _services;

    internal ResultMappingBuilder(IServiceCollection services)
    {
        _services = services;
    }

    /// <summary>
    /// Registers an additional <see cref="IFailureResultMapper"/> type.
    /// </summary>
    public ResultMappingBuilder AddMapper<T>() where T : class, IFailureResultMapper
    {
        _services.AddSingleton<IFailureResultMapper, T>();
        return this;
    }

    /// <summary>
    /// Registers all <see cref="IFailureResultMapper"/> implementations from the given assembly.
    /// </summary>
    public ResultMappingBuilder AddMappersFromAssembly(Assembly assembly)
    {
        var mapperTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface
                && typeof(IFailureResultMapper).IsAssignableFrom(t));

        foreach (var type in mapperTypes)
        {
            _services.AddSingleton(typeof(IFailureResultMapper), type);
        }

        return this;
    }

    /// <summary>
    /// Registers all <see cref="IFailureResultMapper"/> implementations from the assembly containing the specified type.
    /// </summary>
    public ResultMappingBuilder AddMappersFromAssemblyContaining<T>()
    {
        return AddMappersFromAssembly(typeof(T).Assembly);
    }

    /// <summary>
    /// Replaces the fallback mapper with a custom implementation.
    /// </summary>
    public ResultMappingBuilder UseFallback<T>() where T : class, IFallbackFailureResultMapper
    {
        _services.Replace(ServiceDescriptor.Singleton<IFallbackFailureResultMapper, T>());
        return this;
    }
}
