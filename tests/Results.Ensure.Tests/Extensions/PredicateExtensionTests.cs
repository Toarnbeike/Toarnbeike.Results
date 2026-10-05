using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Ensure.Extensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class PredicateExtensionTests
{
    [Test]
    public void Must_Should_ReturnFailure_WhenPredicateFalse()
    {
        string value = "abc";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).Must(v => v.Length > 5, "Must be longer than 5");

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Must be longer than 5");
    }

    [Test]
    public void Must_Should_ReturnSuccess_WhenPredicateTrue()
    {
        string value = "abcdefgh";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).Must(v => v.Length > 5, "Must be longer than 5");

        result.ShouldBeSuccess();
    }

    [Test]
    public void MustNot_Should_ReturnFailure_WhenPredicateTrue()
    {
        string value = "abc";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).MustNot(v => v.Length < 5, "Must not be shorter than 5");

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Must not be shorter than 5");
    }

    [Test]
    public void MustNot_Should_ReturnSuccess_WhenPredicateFalse()
    {
        string value = "abcdefgh";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).MustNot(v => v.Length < 5, "Must not be shorter than 5");

        result.ShouldBeSuccess();
    }
}
