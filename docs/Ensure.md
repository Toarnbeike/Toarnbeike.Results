# Full overview of all Guard Rules

## Collections (`IEnumerable<T>`)

- `Empty()`: Guard collection is empty, that is, contains exactly 0 items.
- `NotEmpty()`: Guard collection is not empty, that is, contains at least 1 item.
- `AtLeast(int min)`: Guard collection contains at least the provided threshold amount of items.
- `AtMost(int max)`: Guard collection contains at most the provided threshold amount of items.
- `Between(int min, int max)`: Guard collection contains between the provided minimum and maximum amount of items, (inclusive).
- `Exactly(int count)`: Guard collection contains exactly the provided amount of items.
- `Single()`: Guard collection contains exactly one item.
- `Unique()`: Guard collection contains only unique items, that is, no duplicates.

Each method has optimizations for `ICollection<T>`.

---

## Dates (`DateOnly`, `DateTime` and `DateTimeOffset`)

- `OnOrAfter(Date other)`: Guard date is after the provided threshold.
- `OnOrBefore(Date other)`: Guard date is before the provided threshold.
- `Between(Date start, Date end)`: Guard date is between the provided start and end dates.
- `Past()`: Guard date is in the past compared to the current date.
- `Future()`: Guard date is in the future compared to the current date.
- `WithinPast(TimeSpan timespan)`: Guard date is a) in the past and b) within the specified time span compared to the current date
- `WithinFuture(TimeSpan timespan)`: Guard date is a) in the future and b) within the specified time span compared to the current date
- `OnDayOfWeek(DayOfWeek dayOfWeek)`: Guard date falls on the specified day of the week.
- `OnDaysOfWeek(DayOfWeek[] allowed)`: Guard date falls on one of the specified days of the week.
- `OnWeekday()`: Guard date falls on a weekday (Monday through Friday).
- `OnWeekend()`: Guard date falls on a weekend day (Saturday or Sunday).

All date comparisons are inclusive, so is the value is equal to the threshold, the result is a success.

---

## Enums

- `IsDefined()`: Guard value is defined on the Enum.
- `OneOf()`: Guard value is one of the provided values.
- `NotOneOf()`: Guard value is not one of the provided values.

---

## Guids

- `NotEmpty()`: Guard Guid is not empty.
- `IsVersion4()`: Guard Guid is not empty and generated as Guid v4.
- `IsVersion7()`: Guard Guid is not empty and generated as Guid v7.

---

## Numbers (any value that implements `INumber<TSelf>`)

- `GreaterThan(Number min)`: Guard number is strictly greater than the provided minimum.
- `AtLeast(Number min)`: Guard number is greater than or equal to the provided minimum.
- `AtMost(Number max)`: Guard number is less than or equal to the provided maximum.
- `LessThan(Number max)`: Guard number is strictly less than the provided maximum.
- `Between(Number min, Number max)`: Guard number is within the provided lower and upper bound.

- `Positive()`: Guard number is strictly positive, that is, greater than 0.
- `AtLeastZero()`: Guard number is at least 0. 
- `AtMostZero()`: Guard number is at most 0.
- `Negative()`: Guard number is strictly negative, that is, less than 0.
- `Zero()`: Guard number is equal to 0. 
- `NotZero()`: Guard number is not equal to 0. 

- `MultipleOf(Number factor)`: Guard number is a multiple of the provided factor.


### Tolerance

All methods except the strict methods (GreaterThan, LessThan, Positive and Negative) use an internal default tolerance and accept a custom tolerance.
This is used to avoid rounding errors on floating point numbers.
The following defaults are used:
- `float (System.Single)`    : 1e-6
- `double (System.Double)`   : 1e-9
- `decimal (System.Decimal)` : 1e-9
- `Half (System.Half)`       : 1e-2

All other types have a default tolerance of 0.

### Integers (any value that implements `IBinaryInteger<TSelf>`)

Some additional extensions are defined specifically for integers
- `Odd()`: Guard number is odd. 
- `Even()`: Guard number is even. 
- `PowerOfTwo()`: Guard number is a power of two.

### Floating points (any value that implements `IFloatingPointIeee754<TSelf>`)

Some additional extensions are defined specifically for floating point numbers
- `WholeNumber()`: Guard number is a whole number, that is, has no decimal places.
- `MaxDecimalPlaces(int maxPlaces)`: Guard number has no more that the provided decimal places.
- `Finite()`: Guard number is a finite number, that is, not positive or negative infinity.
- `NotNaN()`: Guard number is a well defined number, not NaN. 
- 

---

## Strings

- `NotEmpty()`: Guard string is not empty, that is, contains at least 1 character. 
- `NotWhiteSpace()`: Guard string is not empty, that is, contains at least 1 non whitespace character.

- `MinLength(int minLength)`: Guard string has a minimum length.
- `MaxLength(int maxLength)`: Guard string has a maximum length.
- `LengthBetween(int minLength, int maxLength)`: Guard string has a minimum and a maximum length.

- `Matches(string pattern, RegexOptions options)`: Guard string matches the provided regex pattern, optionally with `RegexOptions`. 
- `Alphabetic()`: Guard string contains only letters [A-Z],[a-z].
- `AlphaNumeric()`: Guard string contains only letters [A-Z],[a-z] and numbers [0-9].
- `DigitsOnly()`: Guard string contains only numbers [0-9].
- `Ascii()`: Guard string contains only ascii characters.

- `EmailAddress()`: Guard string is a valid email address as defined by RFC5322.
- `Uri()`: Guard string is a valid Uri (either absolute or relative).
- `AbsoluteUri()`: Guard string is a valid Relative Uri.
- `RelativeUri()`: Guard string is a valid Absolute Uri.
- `IpAddress()`: Guard string is a valid IP address.
- `Slug()`: Guard string is a URL-safe slug.

All string length comparisons are inclusive.

---

## Timespans

- `AtLeast(TimeSpan other)`: Guard duration is at least the provided threshold.
- `AtMost(TimeSpan other)`: Guard duration is at most the provided threshold.
- `Between(TimeSpan min, TimeSpan max)`: Guard duration is between the provided upper and lower thresholds.
- `AtLeastZero()`: Guard duration is at least zero.
- `AtMostZero()`: Guard duration is at most zero.
- `Zero()`: Guard duration is zero.
- `NotZero()`: Guard duration is not zero.

All timespan comparisons are inclusive.

---

## Predicates (applicable for every generic Type)

- `Satisfies(Predicate)`: Guard value satisfies the provided predicate.
- `SatisfiesAsync(AsyncPredicate)`: Guard value satisfies the provided async predicate.
- `NotSatisfies(Predicate)`: Guard value does not satisfy the provided predicate.
- `NotSatisfiesAsync(AsyncPredicate)`: Guard value does not satisfy the provided async predicate.