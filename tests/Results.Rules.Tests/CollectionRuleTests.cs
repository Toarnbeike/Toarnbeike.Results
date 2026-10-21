using Toarnbeike.Results.Rules;
using Toarnbeike.Rules.Tests.MessageTemplateConfiguration;

namespace Toarnbeike.Rules.Tests;

[ParallelGroup(nameof(TestMessageTemplateGroup))]
public class CollectionRuleTests : TestMessageTemplateGroup
{
    [Test]
    public void Contains_ShouldReturnTrue_WhenItemIsInCollection()
    {
        var collection = new List<int> { 1, 2, 3 };
        var rule = CollectionRules.Contains(2);
        var result = rule(collection);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Contains_ShouldReturnFalse_WhenItemIsNotInCollection()
    {
        var collection = new List<int> { 1, 2, 3 };
        var rule = CollectionRules.Contains(4);
        var result = rule(collection);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("ContainsEvaluator");
        result.Message.ShouldContain($"| Attempted Value: {collection}");
        result.Message.ShouldContain("type: Int32");
        result.Message.ShouldContain("expected: 4");
    }

    [Test]
    public void NotContains_ShouldReturnTrue_WhenItemIsNotInCollection()
    {
        var collection = new List<int> { 1, 2, 3 };
        var rule = CollectionRules.NotContains(4);
        var result = rule(collection);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void NotContains_ShouldReturnFalse_WhenItemIsInCollection()
    {
        var collection = new List<int> { 1, 2, 3 };
        var rule = CollectionRules.NotContains(2);
        var result = rule(collection);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("DoesNotContainEvaluator");
        result.Message.ShouldContain($"| Attempted Value: {collection}");
        result.Message.ShouldContain("type: Int32");
        result.Message.ShouldContain("unexpected: 2");
    }

    [Test]
    public void HasExactly_ShouldReturnTrue_WhenCollectionHasExpectedCount()
    {
        var collection = new List<int> { 1, 2, 3 };
        var rule = CollectionRules.HasExactly<int>(3);
        var result = rule(collection);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void HasExactly_ShouldReturnFalse_WhenCollectionDoesNotHaveExpectedCount()
    {
        var collection = new List<int> { 1, 2, 3 };
        var rule = CollectionRules.HasExactly<int>(4);
        var result = rule(collection);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("EqualToEvaluator");
        result.Message.ShouldContain($"| Attempted Value: {collection.Count}");
        result.Message.ShouldContain("type: Int32");
        result.Message.ShouldContain("target: 4");
    }

    [Test]
    public void HasAtLeast_ShouldReturnTrue_WhenCollectionHasAtLeastExpectedCount()
    {
        var collection = new List<int> { 1, 2, 3 };
        var rule = CollectionRules.HasAtLeast<int>(2);
        var result = rule(collection);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void HasAtLeast_ShouldReturnFalse_WhenCollectionHasLessThanExpectedCount()
    {
        var collection = new List<int> { 1, 2, 3 };
        var rule = CollectionRules.HasAtLeast<int>(4);
        var result = rule(collection);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("AtLeastEvaluator");
        result.Message.ShouldContain($"| Attempted Value: {collection.Count}");
        result.Message.ShouldContain("type: Int32");
        result.Message.ShouldContain("min: 4");
    }

    [Test]
    public void HasAtMost_ShouldReturnTrue_WhenCollectionHasAtMostExpectedCount()
    {
        var collection = new List<int> { 1, 2, 3 };
        var rule = CollectionRules.HasAtMost<int>(4);
        var result = rule(collection);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void HasAtMost_ShouldReturnFalse_WhenCollectionHasMoreThanExpectedCount()
    {
        var collection = new List<int> { 1, 2, 3 };
        var rule = CollectionRules.HasAtMost<int>(2);
        var result = rule(collection);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("AtMostEvaluator");
        result.Message.ShouldContain($"| Attempted Value: {collection.Count}");
        result.Message.ShouldContain("type: Int32");
        result.Message.ShouldContain("max: 2");
    }

    [Test]
    public void HasBetween_ShouldReturnTrue_WhenCollectionHasCountWithinRange()
    {
        var collection = new List<int> { 1, 2, 3 };
        var rule = CollectionRules.HasBetween<int>(2, 4);
        var result = rule(collection);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void HasBetween_ShouldReturnFalse_WhenCollectionHasCountOutsideRange()
    {
        var collection = new List<int> { 1, 2, 3 };
        var rule = CollectionRules.HasBetween<int>(4, 5);
        var result = rule(collection);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("BetweenEvaluator");
        result.Message.ShouldContain($"| Attempted Value: {collection.Count}");
        result.Message.ShouldContain("type: Int32");
        result.Message.ShouldContain("min: 4");
        result.Message.ShouldContain("max: 5");
    }
}
