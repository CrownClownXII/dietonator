﻿using Dietonator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dietonator.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    public DbSet<Meal> Meals { get; }
    public DbSet<MealPlan> MealPlans { get; }
    public DbSet<MealProduct> MealProducts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
