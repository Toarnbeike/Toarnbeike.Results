using Toarnbeike.Results.Rules;
using Toarnbeike.Rules.Tests.MessageTemplateConfiguration;

namespace Toarnbeike.Rules.Tests.MessageTests;

[ParallelGroup(nameof(DefaultMessageTemplateGroup))]
public class CollectionRuleMessageTests : DefaultMessageTemplateGroup
{
    [Test]
    public void Contains_ShouldReturnCorrectMessage_WhenItemIsNotInCollection()
    {
        var collection = new List<int> { 1, 2, 3 };
        var rule = CollectionRules.Contains(4);
        var result = rule(collection);
        result.Message.ShouldBe("Collection is not valid, must contain '4'.");
    }

    [Test]
    public void NotContains_ShouldReturnCorrectMessage_WhenItemIsInCollection()
    {
        var collection = new List<int> { 1, 2, 3 };
        var rule = CollectionRules.NotContains(2);
        var result = rule(collection);
        result.Message.ShouldBe("Collection is not valid, must not contain '2'.");
    }

    [Test]
    public void HasExactly_ShouldReturnCorrectMessage_WhenCollectionDoesNotHaveExpectedCount()
    {
        var collection = new List<int> { 1, 2, 3 };
        var rule = CollectionRules.HasExactly<int>(4);
        var result = rule(collection);
        result.Message.ShouldBe("Collection is not valid, must have exactly '4' items, but has '3'.");
    }

    [Test]
    public void HasAtLeast_ShouldReturnCorrectMessage_WhenCollectionDoesNotHaveMinimumCount()
    {
        var collection = new List<int> { 1, 2, 3 };
        var rule = CollectionRules.HasAtLeast<int>(5);
        var result = rule(collection);
        result.Message.ShouldBe("Collection is not valid, must have at least '5' items, but has '3'.");
    }

    [Test]
    public void HasAtMost_ShouldReturnCorrectMessage_WhenCollectionExceedsMaximumCount()
    {
        var collection = new List<int> { 1, 2, 3, 4, 5 };
        var rule = CollectionRules.HasAtMost<int>(3);
        var result = rule(collection);
        result.Message.ShouldBe("Collection is not valid, must have at most '3' items, but has '5'.");
    }

    [Test]
    public void HasBetween_ShouldReturnCorrectMessage_WhenCollectionIsOutsideRange()
    {
        var collection = new List<int> { 1, 2, 3 };
        var rule = CollectionRules.HasBetween<int>(4, 6);
        var result = rule(collection);
        result.Message.ShouldBe("Collection is not valid, must have between '4' and '6' items, but has '3'.");
    }   
}

[ParallelGroup(nameof(DefaultMessageTemplateGroup))]
public class DateRuleMessageTests : DefaultMessageTemplateGroup
{
    [Test]
    public void OnOrAfter_ShouldReturnCorrectMessage_ForDateBeforeMinimum()
    {
        var value = new DateOnly(2023, 1, 1);
        var minDate = new DateOnly(2023, 1, 2);
        var result = DateRules.OnOrAfter(minDate)(value);
        result.Message.ShouldBe("'01/01/2023' is not valid, must be on or after '01/02/2023'.");
    }

    [Test]
    public void OnOrBefore_ShouldReturnCorrectMessage_ForDateAfterMaximum()
    {
        var value = new DateOnly(2023, 1, 3);
        var maxDate = new DateOnly(2023, 1, 2);
        var result = DateRules.OnOrBefore(maxDate)(value);
        result.Message.ShouldBe("'01/03/2023' is not valid, must be on or before '01/02/2023'.");
    }

    [Test]
    public void Between_ShouldReturnCorrectMessage_ForDateOutsideRange()
    {
        var value = new DateOnly(2023, 1, 4);
        var minDate = new DateOnly(2023, 1, 2);
        var maxDate = new DateOnly(2023, 1, 3);
        var result = DateRules.Between(minDate, maxDate)(value);
        result.Message.ShouldBe("'01/04/2023' is not valid, must be between '01/02/2023' and '01/03/2023'.");
    }

    [Test]
    public void Future_ShouldReturnCorrectMessage_ForPastDate()
    {
        var value = new DateOnly(2022, 12, 28);
        var result = DateRules.Future<DateOnly>()(value);
        result.Message.ShouldBe("'12/28/2022' is not valid, must be a future DateOnly.");
    }

    [Test]
    public void Future_ShouldReturnCorrectMessage_ForPastDateTime()
    {
        var value = new DateTime(2022, 12, 28, 12, 0, 0, DateTimeKind.Local);
        var result = DateRules.Future<DateTime>()(value);
        result.Message.ShouldBe("'12/28/2022 12:00:00' is not valid, must be a future DateTime.");
    }

    [Test]
    public void Past_ShouldReturnCorrectMessage_ForFutureDate()
    {
        var value = new DateOnly(2024, 1, 2);
        var result = DateRules.Past<DateOnly>()(value);
        result.Message.ShouldBe("'01/02/2024' is not valid, must be a past DateOnly.");
    }

    [Test]
    public void Past_ShouldReturnCorrectMessage_ForFutureDateTime()
    {
        var value = new DateTime(2024, 1, 1, 14, 0, 0, DateTimeKind.Utc);
        var result = DateRules.Past<DateTime>()(value);
        result.Message.ShouldBe("'01/01/2024 14:00:00' is not valid, must be a past DateTime.");
    }

    [Test]
    public void WithinFuture_ShouldReturnCorrectMessage_ForDateOutsideFutureRange()
    {
        var value = new DateOnly(2023, 1, 10);
        var result = DateRules.WithinFuture(5)(value);
        result.Message.ShouldBe("'01/10/2023' is not valid, must be a future DateOnly before '01/06/2023'.");
    }

    [Test]
    public void WithinFuture_ShouldReturnCorrectMessage_ForDateTimeOutsideFutureRange()
    {
        var value = new DateTime(2023, 1, 10, 12, 0, 0, DateTimeKind.Utc);
        var result = DateRules.WithinFuture(TimeSpan.FromDays(5))(value);
        result.Message.ShouldBe("'01/10/2023 12:00:00 +00:00' is not valid, must be a future DateTimeOffset before '01/06/2023 12:00:00 +00:00'.");
    }

    [Test]
    public void WithinPast_ShouldReturnCorrectMessage_ForDateOutsidePastRange()
    {
        var value = new DateOnly(2022, 12, 25);
        var result = DateRules.WithinPast(5)(value);
        result.Message.ShouldBe("'12/25/2022' is not valid, must be a past DateOnly after '12/27/2022'.");
    }

    [Test]
    public void WithinPast_ShouldReturnCorrectMessage_ForDateTimeOutsidePastRange()
    {
        var value = new DateTime(2022, 12, 25, 12, 0, 0, DateTimeKind.Utc);
        var result = DateRules.WithinPast(TimeSpan.FromDays(5))(value);
        result.Message.ShouldBe("'12/25/2022 12:00:00 +00:00' is not valid, must be a past DateTimeOffset after '12/27/2022 12:00:00 +00:00'.");
    }
}
