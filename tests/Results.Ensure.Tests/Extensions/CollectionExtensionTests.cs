using Toarnbeike.Results.Ensure.Extensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class CollectionExtensionTests
{
    [Test]
    public void Empty_Should_ReturnFormattedFailure()
    {
        IEnumerable<string> list = ["item"];
        var result = list.Empty();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("HasNoItems");
        failure.Message.ShouldBe("'list' must be empty, but contains 1.");
    }

    [Test]
    public void Empty_Should_ReturnCustomMessageFailure()
    {
        IEnumerable<string> list = ["item"];
        var result = list.Empty("Custom failure message.");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom failure message.");
    }

    [Test]
    public void NotEmpty_Should_ReturnFormattedFailure()
    {
        IEnumerable<string> list = [];
        var result = list.NotEmpty();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("HasItems");
        failure.Message.ShouldBe("'list' must not be empty, but is.");
    }

    [Test]
    public void NotEmpty_Should_ReturnCustomMessageFailure()
    {
        IEnumerable<string> list = [];
        var result = list.NotEmpty("Custom failure message.");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom failure message.");
    }

    [Test]
    public void AtLeast_Should_ReturnFormattedFailure()
    {
        IEnumerable<string> list = ["item1", "item2"];
        var result = list.AtLeast(3);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("AtLeast");
        failure.Message.ShouldBe("'list' must contain at least 3 items, but contains 2.");
    }

    [Test]
    public void AtLeast_Should_ReturnCustomMessageFailure()
    {
        IEnumerable<string> list = ["item1", "item2"];
        var result = list.AtLeast(3, "Custom failure message.");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom failure message.");
    }

    [Test]
    public void AtMost_Should_ReturnFormattedFailure()
    {
        IEnumerable<string> list = ["item1", "item2", "item3"];
        var result = list.AtMost(2);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("AtMost");
        failure.Message.ShouldBe("'list' must contain at most 2 items, but contains 3.");
    }

    [Test]
    public void AtMost_Should_ReturnCustomMessageFailure()
    {
        IEnumerable<string> list = ["item1", "item2", "item3"];
        var result = list.AtMost(2, "Custom failure message.");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom failure message.");
    }

    [Test]
    public void Between_Should_ReturnTooManyFormattedFailure()
    {
        IEnumerable<string> list = ["item1", "item2", "item3"];
        var result = list.Between(1, 2);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("AtMost");
        failure.Message.ShouldBe("'list' must contain at most 2 items, but contains 3.");
    }

    [Test]
    public void Between_Should_ReturnTooFewFormattedFailure()
    {
        IEnumerable<string> list = ["item1", "item2", "item3"];
        var result = list.Between(4, 5);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("AtLeast");
        failure.Message.ShouldBe("'list' must contain at least 4 items, but contains 3.");
    }

    [Test]
    public void Between_Should_ReturnCustomMessageFailure()
    {
        IEnumerable<string> list = ["item1", "item2", "item3"];
        var result = list.Between(4, 5, "Custom failure message.");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom failure message.");
    }
}
