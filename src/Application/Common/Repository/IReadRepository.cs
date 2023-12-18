using Dietonator.Application.Common.Models;

namespace Dietonator.Application.Common.Repository;

public interface IReadRepository<T> where T: class
{
    Task<T?> FindByAsync(Guid id, CancellationToken cancellationToken);
}