namespace Toarnbeike.Results.MinimalApi.Mapping;

public class ResultMapper : IResultMapper
{
    public IAspNetResult Map(IToarnbeikeResult result)
    {
        if (!result.TryGetFailure(out var failure))
        {
            return AspNetResults.Ok();
        }

        return AspNetResults.Problem(failure.Message, statusCode: 400);
    }
}