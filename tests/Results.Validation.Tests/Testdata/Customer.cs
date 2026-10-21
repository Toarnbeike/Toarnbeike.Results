namespace Toarnbeike.Results.Validation.Tests.Testdata;

internal sealed record Customer(string Name, int Age, DateTimeOffset CreationDate, List<string> Notes)
{
    public static Customer Valid => 
        new(
            "John Doe", 
            30, 
            new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero), 
            ["Note 1", "Note 2"]);
}
