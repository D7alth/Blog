namespace Blog.Domain.Entities.Posts.Exceptions;

public class NullOrEmptyParameter : Exception
{
    public NullOrEmptyParameter(string param, string message) : base(message)
    {
        
    }
}