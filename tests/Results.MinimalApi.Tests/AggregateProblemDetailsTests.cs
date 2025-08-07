using Microsoft.AspNetCore.Mvc;

namespace Toarnbeike.Results.MinimalApi.Tests;

public class AggregateProblemDetailsTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WhenProblemsAreProvided()
    {
        var problems = new List<ProblemDetails>
        {
            new() { Title = "Problem 1", Detail = "Detail 1" },
            new() { Title = "Problem 2", Detail = "Detail 2" }
        };
        var aggregateProblemDetails = new AggregateProblemDetails(problems);
        aggregateProblemDetails.Problems.ShouldNotBeNull();
        aggregateProblemDetails.Problems.Count.ShouldBe(2);
        aggregateProblemDetails.Problems.ShouldContain(p => p.Title == "Problem 1" && p.Detail == "Detail 1");
        aggregateProblemDetails.Problems.ShouldContain(p => p.Title == "Problem 2" && p.Detail == "Detail 2");
    }

    [Fact]
    public void Constructor_ShouldInitializeExtensions_WhenProblemsAreProvided()
    {
        var problems = new List<ProblemDetails>
        {
            new() { Title = "Problem 1", Detail = "Detail 1" }
        };
        var aggregateProblemDetails = new AggregateProblemDetails(problems);
        
        aggregateProblemDetails.Extensions.ShouldNotBeNull();
        aggregateProblemDetails.Extensions.ContainsKey("problems").ShouldBeTrue();
        aggregateProblemDetails.Extensions["problems"].ShouldBeOfType<List<ProblemDetails>>();
        aggregateProblemDetails.Extensions.ContainsKey("code").ShouldBeTrue();
        aggregateProblemDetails.Extensions["code"].ShouldBe("aggregate");
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenProblemsIsNull()
    {
        ICollection<ProblemDetails> problems = null!;

        Should.Throw<ArgumentNullException>(() => new AggregateProblemDetails(problems));
    }

    [Fact]
    public void JsonConstructor_ShouldInitializeEmptyProblems_WhenCalled()
    {
        var aggregateProblemDetails = new AggregateProblemDetails();
        
        aggregateProblemDetails.Problems.ShouldNotBeNull();
        aggregateProblemDetails.Problems.Count.ShouldBe(0);
        aggregateProblemDetails.Extensions.ShouldNotBeNull();
        aggregateProblemDetails.Extensions.ContainsKey("problems").ShouldBeTrue();
        aggregateProblemDetails.Extensions["problems"].ShouldBeOfType<List<ProblemDetails>>();
        aggregateProblemDetails.Extensions.ContainsKey("code").ShouldBeTrue();
        aggregateProblemDetails.Extensions["code"].ShouldBe("aggregate");
    }
}
