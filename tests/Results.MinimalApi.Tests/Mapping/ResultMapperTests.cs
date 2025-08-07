using Microsoft.AspNetCore.Mvc;
using Toarnbeike.Results.MinimalApi.Mapping.Failures;
using Toarnbeike.Results.MinimalApi.Mapping;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Toarnbeike.Results.MinimalApi.Tests.Mapping;

public class ResultMapperTests
{
    private readonly ResultMapper _mapper;

    public ResultMapperTests()
    {
        _mapper = new ResultMapper([new DummyResultMapper()], new DummyFallbackMapper());
    }

    [Fact]
    public void Map_ShouldReturnMappedProblemDetails_WhenFailureIsMapped()
    {
        Result failure = new DummyFailure();
        
        var response = _mapper.Map(failure);

        response.ShouldBeOfType<ProblemHttpResult>();
        var problemDetails = (ProblemHttpResult)response;
        problemDetails.ShouldNotBeNull();
        problemDetails.ProblemDetails.Title.ShouldBe("Mapped by DummyResultMapper");
        problemDetails.ProblemDetails.Detail.ShouldBe("dummy message");
    }

    [Fact]
    public void Map_ShouldReturnFallbackProblemDetails_WhenFailureIsNotMapped()
    {
        Result failure = new Failure("test", "test message");

        var response = _mapper.Map(failure);

        response.ShouldBeOfType<ProblemHttpResult>();
        var problemDetails = (ProblemHttpResult)response;
        problemDetails.ShouldNotBeNull();
        problemDetails.ProblemDetails.Title.ShouldBe("Fallback");
        problemDetails.ProblemDetails.Detail.ShouldBe("test message");
    }

    [Fact]
    public void Map_ShouldReturn204NoContent_WhenResultIsSuccessWithoutValue()
    {
        Result success = Result.Success();

        var response = _mapper.Map(success);

        response.ShouldBeOfType<NoContent>();
    }

    [Fact]
    public void Map_ShouldReturn200Ok_WhenResultIsSuccessWithValue()
    {
        Result<int> success = Result.Success(42);

        var response = _mapper.Map(success);

        response.ShouldBeOfType<Ok<object>>().Value.ShouldBe(42);
    }

    [Fact]
    public void Map_ShouldReturn200Ok_WhenCustomResultImplementsStaticTryGetValue()
    {
        CustomResult<string> success = new();

        var response = _mapper.Map(success);

        response.ShouldBeOfType<Ok<object>>().Value.ShouldBe("Success from custom mapper");
    }

    [Fact]
    public void Map_ShouldReturn204NoContent_WhenResultIsSuccessWithValue_WithCustomMapperWithoutTryGetValueSuppport()
    {
        CustomResultWithoutTryGetValue<string> success = new();

        var response = _mapper.Map(success);

        response.ShouldBeOfType<NoContent>();
    }

    [Fact]
    public void Map_ShouldReturnMappedProblemDetails_WhenFailureIsMapped_ByLastRegisteredMapper()
    {
        var mapper = new ResultMapper([new DummyResultMapper(), new OverrideResultMapper()], new DummyFallbackMapper());
        Result failure = new DummyFailure();

        var response = mapper.Map(failure);

        response.ShouldBeOfType<ProblemHttpResult>();
        var problemDetails = (ProblemHttpResult)response;
        problemDetails.ShouldNotBeNull();
        problemDetails.ProblemDetails.Title.ShouldBe("Mapped by OverrideResultMapper");
        problemDetails.ProblemDetails.Detail.ShouldBe("dummy message");
    }

    private record DummyFailure() : Failure("dummy", "dummy message");

    private class DummyResultMapper : FailureResultMapper<DummyFailure>
    {
        public override ProblemDetails Map(DummyFailure failure) =>
            new() { Title = "Mapped by DummyResultMapper", Detail = failure.Message };
    }

    private class OverrideResultMapper : FailureResultMapper<DummyFailure>
    {
        public override ProblemDetails Map(DummyFailure failure) =>
            new() { Title = "Mapped by OverrideResultMapper", Detail = failure.Message };
    }

    private class DummyFallbackMapper : FailureResultMapper<Failure>, IFallbackFailureResultMapper
    {
        public override ProblemDetails Map(Failure failure) =>
            new() { Title = "Fallback", Detail = failure.Message };
    }

    private class CustomResult<TValue> : IResult
    {
        public bool IsSuccess => true;
        public bool IsFailure => false;
        public bool TryGetFailure(out Failure failure)
        {
            failure = default!;
            return false;
        }
        public static bool TryGetValue(out TValue value)
        {
            value = typeof(TValue) == typeof(string)
                ? (TValue)(object)"Success from custom mapper"
                : default!;
            return true;
        }
    }

    private class CustomResultWithoutTryGetValue<TValue> : IResult
    {
        public bool IsSuccess => true;
        public bool IsFailure => false;
        public bool TryGetFailure(out Failure failure)
        {
            failure = default!;
            return false;
        }
    }
}
