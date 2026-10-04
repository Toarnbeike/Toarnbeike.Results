# Rules

The `Toarnbeike.Results.Validation.Rules` namespace contains the built-in rules used by the fluent validation and IValidator extensions.
Custom messages can be supplied for each rule unless stated otherwise.

## CollectionRules

Rules for non-null `IEnumerable` values:

| Rule | Description |
|---|---|
| `NotEmpty` | Requires the collection to contain at least one element. |
| `Empty` | Requires the collection to contain no elements. |
| `HasAtLeast(min)` | Requires at least `min` elements. |
| `HasAtMost(max)` | Requires no more than `max` elements. |
| `HasBetween(min, max)` | Requires the count to be within the inclusive range `min`–`max`. |
| `HasExactly(expected)` | Requires exactly `expected` elements. |

## DateRules

Used by the `DateTime`, `DateOnly`, and `DateTimeOffset` extensions. Null date values fail these rules.

| Rule | Description |
|---|---|
| `OnOrAfter(min)` | Requires the date to be greater than or equal to `min`. |
| `OnOrBefore(max)` | Requires the date to be less than or equal to `max`. |
| `MustBeBetween(min, max)` | Requires the date to be within the inclusive range `min`–`max`. |

## EnumRules

| Rule | Description |
|---|---|
| `IsDefined` | Requires the value to be a declared member of its enum type. |
| `OneOf(acceptedValues)` | Requires the value to match one of the accepted values. |
| `NotOneOf(rejectedValues)` | Requires the value not to match any rejected value. |

## FloatingPointRules

Rules for IEEE 754 floating-point values (`float`, `double`, `decimal`, `Half`). Comparison rules accept an optional tolerance; the default tolerance is used when omitted. Invalid tolerances are rejected.

| Rule | Description |
|---|---|
| `AtLeast(min)` | Requires the value to be at least `min`, accounting for tolerance. |
| `AtMost(max)` | Requires the value to be at most `max`, accounting for tolerance. |
| `Between(min, max)` | Requires the value to be within the inclusive range, accounting for tolerance. |
| `MultipleOf(factor)` | Requires the value to be a multiple of `factor` within tolerance. The factor cannot be zero. |
| `Finite` | Rejects positive infinity, negative infinity, and NaN. |
| `NotNaN` | Rejects NaN; infinities are allowed. |

## GuidRules

| Rule | Description |
|---|---|
| `NotEmpty` | Rejects `Guid.Empty`. |
| `Version4` | Requires a non-empty version 4 GUID. |
| `Version7` | Requires a non-empty version 7 GUID. |

## IntegerRules

Rules for binary integer types (`int`, `long`, `short`, etc.):

| Rule | Description |
|---|---|
| `AtLeast(min)` | Requires the value to be greater than or equal to `min`. |
| `AtMost(max)` | Requires the value to be less than or equal to `max`. |
| `Between(min, max)` | Requires the value to be within the inclusive range `min`–`max`. |
| `MultipleOf(factor)` | Requires the value to be evenly divisible by `factor`. The factor cannot be zero. |

## NumericRules

Rules for numeric types, including integers and floating-point values. These rules do not accept tolerances, not even for floating-point types, to ensure strict comparisons.

| Rule | Description |
|---|---|
| `GreaterThan(min)` | Requires the value to be strictly greater than `min`. |
| `LessThan(max)` | Requires the value to be strictly less than `max`. |
| `Positive` | Requires the value to be strictly greater than zero. |

## PredicateRules

A null value fails either predicate rule. These rules require an explicit failure message and have no default message.

| Rule | Description |
|---|---|
| `Must(predicate, message)` | Requires the predicate to return `true`. |
| `MustNot(predicate, message)` | Requires the predicate to return `false`. |

## StringRules

Rules that inspect string content or length reject null values.

| Rule | Description |
|---|---|
| `NotEmpty` | Rejects null and empty strings; whitespace-only strings pass. |
| `NotWhiteSpace` | Rejects null, empty, and whitespace-only strings. |
| `MinLength(minLength)` | Requires at least `minLength` characters. |
| `MaxLength(maxLength)` | Requires no more than `maxLength` characters. |
| `LengthBetween(minLength, maxLength)` | Requires a length within the inclusive range. |
| `Matches(pattern)` | Requires a match for the regular-expression pattern. Pattern matching has a one-second timeout. |
| `Matches(regex)` | Requires a match for the supplied `Regex` instance. |

## TimeSpanRules

| Rule | Description |
|---|---|
| `AtLeast(min)` | Requires the duration to be greater than or equal to `min`. |
| `AtMost(max)` | Requires the duration to be less than or equal to `max`. |
| `MustBeBetween(min, max)` | Requires the duration to be within the inclusive range `min`–`max`. |
| `Around(expected, tolerance)` | Requires the duration to be in the inclusive range `expected - tolerance` through `expected + tolerance`. Tolerance must be non-negative. |

## Related documentation

- [Results.Validation README](../src/Results.Validation/README.md)
