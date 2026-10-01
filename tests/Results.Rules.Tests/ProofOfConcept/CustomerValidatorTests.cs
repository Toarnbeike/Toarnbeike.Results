using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Rules.Tests.ProofOfConcept;

public class CustomerValidatorTests
{
    private readonly CustomerDto _valid = new("Ab", "abc@de.fg", ["12345"], DateTime.UtcNow);
    private readonly CustomerDtoValidator _validator = new CustomerDtoValidator();


    [Test]
    public void CustomerDtoValidator_ShouldReturnSuccess_WhenValid()
    {
        var result = _validator.Validate(_valid);
        result.ShouldBeSuccess();
    }
}