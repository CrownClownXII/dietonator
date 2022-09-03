using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Application.Common.Mappings;
using Dietonator.Domain.Entities;

namespace Dietonator.Application.MealPlans.Queries.GetMealPlanListForUser;

public class MealPlanListDto : IMapFrom<MealPlan>
{
    public Guid Id { get; set; }
    public DateOnly ForDate { get; set; }
}
