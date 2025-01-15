using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.UnitOfWork;

public sealed class UnitOfWork<TDbContext>(TDbContext dbContext) : IUnitOfWork where TDbContext : DbContext
{
    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}