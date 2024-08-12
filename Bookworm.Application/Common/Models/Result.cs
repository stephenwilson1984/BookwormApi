namespace Bookworm.Application.Common.Models;

public class Result<T> where T : class
{
    internal Result(bool succeeded, IEnumerable<string> errors, T? value = null)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
        Value = value;
    }

    public bool Succeeded { get; set; }

    public string[] Errors { get; set; }

    public T? Value { get; set; }

    public static Result<T> Success(T? value)
    {
        return new Result<T>(true, [], value);
    }

    public static Result<T> Failure(IEnumerable<string> errors)
    {
        return new Result<T>(false, errors);
    }
}