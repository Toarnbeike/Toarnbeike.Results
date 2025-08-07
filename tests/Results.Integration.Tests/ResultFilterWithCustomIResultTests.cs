namespace Toarnbeike.Results.Integration.Tests;

public class ResultFilterWithCustomIResultTests(MinimalApiTestApp app) : IClassFixture<MinimalApiTestApp>
{
    private readonly HttpClient _client = app.Client;

    [Fact]
    public async Task CustomSuccess_Should_Return_200()
    {
        var response = await _client.GetAsync("/customSuccess");
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.ShouldContain("Success");
    }

    [Fact]
    public async Task CustomSuccessNoTryGetValue_Should_Return_204()
    {
        var response = await _client.GetAsync("/customSuccessNoTryGetValue");
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
    }
}
