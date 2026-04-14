using Toarnbeike.Results.Extensions;

namespace Toarnbeike.Results.Ensure;

public static class ResultEnsureExtensions
{
    extension(Result)
    {
        public static Result Ensure(Func<Result> guardClause)
        {
            return Result.Success().BindTap(guardClause);
        }

        public static Result Ensure<T>(Func<Result<T>> guardClause)
        {
            return Result.Success().BindTap(guardClause);
        }
    }

    extension(Result result)
    {
        public Result Ensure(Func<IResult> guardClause)
        {
            return result.IsSuccess && guardClause().TryGetFailure(out var checkFailure)
                ? checkFailure
                : result;
        }
    }
}