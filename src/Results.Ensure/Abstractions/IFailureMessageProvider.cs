namespace Toarnbeike.Results.Ensure.Abstractions;

public interface IFailureMessageProvider
{
    string GetMessage(string guardName, object? attemptedValue, object? constraint);
}