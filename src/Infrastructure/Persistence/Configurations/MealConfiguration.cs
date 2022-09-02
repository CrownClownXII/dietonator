using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dietonator.Infrastructure.Persistence.Configurations;

public class MealConfiguration : IEntityTypeConfiguration<Meal>
{
    public void Configure(EntityTypeBuilder<Meal> builder)
    {
        builder.HasMany(c => c.Products)
            .WithOne()
            .IsRequired();
    }
}