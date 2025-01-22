namespace Blog.Domain.Post.Exceptions;

public class NullOrEmptyParameter(string param, string message) : Exception(message) { }
