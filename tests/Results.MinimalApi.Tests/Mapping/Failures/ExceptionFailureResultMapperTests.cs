using Toarnbeike.Results.Failures;
using Toarnbeike.Results.MinimalApi.Mapping.Failures;

namespace Toarnbeike.Results.MinimalApi.Tests.Mapping.Failures;

public class ExceptionFailureResultMapperTests
{
    [Fact]
    public void Map_ShouldReturnProblemDetails_WhenExceptionFailureIsMapped()
    {
        var exception = new Exception("Test exception");
        var failure = new ExceptionFailure(exception);
        var mapper = new ExceptionFailureResultMapper();

        var response = mapper.Map(failure);

        response.Title.ShouldBe("Internal Server Error");
        response.Status.ShouldBe(500);
        response.Detail.ShouldBe("An unexpected error occurred.");
        response.Type.ShouldBe("https://tools.ietf.org/html/rfc7231#section-6.6.1");
        response.Extensions["code"].ShouldBe("exception");
    }
}
