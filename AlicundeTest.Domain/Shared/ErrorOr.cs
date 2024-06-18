namespace AlicundeTest.Domain.Shared;

/// <summary>
/// Simple response object with error handling 
/// </summary>
/// <typeparam name="T">Return object</typeparam>
public sealed class ErrorOr<T>
{
    private readonly T? value;
    private readonly string? error;

    private ErrorOr(T value)
    {
        this.value = value;
        this.error = null;
    }

    private ErrorOr(string error)
    {
        this.value = default;
        this.error = error;
    }

    public T? Value => value;
    public string? Error => error;

    public bool IsSuccess => error is null;
    public bool IsFailure => error is not null;

    public T? GetValueOrDefault() => value;
    public string? GetError() => error;

    /// <summary>
    /// Creation method providing value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static ErrorOr<T> FromValue(T value) => new ErrorOr<T>(value);
    /// <summary>
    /// Creation method providing error
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static ErrorOr<T> FromError(string error) => new ErrorOr<T>(error);

    /// <summary>
    /// Implicit operator for conversion
    /// </summary>
    /// <param name="errorOr"></param>
    public static implicit operator T(ErrorOr<T> errorOr)
    {
        if (errorOr.IsSuccess)
        {
            return errorOr.value!;
        }
        else
        {
            throw new InvalidOperationException("ErrorOr is not a success value.");
        }
    }

    /// <summary>
    /// Custom ToString method for logger
    /// </summary>
    /// <returns></returns>
    public override string ToString() => IsSuccess ? $"Success: {value}" : $"Error: {error}";
}
