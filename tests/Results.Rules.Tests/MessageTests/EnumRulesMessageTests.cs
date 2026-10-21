using Toarnbeike.Results.Rules;
using Toarnbeike.Rules.Tests.MessageTemplateConfiguration;

namespace Toarnbeike.Rules.Tests.MessageTests;

[ParallelGroup(nameof(DefaultMessageTemplateGroup))]
public class EnumRulesMessageTests : DefaultMessageTemplateGroup
{
    [Test]
    public void IsDefined_ShouldReturnCorrectMessage_ForUndefinedEnumValue()
    {
        var value = (DayOfWeek)100; // Undefined value
        var result = EnumRules.IsDefined<DayOfWeek>()(value);
        result.Message.ShouldBe("'100' is not valid, must be a defined DayOfWeek.");
    }

    [Test]
    public void OneOf_ShouldReturnCorrectMessage_ForNonAcceptedEnumValue()
    {
        var value = DayOfWeek.Wednesday;
        var result = EnumRules.OneOf(DayOfWeek.Monday, DayOfWeek.Tuesday)(value);
        result.Message.ShouldBe("'Wednesday' is not valid, must be one of the accepted DayOfWeek values: [Monday, Tuesday].");
    }

    [Test]
    public void NotOneOf_ShouldReturnCorrectMessage_ForRejectedEnumValue()
    {
        var value = DayOfWeek.Tuesday;
        var result = EnumRules.NotOneOf(DayOfWeek.Tuesday, DayOfWeek.Wednesday)(value);
        result.Message.ShouldBe("'Tuesday' is not valid, must not be one of the rejected DayOfWeek values: [Tuesday, Wednesday].");
    }
}
