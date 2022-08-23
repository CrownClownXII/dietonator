using Dietonator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dietonator.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
