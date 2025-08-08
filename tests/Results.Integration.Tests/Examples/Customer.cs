using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.FluentValidation;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.MinimalApi.DependencyInjection;
using Microsoft.AspNetCore.Routing;

namespace Toarnbeike.Results.Integration.Tests.Examples;

/// <summary>
/// Example entity with some properties.
/// </summary>
public record Customer(int Id, string Name, string Email);

/// <summary>
/// Repository service
/// </summary>
public interface ICustomerService
{
    Result<Customer> GetById(int id);
    Task UnsafeSaveAsync(Customer customer);
}

/// <summary>
/// Implementation of the repository service.
/// </summary>
public class CustomerService : ICustomerService
{
    /// <summary>
    /// 1 customer that is always present.
    /// </summary>
    private readonly List<Customer> _customers =
    [
        new Customer(1, "Alice", "alice@test.com")
    ];

    /// <summary>
    /// Returns the customer if found, otherwise a <see cref="Failure"/>
    /// </summary>
    public Result<Customer> GetById(int id)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == id);
        if (customer is null)
        {
            return new Failure("CustomerNotFound", "Customer not found");
        }
        return customer;
    }

    /// <summary>
    /// Method to save a new customer.
    /// This method can throw, and is therefore unsafe.
    /// This is used to show the Result.Try method.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the Id is not unique in the collection.</exception>
    public async Task UnsafeSaveAsync(Customer customer)
    {
        await Task.Delay(1);

        // Check if customer with given Id already exists
        if (_customers.Any(c => c.Id == customer.Id))
        {
            throw new InvalidOperationException("Customer already exists");
        }
        _customers.Add(customer);
    }
}

/// <summary>
/// Validator to check the incoming customer before saving.
/// </summary>
public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0).WithMessage("Id is required");
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(10).WithMessage("Maximum length: 10");
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");
    }
}

/// <summary>
/// Extension to add the <see cref="CustomerService"/> and the <see cref="CustomerValidator"/> to 
/// the service collection by their interfaces, for use in the minimal apis.
/// </summary>
public static class CustomerServiceExtensions
{
    public static IServiceCollection AddCustomerServices(this IServiceCollection services)
    {
        services.AddTransient<ICustomerService, CustomerService>(); // always a fresh instance
        services.AddSingleton<IValidator<Customer>, CustomerValidator>();
        return services;
    }
}

/// <summary>
/// Minimal api's to do some end-to-end tests.
/// </summary>
public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        // MapResultGroup registers the endpoints in this group as accepting a Result as return value.
        var customers = app.MapResultGroup("/customers");

        // Get the customer by their Id using the repository service.
        customers.MapGet("/{id}", (int id, ICustomerService service) =>
        {
            var result = service.GetById(id);
            return result;
        });

        // Add a new customer
        customers.MapPost("", async (Customer customer, ICustomerService service, IValidator<Customer> validator) =>
        {
            Result result = await Result.Success(customer)
                .Validate(validator)
                .Check(c => c.Name != "Alice", () => new ValidationFailure("Name", "Cannot create customer with name 'Alice'"))
                .VerifyAsync(async c => await Result.TryAsync(async () => await service.UnsafeSaveAsync(c)));
            return result;
        });
    }
}