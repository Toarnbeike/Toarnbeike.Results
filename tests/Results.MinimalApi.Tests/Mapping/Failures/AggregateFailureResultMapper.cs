using Microsoft.AspNetCore.Mvc;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.MinimalApi.Mapping.Failures;

namespace Toarnbeike.Results.MinimalApi.Tests.Mapping.Failures;

public class AggregateFailureResultMapperTests
{
    [Fact]
    public void Map_UsesRegisteredMappers_ForEachFailure()
    {
        // Arrange
        var failureA = new DummyFailureA();
        var failureB = new DummyFailureB();
        var aggregate = new AggregateFailure([failureA, failureB]);

        var mapperA = Substitute.For<IFailureResultMapper>();
        mapperA.FailureType.Returns(typeof(DummyFailureA));
        var pdA = new ProblemDetails { Title = "A", Status = 400 };
        mapperA.Map(failureA).Returns(pdA);

        var mapperB = Substitute.For<IFailureResultMapper>();
        mapperB.FailureType.Returns(typeof(DummyFailureB));
        var pdB = new ProblemDetails { Title = "B", Status = 400 };
        mapperB.Map(failureB).Returns(pdB);

        var selfMapper = Substitute.For<IFailureResultMapper>();
        selfMapper.FailureType.Returns(typeof(AggregateFailure));

        var serviceProvider = Substitute.For<IServiceProvider>();
        var mappers = new[] { mapperA, mapperB, selfMapper };
        serviceProvider.GetService(typeof(IEnumerable<IFailureResultMapper>)).Returns(mappers);

        var fallback = Substitute.For<IFallbackFailureResultMapper>();

        var mapper = new AggregateFailureResultMapper(serviceProvider, fallback);

        var result = mapper.Map(aggregate);

        var agg = result.ShouldBeOfType<AggregateProblemDetails>();
        agg.Problems.ShouldBe([pdA, pdB]);
        agg.Status.ShouldBe(400);
    }

    [Fact]
    public void Map_UsesFallback_WhenNoMapperAvailable()
    {
        var unmappedFailure = new UnmappedFailure();
        var aggregate = new AggregateFailure([unmappedFailure]);

        var fallback = Substitute.For<IFallbackFailureResultMapper>();
        var fallbackProblem = new ProblemDetails { Title = "Fallback", Status = 500 };
        fallback.Map(unmappedFailure).Returns(fallbackProblem);

        var serviceProvider = Substitute.For<IServiceProvider>();
        serviceProvider.GetService(typeof(IEnumerable<IFailureResultMapper>))
            .Returns(Array.Empty<IFailureResultMapper>());

        var mapper = new AggregateFailureResultMapper(serviceProvider, fallback);

        var result = mapper.Map(aggregate);

        var agg = result.ShouldBeOfType<AggregateProblemDetails>();
        agg.Problems.ShouldBe([fallbackProblem]);
        agg.Status.ShouldBe(500);
    }

    private record DummyFailureA() : Failure("dummyA", "A");
    private record DummyFailureB() : Failure("dummyB", "B");
    private record UnmappedFailure() : Failure("unmapped", "Details");
}