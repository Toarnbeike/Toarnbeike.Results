namespace Toarnbeike.Results.Tests;

internal record TestFailure : Failure
{
    public TestFailure(string message)
    {
        Message = message;
    }
}
