namespace Toarnbeike.Results.Abstractions.Tests;

internal sealed record TestFailure : Failure
{
    /// <summary>
    /// A short description of the failure for automated parsing.
    /// </summary>
    public string Code { get; }

    public TestFailure(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public override string ToString()
    {
        return base.ToString();
    }
}

public class FailureTests
{
    private readonly TestFailure _testFailure = new("test", "Test failure");

    [Fact]
    public void ToString_ShouldReturn_ErrorMessage()
    {
        _testFailure.ToString().ShouldBe(_testFailure.Message);
    }
}

