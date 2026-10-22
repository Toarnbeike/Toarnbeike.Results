using Toarnbeike.Results.Rules.Tests.MessageTemplateConfiguration;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Tests.TypeRules;

[ParallelGroup(nameof(TestMessageTemplateGroup))]
public class DateRuleTests : TestMessageTemplateGroup
{
    [Test]
    public void OnOrAfter_ShouldReturnValidResult_ForDateOnOrAfterSpecifiedDate()
    {
        var value = new DateTime(2024, 1, 2, 12, 0, 0, DateTimeKind.Local);
        var result = DateRules.OnOrAfter(new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Local))(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void OnOrAfter_ShouldReturnInvalidResult_ForDateBeforeSpecifiedDate()
    {
        var value = new DateOnly(2024, 1, 1);
        var result = DateRules.OnOrAfter(new DateOnly(2024, 1, 2))(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("AtLeastEvaluator");
        result.Message.ShouldContain("| Attempted Value: 01/01/2024");
        result.Message.ShouldContain("type: DateOnly");
        result.Message.ShouldContain("min: 01/02/2024");
    }

    [Test]
    public void OnOrBefore_ShouldReturnValidResult_ForDateOnOrBeforeSpecifiedDate()
    {
        var value = new TimeOnly(12, 0, 0);
        var result = DateRules.OnOrBefore(new TimeOnly(14, 0, 0))(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void OnOrBefore_ShouldReturnInvalidResult_ForDateAfterSpecifiedDate()
    {
        var value = new DateOnly(2024, 1, 3);
        var result = DateRules.OnOrBefore(new DateOnly(2024, 1, 2))(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("AtMostEvaluator");
        result.Message.ShouldContain("| Attempted Value: 01/03/2024");
        result.Message.ShouldContain("type: DateOnly");
        result.Message.ShouldContain("max: 01/02/2024");
    }

    [Test]
    public void Between_ShouldReturnValidResult_ForDateWithinRange()
    {
        var value = new DateTimeOffset(2024, 1, 2, 12, 0, 0, TimeSpan.Zero);
        var result = DateRules.Between(new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero), new DateTimeOffset(2024, 1, 3, 12, 0, 0, TimeSpan.Zero))(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Between_ShouldReturnInvalidResult_ForDateOutsideRange()
    {
        var value = new TimeOnly(10, 0, 0);
        var result = DateRules.Between(new TimeOnly(12, 0, 0), new TimeOnly(14, 0, 0))(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("BetweenEvaluator");
        result.Message.ShouldContain("| Attempted Value: 10:00");
        result.Message.ShouldContain("type: TimeOnly");
        result.Message.ShouldContain("min: 12:00");
        result.Message.ShouldContain("max: 14:00");
    }

    // UTC Now is fixed to 2024-01-01 12:00:00 for testing purposes

    [Test]
    public void Future_ShouldReturnValidResult_ForDateInFuture()
    {
        var value = new DateTime(2024, 1, 1, 13, 0, 0, DateTimeKind.Utc);
        var result = DateRules.Future<DateTime>()(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Future_ShouldReturnInvalidResult_ForDateInPast()
    {
        var value = new DateOnly(2023, 12, 31);
        var result = DateRules.Future<DateOnly>()(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("AtLeastEvaluator");
        result.Message.ShouldContain("| Attempted Value: 12/31/2023");
        result.Message.ShouldContain("type: DateOnly");
        result.Message.ShouldContain("min: 01/01/2024");
    }

    [Test]
    public void Past_ShouldReturnValidResult_ForDateInPast()
    {
        var value = new DateTime(2024, 1, 1, 11, 0, 0, DateTimeKind.Utc);
        var result = DateRules.Past<DateTime>()(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Past_ShouldReturnInvalidResult_ForDateInFuture()
    {
        var value = new DateOnly(2024, 1, 2);
        var result = DateRules.Past<DateOnly>()(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("AtMostEvaluator");
        result.Message.ShouldContain("| Attempted Value: 01/02/2024");
        result.Message.ShouldContain("type: DateOnly");
        result.Message.ShouldContain("max: 01/01/2024");
    }

    [Test]
    public void FutureWithin_ShouldReturnValidResult_ForDateWithinFutureDays()
    {
        var value = new DateOnly(2024, 1, 3);
        var result = DateRules.WithinFuture(5)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void FutureWithin_ShouldReturnInvalidResult_ForDateOutsideFutureDays()
    {
        var value = new DateOnly(2024, 1, 10);
        var result = DateRules.WithinFuture(5)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("BetweenEvaluator");
        result.Message.ShouldContain("| Attempted Value: 01/10/2024");
        result.Message.ShouldContain("type: DateOnly");
        result.Message.ShouldContain("min: 01/01/2024");
        result.Message.ShouldContain("max: 01/06/2024");
    }

    [Test]
    public void FutureWithin_ShouldReturnValidResult_ForDateTimeWithinFutureTimeSpan()
    {
        var value = new DateTime(2024, 1, 2, 12, 0, 0, DateTimeKind.Utc);
        var result = DateRules.WithinFuture(TimeSpan.FromDays(1))(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void FutureWithin_ShouldReturnInvalidResult_ForDateTimeOutsideFutureTimeSpan()
    {
        var value = new DateTime(2024, 1, 2, 12, 0, 0, DateTimeKind.Utc);
        var result = DateRules.WithinFuture(TimeSpan.FromHours(14))(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("BetweenEvaluator");
        result.Message.ShouldContain("| Attempted Value: 01/02/2024 12:00:00");
        result.Message.ShouldContain("type: DateTimeOffset");
        result.Message.ShouldContain("min: 01/01/2024 12:00:00");
        result.Message.ShouldContain("max: 01/02/2024 02:00:00");
    }

    [Test]
    public void PastWithin_ShouldReturnValidResult_ForDateWithinPastDays()
    {
        var value = new DateOnly(2023, 12, 30);
        var result = DateRules.WithinPast(5)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void PastWithin_ShouldReturnInvalidResult_ForDateOutsidePastDays()
    {
        var value = new DateOnly(2023, 12, 25);
        var result = DateRules.WithinPast(5)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("BetweenEvaluator");
        result.Message.ShouldContain("| Attempted Value: 12/25/2023");
        result.Message.ShouldContain("type: DateOnly");
        result.Message.ShouldContain("min: 12/27/2023");
        result.Message.ShouldContain("max: 01/01/2024");
    }

    [Test]
    public void PastWithin_ShouldReturnValidResult_ForDateTimeWithinPastTimeSpan()
    {
        var value = new DateTime(2023, 12, 31, 12, 0, 0, DateTimeKind.Utc);
        var result = DateRules.WithinPast(TimeSpan.FromDays(1))(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void PastWithin_ShouldReturnInvalidResult_ForDateTimeOutsidePastTimeSpan()
    {
        var value = new DateTime(2023, 12, 30, 12, 0, 0, DateTimeKind.Utc);
        var result = DateRules.WithinPast(TimeSpan.FromHours(14))(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("BetweenEvaluator");
        result.Message.ShouldContain("| Attempted Value: 12/30/2023 12:00");
        result.Message.ShouldContain("type: DateTimeOffset");
        result.Message.ShouldContain("min: 12/31/2023 22:00");
        result.Message.ShouldContain("max: 01/01/2024 12:00");
    }
}
