using Toarnbeike.Results.Rules;
using Toarnbeike.Rules.Tests.MessageTemplateConfiguration;

namespace Toarnbeike.Rules.Tests;

[ParallelGroup(nameof(TestMessageTemplateGroup))]
public class PredicateRuleTests : TestMessageTemplateGroup
{
    [Test]
    public void Satisfies_ShouldReturnTrue_WhenPredicateIsSatisfied()
    {
        var rule = PredicateRules.Satisfies<int>(x => x > 0);
        var result = rule(5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Satisfies_ShouldReturnFalse_WhenPredicateIsNotSatisfied()
    {
        var rule = PredicateRules.Satisfies<int>(x => x > 0);
        var result = rule(-5);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("SatisfiesEvaluator");
        result.Message.ShouldContain("| Attempted Value: -5");
        result.Message.ShouldContain("type: Int32");
    }

    [Test]
    public void NotSatisfies_ShouldReturnTrue_WhenPredicateIsNotSatisfied()
    {
        var rule = PredicateRules.NotSatisfies<int>(x => x > 0);
        var result = rule(-5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void NotSatisfies_ShouldReturnFalse_WhenPredicateIsSatisfied()
    {
        var rule = PredicateRules.NotSatisfies<int>(x => x > 0);
        var result = rule(5);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("NotSatisfiesEvaluator");
        result.Message.ShouldContain("| Attempted Value: 5");
        result.Message.ShouldContain("type: Int32");
    }

    [Test]
    public void SatisfiesAsync_ShouldReturnTrue_WhenPredicateIsSatisfied()
    {
        var rule = PredicateRules.SatisfiesAsync<int>(async x => await Task.FromResult(x > 0));
        var result = rule(5).Result;
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void SatisfiesAsync_ShouldReturnFalse_WhenPredicateIsNotSatisfied()
    {
        var rule = PredicateRules.SatisfiesAsync<int>(async x => await Task.FromResult(x > 0));
        var result = rule(-5).Result;
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("SatisfiesAsyncEvaluator");
        result.Message.ShouldContain("| Attempted Value: -5");
        result.Message.ShouldContain("type: Int32");
    }

    [Test]
    public void NotSatisfiesAsync_ShouldReturnTrue_WhenPredicateIsNotSatisfied()
    {
        var rule = PredicateRules.NotSatisfiesAsync<int>(async x => await Task.FromResult(x > 0));
        var result = rule(-5).Result;
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void NotSatisfiesAsync_ShouldReturnFalse_WhenPredicateIsSatisfied()
    {
        var rule = PredicateRules.NotSatisfiesAsync<int>(async x => await Task.FromResult(x > 0));
        var result = rule(5).Result;
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("NotSatisfiesAsyncEvaluator");
        result.Message.ShouldContain("| Attempted Value: 5");
        result.Message.ShouldContain("type: Int32");
    }
}
