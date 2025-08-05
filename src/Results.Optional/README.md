[![NuGet](https://img.shields.io/nuget/v/Toarnbeike.Results.Optional.svg)](https://www.nuget.org/packages/Toarnbeike.Toarnbeike.Results.Optional)
![CI](https://github.com/Toarnbeike/Toarnbeike.Results/actions/workflows/build.yaml/badge.svg)
[![Code Coverage](https://toarnbeike.github.io/Toarnbeike.Results/badge_shieldsio_linecoverage_brightgreen.svg)](https://github.com/Toarnbeike/Toarnbeike.Results/blob/gh-pages/SummaryGithub.md)
[![.NET 9](https://img.shields.io/badge/.NET-9.0-blueviolet.svg)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

# Toarnbeike.Results.Optional

Extension methods for converting between [`Result<T>`](https://www.nuget.org/packages/Toarnbeike.Results) and [`Option<T>`](https://www.nuget.org/packages/Toarnbeike.Optional).

---

## Features

- Convert an `Option<T>` to a `Result<T>` by supplying a failure for `None`.
- Convert a `Result<T>` to an `Option<T>`, discarding the failure.
- Supports both synchronous and asynchronous usage.

---

## Getting started

Install via NuGet:
``` bash
dotnet add package Toarnbeike.Results.Optional
```

---

## Usage

``` csharp
var result1 = Result.Success(42);
var option1 = result1.ToOption();                      // Some(42)

var result2 = Result.Failure<int>(failure);
var option2 = result2.ToOption();                      // None

var option3 = Option.Some(42);
var result3 = option3.ToResult(() => failure);         // Success(42)

var option4 = Option.None<int>();
var result4 = option4.ToResult(() => failure);         // Failure(failure)
```

---