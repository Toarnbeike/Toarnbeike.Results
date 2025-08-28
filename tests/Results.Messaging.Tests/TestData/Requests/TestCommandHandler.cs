using Toarnbeike.Results.Messaging.Requests;

namespace Toarnbeike.Results.Messaging.Tests.TestData.Requests;

public sealed class TestCommandHandler : ICommandHandler<TestCommand>
{
    public Task<Result> HandleAsync(TestCommand command, CancellationToken ct) => Result.SuccessTask();
}