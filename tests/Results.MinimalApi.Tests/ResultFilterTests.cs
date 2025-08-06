namespace Toarnbeike.Results.MinimalApi.Tests;

public class ResultFilterTests(MinimalApiTestApp app) : IClassFixture<MinimalApiTestApp>
{
    private readonly HttpClient _client = app.Client;

    [Fact]
    public async Task Success_Should_Return_204()
    {
        var response = await _client.GetAsync("/success");
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task SuccessWithValue_Should_Return_200()
    {
        var response = await _client.GetAsync("/successWithValue");
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        
        var content = await response.Content.ReadAsStringAsync();
        content.ShouldContain("This is a test success with value");
    }

    [Fact]
    public async Task Failure_Should_Return_400()
    {
        var response = await _client.GetAsync("/failure");
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
        
        var content = await response.Content.ReadAsStringAsync();
        content.ShouldContain("This is a test failure");
    }

    [Fact]
    public async Task NoResult_Should_Return_200()
    {
        var response = await _client.GetAsync("/noResult");
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.ShouldContain("This endpoint does not use a Toarnbeike.Result");
    }
}