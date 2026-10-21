using Toarnbeike.Results.Validation.Extensions;

namespace Toarnbeike.Results.Validation.Tests.Testdata;

internal sealed class CustomerValidator : Validator<Customer>
{
    protected override void RegisterRules(ValidationRuleCollection<Customer> rules)
    {
        rules.That(c => c.Name).LengthBetween(2, 127);
        rules.That(c => c.Age).Between(18, 150);
        rules.That(c => c.CreationDate).OnOrBefore(DateTimeOffset.Now);
        rules.ThatCollection(c => c.Notes).HasAtLeast(2);
        rules.ThatAll(c => c.Notes).NotWhiteSpace();
    }
}