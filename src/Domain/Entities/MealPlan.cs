using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietonator.Domain.Entities;

public class MealPlan : BaseAuditableEntity
{
    public MealPlan()
    {
        Id = Guid.NewGuid();
    }

    public ICollection<Meal> Meals { get; set; } = new List<Meal>();

    public DateOnly ForDate { get; set; }
    public Guid ForUser { get; set; }

    public void AddMeal(Meal meal)
    {
        Meals.Add(meal);
    }

    public int Kcal => Meals.Sum(c => c.Kcal);
    public float Proteins => Meals.Sum(c => c.Proteins);
    public float Fats => Meals.Sum(c => c.Fats);
    public float Carbohydrates => Meals.Sum(c => c.Carbohydrates);
}
