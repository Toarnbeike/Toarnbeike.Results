using Toarnbeike.Results.Validation.Extensions;

namespace Toarnbeike.Results.Validation;

internal static class TestFile
{
    public static void TestSyntax()
    {
        var result = Result.Success(3);
        Result<IEnumerable<int>> results = new List<int>() { 1, 2, 3 };

        result.Validate(x => x).AtLeast(2);
        results.ValidateAny(x => x).AtLeast(2);
    }
}