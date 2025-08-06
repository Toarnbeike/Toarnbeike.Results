using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.MinimalApi.Mapping.Failures;

/// <summary>
/// Maps an <see cref="AggregateFailure"/> to a single <see cref="AggregateProblemDetails"/> object,
/// using the configured failure mappers for the individual failures.
/// </summary>
/// <param name="serviceProvider">The serviceProvider to delay resolving the IEnumerable{IFailureResultMapper} because 
/// early resolving would result in an circular reference.</param>
/// <param name="fallbackFailureResultMapper">The fallback failure result mapper for when no innerMapper is available.</param>
internal sealed class AggregateFailureResultMapper(IServiceProvider serviceProvider,
IFallbackFailureResultMapper fallbackFailureResultMapper) : FailureResultMapper<AggregateFailure>
{
    /// <summary>
    /// Collection of failure mappers, grouped by the type of <see cref="Failure"/> they can handle.
    /// </summary>
    /// <remarks>
    /// For the mapping, we only keep the last mapper for each type, since we 
    /// - first register the default mappers for a failure type,
    /// - the user can register custom mappers for the same failure type.
    /// In which case, the custom mapper should override the default one.
    /// </remarks>
    private readonly Lazy<Dictionary<Type, IFailureResultMapper>> _failureMappers = new(() =>
            serviceProvider
                .GetServices<IFailureResultMapper>() // resolves the Func to get the actual collection
                .Where(m => m.FailureType != typeof(AggregateFailure)) // Exclude the aggregate mapper itself
                .GroupBy(mapper => mapper.FailureType)
                .ToDictionary(group => group.Key, group => group.Last()));

    /// <summary>
    /// Converts the specified <paramref name="failure"/> into a <see cref="ProblemDetails"/> instance.
    /// </summary>
    /// <param name="failure">The aggregate failure to convert.</param>
    /// <returns>
    /// A <see cref="ProblemDetails"/> representing the aggregate failure, with status code 500 and a generic error detail.
    /// </returns>
    public override ProblemDetails Map(AggregateFailure failure)
    {
        var problemDetailsList = new List<ProblemDetails>();

        foreach (var inner in failure.Failures)
        {
            if (_failureMappers.Value.TryGetValue(inner.GetType(), out var mapper))
            {
                // Use the specific mapper for this failure type
                problemDetailsList.Add(mapper.Map(inner));
            }
            else
            {
                // Fallback: no mapper available for this failure, use default mapping.
                problemDetailsList.Add(fallbackFailureResultMapper.Map(inner));
            }
        }

        // Determine the status code — prefer 500 over 400
        var statusCode = GetAggregateStatusCode(problemDetailsList);

        return new AggregateProblemDetails(problemDetailsList)
        {
            Title = "Multiple failures occurred.",
            Status = statusCode,
            Detail = "See problems for details.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };
    }

    private static int GetAggregateStatusCode(IEnumerable<ProblemDetails> problems)
    {
        // Use 500 if any internal server errors exist
        return problems.Any(p => p.Status is >= 500) ? 500 : 400;
    }
}
