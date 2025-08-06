using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Toarnbeike.Results.MinimalApi.Mapping;
using AspNetResults = Microsoft.AspNetCore.Http.Results;

namespace Toarnbeike.Results.MinimalApi.Tests;

public static class Endpoints
{
    public static void MapMinimalApiTestEndpoints(this WebApplication app)
    {
        app.MapGet("/success", () => Result.Success())
           .AddEndpointFilter<ResultMappingEndpointFilter>();

        app.MapGet("/failure", () => Result.Failure(new Failure("Test failure", "This is a test failure")))
           .AddEndpointFilter<ResultMappingEndpointFilter>();

        app.MapGet("/noResult", () => AspNetResults.Ok("This endpoint does not use a Toarnbeike.Result"))
           .AddEndpointFilter<ResultMappingEndpointFilter>();
    }
}
