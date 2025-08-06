using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Toarnbeike.Results.MinimalApi.Mapping;
using AspNetResults = Microsoft.AspNetCore.Http.Results;

namespace Toarnbeike.Results.MinimalApi.Tests.Endpoints;

public static class SuccessEndpoints
{
    public static IEndpointRouteBuilder MapSuccessEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/success", () => Result.Success())
           .AddEndpointFilter<ResultMappingEndpointFilter>();

        app.MapGet("/successWithValue", () => Result.Success("This is a test success with value"))
            .AddEndpointFilter<ResultMappingEndpointFilter>();

        app.MapGet("/noResult", () => AspNetResults.Ok("This endpoint does not use a Toarnbeike.Result"))
            .AddEndpointFilter<ResultMappingEndpointFilter>();

        return app;
    }
}