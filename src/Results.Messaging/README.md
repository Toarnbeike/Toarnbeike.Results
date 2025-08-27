[![NuGet](https://img.shields.io/nuget/v/Toarnbeike.Results.Messaging.svg)](https://www.nuget.org/packages/Toarnbeike.Toarnbeike.Results.Messaging)

![CI](https://github.com/Toarnbeike/Toarnbeike.Results/actions/workflows/build.yaml/badge.svg)
[![Code Coverage](https://toarnbeike.github.io/Toarnbeike.Results/badge_shieldsio_linecoverage_brightgreen.svg)](https://github.com/Toarnbeike/Toarnbeike.Results/blob/gh-pages/SummaryGithub.md)
[![.NET 9](https://img.shields.io/badge/.NET-9.0-blueviolet.svg)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)


# Toarnbeike.Results.Messaging

Lightweight [CQRS](https://en.wikipedia.org/wiki/Command_Query_Responsibility_Segregation) request/response messaging built on top of [Toarnbeike.Results](https://www.nuget.org/packages/Toarnbeike.Results).  
Define **commands** and **queries**, dispatch them dynamically, and process results without exceptions.

---

## Features

- Define requests via `ICommand` and `IQuery` interfaces.  
- Implement request handlers with `ICommandHandler` or `IQueryHandler`.  
- Plug in pipeline behaviours with `PreHandle` and `PostHandle` hooks.  
- Built-in pagination support via `IPaginatedQuery`.  
- Automatic discovery and registration of handlers and pipelines in DI.  
- **Result-first design**: rich error handling without exceptions.

---

## Getting started

``` bash
dotnet add package Toarnbeike.Results.Messaging
```

This package targets `.NET 9+` and depends on:
- [![Toarnbeike.Results](https://img.shields.io/badge/Toarnbeike.Results-v1.0.0-info)](https://www.nuget.org/packages/Toarnbeike.Results/1.0.0)

---

## Usage

1. Register messaging:

``` csharp
  builder.Services.AddRequestMessaging(options =>
        options.FromAssemblyContaining<GetUserQueryHandler>());
```

2. Define request & handler:

``` csharp
public sealed record GetUserQuery(Guid Id) : IQuery<UserDto>;

internal sealed class GetUserQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetUserQuery, UserDto>
{
    public async Task<Result<UserDto>> HandleAsync(GetUserQuery query, CancellationToken ct = default)
    {
        return await Result.Success(query.Id)
            .Map(id => new UserId(id))
            .MapAsync(userRepository.GetByIdAsync, ct)
            .Map(user => user.ToDto());
    }
}
```

3. Dispatch from your service:

``` csharp
public class FrontEndUserService(IRequestDispatcher dispatch)
{
    public async Task<UserDto> GetUserById(Guid id)
    {
        var query = new GetUserQuery(id);
        return await dispatch.HandleAsync(query);
    }
}
```

---

## Pipeline behaviour

Pipeline behaviours wrap request handling with **cross-cutting logic**.
Examples include validation, logging, or caching.

``` csharp
public sealed class LoggingBehaviour<TRequest, TResponse>(
    ILogger<LoggingBehaviour<TRequest, TResponse>> logger) : IPipelineBehaviour<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse> where TResponse : IResult
{
    private string _requestName = "";
    private string _responseName = "";

    /// <inheritdoc />
    public Task<Result> PreHandleAsync(TRequest request, CancellationToken cancellationToken = default) 
    {
        _requestName = request.GetType().PrettyName();
        _responseName = typeof(TResponse).PrettyName();
        
        logger.LogInformation("Handling {RequestType} => {ResponseType}", _requestName, _responseName);
        return Result.SuccessTask();
    }

    /// <inheritdoc />
    public Task<Result> PostHandleAsync(TRequest request, TResponse response, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Handled {RequestType} => {ResponseType}. Result was {Result}",
            _requestName, 
            _responseName, 
            response.IsSuccess ? "Success" : "Failure");

        return Result.SuccessTask();
    }
}
```

Register your pipeline:
``` csharp
builder.Services.AddRequestMessaging(o =>
    o.AddPipelineBehaviour(typeof(LoggingBehaviour<,>)));
```

The order in which the pipelines are registered is the order in which they are executed.

---