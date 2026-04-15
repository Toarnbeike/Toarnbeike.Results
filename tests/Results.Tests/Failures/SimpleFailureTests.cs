using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Tests.Failures;

/// <summary>
/// Tests for the <see cref="ExceptionFailure"/> record.
/// </summary>
public class SimpleFailureTests
{
    [Test]
    public void SimpleFailure_Should_BeCreatedWithCodeAndMessage()
    {
        var failure = new SimpleFailure("Code", "Message");

        failure.Code.ShouldBe("Code");
        failure.Message.ShouldBe("Message");
        failure.Category.ShouldBe(FailureCategory.Unknown);
    }

    [Test]
    public void ExceptionFailure_Should_BeAbleToChangeBaseProperties_UsingWithSyntax()
    {
        var failure = new SimpleFailure("Code", "Message");

        var newFailure = failure with { Message = "Something else" };
        newFailure.Message.ShouldBe("Something else");
    }
}