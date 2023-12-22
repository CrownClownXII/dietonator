using Dietonator.Domain.Entities;

namespace Dietonator.Application.Common.Repository.Meals;


public interface IMealRepository : IWriteRepository<Meal>, IReadRepository<Meal>
{
    
}