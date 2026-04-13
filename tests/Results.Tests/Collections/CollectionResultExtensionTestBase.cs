namespace Toarnbeike.Results.Tests.Collections;

public abstract class CollectionResultExtensionTestBase
{
    protected readonly List<Result> AllSuccessCollection = [Result.Success(), Result.Success()];
    protected readonly List<Result> MixedCollection = [Result.Failure(new Failure("code1", "message1")), Result.Success(), Result.Failure(new Failure("code2", "message2"))];
    protected readonly List<Result> EmptyCollection = [];

    protected readonly List<Task<Result>> AllSuccessTaskCollection = [Result.SuccessTask(), Result.SuccessTask()];
    protected readonly List<Task<Result>> MixedTaskCollection = [Task.FromResult(Result.Failure(new Failure("code1", "message1"))), Result.SuccessTask(), Task.FromResult(Result.Failure(new Failure("code2", "message2")))];
    protected readonly List<Task<Result>> EmptyTaskCollection = [];
}