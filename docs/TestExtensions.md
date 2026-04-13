# Toarnbeike.Results.TestExtensions

Assertion extensions for verifying `Result` and `Result<TValue>` in unit tests.

These extensions provide a minimal set of fluent, exception-based assertions for validating success and failure outcomes.
They are intended for test code only and should not be used in production logic.

---

## Contents
1. [Extensions](#extensions)
2. [Usage](#usage)
3. [Async Usage](#async-usage)
4. [Design](#design)
5. [Conclusion](#conclusion)

---

## Extensions

| Method                             | Description                              |
|------------------------------------|------------------------------------------|
| `ShouldBeSuccess()`                | Asserts the result is successful         |
| `ShouldBeSuccess<T>()`             | Asserts success and returns the value    |
| `ShouldBeFailure()`                | Asserts the result is a failure          |
| `ShouldBeFailureOfType<TFailure>()`| Asserts failure of a specific type       |

---

## Usage

``` csharp
using Toarnbeike.Results;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;

public class MyTests
{
    [Test] or [Fact]
    public void Should_Succeed_WithExpectedValue()
    {
        Result<string> result = GetConfigurationItem();

        var value = result.ShouldBeSuccess();
        value.ShouldBe("configItem1");
    }

    [Test] or [Fact]
    public void Should_Fail_WithExpectedCode()
    {
        Result result = SomeServiceCall();

        var failure = result.ShouldBeFailure();
        failure.Code.ShouldBe("Unauthorized");
    }

    [Test] or [Fact]
    public void Should_Fail_WithExpectedFailureType()
    {
        Result result = AnotherServiceCall();

        var failure = result.ShouldBeFailureOfType<ValidationError>();
        failure.Failures.ShouldNotBeEmpty();
    }
}

```

---

## Async Usage

``` csharp
[Test] or [Fact]
public async Task Should_Succeed_Asynchronously()
{
    var value = await service.Call().ShouldBeSuccess();
    value.ShouldBe(42);
}
```

---

## Design

### Minimal API surface
The assertion API is intentionally small and focused on a few core primitives:
- success vs failure
- retrieving the success value
- retrieving the failure (optionally typed)

More specialized assertions (such as checking codes or messages) are not included.
Instead, the returned value or failure can be verified using standard assertion libraries such as Shouldly.

### Composition over specialization
Rather than providing many specific assertion methods, the API relies on composition:

```csharp
var failure = result.ShouldBeFailure();
failure.Code.ShouldBe("Unauthorized");
failure.Message.ShouldBe("Access denied");
```
This avoids combinatorial growth in assertion methods while remaining expressive and flexible.

---

## Conclusion

- All assertions throw descriptive exceptions on failure
- Async overloads are provided for Task<Result> and Task<Result<T>>
- These extensions are designed to work alongside assertion libraries such as Shouldly or FluentAssertions