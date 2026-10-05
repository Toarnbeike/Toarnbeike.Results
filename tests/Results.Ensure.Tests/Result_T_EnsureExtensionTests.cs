using Toarnbeike.Results.Ensure.Extensions;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests;

public class Result_T_EnsureExtensionTests
{
    [Test]
    public void Ensure_OnResultT_ShouldReturnSuccess_WhenConditionIsTrue()
    {
        var value = 10;
        var result = Result.Success(value);
        result.Ensure(v => v).GreaterThan(5)
              .ShouldBeSuccess();
    }

    [Test]
    public void Ensure_OnResultT_ShouldReturnFailure_WhenConditionIsFalse()
    {
        var value = 3;
        var result = Result.Success(value);
        var failure = result.Ensure(v => v).GreaterThan(5)
              .ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldContain("greater than 5");
    }

    [Test]
    public void Ensure_OnResultT_ShouldReturnFailure_WhenIncomingResultIsFailure()
    {
        Result<int> result = new SimpleFailure("test", "Original failure.");
        var failure = result.Ensure(v => v).GreaterThan(5)
              .ShouldBeFailureOfType<SimpleFailure>();
        failure.Message.ShouldBe("Original failure.");
    }

    [Test]
    public void EnsureAll_OnResultT_ShouldReturnSuccess_WhenAllConditionsAreTrue()
    {
        var values = new[] { 10, 20, 30 };
        var result = Result.Success(values);
        result.EnsureAll(v => v).GreaterThan(5)
              .ShouldBeSuccess();
    }

    [Test]
    public void EnsureAll_OnResultT_ShouldReturnFailure_WhenAnyConditionIsFalse()
    {
        var values = new[] { 3, 10, 30 };
        var result = Result.Success(values);
        var failure = result.EnsureAll(v => v).GreaterThan(5)
              .ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("v[0]");
        failure.Message.ShouldContain("greater than 5");
    }

    [Test]
    public void EnsureAll_OnResultT_ShouldReturnFailure_WhenIncomingResultIsFailure()
    {
        Result<int[]> result = new SimpleFailure("test", "Original failure.");
        var failure = result.EnsureAll(v => v).GreaterThan(5)
              .ShouldBeFailureOfType<SimpleFailure>();
        failure.Message.ShouldBe("Original failure.");
    }

    [Test]
    public void EnsureAll_OnResultT_ShouldReturnFailure_ForFailureInMiddleOfCollection()
    {
        var values = new[] { 10, 3, 30 };
        var result = Result.Success(values);
        var failure = result.EnsureAll(v => v).GreaterThan(5)
              .ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("v[1]");
        failure.Message.ShouldContain("greater than 5");
    }

    [Test]
    public void EnsureAll_OnResultT_ShouldReturnSuccess_WhenCollectionIsEmpty()
    {
        var values = Array.Empty<int>();
        var result = Result.Success(values);
        result.EnsureAll(v => v).GreaterThan(5)
              .ShouldBeSuccess();
    }

    [Test]
    public void EnsureAny_OnResultT_ShouldReturnSuccess_WhenAnyConditionIsTrue()
    {
        var values = new[] { 3, 10, 30 };
        var result = Result.Success(values);
        result.EnsureAny(v => v).GreaterThan(5)
              .ShouldBeSuccess();
    }

    [Test]
    public void EnsureAny_OnResultT_ShouldReturnFailure_WhenAllConditionsAreFalse()
    {
        var values = new[] { 1, 2, 3 };
        var result = Result.Success(values);
        var failure = result.EnsureAny(v => v).GreaterThan(5)
              .ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldContain("greater than 5");
    }

    [Test]
    public void EnsureAny_OnResultT_ShouldReturnFailure_WhenIncomingResultIsFailure()
    {
        Result<int[]> result = new SimpleFailure("test", "Original failure.");
        var failure = result.EnsureAny(v => v).GreaterThan(5)
              .ShouldBeFailureOfType<SimpleFailure>();
        failure.Message.ShouldBe("Original failure.");
    }

    [Test]
    public void EnsureAny_OnResultT_ShouldReturnFailure_WhenCollectionIsEmpty()
    {
        var values = Array.Empty<int>();
        var result = Result.Success(values);
        var failure = result.EnsureAny(v => v).GreaterThan(5)
              .ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldContain("greater than 5");
    }
}
