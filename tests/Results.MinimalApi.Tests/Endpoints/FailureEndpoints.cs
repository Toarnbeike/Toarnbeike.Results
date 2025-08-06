using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Toarnbeike.Results.MinimalApi.Mapping;
using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.MinimalApi.Tests.Endpoints;

public static class FailureEndpoints
{
    public static IEndpointRouteBuilder MapFailureEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/failure", () => Result.Failure(new Failure("Test failure", "This is a test failure")))
           .AddEndpointFilter<ResultMappingEndpointFilter>();

        app.MapGet("/validationFailure", () => Result.Failure(new ValidationFailure("Name", "Value should not exceed 10 characters")))
            .AddEndpointFilter<ResultMappingEndpointFilter>();

        app.MapGet("/validationFailures", () => 
        {
            var failure1 = new ValidationFailure("Name", "Value should not exceed 10 characters");
            var failure2 = new ValidationFailure("Email", "Invalid email format");

            return Result.Failure(new ValidationFailures([failure1, failure2]));
        }).AddEndpointFilter<ResultMappingEndpointFilter>();

        return app;
    }
}