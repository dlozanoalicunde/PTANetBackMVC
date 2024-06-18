namespace AlicundeTest.Domain.Shared;

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

    public bool IsSuccess => error is null;
    public bool IsFailure => error is not null;

    public T? GetValueOrDefault() => value;
    public string? GetError() => error;

    public static ErrorOr<T> FromValue(T value) => new ErrorOr<T>(value);
    public static ErrorOr<T> FromError(string error) => new ErrorOr<T>(error);

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

    public override string ToString() => IsSuccess ? $"Success: {value}" : $"Error: {error}";
}
