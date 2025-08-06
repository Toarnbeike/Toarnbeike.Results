using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Toarnbeike.Results.MinimalApi.Tests;

public class FailureResultFilterTests(MinimalApiTestApp app) : IClassFixture<MinimalApiTestApp>
{
    private readonly HttpClient _client = app.Client;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    [Fact]
    public async Task Failure_Should_Return_400()
    {
        var response = await _client.GetAsync("/failure");
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsStringAsync();
        content.ShouldContain("This is a test failure");
    }

    [Fact]
    public async Task ValidationFailure_Should_ReturnProblemDetails()
    {
        var response = await _client.GetAsync("/validationFailure");
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonSerializer.Deserialize<ValidationProblemDetails>(content, _jsonOptions);

        problemDetails.ShouldNotBeNull();
        problemDetails.Title.ShouldBe("Validation Error");
        problemDetails.Detail.ShouldBe("Name: Value should not exceed 10 characters");
        problemDetails.Status.ShouldBe(400);
        problemDetails.Type.ShouldBe("https://tools.ietf.org/html/rfc7231#section-6.5.1");
        problemDetails.Extensions.ShouldContainKey("code");
        problemDetails.Extensions["code"]!.ToString().ShouldBe("validation_Name");
        problemDetails.Errors.ShouldContainKey("Name");
        problemDetails.Errors["Name"].Single().ShouldBe("Value should not exceed 10 characters");
    }

    [Fact]
    public async Task ValidationFailures_Should_ReturnProblemDetails()
    {
        var response = await _client.GetAsync("/validationFailures");
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonSerializer.Deserialize<ValidationProblemDetails>(content, _jsonOptions);

        problemDetails.ShouldNotBeNull();
        problemDetails.Title.ShouldBe("Validation Errors");
        problemDetails.Detail.ShouldBe("One or more validations failed:");
        problemDetails.Status.ShouldBe(400);
        problemDetails.Type.ShouldBe("https://tools.ietf.org/html/rfc7231#section-6.5.1");
        problemDetails.Extensions.ShouldContainKey("code");
        problemDetails.Extensions["code"]!.ToString().ShouldBe("validation_failures");
        problemDetails.Errors.ShouldContainKey("Name");
        problemDetails.Errors["Name"].Single().ShouldBe("Value should not exceed 10 characters");
        problemDetails.Errors.ShouldContainKey("Email");
        problemDetails.Errors["Email"].Single().ShouldBe("Invalid email format");
    }
}
