using Toarnbeike.Results.Ensure.Extensions;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests;

public class ResultEnsureExtensionsTests
{
    [Test]
    public void Ensure_OnResult_ShouldReturnSuccess_WhenConditionIsTrue()
    {
        var value = 10;
        var result = Result.Success();
        result.Ensure(() => value).GreaterThan(5)
              .ShouldBeSuccess();
    }

    [Test]
    public void Ensure_OnResult_ShouldReturnFailure_WhenConditionIsFalse()
    {
        var value = 3;
        var result = Result.Success();
        var failure = result.Ensure(() => value).GreaterThan(5)
              .ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("value");
        failure.Message.ShouldContain("greater than 5");
    }

    [Test]
    public void Ensure_OnResult_ShouldReturnFailure_WhenIncomingResultIsFailure()
    {
        var result = Result.Failure(new SimpleFailure("test", "Original failure."));
        var failure = result.Ensure(() => 10).GreaterThan(5)
              .ShouldBeFailureOfType<SimpleFailure>();
        failure.Message.ShouldBe("Original failure.");
    }

    [Test]
    public void EnsureAll_OnResult_ShouldReturnSuccess_WhenAllConditionsAreTrue()
    {
        var values = new[] { 10, 20, 30 };
        var result = Result.Success();
        result.EnsureAll(() => values).GreaterThan(5)
              .ShouldBeSuccess();
    }

    [Test]
    public void EnsureAll_OnResult_ShouldReturnFailure_WhenAnyConditionIsFalse()
    {
        var values = new[] { 3, 10, 30 };
        var result = Result.Success();
        var failure = result.EnsureAll(() => values).GreaterThan(5)
              .ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("values[0]");
        failure.Message.ShouldContain("greater than 5");
    }

    [Test]
    public void EnsureAll_OnResult_ShouldReturnFailure_WhenIncomingResultIsFailure()
    {
        var values = new[] { 10, 20, 30 };
        var result = Result.Failure(new SimpleFailure("test", "Original failure."));
        var failure = result.EnsureAll(() => values).GreaterThan(5)
              .ShouldBeFailureOfType<SimpleFailure>();
        failure.Message.ShouldBe("Original failure.");
    }

    [Test]
    public void EnsureAll_OnResult_ShouldReturnFailure_ForFailureInMiddleOfCollection()
    {
        var values = new[] { 10, 3, 30 };
        var result = Result.Success();
        var failure = result.EnsureAll(() => values).GreaterThan(5)
              .ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("values[1]");
        failure.Message.ShouldContain("greater than 5");
    }

    [Test]
    public void EnsureAll_OnResult_ShouldReturnSuccess_WhenCollectionIsEmpty()
    {
        var values = Array.Empty<int>();
        var result = Result.Success();
        result.EnsureAll(() => values).GreaterThan(5)
              .ShouldBeSuccess();
    }

    [Test]
    public void EnsureAny_OnResult_ShouldReturnSuccess_WhenAnyConditionIsTrue()
    {
        var values = new[] { 3, 10, 30 };
        var result = Result.Success();
        result.EnsureAny(() => values).GreaterThan(5)
              .ShouldBeSuccess();
    }

    [Test]
    public void EnsureAny_OnResult_ShouldReturnFailure_WhenAllConditionsAreFalse()
    {
        var values = new[] { 1, 2, 3 };
        var result = Result.Success();
        var failure = result.EnsureAny(() => values).GreaterThan(5)
              .ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("values");
        failure.Message.ShouldContain("greater than 5");
    }

    [Test]
    public void EnsureAny_OnResult_ShouldReturnFailure_WhenIncomingResultIsFailure()
    {
        var values = new[] { 10, 20, 30 };
        var result = Result.Failure(new SimpleFailure("test", "Original failure."));
        var failure = result.EnsureAny(() => values).GreaterThan(5)
              .ShouldBeFailureOfType<SimpleFailure>();
        failure.Message.ShouldBe("Original failure.");
    }

    [Test]
    public void EnsureAny_OnResult_ShouldReturnFailure_WhenCollectionIsEmpty()
    {
        var values = Array.Empty<int>();
        var result = Result.Success();
        var failure = result.EnsureAny(() => values).GreaterThan(5)
              .ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("values");
        failure.Message.ShouldContain("greater than 5");
    }
}
