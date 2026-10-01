namespace Toarnbeike.Results.Rules.Tests.ProofOfConcept;

public record CustomerDto(string Name, string? Email, string[] PhoneNumbers, DateTime Created);

public class CustomerDtoValidator : Validator<CustomerDto>
{
    public CustomerDtoValidator()
    {
        That(dto => dto.Name).MinLength(2);
        That(dto => dto.Email).NotEmpty().MinLength(5);
        ThatEach(dto => dto.PhoneNumbers).MinLength(5);
        That(dto => dto.Created).WithinPast(TimeSpan.FromMinutes(30));
    }
}