using Dietonator.Application.Common.Interfaces;
using Dietonator.Application.Common.Repository.Meals;
using Dietonator.Domain.Entities;

namespace Dietonator.Infrastructure.Persistence.Respository.Meals;

public class MealRepository : BaseRepository<Meal>, IMealRepository
{
    public MealRepository(IApplicationDbContext context) : base(context)
    {
    }

}