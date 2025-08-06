using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Toarnbeike.Results.MinimalApi.Mapping;

namespace Toarnbeike.Results.MinimalApi.Tests;

public static class EndpointsWithCustomIResult
{
    public static void MapEndpointsWithCustomIResult(this WebApplication app)
    {
        app.MapGet("/customSuccess", () => new CustomIResult<string>())
           .AddEndpointFilter<ResultMappingEndpointFilter>();

        app.MapGet("/customSuccessNoTryGetValue", () => new CustomIResultWithoutTryGetValue<string>())
            .AddEndpointFilter<ResultMappingEndpointFilter>();
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