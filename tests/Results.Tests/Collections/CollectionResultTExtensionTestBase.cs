namespace Toarnbeike.Results.Tests.Collections;

public abstract class CollectionResultTExtensionTestBase
{
    protected readonly List<Result<int>> AllSuccessCollection = [1, 2, 3];
    protected readonly List<Result<int>> MixedCollection = [1, new Failure("code1", "message1"), 3, new Failure("code2", "message2")];
    protected readonly List<Result<int>> EmptyCollection = [];

    protected readonly List<Task<Result<int>>> AllSuccessTaskCollection = [Result.SuccessTask(1), Result.SuccessTask(2), Result.SuccessTask(3)];
    protected readonly List<Task<Result<int>>> MixedTaskCollection = [Result.SuccessTask(1), Task.FromResult(Result<int>.Failure(new Failure("code1", "message1"))),
        Result.SuccessTask(3), Task.FromResult(Result<int>.Failure(new Failure("code2", "message2")))];
    protected readonly List<Task<Result<int>>> EmptyTaskCollection = [];
}