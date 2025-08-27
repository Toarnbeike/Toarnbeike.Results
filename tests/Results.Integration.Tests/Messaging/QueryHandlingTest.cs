using Microsoft.Extensions.DependencyInjection;
using Toarnbeike.Results.Integration.Tests.Examples;
using Toarnbeike.Results.Messaging;
using Toarnbeike.Results.Messaging.DependencyInjection;
using Toarnbeike.Results.Messaging.Pagination;
using Toarnbeike.Results.TestHelpers;

namespace Toarnbeike.Results.Integration.Tests.Messaging;

public class QueryHandlingTest
{
    private static ServiceProvider BuildServiceCollectionWithDispatcher()
    {
        var services = new ServiceCollection();

        services.AddRequestMessaging(options =>
            options.FromAssemblyContaining<AddCustomerCommandHandler>());
        services.AddCustomerServices();
        return services.BuildServiceProvider();
    }
    
    [Fact]
    public async Task AddCustomerCommandHandler_Should_BeHandled()
    {
        var serviceProvider = BuildServiceCollectionWithDispatcher();
        var dispatcher = serviceProvider.GetRequiredService<IRequestDispatcher>();

        var command = new AddCustomerCommand("Bob", "bob@test.com");
        var result = await dispatcher.DispatchAsync(command);
        var actual = result.ShouldBeSuccess();
        actual.Name.ShouldBe("Bob");
        actual.Email.ShouldBe("bob@test.com");
    }

    [Fact]
    public async Task UpdateCustomerCommandHandler_Should_BeHandled()
    {
        var serviceProvider = BuildServiceCollectionWithDispatcher();
        var dispatcher = serviceProvider.GetRequiredService<IRequestDispatcher>();

        var command = new UpdateCustomerCommand(1, "Alice Updated");
        var result = await dispatcher.DispatchAsync(command);
        result.ShouldBeSuccess();
    }
    
    [Fact]
    public async Task GetCustomerQueryHandler_Should_BeHandled()
    {
        var serviceProvider = BuildServiceCollectionWithDispatcher();
        var dispatcher = serviceProvider.GetRequiredService<IRequestDispatcher>();

        var query = new GetCustomerQuery(1);
        var result = await dispatcher.DispatchAsync(query);
        var actual = result.ShouldBeSuccess();
        actual.Id.ShouldBe(1);
        actual.Name.ShouldBe("Alice");
        actual.Email.ShouldBe("alice@test.com");
    }

    [Fact]
    public async Task GetAllCustomersQueryHandler_Should_BeHandled()
    {
        var serviceProvider = BuildServiceCollectionWithDispatcher();
        var dispatcher = serviceProvider.GetRequiredService<IRequestDispatcher>();

        var query = new GetAllCustomersQuery(new PagingInformation(1, 1));
        var result = await dispatcher.DispatchAsync(query);
        var actual = result.ShouldBeSuccess();
        actual.Items.Count().ShouldBe(1);
        actual.Items.First().Id.ShouldBe(1);
        actual.Items.First().Name.ShouldBe("Alice");
        actual.Items.First().Email.ShouldBe("alice@test.com");
    }
}
