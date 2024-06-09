﻿namespace ACD.Domain.Models;




/// <summary>
/// Represents the response object for the BalanceServiceProvider entity geting from the web.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Result<T>
{

    public T Value { get; }
    public string Error { get; }
    public bool IsSuccess => Error == null;

    protected Result(T value, string error)
    {
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value) => new Result<T>(value, null);
    public static Result<T> Failure(string error) => new Result<T>(default(T), error);

}

