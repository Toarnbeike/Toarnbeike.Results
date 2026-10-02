namespace Toarnbeike.Results.Validation;

public interface IValidator<T>
{
    Result<T> Validate(T value);
}
