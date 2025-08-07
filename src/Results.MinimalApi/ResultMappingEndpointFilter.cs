using Microsoft.AspNetCore.Http;
using Toarnbeike.Results.MinimalApi.Mapping;

namespace Toarnbeike.Results.MinimalApi;

/// <summary>
/// Filter that maps an <see cref="IToarnbeikeResult"/> from an endpoint to an <see cref="Microsoft.AspNetCore.Http.IResult"/>.
/// </summary>
/// <param name="resultMapper">The mapper that handles the conversions.</param>
public sealed class ResultMappingEndpointFilter(IResultMapper resultMapper) : IEndpointFilter
{
    /// <inheritdoc />
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
