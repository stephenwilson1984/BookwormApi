using System.Data.Common;
using System.Net;

namespace Bookworm.Application.Common.Models;

// Taken as is from https://josef.codes/my-take-on-the-result-class-in-c-sharp/

internal interface IErrorResult
{
    string Message { get; }

    IReadOnlyCollection<Error> Errors { get; }
}

public abstract class Result
{
    public bool Success { get; protected set; }

    public bool Failure => !Success;
}

public abstract class Result<T> : Result
{
    private T? _data;

    protected Result(T? data)
    {
        Data = data;
    }

    public T? Data
    {
        get => Success ? _data : throw new MemberAccessException($"You can't access .{nameof(Data)} when .{nameof(Success)} is false");
        set => _data = value;
    }
}

public class SuccessResult : Result
{
    public SuccessResult()
    {
        Success = true;
    }
}

public class SuccessResult<T> : Result<T>
{
    public SuccessResult(T data) : base(data)
    {
        Success = true;
    }
}

public class ErrorResult : Result, IErrorResult
{
    public ErrorResult(string message) : this(message, [])
    {
    }

    public ErrorResult(string message, IReadOnlyCollection<Error> errors)
    {
        Message = message;
        Success = false;
        Errors = errors;
    }

    public string Message { get; }

    public IReadOnlyCollection<Error> Errors { get; }
}

public class ErrorResult<T> : Result<T>, IErrorResult
{
    public ErrorResult(string message) : this(message, [])
    {    
    }

    public ErrorResult(string message, IReadOnlyCollection<Error> errors) : base(default)
    {
        Message = message;
        Success = false;
        Errors = errors;
    }

    public string Message { get; set; }

    public IReadOnlyCollection<Error> Errors { get; }
}

public class NotFoundErrorResult<T>(string message) : ErrorResult<T>(message);

public class DatabaseErrorResult<T>(DbException exception) : ErrorResult<T>(exception.Message);

public class ValidationErrorResult : ErrorResult
{
    public ValidationErrorResult(string message) : base(message)
    {
    }

    public ValidationErrorResult(string message, IReadOnlyCollection<ValidationError> errors) : base(message, errors)
    {
    }
}

public class HttpErrorResult : ErrorResult
{
    public HttpStatusCode StatusCode { get; }

    public HttpErrorResult(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public HttpErrorResult(string message, IReadOnlyCollection<Error> errors, HttpStatusCode statusCode) : base(message, errors)
    {
        StatusCode = statusCode;
    }
}