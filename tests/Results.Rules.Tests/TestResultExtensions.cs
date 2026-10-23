using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Rules.Tests;

internal static class TestResultExtensions
{
    extension<T>(Result<T> result)
    {
        public RuleTarget<T, T> TestRuleFor(string _) =>
            RuleTargetFactory.Create(result, value => value, string.Empty, static (_, message) => new TestFailure(message));

        public RuleTarget<T, IEnumerable<TItem>> TestRuleForCollection<TItem>(Func<T, IEnumerable<TItem>> collectionSelector) =>
            RuleTargetFactory.Create(result, collectionSelector, string.Empty, static (_, message) => new TestFailure(message));
    }
}