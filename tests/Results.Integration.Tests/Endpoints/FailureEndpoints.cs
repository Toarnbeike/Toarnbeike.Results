using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Collections;
using Toarnbeike.Results.MinimalApi;

namespace Toarnbeike.Results.Integration.Tests.Endpoints;

public static class FailureEndpoints
{
    public static IEndpointRouteBuilder MapFailureEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/failure", () => Result.Failure(new Failure("Test failure", "This is a test failure")))
           .AddEndpointFilter<ResultMappingEndpointFilter>();

        app.MapGet("/exceptionFailure", () => Result.Try(() => throw new Exception("This is a test exception failure")))
            .AddEndpointFilter<ResultMappingEndpointFilter>();

        app.MapGet("/validationFailure", () => Result.Failure(new ValidationFailure("Name", "Value should not exceed 10 characters")))
            .AddEndpointFilter<ResultMappingEndpointFilter>();

        app.MapGet("/validationFailures", () => 
        {
            var failure1 = new ValidationFailure("Name", "Value should not exceed 10 characters");
            var failure2 = new ValidationFailure("Email", "Invalid email format");

            return Result.Failure(new ValidationFailures([failure1, failure2]));
        }).AddEndpointFilter<ResultMappingEndpointFilter>();

        app.MapGet("/aggregateFailures", () =>
        {
            IEnumerable<Result> results =
            [
                Result.Try(() => throw new Exception("This is a test exception")),
                Result.Failure(new ValidationFailure("Name", "Value should not exceed 10 characters"))
            ];
            return results.Aggregate();

        }).AddEndpointFilter<ResultMappingEndpointFilter>();

        app.MapGet("/aggregateFailuresUnmapped", () =>
        {
            IEnumerable<Result> results =
            [
                Result.Failure(new Failure("Test failure", "This is a test failure")),
                Result.Failure(new ValidationFailure("Name", "Value should not exceed 10 characters"))
            ];
            return results.Aggregate();

        }).AddEndpointFilter<ResultMappingEndpointFilter>();

        return app;
    }
}