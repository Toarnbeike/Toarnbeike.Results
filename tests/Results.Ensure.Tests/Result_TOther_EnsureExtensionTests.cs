using Toarnbeike.Results.Ensure.Extensions;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests;

public class Result_TOther_EnsureExtensionTests
{
    [Test]
    public void Ensure_OnResultTOther_ShouldReturnSuccess_WhenConditionIsTrue()
    {
        var value = 10;
        var result = Result.Success("test");
        result.Ensure(() => value).GreaterThan(2)
              .ShouldBeSuccess();
    }

    [Test]
    public void Ensure_OnResultTOther_ShouldReturnFailure_WhenConditionIsFalse()
    {
        var value = 1;
        var result = Result.Success("test");
        var failure = result.Ensure(() => value).GreaterThan(2)
              .ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldContain("greater than 2");
    }

    [Test]
    public void Ensure_OnResultTOther_ShouldReturnFailure_WhenIncomingResultIsFailure()
    {
        Result<string> result = new SimpleFailure("test", "Original failure.");
        var failure = result.Ensure(() => 10).GreaterThan(2)
              .ShouldBeFailureOfType<SimpleFailure>();
        failure.Message.ShouldBe("Original failure.");
    }

    [Test]
    public void EnsureAll_OnResultTOther_ShouldReturnSuccess_WhenAllConditionsAreTrue()
    {
        var values = new[] { 10, 20, 30 };
        var result = Result.Success("test");
        result.EnsureAll(() => values).GreaterThan(5)
              .ShouldBeSuccess();
    }

    [Test]
    public void EnsureAll_OnResultTOther_ShouldReturnFailure_WhenAnyConditionIsFalse()
    {
        var values = new[] { 3, 10, 30 };
        var result = Result.Success("test");
        var failure = result.EnsureAll(() => values).GreaterThan(5)
              .ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("values[0]");
        failure.Message.ShouldContain("greater than 5");
    }

    [Test]
    public void EnsureAll_OnResultTOther_ShouldReturnFailure_WhenIncomingResultIsFailure()
    {
        Result<string> result = new SimpleFailure("test", "Original failure.");
        var failure = result.EnsureAll(() => new[] { 10, 20, 30 }).GreaterThan(5)
              .ShouldBeFailureOfType<SimpleFailure>();
        failure.Message.ShouldBe("Original failure.");
    }

    [Test]
    public void EnsureAll_OnResultTOther_ShouldReturnFailure_ForFailureInMiddleOfCollection()
    {
        var values = new[] { 10, 3, 30 };
        var result = Result.Success("test");
        var failure = result.EnsureAll(() => values).GreaterThan(5)
              .ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("values[1]");
        failure.Message.ShouldContain("greater than 5");
    }

    [Test]
    public void EnsureAll_OnResultTOther_ShouldReturnSuccess_WhenCollectionIsEmpty()
    {
        var values = Array.Empty<int>();
        var result = Result.Success("test");
        result.EnsureAll(() => values).GreaterThan(5)
              .ShouldBeSuccess();
    }

    [Test]
    public void EnsureAny_OnResultTOther_ShouldReturnSuccess_WhenAnyConditionIsTrue()
    {
        var values = new[] { 3, 10, 30 };
        var result = Result.Success("test");
        result.EnsureAny(() => values).GreaterThan(5)
              .ShouldBeSuccess();
    }

    [Test]
    public void EnsureAny_OnResultTOther_ShouldReturnFailure_WhenAllConditionsAreFalse()
    {
        var values = new[] { 1, 2, 3 };
        var result = Result.Success("test");
        var failure = result.EnsureAny(() => values).GreaterThan(5)
              .ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldContain("greater than 5");
    }

    [Test]
    public void EnsureAny_OnResultTOther_ShouldReturnFailure_WhenIncomingResultIsFailure()
    {
        Result<string> result = new SimpleFailure("test", "Original failure.");
        var failure = result.EnsureAny(() => new[] { 10, 20, 30 }).GreaterThan(5)
              .ShouldBeFailureOfType<SimpleFailure>();
        failure.Message.ShouldBe("Original failure.");
    }

    [Test]
    public void EnsureAny_OnResultTOther_ShouldReturnFailure_WhenCollectionIsEmpty()
    {
        var values = Array.Empty<int>();
        var result = Result.Success("test");
        var failure = result.EnsureAny(() => values).GreaterThan(5)
              .ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldContain("greater than 5");
    }
}