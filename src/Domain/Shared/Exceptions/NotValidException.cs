namespace Blog.Domain.Shared.Exceptions;

public sealed class NotValidException : FormatException
{
    public NotValidException(string parameter)
        : base($"Parameter {parameter} is invalid or bad formatted") { }

    public NotValidException(string parameter, Exception? innerException)
        : base($"Parameter {parameter} is invalid or bad formatted + {innerException}") { }
}
