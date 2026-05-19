//using Toarnbeike.Results.Ensure.Extensions;
//using Toarnbeike.Results.TestExtensions;

//namespace Toarnbeike.Results.Ensure.Tests.Extensions;

//// Please note: DateOnlyExtensionTests, DateTimeExtensionTests and DateTimeOffsetExtensionTests
//// are copies of each other, besides the types and the failure messages.
//// When modifying or adding a test here, check if the change must also be performed in the other classes.
//public class DateTimeExtensionTests : InvariantCultureTestBase
//{
//    private readonly DateTime _date = new(2026, 1, 1); //Thursday
//    private readonly DateTime _earlier = new(2025, 12, 31);
//    private readonly DateTime _later = new(2026, 1, 2);

//    private readonly string _customFailureMessage = "CustomFailureMessage";

//    /*
//     * Please note: comparison feels unintuitive, but is correct.
//     * WhenEarlier: comparison is after _date, so use _later as comparison.
//     * WhenLater: comparison is before _date, so use _earlier as comparison.
//     */

//    [Test]
//    public void After_Should_ReturnFormattedFailure()
//    {
//        var result = _date.After(_later);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("OnOrAfter");
//        failure.Message.ShouldBe("'_date' must be after 01/02/2026 00:00:00, but is 01/01/2026 00:00:00.");
//    }

//    [Test]
//    public void After_Should_ReturnCustomMessageFailure()
//    {
//        var result = _date.After(_later, _customFailureMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customFailureMessage);
//    }

//    [Test]
//    public void Before_Should_ReturnFormattedFailure()
//    {
//        var result = _date.Before(_earlier);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("OnOrBefore");
//        failure.Message.ShouldBe("'_date' must be before 12/31/2025 00:00:00, but is 01/01/2026 00:00:00.");
//    }

//    [Test]
//    public void Before_Should_ReturnCustomMessageFailure()
//    {
//        var result = _date.Before(_earlier, _customFailureMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customFailureMessage);
//    }

//    [Test]
//    public void Between_Should_ReturnFormattedFailure_WhenTooEarly()
//    {
//        var result = _date.Between(_later, _later);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("OnOrAfter");
//        failure.Message.ShouldBe("'_date' must be after 01/02/2026 00:00:00, but is 01/01/2026 00:00:00.");
//    }

//    [Test]
//    public void Between_Should_ReturnFormattedFailure_WhenTooLate()
//    {
//        var result = _date.Between(_earlier, _earlier);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("OnOrBefore");
//        failure.Message.ShouldBe("'_date' must be before 12/31/2025 00:00:00, but is 01/01/2026 00:00:00.");
//    }

//    [Test]
//    public void Between_Should_ReturnCustomMessageFailure()
//    {
//        var result = _date.Between(_earlier, _earlier, _customFailureMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("OnOrBefore");
//        failure.Message.ShouldBe(_customFailureMessage);
//    }

//    [Test]
//    public void InPast_Should_ReturnFormattedFailure()
//    {
//        var date = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
//        var result = date.Past();
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("OnOrBefore");
//        failure.Message.ShouldContain("'date' must be before ");
//    }

//    [Test]
//    public void InPast_Should_ReturnCustomMessageFailure()
//    {
//        var date = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
//        var result = date.Past(_customFailureMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customFailureMessage);
//    }

//    [Test]
//    public void InFuture_Should_ReturnFormattedFailure()
//    {
//        var date = DateOnly.FromDateTime(DateTime.Today).AddDays(-1);
//        var result = date.Future();
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("OnOrAfter");
//        failure.Message.ShouldContain("'date' must be after ");
//    }

//    [Test]
//    public void InFuture_Should_ReturnCustomMessageFailure()
//    {
//        var date = DateOnly.FromDateTime(DateTime.Today).AddDays(-1);
//        var result = date.Future(_customFailureMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customFailureMessage);
//    }

//    [Test]
//    public void InPastWithin_Should_ReturnFormattedFailure()
//    {
//        var date = DateOnly.FromDateTime(DateTime.Today).AddDays(91);
//        var result = date.PastWithin(TimeSpan.FromDays(90));
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("OnOrBefore");
//        failure.Message.ShouldContain("'date' must be before ");
//    }

//    [Test]
//    public void InPastWithin_Should_ReturnCustomMessageFailure()
//    {
//        var date = DateOnly.FromDateTime(DateTime.Today).AddDays(91);
//        var result = date.PastWithin(TimeSpan.FromDays(90), _customFailureMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customFailureMessage);
//    }

//    [Test]
//    public void InFutureWithin_Should_ReturnFormattedFailure()
//    {
//        var date = DateOnly.FromDateTime(DateTime.Today).AddDays(-91);
//        var result = date.InFutureWithin(TimeSpan.FromDays(90));
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("OnOrAfter");
//        failure.Message.ShouldContain("'date' must be after ");
//    }

//    [Test]
//    public void InFutureWithin_Should_ReturnCustomMessageFailure()
//    {
//        var date = DateOnly.FromDateTime(DateTime.Today).AddDays(-91);
//        var result = date.InFutureWithin(TimeSpan.FromDays(90), _customFailureMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customFailureMessage);
//    }

//    [Test]
//    public void OnDayOfWeek_Should_ReturnFormattedFailure()
//    {
//        var result = _date.OnDayOfWeek(DayOfWeek.Monday);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("OnDayOfWeek");
//        failure.Message.ShouldBe("'_date' must be on a Monday, but is a Thursday.");
//    }

//    [Test]
//    public void OnDayOfWeek_Should_ReturnCustomMessageFailure()
//    {
//        var result = _date.OnDayOfWeek(DayOfWeek.Monday, _customFailureMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customFailureMessage);
//    }

//    [Test]
//    public void OnDayOfWeekFrom_Should_ReturnFormattedFailure()
//    {
//        var result = _date.OnDayOfWeekFrom([DayOfWeek.Monday, DayOfWeek.Tuesday]);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("OnDaysOfWeek");
//        failure.Message.ShouldBe("'_date' must be on one of the following days: Monday, Tuesday, but is a Thursday.");
//    }

//    [Test]
//    public void OnDayOfWeekFrom_Should_ReturnCustomMessageFailure()
//    {
//        var result = _date.OnDayOfWeekFrom([DayOfWeek.Monday, DayOfWeek.Tuesday], _customFailureMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customFailureMessage);
//    }

//    [Test]
//    public void OnWeekday_Should_ReturnFormattedFailure()
//    {
//        var date = _date.AddDays(2); // to make it a saturday.
//        var result = date.OnWeekday();
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("OnWeekday");
//        failure.Message.ShouldBe("'date' must be on a weekday, but is a Saturday.");
//    }

//    [Test]
//    public void OnWeekday_Should_ReturnCustomMessageFailure()
//    {
//        var date = _date.AddDays(2); // to make it a saturday.
//        var result = date.OnWeekday(_customFailureMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customFailureMessage);
//    }

//    [Test]
//    public void OnWeekend_Should_ReturnFormattedFailure()
//    {
//        var result = _date.OnWeekend();
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("OnWeekend");
//        failure.Message.ShouldBe("'_date' must be on a weekend day, but is a Thursday.");
//    }

//    [Test]
//    public void OnWeekend_Should_ReturnCustomMessageFailure()
//    {
//        var result = _date.OnWeekend(_customFailureMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customFailureMessage);
//    }
//}