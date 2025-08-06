using Microsoft.Extensions.DependencyInjection;
using Toarnbeike.Results.MinimalApi.Mapping;

namespace Toarnbeike.Results.MinimalApi.DependencyInjection;

public static class ResultMappingRegistrationExtension
{
    public static IServiceCollection AddResultMapping(this IServiceCollection services)
    {
        services.AddSingleton<IResultMapper, ResultMapper>();
        return services;
    }
}
