using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Toarnbeike.Results.MinimalApi;

namespace Toarnbeike.Results.Integration.Tests.Endpoints;

public static class EndpointsWithCustomIResult
{
    public static IEndpointRouteBuilder MapEndpointsWithCustomIResult(this IEndpointRouteBuilder app)
    {
        app.MapGet("/customSuccess", () => new CustomIResult<string>())
           .AddEndpointFilter<ResultMappingEndpointFilter>();

        app.MapGet("/customSuccessNoTryGetValue", () => new CustomIResultWithoutTryGetValue<string>())
            .AddEndpointFilter<ResultMappingEndpointFilter>();

        return app;
    }


}