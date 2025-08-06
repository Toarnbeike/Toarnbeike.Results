using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Toarnbeike.Results.MinimalApi.Mapping;

namespace Toarnbeike.Results.MinimalApi.Tests.Endpoints;

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

    private class CustomIResult<TValue> : IResult
    {
        public bool IsSuccess => true;
        public bool IsFailure => false;
        public bool TryGetFailure(out Failure failure)
        {
            failure = default!;
            return false;
        }
        public static bool TryGetValue(out string value)
        {
            value = "Success";
            return true;
        }
    }

    private class CustomIResultWithoutTryGetValue<TValue> : IResult
    {
        public bool IsSuccess => true;
        public bool IsFailure => false;
        public bool TryGetFailure(out Failure failure)
        {
            failure = default!;
            return false;
        }
    }
}