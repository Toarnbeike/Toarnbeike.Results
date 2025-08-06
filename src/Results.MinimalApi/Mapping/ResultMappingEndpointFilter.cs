using Microsoft.AspNetCore.Http;

namespace Toarnbeike.Results.MinimalApi.Mapping;

public sealed class ResultMappingEndpointFilter(IResultMapper resultMapper) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);

        return result switch
        {
            IToarnbeikeResult toarnbeikeResult => resultMapper.Map(toarnbeikeResult),
            _ => result
        };
    }
}
