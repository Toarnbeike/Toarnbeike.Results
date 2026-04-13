# Failure Types

`Toarnbeike.Results` provides a small set of built-in `Failure` types that represent common failure scenarios.

A `Failure` always represents a single, atomic error state.
Some types (such as summaries) may contain multiple failures, but only as a read-only snapshot, not as part of the result composition model.

## Content

1. [Failure overview](#failure-overview)
1. [Usage guidelines](#usage-guidelines)
1. [Design notes](#design-notes)
1. [Summary](#summary)

---

## Failure overview

| Failure                                                 | Description                                                    |
|---------------------------------------------------------|----------------------------------------------------------------|
| [`DefaultFailure`](#defaultfailure)                     |	Generic fallback failure with a code and message               |
| [`ExceptionFailure`](#exceptionfailure)                 |	Wraps an exception thrown during execution                     |
| [`ValidationFailure`](#validationfailure)               |	Represents a single validation error on a property             |
| [`ValidationFailureSummary`](#validationfailuresummary) |	Snapshot of multiple validation failures grouped by property   |
| [`AggregateFailureSummary`](#aggregatefailuresummary)   |	Snapshot of multiple failures from aggregation                 |

---

### DefaultFailure
Represents a generic failure when no more specific failure type is available.
``` csharp
new DefaultFailure("not_found", "User was not found");
```
Use this type as a **fallback**, not as a primary modeling tool.
Prefer more specific failure types when the failure has clear semantics.

---

### ExceptionFailure
Wraps an exception that occurred during execution.
``` csharp
try
{
    // ...
}
catch (Exception ex)
{
    return new ExceptionFailure(ex);
}
```
This type is typically used by `Result.Try(...)`-style APIs to convert exceptions into failures.

The original exception is preserved for debugging purposes.

---

### ValidationFailure

Represents a single validation error for a specific property.

```csharp
new ValidationFailure("Email", "Email address is invalid");
```
Use this type for:

- input validation
- domain validation rules

Each instance represents **one validation issue**.

---

### ValidationFailureSummary

Represents a snapshot of multiple validation failures.
``` csharp
new ValidationFailureSummary(validationFailures);
```
This type groups multiple `ValidationFailure` instances, typically by property.

#### Notes
- Intended for reporting validation results
- Not used during result composition
- Prefer working with individual ValidationFailure instances in pipelines

---

### AggregateFailureSummary

Represents a snapshot of multiple failures, typically produced during aggregation of multiple results.
``` csharp
new AggregateFailureSummary(failures);
```
This type is used when combining multiple Result instances (e.g. via Aggregate), and more than one failure occurred.

#### Notes
- The contained failures are flattened and read-only
- This type is not used for composition, only as a final result
- No additional failures can be added after creation

---

## Usage Guidelines
- Use DefaultFailure only for simple or unknown failure cases
- Use specific failure types to improve clarity and intent
- Use summary types only when combining multiple results
- Avoid creating custom failure hierarchies unless necessary

---

## Design notes

1. Failures are atomic => Each `Failure` represents a sing error. Even summary types represent a **single failure state**, not a composable structure.
1. Prefer specific types => Use specific failure types when possible. Use `DefaultFailure` only as a fallback.
1. Summary types are terminal => Each `FailureSummary` is read-only and non-composable.
1. No inheritance chains => Failure types are intentionally shallow: no deep inheritance, no recursive structures and no mutatio or combination logic.

---

## Summary

The failure model is designed to be:

- minimal
- explicit
- composable at the Result level
- easy to reason about

Failures describe *what went wrong*, while Result controls *how failures propagate*.