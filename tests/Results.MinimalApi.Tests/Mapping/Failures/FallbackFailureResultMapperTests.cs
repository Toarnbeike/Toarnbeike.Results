using Toarnbeike.Results.MinimalApi.Mapping.Failures;

namespace Toarnbeike.Results.MinimalApi.Tests.Mapping.Failures;

public class FallbackFailureResultMapperTests
{
    [Fact]
    public void Map_ShouldReturnProblemDetails_WhenFallbackIsUsed()
    {
        var failure = new Failure("fallback", "Fallback error");

        var mapper = new FallbackFailureResultMapper();

        var response = mapper.Map(failure);
        response.Title.ShouldBe("Unmapped Failure occured");
        response.Status.ShouldBe(400);
        response.Detail.ShouldBe("Fallback error");
        response.Type.ShouldBe("https://tools.ietf.org/html/rfc7231#section-6.5.1");
        response.Extensions["code"].ShouldBe("fallback");
    }
}
