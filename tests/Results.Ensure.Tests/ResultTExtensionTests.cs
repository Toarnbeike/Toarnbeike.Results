using Toarnbeike.Results.Rules.Extensions;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests;

public class ResultTExtensionTests
{
    [Test]
    public void Ensure_Should_ReturnFailure_WithoutSelector_WhenResultIsFailure()
    {
        Result<string> result = new TestFailure("Initial failure");
        var ensureResult = result.Ensure("value").MinLength(5);
        var failure = ensureResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Initial failure");
    }

    [Test]
    public void Ensure_Should_ReturnSuccess_WithoutSelector_WhenRuleSucceeds()
    {
        Result<string> result = Result.Success("Hello");
        var ensureResult = result.Ensure("value").MinLength(5);
        ensureResult.ShouldBeSuccess();
        ensureResult.ShouldBeOfType<Result<string>>();
    }

    [Test]
    public void Ensure_Should_ReturnFailure_WithoutSelector_WhenRuleFails()
    {
        Result<string> result = Result.Success("Hi");
        var ensureResult = result.Ensure("value").MinLength(5);
        var failure = ensureResult.ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("value");
    }

    [Test]
    public void Ensure_Should_ReturnFailure_WithSelector_WhenResultIsFailure()
    {
        Result<string> result = new TestFailure("Initial failure");
        var ensureResult = result.Ensure(x => x).MinLength(5);
        var failure = ensureResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Initial failure");
    }

    [Test]
    public void Ensure_Should_ReturnSuccess_WithSelector_WhenRuleSucceeds()
    {
        Result<string> result = Result.Success("Hello");
        var ensureResult = result.Ensure(x => x).MinLength(5);
        ensureResult.ShouldBeSuccess();
        ensureResult.ShouldBeOfType<Result<string>>();
    }

    [Test]
    public void Ensure_Should_ReturnFailure_WithSelector_WhenRuleFails()
    {
        Result<string> result = Result.Success("Hi");
        var ensureResult = result.Ensure(x => x).MinLength(5);
        var failure = ensureResult.ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("x");
    }

    [Test]
    public void Ensure_Should_ReturnFailure_WithSelector_WithCustomerPropertyName()
    {
        Result<string> result = Result.Success("Hi");
        var ensureResult = result.Ensure(x => x, "CustomPropertyName").MinLength(5);
        var failure = ensureResult.ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("CustomPropertyName");
    }

    [Test]
    public void EnsureCollection_Should_ReturnFailure_WhenResultIsFailure()
    {
        Result<List<string>> result = new TestFailure("Initial failure");
        var ensureResult = result.EnsureCollection(x => x).NotEmpty();
        var failure = ensureResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Initial failure");
    }

    [Test]
    public void EnsureCollection_Should_ReturnSuccess_WhenRuleSucceeds()
    {
        Result<List<string>> result = Result.Success(new List<string> { "item1" });
        var ensureResult = result.EnsureCollection(x => x).NotEmpty();
        ensureResult.ShouldBeSuccess();
        ensureResult.ShouldBeOfType<Result<List<string>>>();
    }

    [Test]
    public void EnsureCollection_Should_ReturnFailure_WhenRuleFails()
    {
        Result<List<string>> result = Result.Success(new List<string>());
        var ensureResult = result.EnsureCollection(x => x).NotEmpty();
        var failure = ensureResult.ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("x");
    }

    [Test]
    public void EnsureCollection_Should_ReturnFailure_WithCustomerPropertyName()
    {
        Result<List<string>> result = Result.Success(new List<string>());
        var ensureResult = result.EnsureCollection(x => x, "CustomPropertyName").NotEmpty();
        var failure = ensureResult.ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("CustomPropertyName");
    }
}