## LINQ Query syntax support

Toarnbeike.Results supports optional integration with [C# LINQ query syntax](https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/write-linq-queries),
making it easier to compose multiple `Result<TValue>` computations in a declarative style.

### Why use LINQ Query Syntax?
While method chaining works well for most scenarios, C#'s LINQ query syntax can make some workflows more expressive and readable; especially when you want to:

- Name intermediate results using `let`
- Compose complex Result pipelines in a declarative way
- Avoid deeply nested lambdas in `Bind` and `Map`

### Example:
```csharp
using Toarnbeike.Results.Linq;

var result =
    from id in GetUserId()
    from user in GetUserById(id)
    let fullName = $"{user.FirstName} {user.LastName}"
    select new UserDto(fullName, user.Email);
```

When comparing that with method chaining, the LINQ query syntax can be more readable, especially for complex workflows:

```csharp
var result = GetUserId()
    .Bind(GetUserById)
    .Map(user =>
    {
        var name = $"{user.FirstName} {user.LastName}";
        return new UserDto(name, user.Email);
    });
```

Which also works, but makes name only available inside the `Map` lambda.