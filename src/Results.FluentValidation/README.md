[![NuGet](https://img.shields.io/nuget/v/Toarnbeike.Results.FluentValidation.svg)](https://www.nuget.org/packages/Toarnbeike.Toarnbeike.Results.FluentValidation)

![CI](https://github.com/Toarnbeike/Toarnbeike.Results/actions/workflows/build.yaml/badge.svg)
[![Code Coverage](https://toarnbeike.github.io/Toarnbeike.Results/badge_shieldsio_linecoverage_brightgreen.svg)](https://github.com/Toarnbeike/Toarnbeike.Results/blob/gh-pages/SummaryGithub.md)
[![.NET 9](https://img.shields.io/badge/.NET-9.0-blueviolet.svg)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

# Toarnbeike.Results.FluentValidation

Integrate fluent result handling from [Toarnbeike.Results](https://www.nuget.org/packages/Toarnbeike.Results) with expressive validation logic from [FluentValidation](https://fluentvalidation.net/), 
providing seamless validation support for `Result<T>` using one or more `IValidator<T>` instances.

---

## Features

- Extension methods to validate `Result<T>` with one or more FluentValidation validators.
- Support for both synchronous and asynchronous validation (`Validate` / `ValidateAsync`).
- Automatic conversion of FluentValidation's `ValidationFailure` to `Toarnbeike.Results.Failures.ValidationFailure`.
- Useful for pipeline-based validation or standalone use in service or domain layers.

---

## Getting started

``` bash
dotnet add package Toarnbeike.Results.FluentValidation
```

This package targets `.NET 9+` and depends on:
- [![Toarnbeike.Results](https://img.shields.io/badge/Toarnbeike.Results-v1.0.0-info)](https://www.nuget.org/packages/Toarnbeike.Results/1.0.0)
- [![FluentValidation](https://img.shields.io/badge/FluentValidation-v12.0.0-info)](https://www.nuget.org/packages/FluentValidation/12.0.0)

---

## Usage

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

> There is currently no automatic way to detect whether an `IValidator<T>` from `FluentValidation` uses async logic; 
when choosing the async method the full validation is always performed, even when the `IValidator<T>` does not contain async logic.

