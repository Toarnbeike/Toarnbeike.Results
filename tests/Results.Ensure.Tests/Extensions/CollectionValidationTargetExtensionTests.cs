using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class CollectionExtensionTests
{
    [Test]
    public void NotEmpty_Should_ReturnFailure_WhenEmpty()
    {
        List<string> list = [];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).NotEmpty();

        _ = result.ShouldBeFailureOfType<GuardFailure>();
    }

    [Test]
    public void NotEmpty_Should_ReturnSuccess_WhenNotEmpty()
    {
        List<string> list = ["item"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).NotEmpty();

        result.ShouldBeSuccess();
    }

    [Test]
    public void NotEmpty_Should_UseCustomMessage_WhenProvided()
    {
        List<string> list = [];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).NotEmpty("Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void Empty_Should_ReturnFailure_WhenNotEmpty()
    {
        List<string> list = ["item"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).Empty();

        _ = result.ShouldBeFailureOfType<GuardFailure>();
    }

    [Test]
    public void Empty_Should_ReturnSuccess_WhenEmpty()
    {
        List<string> list = [];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).Empty();

        result.ShouldBeSuccess();
    }

    [Test]
    public void Empty_Should_UseCustomMessage_WhenProvided()
    {
        List<string> list = ["item"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).Empty("Custom message");

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void HasAtLeast_Should_ReturnFailure_WhenBelowMin()
    {
        List<string> list = ["a"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).HasAtLeast(2);

        _ = result.ShouldBeFailureOfType<GuardFailure>();
    }

    [Test]
    public void HasAtLeast_Should_ReturnSuccess_WhenMeetsMin()
    {
        List<string> list = ["a", "b"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).HasAtLeast(2);

        result.ShouldBeSuccess();
    }

    [Test]
    public void HasAtLeast_Should_UseCustomMessage_WhenProvided()
    {
        List<string> list = ["a"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).HasAtLeast(2, "Custom message");

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void HasAtMost_Should_ReturnFailure_WhenAboveMax()
    {
        List<string> list = ["a", "b", "c"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).HasAtMost(2);

        _ = result.ShouldBeFailureOfType<GuardFailure>();
    }

    [Test]
    public void HasAtMost_Should_ReturnSuccess_WhenWithinMax()
    {
        List<string> list = ["a", "b"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).HasAtMost(2);

        result.ShouldBeSuccess();
    }

    [Test]
    public void HasAtMost_Should_UseCustomMessage_WhenProvided()
    {
        List<string> list = ["a", "b", "c"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).HasAtMost(2, "Custom message");

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void HasBetween_Should_ReturnFailure_WhenOutsideRange()
    {
        List<string> list = ["a"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).HasBetween(2, 3);

        _ = result.ShouldBeFailureOfType<GuardFailure>();
    }

    [Test]
    public void HasBetween_Should_ReturnSuccess_WhenWithinRange()
    {
        List<string> list = ["a", "b"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).HasBetween(2, 3);

        result.ShouldBeSuccess();
    }

    [Test]
    public void HasBetween_Should_UseCustomMessage_WhenProvided()
    {
        List<string> list = ["a", "b", "c", "d"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).HasBetween(2, 3, "Custom message");

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void HasExactly_Should_ReturnFailure_WhenNotExact()
    {
        List<string> list = ["a", "b"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).HasExactly(1);

        _ = result.ShouldBeFailureOfType<GuardFailure>();
    }

    [Test]
    public void HasExactly_Should_ReturnSuccess_WhenExact()
    {
        List<string> list = ["a", "b", "c"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).HasExactly(3);

        result.ShouldBeSuccess();
    }

    [Test]
    public void HasExactly_Should_UseCustomMessage_WhenProvided()
    {
        List<string> list = ["a", "b"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).HasExactly(1, "Custom message");

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void DoesContain_Should_ReturnSuccess_WhenCollectionContainsExpectedItem()
    {
        List<string> list = ["a", "b"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).Contains("b");

        result.ShouldBeSuccess();
    }

    [Test]
    public void DoesContain_Should_ReturnFailure_WhenCollectionDoesNotContainExpectedItem()
    {
        List<string> list = ["a", "b"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).Contains("c");

        _ = result.ShouldBeFailureOfType<GuardFailure>();
    }

    [Test]
    public void DoesContain_Should_UseCustomMessage_WhenProvided()
    {
        List<string> list = ["a", "b"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).Contains("c", "Custom message");

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void DoesNotContain_Should_ReturnSuccess_WhenCollectionDoesNotContainsExpectedItem()
    {
        List<string> list = ["a", "b"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).NotContains("c");

        result.ShouldBeSuccess();
    }

    [Test]
    public void DoesNotContain_Should_ReturnFailure_WhenCollectionContainsExpectedItem()
    {
        List<string> list = ["a", "b"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).NotContains("b");
        _ = result.ShouldBeFailureOfType<GuardFailure>();
    }

    [Test]
    public void DoesNotContain_Should_UseCustomMessage_WhenProvided()
    {
        List<string> list = ["a", "b"];
        var result =
            Result.Success()
            .WithValue(list)
            .EnsureCollection(x => x).NotContains("b", "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }
}