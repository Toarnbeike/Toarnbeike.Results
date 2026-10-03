using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Validation.Extensions;

namespace Toarnbeike.Results.Validation;

internal static class TestFile
{
    public sealed record Customer(
        string Name,
        int Age,
        List<string> Notes);

    public sealed class CustomerValidator : Validator<Customer>
    {
        protected override void RegisterRules(ValidationRuleCollection<Customer> rules)
        {
            rules.That(c => c.Age).AtLeast(18);
            rules.That(c => c.Age).AtMost(80);
            rules.That(c => c.Age).Between(18, 80);
            rules.That(c => c.Name).MinLength(3);
            rules.That(c => c.Notes).HasAtLeast(2);
            rules.ThatAll(c => c.Notes).MinLength(5);
        }
    }

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
            .ValidateAll(c => c.Notes).MinLength(5)
            .Map(c => c.Notes)
            .Validate(n => n).HasAtLeast(2)
            .ValidateAll(n => n).MinLength(3);
    }

    public static void TestValidatorSyntax()
    {
        var validator = new CustomerValidator();
        var customer = new Customer(
            Name: "John Doe",
            Age: 30,
            Notes: ["Note 1", "Note 2"]);
        var result = validator.Validate(customer);
    }
}