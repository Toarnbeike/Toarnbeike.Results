using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.Dates;

public class OnOrBeforeTests
{
    private readonly DateOnly _date = new(2023, 1, 1);
    private readonly DateOnly _later = new(2023, 1, 2);
    private readonly DateOnly _earlier = new(2022, 12, 31);

    [Test]
    public void OnOrBefore_Predicate_Succeeds_WhenValueEqualsMax()
    {
        var rule = DateRules.OnOrBefore(_date, null);
        rule.Predicate(_date).ShouldBeTrue();
    }

    [Test]
    public void OnOrBefore_Predicate_Succeeds_WhenValueIsBeforeMax()
    {
        var rule = DateRules.OnOrBefore(_date, null);
        rule.Predicate(_earlier).ShouldBeTrue();
    }

    [Test]
    public void OnOrBefore_Predicate_Fails_WhenValueExceedsMax()
    {
        var rule = DateRules.OnOrBefore(_date, null);
        rule.Predicate(_later).ShouldBeFalse();
    }

    [Test]
    public void OnOrBefore_UsesCustomMessage()
    {
        var customMessage = "Custom message";
        var rule = DateRules.OnOrBefore(_date, customMessage);
        rule.Message.ShouldBe(customMessage);
    }
}
