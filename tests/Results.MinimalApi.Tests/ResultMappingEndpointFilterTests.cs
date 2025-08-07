using Microsoft.AspNetCore.Http;
using Toarnbeike.Results.MinimalApi.Mapping;

namespace Toarnbeike.Results.MinimalApi.Tests;
public class ResultMappingEndpointFilterTests
{
    [Fact]
    public async Task Invoke_Should_InvokeMapper_WhenResultIsIResult()
    {
        var toarnbeikeResult = Substitute.For<IResult>();
        var mappedResult = AspNetResults.Ok("Mapped");
        var mapper = Substitute.For<IResultMapper>();
        mapper.Map(toarnbeikeResult).Returns(mappedResult);

        var filter = new ResultMappingEndpointFilter(mapper);

        var context = Substitute.For<EndpointFilterInvocationContext>();
        var next = Substitute.For<EndpointFilterDelegate>();
        next(context).Returns(toarnbeikeResult);

        var result = await filter.InvokeAsync(context, next);

        result.ShouldBe(mappedResult);
    }

    [Fact]
    public async Task Invoke_Should_NotInvokeMapper_WhenResultIsAspNetResult()
    {
        var originalResult = AspNetResults.Ok("Original");
        var mapper = Substitute.For<IResultMapper>();
        var filter = new ResultMappingEndpointFilter(mapper);

        var context = Substitute.For<EndpointFilterInvocationContext>();
        var next = Substitute.For<EndpointFilterDelegate>();
        next(context).Returns(originalResult);

        var result = await filter.InvokeAsync(context, next);

        result.ShouldBe(originalResult);
        mapper.DidNotReceiveWithAnyArgs().Map(default!);
    }
}