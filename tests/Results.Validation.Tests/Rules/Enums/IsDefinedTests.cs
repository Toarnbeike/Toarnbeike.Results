using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.Enums;

public class IsDefinedTests
{
    [Test]
    public void IsDefined_Predicate_Succeeds_ForDefinedEnumValue()
    {
        var rule = EnumRules.IsDefined<DayOfWeek>(null);
        var value = DayOfWeek.Monday;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void IsDefined_Predicate_Fails_ForUndefinedEnumValue()
    {
        var rule = EnumRules.IsDefined<DayOfWeek>(null);
        var value = (DayOfWeek)100;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void IsDefined_DefaultMessage_UsesEnumTypeName()
    {
        var rule = EnumRules.IsDefined<DayOfWeek>(null);
        rule.Message.ShouldBe("Value must be a defined value of the DayOfWeek enum.");
    }

    [Test]
    public void IsDefined_UsesCustomMessage()
    {
        var rule = EnumRules.IsDefined<DayOfWeek>("Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
