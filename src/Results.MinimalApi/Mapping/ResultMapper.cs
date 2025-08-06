using System.Diagnostics.CodeAnalysis;

namespace Toarnbeike.Results.MinimalApi.Mapping;

/// <inheritdoc />
public class ResultMapper : IResultMapper
{
    /// <inheritdoc />
    public IAspNetResult Map(IResult result)
    {
        if (result.TryGetFailure(out var failure))
        {
            // This is going to be improved with discriminations based on the Failure type in the future.
            return AspNetResults.Problem(failure.Message, statusCode: 400);
        }

        // If the result is a generic Result<TValue>, we return the value as an 200 Ok with the value.
        if (TryGetValue(result, out var value))
        {
            return AspNetResults.Ok(value);
        }

        // If the result is a non-generic Result, we return a 204 No Content.
        return AspNetResults.NoContent();
    }

    /// <summary>
    /// Attempts to retrieve the value encapsulated by the specified <paramref name="result"/>.
    /// </summary>
    /// <remarks>
    /// This method uses reflection to determine whether <paramref name="result"/> is a generic result type 
    /// that defines a public <c>TryGetValue(out T)</c> method. If such a method exists and returns <see langword="true"/>, 
    /// the contained value is extracted and returned via the <paramref name="value"/> parameter.
    /// </remarks>
    /// <param name="result">
    /// The result object to extract the value from. Expected to be a generic type (e.g., <c>Result&lt;T&gt;</c>) 
    /// that implements a <c>TryGetValue(out T)</c> method.
    /// </param>
    /// <param name="value">
    /// When this method returns <see langword="true"/>, contains the value extracted from <paramref name="result"/>. 
    /// Otherwise, <see langword="null"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the value was successfully retrieved from <paramref name="result"/>; otherwise, <see langword="false"/>.
    /// </returns>
    private static bool TryGetValue(IResult result, [NotNullWhen(true)] out object? value)
    {
        value = null;

        var type = result.GetType();

        if (!type.IsGenericType)
        {
            return false;
        }

        var method = type.GetMethod(nameof(Result<>.TryGetValue), [type.GetGenericArguments()[0].MakeByRefType()]);

        if (method is null)
        {
            return false;
        }

        var parameters = new object?[] { null };
        bool success = (bool)method!.Invoke(result, parameters)!;

        if (success)
        {
            value = parameters[0];
        }

        return success;
    }
}