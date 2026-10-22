using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Validation.Tests.Testdata;

namespace Toarnbeike.Results.Validation.Tests;

public class ResultValidateExtensionTests
{
    [Test]
    public void ValidateUsing_ShouldRetainFailure_WhenResultIsFailure()
    {
        Result<Customer> result = new TestFailure("Original failure");
        var validatedResult = result.ValidateUsing(new CustomerValidator());
        var failure = validatedResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Original failure");
    }

    [Test]
    public void ValidateUsing_ShouldReturnFailure_WhenValidationFails()
    {
        Result<Customer> result = Customer.Valid with { Name= "A" };
        var validatedResult = result.ValidateUsing(new CustomerValidator());
        var failure = validatedResult.ShouldBeFailureOfType<ValidationFailureSummary>();
        failure.GetFailuresFor("Name").ShouldNotBeNull();
    }

    [Test]
    public void ValidateUsing_ShouldReturnSuccess_WhenValidationPasses()
    {
        Result<Customer> result = Customer.Valid;
        var validatedResult = result.ValidateUsing(new CustomerValidator());
        validatedResult.ShouldBeSuccess();
    }
}
