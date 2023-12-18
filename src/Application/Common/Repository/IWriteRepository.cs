using Dietonator.Application.Common.Models;

namespace Dietonator.Application.Common.Repository;

public interface IWriteRepository<T> where T: class
{
    void Add(T entity);
    void Remove(T entity);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}