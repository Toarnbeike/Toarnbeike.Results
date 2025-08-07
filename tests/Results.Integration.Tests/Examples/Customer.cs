using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Toarnbeike.Results.MinimalApi;
using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.FluentValidation;
using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Integration.Tests.Examples;

public record Customer(string Id, string Name, string Email);

public interface ICustomerService
{
    Result<Customer> GetById(string id);
    Task UnsafeSaveAsync(Customer customer);
}

public class CustomerService : ICustomerService
{
    private readonly List<Customer> _customers =
    [
        new Customer("1", "Alice", "alice@test.com")
    ];

    public Result<Customer> GetById(string id)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == id);
        if (customer is null)
        {
            return new Failure("CustomerNotFound", "Customer not found");
        }
        return customer;
    }

    public async Task UnsafeSaveAsync(Customer customer)
    {
        await Task.Delay(1);

        // Check if customer already exists
        if (_customers.Any(c => c.Id == customer.Id))
        {
            throw new InvalidOperationException("Customer already exists");
        }
        _customers.Add(customer);
    }
}

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(10).WithMessage("Maximum length: 10");
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");
    }
}

public static class CustomerServiceExtensions
{
    public static IServiceCollection AddCustomerServices(this IServiceCollection services)
    {
        services.AddTransient<ICustomerService, CustomerService>(); // always a fresh instance
        services.AddSingleton<IValidator<Customer>, CustomerValidator>();
        return services;
    }
}

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        app.MapGet("/customers/{id}", (string id, ICustomerService service) =>
        {
            var result = service.GetById(id);
            return result;
        }).AddEndpointFilter<ResultMappingEndpointFilter>();

        app.MapPost("/customers", async (Customer customer, ICustomerService service, IValidator<Customer> validator) =>
        {
            Result result = await Result.Success(customer)
                .Validate(validator)
                .Ensure(c => c.Name != "Alice", () => new ValidationFailure("Name", "Cannot create customer with name 'Alice'"))
                .VerifyAsync(async c => await Result.TryAsync(async () => await service.UnsafeSaveAsync(c)));
            return result;
        }).AddEndpointFilter<ResultMappingEndpointFilter>();
    }
}