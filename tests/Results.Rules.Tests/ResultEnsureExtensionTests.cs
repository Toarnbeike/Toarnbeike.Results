using Toarnbeike.Results.Rules.Extensions;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Rules.Tests;

public class ResultEnsureExtensionTests
{
    [Test]
    public void Ensure_Should_ReturnFailure_WhenResultIsFailure()
    {
        var value = "Hello";
        Result result = new TestFailure("Initial failure");
        var ensureResult = result.Ensure(() => value).MinLength(5);
        var failure = ensureResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Initial failure");
    }

    [Test]
    public void Ensure_Should_ReturnSuccess_WhenRuleSucceeds()
    {
        var value = "Hello";
        Result result = Result.Success();
        var ensureResult = result.Ensure(() => value).MinLength(5);
        ensureResult.ShouldBeSuccess();
        ensureResult.ShouldBeOfType<Result<string>>();
    }

    [Test]
    public void Ensure_Should_ReturnFailure_WhenRuleFails()
    {
        var value = "Hi";
        Result result = Result.Success();
        var ensureResult = result.Ensure(() => value).MinLength(5);
        var failure = ensureResult.ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("value");
    }

    [Test]
    public void Ensure_Should_ReturnFailure_WithCustomerPropertyName()
    {
        var value = "Hi";
        Result result = Result.Success();
        var ensureResult = result.Ensure(() => value, "CustomPropertyName").MinLength(5);
        var failure = ensureResult.ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("CustomPropertyName");
    }

    [Test]
    public void Ensure_ShouldUseCustomRuleMessage_WhenRuleFails()
    {
        var value = "Hi";
        Result result = Result.Success();
        var ensureResult = result.Ensure(() => value).MinLength(5, "Custom failure message");
        var failure = ensureResult.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom failure message");
    }

    [Test]
    public void Ensure_Should_ThrowNullException_WhenSelectorIsNull()
    {
        Result result = Result.Success();
        Should.Throw<ArgumentNullException>(() => result.Ensure<string?>(null!));
    }

    [Test]
    public void EnsureCollection_Should_ReturnFailure_WhenResultIsFailure()
    {
        var list = new List<string> { "item1" };
        Result result = new TestFailure("Initial failure");
        var ensureResult = result.EnsureCollection(() => list).NotEmpty();
        var failure = ensureResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Initial failure");
    }

    [Test]
    public void EnsureCollection_Should_ReturnSuccess_WhenRuleSucceeds()
    {
        var list = new List<string> { "item1" };
        Result result = Result.Success();
        var ensureResult = result.EnsureCollection(() => list).NotEmpty();
        ensureResult.ShouldBeSuccess();
        ensureResult.ShouldBeOfType<Result<IEnumerable<string>>>();
    }

    [Test]
    public void EnsureCollection_Should_ReturnSuccess_WhenRuleSucceeds_WithEmptyCollection()
    {
        var list = new List<string>();
        Result result = Result.Success();
        var ensureResult = result.EnsureCollection(() => list).HasAtMost(5);
        ensureResult.ShouldBeSuccess();
    }

    [Test]
    public void EnsureCollection_Should_ReturnFailure_WhenRuleFails()
    {
        var list = new List<string>();
        Result result = Result.Success();
        var ensureResult = result.EnsureCollection(() => list).NotEmpty();
        var failure = ensureResult.ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("list");
    }

    [Test]
    public void EnsureCollection_Should_ReturnFailure_WhenRuleFails_WithEmptyCollection()
    {
        var list = new List<string>();
        Result result = Result.Success();
        var ensureResult = result.EnsureCollection(() => list).HasAtLeast(5);
        var failure = ensureResult.ShouldBeFailureOfType<GuardFailure>();
    }

    [Test]
    public void EnsureCollection_Should_ReturnFailure_WithCustomerPropertyName()
    {
        var list = new List<string>();
        Result result = Result.Success();
        var ensureResult = result.EnsureCollection(() => list, "CustomPropertyName").NotEmpty();
        var failure = ensureResult.ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("CustomPropertyName");
    }

    [Test]
    public void EnsureCollection_Should_ThrowNullException_WhenSelectorIsNull()
    {
        Result result = Result.Success();
        Should.Throw<ArgumentNullException>(() => result.EnsureCollection<string>(null!));
    }
}