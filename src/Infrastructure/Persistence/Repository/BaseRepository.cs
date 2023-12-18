using Dietonator.Application.Common.Interfaces;
using Dietonator.Application.Common.Repository;

namespace Dietonator.Infrastructure.Persistence.Respository;

public class BaseRepository<T> : IReadRepository<T>, IWriteRepository<T> where T: class
{
    private readonly IApplicationDbContext _context;

    public BaseRepository(IApplicationDbContext context)
    {
        _context = context;

    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public async Task<T?> FindByAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

}