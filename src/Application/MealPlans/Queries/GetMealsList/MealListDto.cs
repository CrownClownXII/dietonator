﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Application.Common.Mappings;
using Dietonator.Domain.Entities;

namespace Dietonator.Application.MealPlans.Queries.GetMealsList;

public class MealListDto : IMapFrom<Meal>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Kcal { get; set; }
    public float Proteins { get; set; }
    public float Fats { get; set; }
    public float Carbohydrates { get; set; }
}

public class MealPlanDetailsDto : IMapFrom<MealPlan>
{
    public Guid Id { get; set; }
    public DateOnly ForDate { get; set; }

    public IEnumerable<MealListDto> Meals { get; set; } = new List<MealListDto>();
}