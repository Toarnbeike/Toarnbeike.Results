using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Tests.Failures;

/// <summary>
/// Tests for the <see cref="ExceptionFailure"/> record.
/// </summary>
public class ExceptionFailureTests
{
    [Fact]
    public void ExceptionFailure_Should_BeCreatedFromAnException()
    {
        var exception = new ArgumentOutOfRangeException("argument");
        var failure = new ExceptionFailure(exception);

        failure.Exception.ShouldBeOfType<ArgumentOutOfRangeException>();
        failure.Code.ShouldBe("exception:ArgumentOutOfRangeException");
        failure.Message.ShouldBe("Specified argument was out of the range of valid values. (Parameter 'argument')");
        failure.ExceptionType.ShouldBe("ArgumentOutOfRangeException");
    }

    [Fact]
    public void ExceptionFailure_Should_BeAbleToChangeBaseProperties_UsingWithSyntax()
    {
        var exception = new ArgumentOutOfRangeException("Argument bad");
        var firstFailure = new ExceptionFailure(exception);

        var newFailure = firstFailure with { Code = "Something else" };
        newFailure.Code.ShouldBe("Something else");
    }
}
