using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Toarnbeike.Results.MinimalApi;

/// <summary>
/// Represents a collection of multiple <see cref="ProblemDetails"/> instances
/// grouped together in a single response.
/// </summary>
/// <remarks>
/// Use this class when multiple failures are returned together, typically
/// an aggregate of mixed failures.
/// The individual problems are available in <see cref="Problems"/>.
/// All problems are also serialized into the <see cref="ProblemDetails.Extensions"/> dictionary
/// under the key "problems" for compatibility.
/// </remarks>
public sealed class AggregateProblemDetails : ProblemDetails
{
    private const string _extensionKey = "problems";

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateProblemDetails"/> class
    /// with the specified collection of <see cref="ProblemDetails"/>.
    /// </summary>
    /// <param name="problems">The collection of problem details to aggregate.</param>
    public AggregateProblemDetails(ICollection<ProblemDetails> problems)
    {
        ArgumentNullException.ThrowIfNull(problems);
        Problems = problems.ToList();
        // Store the problems also in the Extensions dictionary for serialization
        Extensions[_extensionKey] = Problems;
        Extensions["code"] = "aggregate";
    }

    /// <summary>
    /// Gets the collection of aggregated <see cref="ProblemDetails"/>.
    /// </summary>
    public List<ProblemDetails> Problems { get; init; }

    [JsonConstructor]
    public AggregateProblemDetails() : this([])
    {
        // This constructor is for JSON deserialization only.
        // It initializes an empty collection of problems.
    }
}
