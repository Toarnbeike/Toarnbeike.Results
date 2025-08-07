using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;
using Toarnbeike.Results.Integration.Tests.Examples;

namespace Toarnbeike.Results.Integration.Tests;

public class CustomerTests(MinimalApiTestApp app) : IClassFixture<MinimalApiTestApp>
{
    private readonly HttpClient _client = app.Client;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    [Fact]
    public async Task GetById_ShouldReturnCustomer_WhenExists()
    {
        var customerId = "1";

        var response = await _client.GetAsync($"/customers/{customerId}");

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();

        var customer = JsonSerializer.Deserialize<Customer>(content, _jsonOptions);
        customer.ShouldNotBeNull();
        customer.Id.ShouldBe("1");
        customer.Name.ShouldBe("Alice");
        customer.Email.ShouldBe("alice@test.com");
    }

    [Fact]
    public async Task GetById_ShouldReturnFailure_WhenCustomerDoesNotExist()
    {
        var customerId = "999";

        var response = await _client.GetAsync($"/customers/{customerId}");

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();

        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content, _jsonOptions);
        problemDetails.ShouldNotBeNull();
        problemDetails.Title.ShouldBe("Unmapped Failure occured");
        problemDetails.Detail.ShouldBe("Customer not found");
    }

    [Fact]
    public async Task Post_ShouldReturn204NoContent_WhenCustomerIsInserted()
    {
        var newCustomer = new Customer("2", "Bob", "bob@test.com");

        var response = await _client.PostAsJsonAsync("/customers", newCustomer, _jsonOptions);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Post_ShouldReturn400BadRequest_WhenValidatorFails()
    {
        var invalidCustomer = new Customer("", "VeryLongName", "invalid-email");

        var response = await _client.PostAsJsonAsync("/customers", invalidCustomer, _jsonOptions);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();

        var problemDetails = JsonSerializer.Deserialize<ValidationProblemDetails>(content, _jsonOptions);
        problemDetails.ShouldNotBeNull();
        problemDetails.Title.ShouldBe("Validation Errors");
        problemDetails.Errors.ShouldContainKey("Id");
        problemDetails.Errors.ShouldContainKey("Name");
        problemDetails.Errors.ShouldContainKey("Email");
    }

    [Fact]
    public async Task Post_ShouldReturn400BadRequest_WhenCustomEnsureFails()
    {
        var invalidCustomer = new Customer("2", "Alice", "alice2@test.com");

        var response = await _client.PostAsJsonAsync("/customers", invalidCustomer, _jsonOptions);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();

        var problemDetails = JsonSerializer.Deserialize<ValidationProblemDetails>(content, _jsonOptions);
        problemDetails.ShouldNotBeNull();
        problemDetails.Title.ShouldBe("Validation Error");
        problemDetails.Errors.ShouldContainKey("Name");
    }

    [Fact]
    public async Task Post_ShouldReturn500InternalServerError_WhenUnsafeSaveThrows()
    {
        var duplicateCustomer = new Customer("1", "Bob", "bob@test.com");

        var response = await _client.PostAsJsonAsync("/customers", duplicateCustomer, _jsonOptions);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.InternalServerError);
        var content = await response.Content.ReadAsStringAsync();

        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content, _jsonOptions);
        problemDetails.ShouldNotBeNull();
        problemDetails.Title.ShouldBe("Internal Server Error");
        problemDetails.Detail.ShouldBe("An unexpected error occurred.");
    }
}