using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Tests.Internal;

namespace Toarnbeike.Results.Tests.Extensions.Zip;

public class ZipIntegrationTests
{
    [Fact]
    public void TupleResult_Should_HandleOtherExtensionMethod_Sync()
    {

        var result = Result.Success(20)
            .Zip(value => value > 10 ? Result<decimal>.Success(5.3m) : new Failure("out of bound", "result should exceed 20"));

        result.ShouldBeSuccessWithValue((20, 5.3m));

        decimal value = 0;
        result.Tap(((int @base, decimal multiplier) tuple) => value = tuple.@base * tuple.multiplier);
        value.ShouldBe(106m);

        result.Ensure(((int @base, decimal multiplier) tuple) => tuple.multiplier > 5, () => new Failure("out of bound", "multiplier should exceed 20"));

        var actual = result
            .Map(((int @base, decimal multiplier) tuple) => tuple.@base * tuple.multiplier)
            .Match(value => value, _ => 0m);

        actual.ShouldBe(106m);
    }
}
