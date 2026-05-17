using Toarnbeike.Results.Ensure.Implementation.Chaining;

namespace Toarnbeike.Results.Ensure;

public static class EnsureExtensions
{
    extension(Result)
    {
        public static IGuardChain Ensure()
        {
            return new EnsureGuardChain();
        }

        public static IGuardChain Validate()
        {
            return new ValidateGuardChain();
        }
    }
}