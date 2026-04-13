# LINQ Query syntax support

`Toarnbeike.Results` supports C# LINQ query syntax for composing `Result<TValue>` computations using standard query expressions.

This is syntactic sugar over Bind and Map, preserving the same failure propagation semantics.

## Contents

1. [How it works](#how-it-works)
1. [Example](#example)
1. [Failure propagation](#failure-propagation)
1. [When to use](#when-to-use)
1. [When not to use](#when-not-to-use)
1. [Design](#design)

## How it works
LINQ query syntax is translated by the compiler into method calls:

- `from x in ...` → `Bind`
- `select ...` → `Map`
- `let ...` → `Map` (intermediate projection)
- `where ...` → `Where`

Failures are propagated automatically without executing subsequent steps.

The `Where` clause acts as a guard: if the predicate is not satisfied, the result transitions into a failure and the pipeline is short-circuited.

---

## Example
```csharp
using Toarnbeike.Results.Linq;

var result =
    from id in GetUserId()
    from user in GetUserById(id)
    where user.IsActive
    let fullName = $"{user.FirstName} {user.LastName}"
    select new UserDto(fullName, user.Email);
```

Equivalent method chain:

```csharp
var result = GetUserId()
    .Bind(GetUserById)
    .BindTap(user => user.IsActive ? Result.Success() : new Failure("whereLinq", "LINQ predicate was not satisfied."))
    .Map(user =>
    {
        var name = $"{user.FirstName} {user.LastName}";
        return new UserDto(name, user.Email);
    });
```

---

## Failure propagation

If any step returns a failure:
- the query is short-circuited
- remaining expressions are not evaluated
- the failure is returned as-is
``` csharp
from id in GetUserId()
from user in GetUserById(id) // if this fails, select is skipped
select user.Email;
```

---

## When to use
LINQ syntax is most useful when:
- multiple dependent steps are chained
- intermediate values need naming (let)
- conditional filtering (`where`) is part of the pipeline
- nested Bind/Map calls reduce readability

---

## When not to use
Prefer method chaining when:
- only 1–2 transformations are involved
- no intermediate naming is required
- performance-critical code prefers explicit flow

---

Design notes

1. Pure syntactic sugar => No new semantics are introduced
1. No behaviour differences => Both styles propagate failures identically
1. Optional feature => LINQ support is intentionally lightweight and does not replace the core fluent API.