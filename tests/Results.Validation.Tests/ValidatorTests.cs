using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Validation.Tests;

public sealed record Customer(string Name, int Age, List<string> Notes)
{
    public static Customer ValidInstance => new("John Doe", 30, ["Note 1", "Note 2"]);
};

public sealed class CustomerValidator : Validator<Customer>
{
    protected override void RegisterRules(ValidationRuleCollection<Customer> rules)
    {
        rules.That(c => c.Name).NotWhiteSpace("Name cannot be null or empty or whitespace.");
        rules.That(c => c.Age).GreaterThan(18);
        rules.That(c => c.Notes).HasAtLeast(1);
        rules.ThatAll(c => c.Notes).MinLength(5);
    }
}

internal class ValidatorTests
{
    [Test]
    public void Validate_ShouldReturnSuccess_WhenAllRulesPass()
    {
        var customer = Customer.ValidInstance;
        var validator = new CustomerValidator();
        var result = validator.Validate(customer);
        result.ShouldBeSuccess();
    }

    [Test]
    public void Validate_ShouldReturnFailure_WhenAnyRuleFails()
    {
        var customer = Customer.ValidInstance with { Name = " "};
        var validator = new CustomerValidator();
        var result = validator.Validate(customer);
        
        var failureSummary = result.ShouldBeFailureOfType<ValidationFailureSummary>();
        var failures = failureSummary.Failures.ToList();
        failures.Count.ShouldBe(1);
        var nameFailure = failures[0];
        nameFailure.Key.ShouldBe("Name");
        nameFailure.Value.Length.ShouldBe(1);
        nameFailure.Value.Single().ShouldBe("Name cannot be null or empty or whitespace.");
    }

    [Test]
    public void Validate_ShouldReturnFailure_WhenCollectionRuleFails()
    {
        var customer = Customer.ValidInstance with { Notes = [] };
        var validator = new CustomerValidator();
        var result = validator.Validate(customer);

        var failureSummary = result.ShouldBeFailureOfType<ValidationFailureSummary>();
        var failures = failureSummary.Failures.ToList();
        failures.Count.ShouldBe(1);
        var notesFailure = failures[0];
        notesFailure.Key.ShouldBe("Notes");
        notesFailure.Value.Length.ShouldBe(1);
        notesFailure.Value.Single().ShouldContain("at least 1");
    }

    [Test]
    public void Validate_ShouldReturnFailure_WhenCollectionItemRuleFails()
    {
        var customer = Customer.ValidInstance with { Notes = ["Note", "Short"] };
        var validator = new CustomerValidator();
        var result = validator.Validate(customer);
        var failureSummary = result.ShouldBeFailureOfType<ValidationFailureSummary>();
        var failures = failureSummary.Failures.ToList();
        failures.Count.ShouldBe(1);
        var notesFailure = failures[0];
        notesFailure.Key.ShouldBe("Notes[0]");
        notesFailure.Value.Length.ShouldBe(1);
        notesFailure.Value.Single().ShouldContain("at least 5");
    }

    [Test]
    public void Validate_ShouldReturnMultipleFailures_WhenMultipleRulesFail()
    {
        var customer = Customer.ValidInstance with { Name = " ", Age = 17, Notes = [] };
        var validator = new CustomerValidator();
        var result = validator.Validate(customer);
        var failureSummary = result.ShouldBeFailureOfType<ValidationFailureSummary>();
        var failures = failureSummary.Failures.ToList();
        failures.Count.ShouldBe(3);

        var nameFailure = failures.FirstOrDefault(f => f.Key == "Name");
        nameFailure.Value.Length.ShouldBe(1);
        nameFailure.Value.Single().ShouldBe("Name cannot be null or empty or whitespace.");
        var ageFailure = failures.FirstOrDefault(f => f.Key == "Age");
        ageFailure.Value.Length.ShouldBe(1);
        ageFailure.Value.Single().ShouldContain("greater than 18");
        var notesFailure = failures.FirstOrDefault(f => f.Key == "Notes");
        notesFailure.Value.Length.ShouldBe(1);
        notesFailure.Value.Single().ShouldContain("at least 1");
    }
}
