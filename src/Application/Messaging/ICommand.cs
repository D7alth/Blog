using MediatR;

namespace Blog.Application.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>
{ }

public interface ICommand : IRequest
{ }