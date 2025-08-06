namespace Toarnbeike.Results.MinimalApi.Mapping;

public interface IResultMapper
{
    IAspNetResult Map(IToarnbeikeResult result);
}
