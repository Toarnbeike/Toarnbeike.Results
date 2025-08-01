namespace Toarnbeike.Results.Tests;

public class FailureTests
{
    private readonly Failure _testFailure = new("test", "Test failure");

    [Fact]
    public void ToString_ShouldReturn_ErrorMessage()
    {
        _testFailure.ToString().ShouldBe(_testFailure.Message);
    }
}
