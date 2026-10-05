namespace Toarnbeike.Results.Rules.Tests.Enums;

public class OneOfTests
{
    [Test]
    public void OneOf_Predicate_Succeeds_ForAcceptedValue()
    {
        var rule = EnumRules.OneOf([DayOfWeek.Monday, DayOfWeek.Tuesday], null);
        var value = DayOfWeek.Monday;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void OneOf_Predicate_Succeeds_WhenValueIsLastAcceptedValue()
    {
        var rule = EnumRules.OneOf([DayOfWeek.Monday, DayOfWeek.Tuesday], null);
        var value = DayOfWeek.Tuesday;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void OneOf_Predicate_Succeeds_WhenOnlyAcceptedValue()
    {
        var rule = EnumRules.OneOf([DayOfWeek.Monday], null);
        var value = DayOfWeek.Monday;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void OneOf_Predicate_Fails_ForRejectedValue()
    {
        var rule = EnumRules.OneOf([DayOfWeek.Monday, DayOfWeek.Tuesday], null);
        var value = DayOfWeek.Wednesday;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void OneOf_Predicate_Fails_ForUndefinedEnumValue()
    {
        var rule = EnumRules.OneOf([DayOfWeek.Monday, DayOfWeek.Tuesday], null);
        var value = (DayOfWeek)100;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void OneOf_Predicate_Fails_WhenNoAcceptedValues()
    {
        var rule = EnumRules.OneOf<DayOfWeek>([], null);
        var value = DayOfWeek.Monday;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void OneOf_Predicate_Throws_WhenAcceptedValuesIsNull()
    {
        Should.Throw<ArgumentNullException>(() => EnumRules.OneOf<DayOfWeek>(null!, null));
    }

    [Test]
    public void OneOf_DefaultMessage_ListsAcceptedValues()
    {
        var rule = EnumRules.OneOf([DayOfWeek.Monday, DayOfWeek.Tuesday], null);
        rule.Message.ShouldBe("Value must be one of [Monday, Tuesday].");
    }

    [Test]
    public void OneOf_UsesCustomMessage()
    {
        var rule = EnumRules.OneOf([DayOfWeek.Monday, DayOfWeek.Tuesday], "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
