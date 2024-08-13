namespace Bookworm.Application.Common.Models;

public class Error(string code, string details)
{
    public Error(string details) : this(string.Empty, details)
    {
    }

    public string Code { get; } = code;

    public string Details { get; } = details;
}

public class ValidationError(string propertyName, string details) : Error(string.Empty, details)
{
    public string PropertyName { get; } = propertyName;
}