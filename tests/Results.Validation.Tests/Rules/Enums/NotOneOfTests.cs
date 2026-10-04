using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.Enums;

public class NotOneOfTests
{
    [Test]
    public void NotOneOf_Predicate_Succeeds_ForRejectedValue()
    {
        var rule = EnumRules.NotOneOf([DayOfWeek.Monday, DayOfWeek.Tuesday], null);
        var value = DayOfWeek.Wednesday;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void NotOneOf_Predicate_Succeeds_WhenNoRejectedValues()
    {
        var rule = EnumRules.NotOneOf<DayOfWeek>([], null);
        var value = DayOfWeek.Monday;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void NotOneOf_Predicate_Fails_ForAcceptedValue()
    {
        var rule = EnumRules.NotOneOf([DayOfWeek.Monday, DayOfWeek.Tuesday], null);
        var value = DayOfWeek.Monday;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void NotOneOf_Predicate_Succeeds_ForUndefinedEnumValue()
    {
        var rule = EnumRules.NotOneOf([DayOfWeek.Monday, DayOfWeek.Tuesday], null);
        var value = (DayOfWeek)100;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void NotOneOf_Message_ListsRejectedValues()
    {
        var rule = EnumRules.NotOneOf([DayOfWeek.Monday, DayOfWeek.Tuesday], null);
        rule.Message.ShouldBe("Value must not be one of [Monday, Tuesday].");
    }

    [Test]
    public void NotOneOf_UsesCustomMessage()
    {
        var rule = EnumRules.NotOneOf([DayOfWeek.Monday, DayOfWeek.Tuesday], "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
