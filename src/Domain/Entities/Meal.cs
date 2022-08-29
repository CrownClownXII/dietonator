using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dietonator.Domain.Enums;

namespace Dietonator.Domain.Entities;

public class Meal: BaseAuditableEntity
{
    public Meal(string name, MealTypeEnum type = MealTypeEnum.CalculableMeal)
    {
        Id = Guid.NewGuid();
        Name = name;
        Type = type;
    }

    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<MealPlan> MealPlans { get; set; } = new List<MealPlan>();

    public string Name { get; set; }
    public MealTypeEnum Type { get; set; }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public int Kcal => Products.Sum(c => c.Kcal);
    public float Proteins => Products.Sum(c => c.Proteins);
    public float Fats => Products.Sum(c => c.Fats);
    public float Carbohydrates => Products.Sum(c => c.Carbohydrates);
}
