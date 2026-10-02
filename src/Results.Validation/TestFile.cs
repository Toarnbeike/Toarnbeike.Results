using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Validation.Extensions;

namespace Toarnbeike.Results.Validation;

internal static class TestFile
{
    public sealed record Customer(
        string Name,
        int Age,
        List<string> Notes);

    public static void TestValidateSyntax()
    {
        var customer = new Customer(
            Name: "John Doe",
            Age: 30,
            Notes: ["Note 1", "Note 2"]);


        Result.Success()
            .WithValue(customer)
            .Validate(c => c.Age).AtLeast(18)
            .Validate(c => c.Age).AtMost(80)
            .Validate(c => c.Age).Between(18, 80)
            .Validate(c => c.Name).MinLength(3)
            .Validate(c => c.Notes).HasAtLeast(2)
            .Map(c => c.Notes)
            .Validate(n => n).HasAtLeast(2)
            .ValidateAll(n => n).MinLength(3);
    }
}