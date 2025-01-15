using MediatR;

namespace Blog.Infrastructure.UnitOfWork;

public class UnitOfWorkCommandHandlerDecorator<T>(
    IRequestHandler<T> decorated,
    IUnitOfWork unitOfWork
) : IRequestHandler<T>
    where T : IRequest
{
    public async Task Handle(T request, CancellationToken cancellationToken)
    {
        await decorated.Handle(request, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
