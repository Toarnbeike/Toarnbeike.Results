using Toarnbeike.Results.Messaging.Requests;
using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Messaging.Pagination;

namespace Toarnbeike.Results.Integration.Tests.Examples;

public sealed record AddCustomerCommand(string Name, string Email) : ICreateCommand<Customer>;

internal sealed class AddCustomerCommandHandler(ICustomerRepository customerRepository) 
    : ICreateCommandHandler<AddCustomerCommand, Customer>
{
    public async Task<Result<Customer>> HandleAsync(AddCustomerCommand request, CancellationToken cancellationToken = default)
    {
        var customer = new Customer(12, request.Name, request.Email);
        return await Result.TryAsync(() => customerRepository.UnsafeSaveAsync(customer))
            .WithValue(customer);
    }
}

public sealed record UpdateCustomerCommand(int Id, string Name) : ICommand;

internal sealed class UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
    : ICommandHandler<UpdateCustomerCommand>
{
    public async Task<Result> HandleAsync(UpdateCustomerCommand request, CancellationToken cancellationToken = default)
    {
        return await customerRepository.GetById(request.Id)
            .TapAsync(customer => customerRepository.UpdateAsync(customer, request.Name));
    }
}

public sealed record GetCustomerQuery(int Id) : IQuery<Customer>;

internal sealed class GetCustomerQueryHandler(ICustomerRepository customerRepository) : IQueryHandler<GetCustomerQuery, Customer>
{
    public Task<Result<Customer>> HandleAsync(GetCustomerQuery request, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(customerRepository.GetById(request.Id));
    }
}

public sealed record GetAllCustomersQuery(PagingInformation Paging) : IPaginatedQuery<Customer>;

internal sealed class GetAllCustomersQueryHandler(ICustomerRepository customerRepository) : IPaginatedQueryHandler<GetAllCustomersQuery, Customer>
{
    public async Task<Result<PaginatedCollection<Customer>>> HandleAsync(GetAllCustomersQuery request, CancellationToken cancellationToken = default)
    {
        return await customerRepository.GetAll()
            .Map(collection => collection.ToPaginatedCollection(request.Paging));
    }
}