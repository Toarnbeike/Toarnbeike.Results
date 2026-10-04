using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.Dates;

public class OnOrAfterTests
{
    private readonly DateOnly _date = new(2023, 1, 1);
    private readonly DateOnly _later = new(2023, 1, 2);
    private readonly DateOnly _earlier = new(2022, 12, 31);

    [Test]
    public void OnOrAfter_Predicate_Succeeds_WhenValueEqualsMin()
    {
        var rule = DateRules.OnOrAfter(_date, null);
        rule.Predicate(_date).ShouldBeTrue();
    }

    [Test]
    public void OnOrAfter_Predicate_Succeeds_WhenValueExceedsMin()
    {
        var rule = DateRules.OnOrAfter(_date, null);
        rule.Predicate(_later).ShouldBeTrue();
    }

    [Test]
    public void OnOrAfter_Predicate_Fails_WhenValueIsBeforeMin()
    {
        var rule = DateRules.OnOrAfter(_date, null);
        rule.Predicate(_earlier).ShouldBeFalse();
    }

    [Test]
    public void OnOrAfter_UsesCustomMessage()
    {
        var customMessage = "Custom message";
        var rule = DateRules.OnOrAfter(_date, customMessage);
        rule.Message.ShouldBe(customMessage);
    }
}
