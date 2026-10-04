using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.Dates;

public class MustBeBetweenTests
{
    private readonly DateOnly _min = new(2023, 1, 1);
    private readonly DateOnly _max = new(2023, 1, 31);
    private readonly DateOnly _within = new(2023, 1, 15);
    private readonly DateOnly _before = new(2022, 12, 31);
    private readonly DateOnly _after = new(2023, 2, 1);

    [Test]
    public void MustBeBetween_Predicate_Succeeds_WhenValueIsWithinRange()
    {
        var rule = DateRules.MustBeBetween(_min, _max, null);
        rule.Predicate(_within).ShouldBeTrue();
    }

    [Test]
    public void MustBeBetween_Predicate_Succeeds_WhenValueEqualsMin()
    {
        var rule = DateRules.MustBeBetween(_min, _max, null);
        rule.Predicate(_min).ShouldBeTrue();
    }

    [Test]
    public void MustBeBetween_Predicate_Succeeds_WhenValueEqualsMax()
    {
        var rule = DateRules.MustBeBetween(_min, _max, null);
        rule.Predicate(_max).ShouldBeTrue();
    }

    [Test]
    public void MustBeBetween_Predicate_Fails_WhenValueIsBeforeMin()
    {
        var rule = DateRules.MustBeBetween(_min, _max, null);
        rule.Predicate(_before).ShouldBeFalse();
    }

    [Test]
    public void MustBeBetween_Predicate_Fails_WhenValueIsAfterMax()
    {
        var rule = DateRules.MustBeBetween(_min, _max, null);
        rule.Predicate(_after).ShouldBeFalse();
    }

    [Test]
    public void MustBeBetween_UsesCustomMessage()
    {
        var customMessage = "Custom message";
        var rule = DateRules.MustBeBetween(_min, _max, customMessage);
        rule.Message.ShouldBe(customMessage);
    }
}
