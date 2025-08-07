using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Toarnbeike.Results.MinimalApi.DependencyInjection;

public static class ResultMappingEndpointGroupExtensions
{
    /// <summary>
    /// Creates a <see cref="RouteGroupBuilder"/> with the <see cref="ResultMappingEndpointFilter"/> applied,
    /// allowing automatic conversion of <see cref="Toarnbeike.Results.Result" /> objects to <see cref="Microsoft.AspNetCore.Http.IResult"/>.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance.</param>
    /// <param name="prefix">The route prefix for the group.</param>
    /// <returns>A <see cref="RouteGroupBuilder"/> with result mapping enabled.</returns>
    public static RouteGroupBuilder MapResultGroup(this IEndpointRouteBuilder app, string prefix)
        => app.MapGroup(prefix)
              .AddEndpointFilter<ResultMappingEndpointFilter>();
}