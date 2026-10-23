using Toarnbeike.Results.Validation.Tests.Testdata;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;

namespace Toarnbeike.Results.Validation.Tests;

public class ValidatorTests
{
    [Test]
    public void ValidateUsing_ShouldReturnSuccess_WhenAllRulesPass()
    {
        Result<Customer> customer = Customer.Valid;
        var result = customer.ValidateUsing(new CustomerValidator());
        result.ShouldBeSuccess();
    }

    [Test]
    public void ValidateUsing_ShouldReturnFailure_WhenAnyRuleFails()
    {
        Result<Customer> customer = Customer.Valid with { Name = " " };
        var result = customer.ValidateUsing(new CustomerValidator());

        result.ShouldBeFailureOfType<ValidationFailureSummary>();
    }

    [Test]
    public void ValidateUsing_ShouldReturnFailure_WhenOriginalResultIsFailure()
    {
        Result<Customer> customer = new TestFailure("test");
        var result = customer.ValidateUsing(new CustomerValidator());
        result.ShouldBeFailureOfType<TestFailure>().Identifier.ShouldBe("test");
    }

    [Test]
    public void ValidateUsing_Throws_ArgumentNullException_WhenValidatorIsNull()
    {
        Result<Customer> customer = Customer.Valid;
        Should.Throw<ArgumentNullException>(() => customer.ValidateUsing(null!));
    }

    [Test]
    public void Validate_ShouldReturnSuccess_WhenAllRulesPass()
    {
        var customer = Customer.Valid;
        var validator = new CustomerValidator();
        var result = validator.Validate(customer);
        result.ShouldBeSuccess();
    }

    [Test]
    public void Validate_ShouldReturnFailure_WhenAnyRuleFails()
    {
        var customer = Customer.Valid with { Name = "A" }; // Invalid name
        var validator = new CustomerValidator();
        var result = validator.Validate(customer);
        var failureSummary = result.ShouldBeFailureOfType<ValidationFailureSummary>();
        failureSummary.GetFailuresFor("Name").ShouldNotBeNull();
    }

    [Test]
    public void Validate_ShouldReturnFailureSummary_WhenMultipleRulesFail()
    {
        var customer = Customer.Valid with { Name = "A", Age = 10 }; // Invalid name and age
        var validator = new CustomerValidator();
        var result = validator.Validate(customer);
        var failureSummary = result.ShouldBeFailureOfType<ValidationFailureSummary>();
        failureSummary.GetFailuresFor("Name").ShouldNotBeNull();
        failureSummary.GetFailuresFor("Age").ShouldNotBeNull();
    }

    [Test]
    public void Validate_ShouldReturnFailureSummary_WhenRuleFailsForCollection()
    {
        var customer = Customer.Valid with { Notes = ["Note 1"] }; // Invalid note amount
        var validator = new CustomerValidator();
        var result = validator.Validate(customer);
        var failureSummary = result.ShouldBeFailureOfType<ValidationFailureSummary>();
        failureSummary.GetFailuresFor("Notes").ShouldNotBeNull();
    }

    [Test]
    public void Validate_ShouldReturnFailureSummary_WhenRuleFailsForCollectionElement()
    {
        var customer = Customer.Valid with { Notes = ["Note 1", "   "] }; // Invalid note
        var validator = new CustomerValidator();
        var result = validator.Validate(customer);
        var failureSummary = result.ShouldBeFailureOfType<ValidationFailureSummary>();
        failureSummary.GetFailuresFor("Notes[1]").ShouldNotBeNull();
    }

    [Test]
    public void Validate_ShouldSucceed_WhenAnyCollectionItemPasses()
    {
        var result = new NumberCollectionValidator().Validate(new NumberCollection([-1, 2]));

        result.ShouldBeSuccess();
    }

    [Test]
    public void Validate_ShouldFailAtCollectionPath_WhenNoCollectionItemPassesAnyRule()
    {
        var result = new NumberCollectionValidator().Validate(new NumberCollection([-1, 0]));
        var failureSummary = result.ShouldBeFailureOfType<ValidationFailureSummary>();

        failureSummary.GetFailuresFor("Values").ShouldNotBeEmpty();
    }

    [Test]
    public void Validate_ShouldPrefixNestedFailuresWithPropertyAndCollectionPaths()
    {
        var value = new NestedModel(
            new Address(" "),
            [new Address(" "), new Address("")]);
        var result = new NestedModelValidator().Validate(value);
        var failureSummary = result.ShouldBeFailureOfType<ValidationFailureSummary>();

        failureSummary.GetFailuresFor("PrimaryAddress.City").ShouldNotBeEmpty();
        failureSummary.GetFailuresFor("Addresses[0].City").ShouldNotBeEmpty();
        failureSummary.GetFailuresFor("Addresses[1].City").ShouldNotBeEmpty();
    }

    private sealed record NumberCollection(IEnumerable<int> Values);

    private sealed class NumberCollectionValidator : Validator<NumberCollection>
    {
        protected override void RegisterRules(ValidationRuleCollection<NumberCollection> rules) =>
            rules.ThatCollection(value => value.Values).Any().GreaterThan(0);
    }

    private sealed record Address(string City);

    private sealed record NestedModel(Address PrimaryAddress, IEnumerable<Address> Addresses);

    private sealed class AddressValidator : Validator<Address>
    {
        protected override void RegisterRules(ValidationRuleCollection<Address> rules) =>
            rules.That(value => value.City).NotWhiteSpace();
    }

    private sealed class NestedModelValidator : Validator<NestedModel>
    {
        protected override void RegisterRules(ValidationRuleCollection<NestedModel> rules)
        {
            rules.That(value => value.PrimaryAddress, "PrimaryAddress").ValidateUsing(new AddressValidator());
            rules.ThatCollection(value => value.Addresses).All().ValidateUsing(new AddressValidator());
        }
    }
}
