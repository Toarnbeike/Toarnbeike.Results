namespace Toarnbeike.Results.Abstractions.Tests;

public class FailureTests
{
    private readonly Failure _testFailure = new("test");

    [Fact]
    public void ToString_ShouldReturn_ErrorMessage()
    {
        _testFailure.ToString().ShouldBe(_testFailure.Message);
    }
}

