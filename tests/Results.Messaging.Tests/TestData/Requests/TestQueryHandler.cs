using Toarnbeike.Results.Messaging.Requests;

namespace Toarnbeike.Results.Messaging.Tests.TestData.Requests;

public sealed class TestQueryHandler : IQueryHandler<TestQuery, string>
{
    public Task<Result<string>> HandleAsync(TestQuery query, CancellationToken ct) => Result.SuccessTask("Success");
}