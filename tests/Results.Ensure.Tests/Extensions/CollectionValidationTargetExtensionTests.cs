using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Ensure.Extensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class CollectionExtensionTests
{
    [Test]
    public void NotEmpty_Should_ReturnFailure_WhenEmpty()
    {
        IEnumerable<string> list = [];
        var result = 
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).NotEmpty();

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Collection must not be empty.");
    }

    [Test]
    public void NotEmpty_Should_ReturnSuccess_WhenNotEmpty()
    {
        IEnumerable<string> list = ["item"];
        var result = 
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).NotEmpty();

        result.ShouldBeSuccess();
    }

    [Test]
    public void NotEmpty_Should_UseCustomMessage_WhenProvided()
    {
        IEnumerable<string> list = [];
        var result =
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).NotEmpty("Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void Empty_Should_ReturnFailure_WhenNotEmpty()
    {
        IEnumerable<string> list = ["item"];
        var result =
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).Empty();

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Collection must be empty.");
    }

    [Test]
    public void Empty_Should_ReturnSuccess_WhenEmpty()
    {
        IEnumerable<string> list = [];
        var result =
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).Empty();

        result.ShouldBeSuccess();
    }

    [Test]
    public void Empty_Should_UseCustomMessage_WhenProvided()
    {
        IEnumerable<string> list = ["item"];
        var result =
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).Empty("Custom message");

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void HasAtLeast_Should_ReturnFailure_WhenBelowMin()
    {
        IEnumerable<string> list = ["a"];
        var result =
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).HasAtLeast(2);

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Collection must contain at least 2 element(s).");
    }

    [Test]
    public void HasAtLeast_Should_ReturnSuccess_WhenMeetsMin()
    {
        IEnumerable<string> list = ["a","b"];
        var result =
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).HasAtLeast(2);

        result.ShouldBeSuccess();
    }

    [Test]
    public void HasAtLeast_Should_UseCustomMessage_WhenProvided()
    {
        IEnumerable<string> list = ["a"];
        var result =
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).HasAtLeast(2, "Custom message");

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void HasAtMost_Should_ReturnFailure_WhenAboveMax()
    {
        IEnumerable<string> list = ["a","b","c"];
        var result =
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).HasAtMost(2);

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Collection must contain at most 2 element(s).");
    }

    [Test]
    public void HasAtMost_Should_ReturnSuccess_WhenWithinMax()
    {
        IEnumerable<string> list = ["a","b"];
        var result =
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).HasAtMost(2);

        result.ShouldBeSuccess();
    }

    [Test]
    public void HasAtMost_Should_UseCustomMessage_WhenProvided()
    {
        IEnumerable<string> list = ["a","b","c"];
        var result =
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).HasAtMost(2, "Custom message");

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void HasBetween_Should_ReturnFailure_WhenOutsideRange()
    {
        IEnumerable<string> list = ["a"];
        var result =
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).HasBetween(2, 3);

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Collection must contain between 2 and 3 element(s).");
    }

    [Test]
    public void HasBetween_Should_ReturnSuccess_WhenWithinRange()
    {
        IEnumerable<string> list = ["a","b"];
        var result =
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).HasBetween(2, 3);

        result.ShouldBeSuccess();
    }

    [Test]
    public void HasBetween_Should_UseCustomMessage_WhenProvided()
    {
        IEnumerable<string> list = ["a", "b", "c", "d"];
        var result =
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).HasBetween(2, 3, "Custom message");

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void HasExactly_Should_ReturnFailure_WhenNotExact()
    {
        IEnumerable<string> list = ["a","b"];
        var result =
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).HasExactly(1);

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Collection must contain exactly 1 element(s).");
    }

    [Test]
    public void HasExactly_Should_ReturnSuccess_WhenExact()
    {
        IEnumerable<string> list = ["a","b","c"];
        var result =
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).HasExactly(3);

        result.ShouldBeSuccess();
    }

    [Test]
    public void HasExactly_Should_UseCustomMessage_WhenProvided()
    {
        IEnumerable<string> list = ["a","b"];
        var result =
            Result.Success()
            .WithValue(list)
            .Ensure(x => x).HasExactly(1, "Custom message");

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }
}