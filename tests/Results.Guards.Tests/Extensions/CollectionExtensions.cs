using Toarnbeike.Results.Guards.Extensions;

namespace Toarnbeike.Results.Guards.Tests.Extensions;

public class CollectionExtensionTests
{
    [Test]
    public void NotEmpty_Should_ReturnFormattedFailure()
    {
        List<string> list = [];
        var result = list.NotEmpty();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: list,
            expectedMessage: "'list' must not be empty, but is.");
    }

    [Test]
    public void Empty_Should_ReturnFormattedFailure()
    {
        List<string> list = ["item"];
        var result = list.Empty();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: list,
            expectedMessage: "'list' must be empty, but contains 1 item.");
    }

    [Test]
    public void AtLeast_Should_ReturnFormattedFailure()
    {
        List<string> list = ["item1", "item2"];
        var result = list.AtLeast(3);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: list,
            expectedMessage: "'list' must contain at least 3 items, but contains 2 items.");
    }

    [Test]
    public void AtMost_Should_ReturnFormattedFailure()
    {
        List<string> list = ["item1", "item2", "item3"];
        var result = list.AtMost(2);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: list,
            expectedMessage: "'list' must contain at most 2 items, but contains 3 items.");
    }

    [Test]
    public void Between_Should_ReturnTooManyFormattedFailure()
    {
        List<string> list = ["item1", "item2", "item3"];
        var result = list.Between(1, 2);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: list,
            expectedMessage: "'list' must contain between 1 and 2 items, but contains 3 items.");
    }

    [Test]
    public void Exactly_Should_ReturnTooManyFormattedFailure()
    {
        List<string> list = ["item1", "item2", "item3"];
        var result = list.Exactly(2);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: list,
            expectedMessage: "'list' must contain exactly 2 items, but contains 3 items.");
    }

    [Test]
    public void Single_Should_ReturnTooManyFormattedFailure()
    {
        List<string> list = ["item1", "item2"];
        var result = list.Single();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: list,
            expectedMessage: "'list' must contain exactly 1 item, but contains 2 items.");
    }
}