namespace Toarnbeike.Results.FluentValidation.Tests;

internal record TestFailure : Failure
{
    public TestFailure(string message)
    {
        Message = message;
    }
}
