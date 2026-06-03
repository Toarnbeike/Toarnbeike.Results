using System.Globalization;

namespace Toarnbeike.Results.Guards.Messages;

public interface IFailureMessageProvider
{
    CultureInfo Culture { get; }
    string CreateMessage(FailureMessageContext context);
}