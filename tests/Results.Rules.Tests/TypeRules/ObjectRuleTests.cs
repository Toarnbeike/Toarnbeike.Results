using Toarnbeike.Results.Rules.Tests.MessageTemplateConfiguration;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Tests.TypeRules;

[ParallelGroup(nameof(TestMessageTemplateGroup))]
public class ObjectRuleTests : TestMessageTemplateGroup
{
    [Test]
    public void EqualTo_ShouldReturnTrue_WhenValuesAreEqual()
    {
        var rule = ObjectRules.EqualTo<int>(5);
        var result = rule(5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void EqualTo_ShouldReturnFalse_WhenValuesAreNotEqual()
    {
        var rule = ObjectRules.EqualTo<int>(5);
        var result = rule(10);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("EqualityEvaluator");
        result.Message.ShouldContain("| Attempted Value: 10");
        result.Message.ShouldContain("type: Int32");
    }

    [Test]
    public void NotEqualTo_ShouldReturnTrue_WhenValuesAreNotEqual()
    {
        var rule = ObjectRules.NotEqualTo<int>(5);
        var result = rule(10);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void NotEqualTo_ShouldReturnFalse_WhenValuesAreEqual()
    {
        var rule = ObjectRules.NotEqualTo<int>(5);
        var result = rule(5);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("NotEqualityEvaluator");
        result.Message.ShouldContain("| Attempted Value: 5");
        result.Message.ShouldContain("type: Int32");
    }


    [Test]
    public void Satisfies_ShouldReturnTrue_WhenPredicateIsSatisfied()
    {
        var rule = ObjectRules.Satisfies<int>(x => x > 0);
        var result = rule(5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Satisfies_ShouldReturnFalse_WhenPredicateIsNotSatisfied()
    {
        var rule = ObjectRules.Satisfies<int>(x => x > 0);
        var result = rule(-5);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("SatisfiesEvaluator");
        result.Message.ShouldContain("| Attempted Value: -5");
        result.Message.ShouldContain("type: Int32");
    }

    [Test]
    public void NotSatisfies_ShouldReturnTrue_WhenPredicateIsNotSatisfied()
    {
        var rule = ObjectRules.NotSatisfies<int>(x => x > 0);
        var result = rule(-5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void NotSatisfies_ShouldReturnFalse_WhenPredicateIsSatisfied()
    {
        var rule = ObjectRules.NotSatisfies<int>(x => x > 0);
        var result = rule(5);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("NotSatisfiesEvaluator");
        result.Message.ShouldContain("| Attempted Value: 5");
        result.Message.ShouldContain("type: Int32");
    }

    [Test]
    public void SatisfiesAsync_ShouldReturnTrue_WhenPredicateIsSatisfied()
    {
        var rule = ObjectRules.SatisfiesAsync<int>(async x => await Task.FromResult(x > 0));
        var result = rule(5).Result;
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void SatisfiesAsync_ShouldReturnFalse_WhenPredicateIsNotSatisfied()
    {
        var rule = ObjectRules.SatisfiesAsync<int>(async x => await Task.FromResult(x > 0));
        var result = rule(-5).Result;
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("SatisfiesAsyncEvaluator");
        result.Message.ShouldContain("| Attempted Value: -5");
        result.Message.ShouldContain("type: Int32");
    }

    [Test]
    public void NotSatisfiesAsync_ShouldReturnTrue_WhenPredicateIsNotSatisfied()
    {
        var rule = ObjectRules.NotSatisfiesAsync<int>(async x => await Task.FromResult(x > 0));
        var result = rule(-5).Result;
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void NotSatisfiesAsync_ShouldReturnFalse_WhenPredicateIsSatisfied()
    {
        var rule = ObjectRules.NotSatisfiesAsync<int>(async x => await Task.FromResult(x > 0));
        var result = rule(5).Result;
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("NotSatisfiesAsyncEvaluator");
        result.Message.ShouldContain("| Attempted Value: 5");
        result.Message.ShouldContain("type: Int32");
    }
}
