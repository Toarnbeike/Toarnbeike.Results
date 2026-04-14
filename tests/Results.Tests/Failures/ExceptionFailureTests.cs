using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Tests.Failures;

/// <summary>
/// Tests for the <see cref="ExceptionFailure"/> record.
/// </summary>
public class ExceptionFailureTests
{
    [Test]
    public void ExceptionFailure_Should_BeCreatedFromAnException()
    {
        var exception = new ArgumentOutOfRangeException("argument");
        var failure = new ExceptionFailure(exception);

        failure.Exception.ShouldBeOfType<ArgumentOutOfRangeException>();
        failure.Message.ShouldBe("Exception: Specified argument was out of the range of valid values. (Parameter 'argument')");
        failure.Category.ShouldBe(FailureCategory.System);
    }

    [Test]
    public void ExceptionFailure_Should_BeAbleToChangeBaseProperties_UsingWithSyntax()
    {
        var exception = new ArgumentOutOfRangeException("Argument bad");
        var firstFailure = new ExceptionFailure(exception);

        var newFailure = firstFailure with { Message = "Something else" };
        newFailure.Message.ShouldBe("Something else");
    }
}
