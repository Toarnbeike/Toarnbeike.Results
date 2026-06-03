using FsCheck;
using Toarnbeike.Results.Ensure.Guards;
using Toarnbeike.Results.Ensure.Tests.TestUtilities;
using TUnit.FsCheck;

namespace Toarnbeike.Results.Ensure.Tests.Guards;

public class FloatingPointGuardTests
{
    //[Test, FsCheckProperty(Arbitrary = [typeof(FiniteDouble), typeof(NonZeroFiniteDouble)])]
    //public bool MultipleOf_MatchesModulo(FiniteDouble value, NonZeroFiniteDouble factor)
    //{
    //    return FloatingPointGuards.MultipleOf(value, factor, 0d) == (value % factor == 0);
    //}

    //[Test, FsCheckProperty(Arbitrary = [typeof(NonZeroFiniteDouble), typeof(PositiveFiniteDouble)])]
    //public bool MultipleOf_ExactMultiple_ShouldSucceed(int multipier, NonZeroFiniteDouble factor,
    //    PositiveFiniteDouble tolerance)
    //{
    //    var value = multipier * factor;
    //    return FloatingPointGuards.MultipleOf(value, factor, tolerance);
    //}

    //[Test, FsCheckProperty]
    //public bool MultipleOf_ShouldBeMultipleOfSelf(NonZeroInt value)
    //{
    //    return IntegerGuards.MultipleOf(value.Item, value.Item);
    //}
}