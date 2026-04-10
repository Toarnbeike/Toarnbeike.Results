namespace Toarnbeike.Results;

public static class ResultFactoryExtensions
{
    extension(Result)
    {
        /// <summary>
        /// Creates a new <see cref="Result"/> representing a successful operation.
        /// </summary>
        /// <returns>A success <see cref="Result"/> instance, wrapped in a task.</returns>
        public static Task<Result> SuccessTask() => Task.FromResult(Result.Success());

        /// <summary>
        /// Creates a new successful <see cref="Result{TValue}"/> with the specified <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="value">The value to encapsulate.</param>
        /// <returns>A success <see cref="Result{TValue}"/> instance containing the specified value.</returns>
        public static Task<Result<TValue>> SuccessTask<TValue>(TValue value) => Task.FromResult(Result.Success(value));
    }
}

