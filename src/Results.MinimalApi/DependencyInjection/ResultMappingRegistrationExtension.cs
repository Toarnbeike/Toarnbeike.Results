using Microsoft.Extensions.DependencyInjection;
using Toarnbeike.Results.MinimalApi.Mapping;
using Toarnbeike.Results.MinimalApi.Mapping.Failures;

namespace Toarnbeike.Results.MinimalApi.DependencyInjection;

public static class ResultMappingRegistrationExtension
{
    public static IServiceCollection AddResultMapping(this IServiceCollection services)
    {
        services.AddSingleton<IResultMapper, ResultMapper>();

        // add the default failure mappers:
        services.AddSingleton<IFailureResultMapper, AggregateFailureResultMapper>();
        services.AddSingleton<IFailureResultMapper, ExceptionFailureResultMapper>();
        services.AddSingleton<IFailureResultMapper, ValidationFailureResultMapper>();
        services.AddSingleton<IFailureResultMapper, ValidationFailuresResultMapper>();
        services.AddSingleton<IFallbackFailureResultMapper, FallbackFailureResultMapper>();

        return services;
    }
}
