using Toarnbeike.Results.Rules.Tests.MessageTemplateConfiguration;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Tests.TypeRules;

[ParallelGroup(nameof(TestMessageTemplateGroup))]
public class BoolRuleTests : TestMessageTemplateGroup
{
    [Test]
    public void IsTrue_ShouldReturnTrue_WhenValueIsTrue()
    {
        var rule = BoolRules.IsTrue();
        var result = rule(true);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void IsTrue_ShouldReturnFalse_WhenValueIsFalse()
    {
        var rule = BoolRules.IsTrue();
        var result = rule(false);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("EqualToEvaluator");
        result.Message.ShouldContain("| Attempted Value: False");
        result.Message.ShouldContain("type: Boolean");
    }

    [Test]
    public void IsFalse_ShouldReturnTrue_WhenValueIsFalse()
    {
        var rule = BoolRules.IsFalse();
        var result = rule(false);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void IsFalse_ShouldReturnFalse_WhenValueIsTrue()
    {
        var rule = BoolRules.IsFalse();
        var result = rule(true);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("EqualToEvaluator");
        result.Message.ShouldContain("type: Boolean");
        result.Message.ShouldContain("| Attempted Value: True");
    }
}
