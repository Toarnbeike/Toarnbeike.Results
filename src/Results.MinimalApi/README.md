![CI](https://github.com/Toarnbeike/Toarnbeike.Results/actions/workflows/build.yaml/badge.svg)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-blueviolet.svg)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

# Toarnbeike.Results.MinimalApi

Seamlessly return [Toarnbeike.Results](https://www.nuget.org/packages/Toarnbeike.Results) from your Minimal API endpoints with automatic HTTP response mapping.

## Features

- Automatic mapping of `Result` and `Result<TValue>` types to IResult using an EndpointFilter
- Built-in mappers for common failures to standardized `ProblemDetails` responses
- Support for custom mappers to override or extend default behavior
- Fully configurable through dependency injection

---

## Contents
1. [Quick start](#quick-start)
1. [Core concepts](#core-concepts)
1. [Design principles](#design-principles-and-best-practices)

---

## Quick start

Apply the `ResultMappingEndpointFilter` to any endpoint that returns a `Result` or `Result<TValue>`:
``` csharp
  App.MapGet("customer/{id}", (int id, ICustomerService service) =>
  {
      Result<Customer> result = service.GetById(id);
      return result;
  }).AddEndpointFilter<ResultMappingEndpointFilter>();
```

This will convert the `Result` into an appropriate HTTP response based on its success or failure state, without needing to manually check the result in your endpoint logic.

---

## Core concepts

### Installation

``` bash
dotnet add package Toarnbeike.Results.MinimalApi
```

This package targets `.NET 10` and depends on:
- [![Toarnbeike.Results](https://img.shields.io/badge/Toarnbeike.Results-v1.1.4-info)](https://www.nuget.org/packages/Toarnbeike.Results/1.1.4)
- [AspNetCore](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/metapackage-app?view=aspnetcore-10.0)

### Register result mapping

Add result mappping support to your DI container:
``` csharp
  builder.Services.AddResultMapping();
```

### Apply the endpoint filter

Apply the `ResultMappingEndpointFilter` to any endpoint that returns a `Result` or `Result<TValue>`:
``` csharp
  App.MapGet("customer/{id}", (int id, ICustomerService service) =>
  {
      Result<Customer> result = service.GetById(id);
      return result;
  }).AddEndpointFilter<ResultMappingEndpointFilter>();
```

### Mapping behavior
| Result Type                   | HTTP Response                                                 | 
|-------------------------------|---------------------------------------------------------------|
| `Result<T>.Success(value)`    | `200 OK` with `value` as JSON payload                         |
| `Result<T>.Success()`         | `204 No Content`                                              |
| `Result<T>.Failure(failure)`  | `ProblemDetails` response (status code based on failure type) |

Custom failures can be mapped by registing your own mappers:
``` csharp
  builder.Services.AddResultMapping(options =>
  {
      options.AddMapper<ConflictFailureMapper>();                           // add single mapper
      options.AddMappersFromAssemblyContaining<ConflictFailureMapper>();    // add all mappers from an assembly
      options.UseFallback<CustomFallbackMapper>();                          // override the default fallback mapper for unmapped failures
  });

  public class ConflictFailureMapper : FailureResultMapper<ConflictFailure>
  {
      public ProblemDetails Map(CustomFailure failure)
      {
          return Problem(failure.Message, statusCode: 409);
      }
  }
```

---

## Design Principles and best practices
To avoid repeating `.AddEndpointFilter<ResultMappingEndpointFilter>()` on every endpoint, 
you can use the `MapResultGroup()` extension method to apply the filter to a group of endpoints:

```csharp
  var group = app.MapResultGroup("/customers");

  group.MapGet("/{id}", (int id, ICustomerService service) =>
  {
    Result<Customer> result = service.GetById(id);
    return result;
  });
```

Every endpoint in the group will automatically map `Result` and `Result<TValue>` types to appropriate HTTP responses.

If you rely on Result mapping for all endpoints in your application, you can create a /api group for all endpoints:
```csharp
  var endpoints = app.MapResultGroup("/api");
```

---