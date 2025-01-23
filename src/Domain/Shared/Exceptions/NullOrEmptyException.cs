namespace Blog.Domain.Shared.Exceptions;

public sealed class NullOrEmptyException : ArgumentException
{
    public NullOrEmptyException(string parameter)
        : base($"Parameter {parameter} cannot be null or empty") { }

    public NullOrEmptyException(string parameter, Exception? innerException)
        : base($"Parameter cannot be null {parameter} + {innerException}") { }
}
