using Toarnbeike.Results.Ensure.Implementation.FailureMessages;

namespace Toarnbeike.Results.Ensure.Abstractions;

public interface IFailureMessageProvider
{
    string CreateMessage(FailureMessageContext context);
}