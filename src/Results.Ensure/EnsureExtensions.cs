using Toarnbeike.Results.Ensure.Abstractions;
using Toarnbeike.Results.Ensure.Implementation.Chaining;
using Toarnbeike.Results.Ensure.Implementation.FailureMessages;
using Toarnbeike.Results.Ensure.Implementation.Tolerances;

namespace Toarnbeike.Results.Ensure;

public static class EnsureExtensions
{
    extension(Result)
    {
        public static IGuardChain Ensure(
            IFailureMessageProvider? failureMessageProvider = null, 
            IToleranceProvider? toleranceProvider = null,
            TimeProvider? timeProvider = null)
        {
            return new EnsureGuardChain(
                failureMessageProvider ?? new DefaultFailureMessageProvider(), 
                toleranceProvider ?? new DefaultToleranceProvider(),
                timeProvider ?? TimeProvider.System);
        }

        public static IGuardChain Validate(
            IFailureMessageProvider? failureMessageProvider = null, 
            IToleranceProvider? toleranceProvider = null,
            TimeProvider? timeProvider = null)
        {
            return new ValidateGuardChain(
                failureMessageProvider ?? new DefaultFailureMessageProvider(), 
                toleranceProvider ?? new DefaultToleranceProvider(),
                timeProvider ?? TimeProvider.System);
        }
    }
}