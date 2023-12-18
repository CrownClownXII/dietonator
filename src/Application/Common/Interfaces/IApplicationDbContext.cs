using Dietonator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dietonator.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    public DbSet<Meal> Meals { get; }
    public DbSet<MealProduct> MealProducts { get; }
    DbSet<T> Set<T>() where T : class; 
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
