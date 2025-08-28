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
- Fire-and-forget **notifications** with support for multiple handlers and outbox-style persistence.

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
Typical examples include validation, logging, or caching.

### Build in pipelines:
- `PerformanceLoggingPipelineBehaviour`: Logs incoming requests, measures execution time, and sends a `RequestExceedsExpectedDurationNotification` if the configured threshold (default 5 seconds) is exceeded.
- `FluentValidationPipelineBehaviour`: Validates incoming requests using registered `IValidator<TRequest>` instances. Short-circuits execution if the request is invalid.

### Custom pipeline example:
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

### Registering behaviours:
``` csharp
builder.Services.AddRequestMessaging(o =>
    o.AddValidationBehaviour();
    o.AddPerformanceLoggingBehavior(configure => configure.MaxExpectedDuration = Timespan.FromSeconds(1));
    o.AddPipelineBehaviour(typeof(LoggingBehaviour<,>)));
```

> The order in which the pipelines are registered is the order in which they are executed.

---

## Notifications 

In addition to commands and queries, this package supports **notifications**.
Notifications are **fire-and-forget** messages that can be processed by **multiple handlers** at once.

They follow an outbox-style flow:
1. Notificaitons are fist stored in an `INotificationStore`.
1. The `INotificationPublisher` retrieves and dispatches them.
1. Each notificiaotn is delivered to all matching handlers.

### Flow:

1. Register notification messaging in your DI setup:
    ``` csharp
    services.AddNotificationMessaging(options =>
    {
        // Assemblies containing notification handlers
        options.FromAssemblyContaining<MyNotificationHandler>();

        // Optionally: register a custom notification store (e.g. EF Core)
        options.UseCustomNotificationStore<MyEfCoreNotificationStore>();
    });
    ```
    >If no custom store is provided, an in-memory store is used by default.

1. Define a notification by inheriting NotificationBase
    ``` csharp
    public record CustomerCreatedNotification(int CustomerId) : NotificationBase("Sender");
    ```

1. Write a handler for the notification
    ``` csharp
    public sealed class SendWelcomeEmailHandler 
    : INotificationHandler<CustomerCreatedNotification>
    {
        public Task HandleAsync(CustomerCreatedNotification notification, CancellationToken cancellationToken)
        {
            // Send welcome email
            return Task.CompletedTask;
        }
    }
    ```

1. Publish the notification
    ``` csharp
    public sealed class CustomerService(INotificationStore notificationStore)
    {
        public async Task AddCustomerAsync()
        {
            // ... register user ...
            var customerId = GetNewId();
            await _store.AddAsync(new UserRegisteredNotification(customerId));
        }
    }
    ```
   Notifications are **persisted first** and later **dispatched asynchronously** by the publisher.

---

## Summary

- **Commands/Queries** -> request/response with rich result handling.
- **Pipelines** -> plug in cross-cutting behaviours (validation, logging, caching).
- **Notifications** -> fire-and-forget messages; processed reliably by multiple handlers.

---