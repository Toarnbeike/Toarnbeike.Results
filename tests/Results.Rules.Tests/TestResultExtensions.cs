using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Rules.Tests;

internal static class TestResultExtensions
{
    extension<T>(Result<T> result)
    {
        public RuleTarget<T, T> TestRuleFor(string _)
        {
            var executor = new RuleExecutor<T, T>((value, rule, message) =>
            {
                var testResult = rule(value);
                return testResult.IsValid
                    ? Result.Success()
                    : new TestFailure(message ?? testResult.Message);
            });

            return new RuleTarget<T, T>(result, executor);
        }

        public RuleTarget<T, IEnumerable<TItem>> TestRuleForCollection<TItem>(Func<T, IEnumerable<TItem>> collectionSelector)
        {
            ArgumentNullException.ThrowIfNull(collectionSelector);

            var executor = new RuleExecutor<T, IEnumerable<TItem>>((value, rule, message) =>
            {
                var validationResult = rule(collectionSelector(value));
                return validationResult.IsValid
                    ? Result.Success()
                    : new TestFailure(message ?? validationResult.Message);
            });

            return new RuleTarget<T, IEnumerable<TItem>>(result, executor);
        }
    }
}
