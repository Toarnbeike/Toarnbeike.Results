using Toarnbeike.Results.Rules;
using Toarnbeike.Rules.Tests.MessageTemplateConfiguration;

namespace Toarnbeike.Rules.Tests;

[ParallelGroup(nameof(TestMessageTemplateGroup))]
public class EnumRuleTests : TestMessageTemplateGroup
{
    [Test]
    public void IsDefined_ShouldReturnValidResult_ForDefinedEnumValue()
    {
        var value = DayOfWeek.Monday;
        var result = EnumRules.IsDefined<DayOfWeek>()(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void IsDefined_ShouldReturnInvalidResult_ForUndefinedEnumValue()
    {
        var value = (DayOfWeek)100; // Undefined value
        var result = EnumRules.IsDefined<DayOfWeek>()(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("IsDefinedEvaluator");
        result.Message.ShouldContain("| Attempted Value: 100");
        result.Message.ShouldContain("enumType: DayOfWeek");
    }

    [Test]
    public void OneOf_ShouldReturnValidResult_ForAcceptedEnumValue()
    {
        var value = DayOfWeek.Monday;
        var result = EnumRules.OneOf(DayOfWeek.Monday, DayOfWeek.Tuesday)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void OneOf_ShouldReturnInvalidResult_ForNonAcceptedEnumValue()
    {
        var value = DayOfWeek.Wednesday;
        var result = EnumRules.OneOf(DayOfWeek.Monday, DayOfWeek.Tuesday)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("OneOfEvaluator");
        result.Message.ShouldContain("| Attempted Value: Wednesday");
        result.Message.ShouldContain("enumType: DayOfWeek");
        result.Message.ShouldContain("acceptedValues: Monday, Tuesday");
    }

    [Test]
    public void NotOneOf_ShouldReturnValidResult_ForNonRejectedEnumValue()
    {
        var value = DayOfWeek.Monday;
        var result = EnumRules.NotOneOf(DayOfWeek.Tuesday, DayOfWeek.Wednesday)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void NotOneOf_ShouldReturnInvalidResult_ForRejectedEnumValue()
    {
        var value = DayOfWeek.Tuesday;
        var result = EnumRules.NotOneOf(DayOfWeek.Tuesday, DayOfWeek.Wednesday)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("NotOneOfEvaluator");
        result.Message.ShouldContain("| Attempted Value: Tuesday");
        result.Message.ShouldContain("enumType: DayOfWeek");
        result.Message.ShouldContain("rejectedValues: Tuesday, Wednesday");
    }
}