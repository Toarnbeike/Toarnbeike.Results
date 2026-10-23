# Rules reference

This reference describes the fluent rules provided by `Toarnbeike.Results.Rules.Extensions`.

Rules are applied after selecting a value with `Ensure`, `Validate`, `EnsureCollection`, or `ValidateCollection`. A rule returns the original successful result when it passes. When it fails, `Ensure` returns a `GuardFailure` and `Validate` returns a `ValidationFailure`.

```csharp
using Toarnbeike.Results.Rules;
using Toarnbeike.Results.Rules.Extensions;
using Toarnbeike.Results.Validation;

var guarded = order.Ensure(o => o.Quantity).AtLeast(1);
var validated = request.Validate(r => r.Email).NotWhiteSpace();
```

All rules accept an optional custom failure message unless the rule table states otherwise. If no custom message is supplied, the configured message provider generates one.

## Contents

1. [General behavior](#general-behavior)
1. [String rules](#string-rules)
1. [Collection rules](#collection-rules)
1. [Collection item rules](#collection-item-rules)
1. [Date and time rules](#date-and-time-rules)
1. [TimeSpan rules](#timespan-rules)
1. [Enum rules](#enum-rules)
1. [Guid rules](#guid-rules)
1. [Boolean rules](#boolean-rules)
1. [Integer rules](#integer-rules)
1. [Numeric rules](#numeric-rules)
1. [Floating-point rules](#floating-point-rules)
1. [Object rules](#object-rules)
1. [Messages and evaluation settings](#messages-and-evaluation-settings)

---

## General behavior

- Rule chains short-circuit: after a rule returns a failed result, later operations in that result pipeline do not run.
- `Between` and `LengthBetween` bounds are inclusive.
- Rules are defined for non-null target values unless a rule explicitly describes null behavior. Selectors that can return `null` should be guarded or handled before applying rules that dereference or enumerate the value.
- The property name in a `GuardFailure` or `ValidationFailure` is inferred from the selector expression. Supply the optional `propertyName` argument to `Ensure`, `Validate`, `EnsureCollection`, or `ValidateCollection` to override it.

---

## String rules

Applicable to `string` targets.

| Method | Behavior |
|---|---|
| `NotEmpty()` | Requires a non-null, non-empty string. Whitespace-only strings pass. |
| `NotWhiteSpace()` | Requires a non-null string containing at least one non-whitespace character. |
| `MinLength(minLength)` | Requires a length greater than or equal to `minLength`. |
| `MaxLength(maxLength)` | Requires a length less than or equal to `maxLength`. |
| `LengthBetween(minLength, maxLength)` | Requires a length in the inclusive range from `minLength` through `maxLength`. |
| `MatchesRegex(pattern, patternDescription = null)` | Requires a match for `pattern`. `patternDescription` is used in the generated failure message; when omitted, the pattern is used. Matching has a one-second timeout. |

---

## Collection rules

Applicable to `IEnumerable<TItem>` targets selected through `EnsureCollection` or `ValidateCollection`. Collection-count rules enumerate the sequence to determine its count.

| Method | Behavior |
|---|---|
| `Contains(expected)` | Requires an item equal to `expected`. The expected item cannot be `null`. |
| `NotContains(expected)` | Requires no item equal to `expected`. |
| `NotEmpty()` | Requires at least one item. |
| `Empty()` | Requires zero items. |
| `HasAtLeast(min)` | Requires at least `min` items. |
| `HasAtMost(max)` | Requires at most `max` items. |
| `HasBetween(min, max)` | Requires an item count in the inclusive range from `min` through `max`. |
| `HasExactly(expected)` | Requires exactly `expected` items. |

```csharp
var result = request.ValidateCollection(r => r.Tags)
    .NotEmpty()
    .HasAtMost(5);
```

---

## Collection item rules

After selecting a collection, use `All` or `Any` to apply the next rule to its items instead of the collection itself.

| Method | Behavior |
|---|---|
| `All()` | Requires every item to pass the next rule. Items are evaluated in enumeration order and evaluation stops at the first failed item. An empty collection passes. |
| `All(selector)` | Projects each item with `selector` and requires every projected value to pass the next rule. An empty collection passes. |
| `Any()` | Requires at least one item to pass the next rule. Evaluation stops at the first passing item. An empty collection fails. |
| `Any(selector)` | Projects each item with `selector` and requires at least one projected value to pass the next rule. An empty collection fails. |

```csharp
var validLines = order
    .EnsureCollection(o => o.Lines)
    .All(line => line.Quantity).AtLeast(1);

var validTags = request
    .ValidateCollection(r => r.Tags)
    .All().NotWhiteSpace();

var hasEnabledLine = order
    .EnsureCollection(o => o.Lines)
    .Any(line => line.IsEnabled).EqualTo(true);
```

The failure is associated with the selected collection property; `All` and `Any` do not add an item index to that property name.

---

## Date and time rules

Applicable to `DateOnly`, `DateTime`, and `DateTimeOffset` targets.

| Method | Behavior |
|---|---|
| `OnOrAfter(min)` | Requires a value greater than or equal to `min`. |
| `OnOrBefore(max)` | Requires a value less than or equal to `max`. |
| `Between(min, max)` | Requires a value in the inclusive range from `min` through `max`. |
| `Future()` | Requires a value strictly greater than the time provided by the registered date time provider. |
| `FutureWithin(duration)` | Requires a value strictly greater than the time provided by the registered date time provider and within the specified duration. |
| `Past()` | Requires a value strictly less than the time provided by the registered date time provider. |
| `PastWithin(duration)` | Requires a value strictly less than the time provided by the registered date time provider and within the specified duration. |

Remarks:
- These rules compare the values supplied to them; they do not convert or normalize time zones or `DateTime.Kind` values.
- Use `RuleContext.UseSystemUtcNow()` or `RuleContext.UseFixedUtcNow()` to configure the UTC clock used by the relevant rules.
- The `FutureWithin` and `PastWithin` rules use a `TimeSpan` duration to define the time window relative to the current time, or a int days duration for `DateOnly` targets.

---

## TimeSpan rules

Applicable to `TimeSpan` targets.

| Method | Behavior |
|---|---|
| `AtLeast(min)` | Requires a duration greater than or equal to `min`. |
| `AtMost(max)` | Requires a duration less than or equal to `max`. |
| `Between(min, max)` | Requires a duration in the inclusive range from `min` through `max`. |
| `Around(expected, tolerance)` | Requires a duration from `expected - tolerance` through `expected + tolerance`, inclusive. |
| `AtLeastZero()` | Requires a duration greater than or equal to `TimeSpan.Zero`. |

---

## Enum rules

Applicable to value types constrained as `enum`.

| Method | Behavior |
|---|---|
| `IsDefined()` | Requires a value declared by its enum type. |
| `OneOf(acceptedValues)` | Requires a value contained in the supplied accepted values. |
| `NotOneOf(rejectedValues)` | Requires a value not contained in the supplied rejected values. |

---

## Guid rules

Applicable to `Guid` targets.

| Method | Behavior |
|---|---|
| `NotEmpty()` | Rejects both `Guid.Empty` and `Guid.AllBitsSet`. |
| `Version4()` | Requires a version 4 GUID. |
| `Version7()` | Requires a version 7 GUID. |

---

## Boolean rules

Applicable to `bool` targets.

| Method | Behavior |
|---|---|
| `IsTrue()` | Requires the value to be `true`. |
| `IsFalse()` | Requires the value to be `false`. |

---

## Integer rules

Applicable to binary integer types (`IBinaryInteger<T>`), including `byte`, `short`, `int`, `long`, and their unsigned counterparts.

| Method | Behavior |
|---|---|
| `AtLeast(min)` | Requires a value greater than or equal to `min`. |
| `AtMost(max)` | Requires a value less than or equal to `max`. |
| `Between(min, max)` | Requires a value in the inclusive range from `min` through `max`. |
| `MultipleOf(factor)` | Requires an exact multiple of `factor`. `factor` cannot be zero. |

---

## Numeric rules

Applicable to numeric types constrained as `INumber<T>`. These rules are strict and never use a floating-point tolerance.

| Method | Behavior |
|---|---|
| `GreaterThan(min)` | Requires a value strictly greater than `min`. |
| `LessThan(max)` | Requires a value strictly less than `max`. |
| `Positive()` | Requires a value strictly greater than zero. |

---

## Floating-point rules

Applicable to floating-point numeric types constrained as `IFloatingPoint<T>` and `IMinMaxValue<T>`, including `Half`, `float`, `double`, and `decimal`.

| Method | Behavior |
|---|---|
| `AtLeast(min, tolerance = null)` | Requires a value greater than or equal to `min - tolerance`. |
| `AtMost(max, tolerance = null)` | Requires a value less than or equal to `max + tolerance`. |
| `Between(min, max, tolerance = null)` | Requires a value in the inclusive range from `min - tolerance` through `max + tolerance`. |
| `MultipleOf(factor, tolerance = null)` | Requires a multiple of `factor` within tolerance. `factor` cannot be zero. |
| `Finite()` | Rejects `NaN`, positive infinity, and negative infinity. |

When `tolerance` is omitted, the configured `RuleContext` tolerance is used. A negative tolerance throws `ArgumentOutOfRangeException`.

---

## Object rules

Applicable to any selected property type.

| Method | Behavior |
|---|---|
| `EqualTo(expected)` | Requires equality according to `EqualityComparer<T>.Default`. |
| `NotEqualTo(expected)` | Requires inequality according to `EqualityComparer<T>.Default`. |
| `Must(predicate, message)` | Requires `predicate` to return `true`. `message` is required. |
| `MustNot(predicate, message)` | Requires `predicate` to return `false`. `message` is required. |
| `MustAsync(predicate, message)` | Asynchronously requires a `Func<TProperty, Task<bool>>` predicate to return `true`. `message` is required; returns `Task<Result<T>>`. |
| `MustNotAsync(predicate, message)` | Asynchronously requires a `Func<TProperty, Task<bool>>` predicate to return `false`. `message` is required; returns `Task<Result<T>>`. |

The predicate receives the selected value unchanged. The application is responsible for handling nullable values in its predicate when the selected property can be `null`.

---

## Messages and evaluation settings

The library uses built-in English messages when a failed rule has no custom message. Generated messages include evaluation details such as the attempted value and comparison bounds. Their value formatting uses the culture configured in `RuleContext`.

`RuleContext` is process-wide. It can configure:

- The message provider (`UseDefaultMessageProvider` or `UseCustomMessageProvider`).
- The culture used for generated message values (`UseInvariantCulture`, `UseCulture`, or `UseSystemCulture`).
- The default floating-point tolerance (`UseDefaultTolerance`, `UseZeroTolerance`, or `UseCustomToleranceProvider`).
- The UTC clock used by time-relative lower-level date rules (`UseSystemUtcNow` or `UseFixedUtcNow`), which supports deterministic tests.

See the [Results.Rules README](../src/Results.Rules/README.md) for configuration examples and guidance on choosing `Ensure` or `Validate`.