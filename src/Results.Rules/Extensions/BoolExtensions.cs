using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Extensions;

public static class BoolExtensions
{
    extension<T>(RuleTarget<T, bool> target)
    {
        /// <summary>
        /// Verifies that the target boolean value is true.
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target boolean value is true.</returns>
        public Result<T> IsTrue(string? message = null) =>
            target.Apply(BoolRules.IsTrue(), message);

        /// <summary>
        /// Verifies that the target boolean value is false.
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target boolean value is false.</returns>
        public Result<T> IsFalse(string? message = null) =>
            target.Apply(BoolRules.IsFalse(), message);
    }
}
