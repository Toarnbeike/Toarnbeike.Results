# Future

This document describes the future plans for the `Toarnbeike.Results` library.

## Version 2.0

- Cleanup and refactor of the codebase, including the removal of some deprecated APIs.
- Introduction of a more structured approach to representing failures, with a focus on extensibility and better error handling.
- Integration with other `Toarnbeike` libraries, working together to provide a **Functional Programming** experience in C#.

## Failure redesign spec

### 1. Design goals

Primary goals
- Failure is a **first-class immutable value**
- Geen complexe inheritance hierarchies
- Geen recursive Failure-structuren
- Consistente semantics met `Result` pipeline (`Bind`, `Map`, `Combine`)
- Debuggable en test-friendely
- Expliciete retry / categorization signals

Non-goals
- Geen "rich exception framework"
- Geen full validation framework replacement
- Geen domain-event model
- Geen polymorphic failure trees

---

### 2. Core concept

Failures is a single atomic value
``` csharp
public abstract record Failure
{
    public string Message { get; init; } = string.Empty;
    public FailureCategory Category { get; init; }
}
```

Key change:
- `Failure` is not a container
- `Failure` is not a hierarchy root for structure
- only for semantic typing

---

### 3. Failure categories (new)
Introduce explicit classification:
```csharp
public enum FailureCategory
{
    Validation,
    Business,
    System,
    External,
    Unknown
}
```

Purpose:
- retry logic guessing
- 4xx vs 5xx confusion
- implicit semantics in exception types

---

### 4. Build-in Failure types

#### 4.1 ValudationFailure

```csharp
public sealed record ValidationFailure(
    string Property,
    string Message
) : Failure
{
    public ValidationFailure(string property, string message)
    {
        Property = property;
        Message = message;
        Category = FailureCategory.Validation;
    }
}
```

Notes: 
- replaces both `ValidationFailure` and `ValidationFailures`
- no aggregation inside this type

#### 4.2 ExceptionFailure

``` csharp
public sealed record ExceptionFailure : Failure
{
    public Exception Exception { get; }

    public ExceptionFailure(Exception exception)
    {
        Exception = exception;
        Message = exception.Message;
        Category = FailureCategory.System;
    }
}
```

#### 4.3 GenericFailure

``` csharp
public sealed record GenericFailure(
    string Code,
    string Message,
    FailureCategory Category = FailureCategory.Business
) : Failure;
```