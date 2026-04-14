namespace Toarnbeike.Results.Abstractions.Tests;

public class DebuggerToStringTests
{
    [Fact]
    public void Failure_DebuggerToString_Should_ReturnMessage()
    {
        var failure = new TestFailure("Code", "Message");
        failure.DebuggerToString().ShouldBe("Failure: Message");
    }

    [Fact]
    public void Result_DebuggerToString_Should_ReturnSuccess_WhenSuccess()
    {
        var result = Result.Success();
        result.DebuggerToString().ShouldBe("Success");
    }

    [Fact]
    public void Result_DebuggerToString_Should_ReturnFailureAndMessage_WhenFailure()
    {
        Result result = new TestFailure("Code", "Message");
        result.DebuggerToString().ShouldBe("Failure: Message");
    }

    [Fact]
    public void ResultT_DebuggerToString_Should_ReturnSuccessAndValue_WhenSuccess()
    {
        var result = Result.Success(1);
        result.DebuggerToString().ShouldBe("Success: 1");
    }

    [Fact]
    public void ResultT_DebuggerToString_Should_ReturnFailureAndMessage_WhenFailure()
    {
        Result<int> result = new TestFailure("Code", "Message");
        result.DebuggerToString().ShouldBe("Failure: Message");
    }
}
