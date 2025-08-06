namespace Toarnbeike.Results.MinimalApi.Mapping;

public interface IResultMapper
{
    /// <summary>
    /// Map the specified <paramref name="result"/> to an <see cref="IAspNetResult"/>.
    /// </summary>
    /// <param name="result">The result to map</param>
    /// <returns>
    /// A 200 Ok with value if the result is successful and contains a value,
    /// A 204 No Content if the result is successful but has no value,
    /// A failing <see cref="IAspNetResult"/> if the result is a failure, of which the type is
    /// determined by the <see cref="Failure"/> type.
    /// </returns>
    IAspNetResult Map(IToarnbeikeResult result);
}
