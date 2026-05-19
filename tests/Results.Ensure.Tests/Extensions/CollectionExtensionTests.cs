using Toarnbeike.Results.Ensure.Extensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class CollectionExtensionTests
{
    [Test]
    public void NotEmpty_Should_ReturnFormattedFailure()
    {
        List<string> list = [];
        var result = list.NotEmpty();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: list,
            expectedArgumentName: "list",
            expectedGuardName: "CollectionRules.NotEmpty",
            expectedMessage: "expected");
        //"'list' must not be empty, but is.");
    }

    [Test]
    public void Empty_Should_ReturnFormattedFailure()
    {
        List<string> list = ["item"];
        var result = list.Empty();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: list,
            expectedArgumentName: "list",
            expectedGuardName: "CollectionRules.Empty");
    }

    [Test]
    public void AtLeast_Should_ReturnFormattedFailure()
    {
        List<string> list = ["item1", "item2"];
        var result = list.AtLeast(3);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: list,
            expectedArgumentName: "list",
            expectedGuardName: "CollectionRules.AtLeast");
    }

    [Test]
    public void AtMost_Should_ReturnFormattedFailure()
    {
        List<string> list = ["item1", "item2", "item3"];
        var result = list.AtMost(2);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: list,
            expectedArgumentName: "list",
            expectedGuardName: "CollectionRules.AtMost");
    }

    [Test]
    public void Between_Should_ReturnTooManyFormattedFailure()
    {
        List<string> list = ["item1", "item2", "item3"];
        var result = list.Between(1, 2);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: list,
            expectedArgumentName: "list",
            expectedGuardName: "CollectionRules.Between");
    }

    [Test]
    public void Exactly_Should_ReturnTooManyFormattedFailure()
    {
        List<string> list = ["item1", "item2", "item3"];
        var result = list.Exactly(2);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: list,
            expectedArgumentName: "list",
            expectedGuardName: "CollectionRules.Exactly");
    }

    [Test]
    public void Single_Should_ReturnTooManyFormattedFailure()
    {
        List<string> list = ["item1", "item2"];
        var result = list.Single();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: list,
            expectedArgumentName: "list",
            expectedGuardName: "CollectionRules.Single");
    }
}
