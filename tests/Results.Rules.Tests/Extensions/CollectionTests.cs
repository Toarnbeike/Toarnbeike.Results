using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;

namespace Toarnbeike.Results.Rules.Tests.Extensions;

public class CollectionTests
{
    private readonly Result<List<int>> _result = new List<int> { 1, 2, 3 };
    private readonly Result<List<int>> _emptyResult = new List<int>();

    [Test]
    public void Contains_ShouldReturnSuccess_WhenItemIsInCollection()
    {
        var validationResult = _result.TestRuleForCollection(x => x).Contains(2);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Contains_ShouldReturnFailure_WhenItemIsNotInCollection()
    {
        var validationResult = _result.TestRuleForCollection(x => x).Contains(4);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void Contains_ShouldReturnFailureWithCustomMessage_WhenItemIsNotInCollection()
    {
        var validationResult = _result.TestRuleForCollection(x => x).Contains(4, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void NotContains_ShouldReturnSuccess_WhenItemIsNotInCollection()
    {
        var validationResult = _result.TestRuleForCollection(x => x).NotContains(4);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void NotContains_ShouldReturnFailure_WhenItemIsInCollection()
    {
        var validationResult = _result.TestRuleForCollection(x => x).NotContains(2);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void NotContains_ShouldReturnFailureWithCustomMessage_WhenItemIsInCollection()
    {
        var validationResult = _result.TestRuleForCollection(x => x).NotContains(2, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void NotEmpty_ShouldReturnSuccess_WhenCollectionIsNotEmpty()
    {
        var validationResult = _result.TestRuleForCollection(x => x).NotEmpty();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void NotEmpty_ShouldReturnFailure_WhenCollectionIsEmpty()
    {
        var validationResult = _emptyResult.TestRuleForCollection(x => x).NotEmpty();
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void NotEmpty_ShouldReturnFailureWithCustomMessage_WhenCollectionIsEmpty()
    {
        var validationResult = _emptyResult.TestRuleForCollection(x => x).NotEmpty("Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void Empty_ShouldReturnSuccess_WhenCollectionIsEmpty()
    {
        var validationResult = _emptyResult.TestRuleForCollection(x => x).Empty();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Empty_ShouldReturnFailure_WhenCollectionIsNotEmpty()
    {
        var validationResult = _result.TestRuleForCollection(x => x).Empty();
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void Empty_ShouldReturnFailureWithCustomMessage_WhenCollectionIsNotEmpty()
    {
        var validationResult = _result.TestRuleForCollection(x => x).Empty("Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void HasAtLeast_ShouldReturnSuccess_WhenCollectionHasAtLeastSpecifiedCount()
    {
        var validationResult = _result.TestRuleForCollection(x => x).HasAtLeast(2);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void HasAtLeast_ShouldReturnFailure_WhenCollectionHasFewerThanSpecifiedCount()
    {
        var validationResult = _emptyResult.TestRuleForCollection(x => x).HasAtLeast(2);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void HasAtLeast_ShouldReturnFailureWithCustomMessage_WhenCollectionHasFewerThanSpecifiedCount()
    {
        var validationResult = _emptyResult.TestRuleForCollection(x => x).HasAtLeast(2, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void HasAtMost_ShouldReturnSuccess_WhenCollectionHasAtMostSpecifiedCount()
    {
        var validationResult = _result.TestRuleForCollection(x => x).HasAtMost(4);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void HasAtMost_ShouldReturnFailure_WhenCollectionHasMoreThanSpecifiedCount()
    {
        var validationResult = _result.TestRuleForCollection(x => x).HasAtMost(2);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void HasAtMost_ShouldReturnFailureWithCustomMessage_WhenCollectionHasMoreThanSpecifiedCount()
    {
        var validationResult = _result.TestRuleForCollection(x => x).HasAtMost(2, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void HasBetween_ShouldReturnSuccess_WhenCollectionHasBetweenSpecifiedCounts()
    {
        var validationResult = _result.TestRuleForCollection(x => x).HasBetween(1, 3);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void HasBetween_ShouldReturnFailure_WhenCollectionHasFewerThanMinCount()
    {
        var validationResult = _result.TestRuleForCollection(x => x).HasBetween(5, 7);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void HasBetween_ShouldReturnFailureWithCustomMessage_WhenCollectionHasFewerThanMinCount()
    {
        var validationResult = _result.TestRuleForCollection(x => x).HasBetween(5, 7, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void HasExactly_ShouldReturnSuccess_WhenCollectionHasExactlySpecifiedCount()
    {
        var validationResult = _result.TestRuleForCollection(x => x).HasExactly(3);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void HasExactly_ShouldReturnFailure_WhenCollectionDoesNotHaveExactlySpecifiedCount()
    {
        var validationResult = _result.TestRuleForCollection(x => x).HasExactly(2);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void HasExactly_ShouldReturnFailureWithCustomMessage_WhenCollectionDoesNotHaveExactlySpecifiedCount()
    {
        var validationResult = _result.TestRuleForCollection(x => x).HasExactly(2, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }
}
