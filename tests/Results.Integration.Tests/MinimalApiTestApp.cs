using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Toarnbeike.Results.Integration.Tests.Examples;
using Toarnbeike.Results.MinimalApi.DependencyInjection;

namespace Toarnbeike.Results.Integration.Tests;

public class MinimalApiTestApp : IAsyncLifetime
{
    public HttpClient Client { get; private set; } = null!;
    public WebApplication App { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();

        builder.Services.AddCustomerServices();
        builder.Services.AddResultMapping();

        App = builder.Build();

        App.MapCustomerEndpoints();

        await App.StartAsync();

        Client = App.GetTestClient();
    }

    public async Task DisposeAsync()
    {
        if (App is not null)
        {
            await App.DisposeAsync();
            Client.Dispose();
        }

        App = null!;
        Client = null!;
    }
}