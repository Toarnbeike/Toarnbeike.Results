![CI](https://github.com/Toarnbeike/Toarnbeike.Results/actions/workflows/build.yaml/badge.svg)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-blueviolet.svg)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

# Toarnbeike.Results.FluentValidation

Integrate fluent result handling from [Toarnbeike.Results](https://www.nuget.org/packages/Toarnbeike.Results) with expressive validation logic from [FluentValidation](https://fluentvalidation.net/), 
providing seamless validation support for `Result<T>` using one or more `IValidator<T>` instances.

## Features

- Extension methods to validate `Result<T>` with one or more FluentValidation validators.
- Support for both synchronous and asynchronous validation (`Validate` / `ValidateAsync`).
- Automatic conversion of FluentValidation's `ValidationFailure` to `Toarnbeike.Results.Failures.ValidationFailure`.
- Useful for pipeline-based validation or standalone use in service or domain layers.

---

## Contents
1. [Quick start](#quick-start)
1. [Core concepts](#core-concepts)

---

## Quick start

This example demonstrates how to use `Toarnbeike.Results.FluentValidation` to validate a dto using a FluentValidation validator and return a `Result`.
``` csharp
Result<RegisterUserCommand> result = new RegisterUserCommand("John", "john@email.com");

// Validate using one or more FluentValidation validators
var validated = result.Validate(new RegisterUserCommandValidator());
```

## Core concepts

### Installation
``` bash
dotnet add package Toarnbeike.Results.FluentValidation
```

This package targets `.NET 10` and depends on:
- [![Toarnbeike.Results](https://img.shields.io/badge/Toarnbeike.Results-v1.1.4-info)](https://www.nuget.org/packages/Toarnbeike.Results)
- [![FluentValidation](https://img.shields.io/badge/FluentValidation-v12.1.1-info)](https://www.nuget.org/packages/FluentValidation)

### Usage

Validate the value of a `Result<TValue>` if the result is successful.
``` csharp
var result = Result.Success(new RegisterUserCommand("John", "john@email.com"));

// Validate using one or more FluentValidation validators
var validated = result.Validate(new RegisterUserCommandValidator());
```

Validate asynchronously
``` csharp
var result = Result.Success(new RegisterUserCommand("John", "john@email.com"));

var validated = result.ValidateAsync(new RegisterUserCommandValidator());
```

Validate using multiple validators (coming from DI)
``` csharp
public class CreateUserCommandHandler(IEnumerable<IValidator<CreateUserCommand>> validators) 
{
    public async Task<Result> Handle(CreateUserCommand command)
    {
        return await Result.Success(command)
            .ValidateAsync(validators);
            // other extensions methods that handle the command.
    }
}
```

### When to use `Validate` vs. `ValidateAsync`
Use:

- `.Validate()` when your validators are fully synchronous
- `.ValidateAsync()` when any of your validators use asynchronous logic, such as MustAsync, or if you are not sure.

There is currently no automatic way to detect whether an `IValidator<T>` from `FluentValidation` uses async logic; 
when choosing the async method the full validation is always performed, even when the `IValidator<T>` does not contain async logic.

